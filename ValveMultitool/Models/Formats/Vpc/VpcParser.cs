using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    /// <summary>
    /// Simple (or not so simple) parser for .vpc (valve project creator)
    /// inspired by the steamdb kv3 parser, uses similar state machine
    /// </summary>
    internal class VpcParser
    {
        private readonly Stream _stream;
        private readonly Queue<char> _buffer = new Queue<char>();
        private readonly StringBuilder _builder = new StringBuilder();

        // Stacks and states
        private readonly Stack<VpcObject> _objectStack = new Stack<VpcObject>();
        private readonly Stack<VpcConditionalCollection> _conditionalStack = new Stack<VpcConditionalCollection>();
        private readonly Stack<ParserState> _stateStack = new Stack<ParserState>();

        // last conditionals
        private bool _nextNegated;
        private IVpcConditional _lastConditional;

        internal VpcParser(Stream stream)
        {
            _stream = stream;
            _objectStack.Push(new VpcObject()); // root object
            _stateStack.Push(ParserState.SeekType);
        }

        internal VpcObject Parse()
        {
            while (_stream.Position < _stream.Length)
            {
                var @char = NextChar();
                var state = _stateStack.Peek();

                switch (state)
                {
                    case ParserState.SeekType:
                        SeekType(@char);
                        break;
                    case ParserState.Type:
                        ReadType(@char);
                        break;
                    case ParserState.Key:
                        ReadKey(@char);
                        break;
                    case ParserState.ValueString:
                        ReadValueString(@char);
                        break;
                    case ParserState.ValueArray:
                        SeekType(@char);
                        break;
                    case ParserState.Condition:
                        ReadCondition(@char);
                        break;
                    case ParserState.Comment:
                        ReadComment(@char);
                        break;
                }
            }

            return _objectStack.Pop();
        }

        /// <summary>
        /// Reads the next character from the file stream
        /// </summary>
        private char NextChar()
        {
            // if the buffer has any, give one from there instead
            if (_buffer.Any())
                return _buffer.Dequeue();

            return (char) _stream.ReadByte();
        }

        private void PopCurrentObject()
        {
            var obj = _objectStack.Pop();
            _objectStack.Peek().Add(new VpcValue { Type = VpcValueType.Object, Value = obj });
            _builder.Clear();
            _stateStack.Pop();
        }

        private void SeekType(char @char)
        {
            // Ignore whitespace
            if (char.IsWhiteSpace(@char)) return;

            // Start of type
            if (@char == '$')
            {
                _objectStack.Push(new VpcObject());
                _stateStack.Push(ParserState.Type);
            }

            // Start of comment
            else if (@char == '/')
            {
                _objectStack.Push(new VpcObject());
                _stateStack.Push(ParserState.Comment);
                _builder.Clear();
                _builder.Append(@char);
            }

            // end of array
            if (@char == '}') PopCurrentObject();
        }

        /// <summary>
        /// The purpose of this method is to read the "section type"
        /// e.g. $Configuration, $Linker, $Project
        /// </summary>
        private void ReadType(char @char)
        {
            // Are we at the end of the type?
            if (char.IsWhiteSpace(@char))
            {
                _stateStack.Pop();
                _stateStack.Push(ParserState.Key);

                _objectStack.Peek().Type = _builder.ToString();
                _builder.Clear();
                return;
            }

            // TODO: hack? maybe?
            if (@char == '@') return;

            _builder.Append(@char);
        }

        private void ReadKey(char @char)
        {
            // ignore whitespace
            if (char.IsWhiteSpace(@char)) return;

            // if the first character that's not whitespace
            // is a ", jump straight to value
            if (@char == '"')
            {
                _stateStack.Pop();
                _stateStack.Push(ParserState.ValueString);

                _objectStack.Peek().Key = _builder.ToString();
                _builder.Clear();
            }

            // array value
            else if (@char == '{')
            {
                _stateStack.Pop();
                _stateStack.Push(ParserState.ValueArray);

                _objectStack.Peek().Key = _builder.ToString();
                _objectStack.Peek().IsArray = true;
                _builder.Clear();
            }

            // overrun characters
            else if (@char == '$' || @char == '/' || @char == '}')
            {
                // pop ourselves
                PopCurrentObject();

                switch (@char)
                {
                    case '$':
                        // push a new object
                        _objectStack.Push(new VpcObject());
                        _stateStack.Push(ParserState.Type);
                        break;
                    case '/':
                        _objectStack.Push(new VpcObject());
                        _stateStack.Push(ParserState.Comment);
                        _builder.Clear();
                        _builder.Append(@char);
                        break;
                    case '}':
                        PopCurrentObject();
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            // actual key
            else _builder.Append(@char);
        }

        private void ReadValueString(char @char)
        {
            // end of value
            if (@char == '"')
            {
                // push the string
                _objectStack.Peek().Add(new VpcValue { Type = VpcValueType.String, Value = _builder.ToString() });
            }
            else if (@char == '\n') PopCurrentObject();
            else if (@char == '[')
            {
                _conditionalStack.Push(_objectStack.Peek().Conditions);
                _stateStack.Push(ParserState.Condition);
                _builder.Clear();
            }
            else _builder.Append(@char);
        }

        private void ReadCondition(char @char)
        {
            // ignore whitespace
            if (char.IsWhiteSpace(@char)) return;

            // operator
            if (@char == '&' || @char == '|')
            {
                // If we see any operator, set the operator of the last statement
                if (NextChar() != @char) throw new Exception("Invalid conditional operator!");
                _lastConditional.Operator = (VpcOperator) @char;

                // we can probably do it here
                EndVariable();
            }

            // negation
            else if (@char == '!') _nextNegated = true;

            // end
            else if (@char == ']')
            {
                // Still characters left in the builder? Add one.
                if (_builder.Length > 0)
                    EndVariable();

                PopCurrentObject();

                // pop the conditional
                _conditionalStack.Pop();

                // pop the state again to get out of the array
                _stateStack.Pop();
            }
            // start of conditional sub-array
            else if (@char == '(') _conditionalStack.Push(new VpcConditionalCollection());
            else if (@char == ')')
            {
                // end of conditional sub-array
                EndVariable();
                _lastConditional = _conditionalStack.Pop();
            }
            else if (@char == '$')
            {
                // start of variable also terminates the last variable
                EndVariable();
                _lastConditional = new VpcConditional(null, VpcOperator.None);
            }
            else _builder.Append(@char);
        }

        private void ReadComment(char @char)
        {
            // Are we at the end?
            if (@char == '\n')
            {
                _objectStack.Peek().Add(new VpcValue { Type = VpcValueType.Comment, Value = _builder.ToString() });
                PopCurrentObject();
            }
            else if (@char != '\r') _builder.Append(@char);
        }

        private void EndVariable()
        {
            if (_lastConditional == null) return;

            // negation
            _lastConditional.Negated = _nextNegated;

            // set its name
            if (_lastConditional is VpcConditional conditional)
                conditional.Value = _builder.ToString();
            _builder.Clear();

            _conditionalStack.Peek().Add(_lastConditional); // append to the next one up the stack

            // reset
            _nextNegated = false;
            _lastConditional = null;
        }

        private enum ParserState
        {
            SeekType,
            Type,
            Key,
            ValueArray,
            ValueString,
            Condition,
            Comment
        }
    }
}
