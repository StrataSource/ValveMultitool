using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    internal class VpcSerialiser
    {
        private readonly VpcObject _object;
        private readonly TextWriter _writer;
        private bool _lastArrayTag = false;
        private int _indentation;

        internal VpcSerialiser(Stream stream, VpcObject obj)
        {
            _writer = new StreamWriter(stream, Encoding.UTF8, 1024, true) {NewLine = "\n"};
            _object = obj;
        }

        internal void Serialise()
        {
            // TODO: hack lol
            foreach (var obj in _object)
                WriteFullObject((VpcObject)obj.Value);
            _writer.Dispose();
        }

        private void WriteFullObject(VpcObject obj)
        {
            // Extra newline is placed for array closing tags.
            if (_lastArrayTag)
            {
                _writer.Write('\n');
                _lastArrayTag = false;
            }

            WriteIndentation();

            // Write type
            _writer.Write($"${obj.Type}");

            // Write key (if present)
            if (!string.IsNullOrEmpty(obj.Key))
                _writer.Write($" \"{obj.Key}\"");

            WriteObject(obj);
        }

        private void WriteObject(VpcObject obj)
        {
            if (obj.Any())
            {
                _writer.Write(' ');
                if (obj.IsArray)
                {
                    _writer.Write(_writer.NewLine);
                    WriteIndentation();
                    _writer.WriteLine("{");
                    _indentation++;

                    foreach (var item in obj)
                        WriteArrayValue(item);

                    _indentation--;
                    WriteIndentation();
                    _writer.Write("}");
                    _lastArrayTag = true;
                }
                else
                {
                    // write string value
                    _writer.Write($"\"{(string)obj.First().Value}\"");
                }
            }

            WriteConditions(obj.Conditions);
            _writer.Write(_writer.NewLine);
        }

        private void WriteArrayValue(VpcValue value)
        {
            switch (value.Type)
            {
                case VpcValueType.Object:
                    WriteFullObject((VpcObject)value.Value);
                    break;
                case VpcValueType.String:
                    WriteIndentation();
                    _writer.WriteLine($"\"{(string)value.Value}\"");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteConditions(VpcConditionalCollection value)
        {
            // avoids empty brackets
            if (!value.Any()) return;

            _writer.Write(' ');
            _writer.Write('[');

            foreach (var item in value)
                WriteCondition(item);

            _writer.Write(']');
        }

        private void WriteCondition(IVpcConditional value)
        {
            if (value.Negated) _writer.Write('!');

            switch (value)
            {
                case VpcConditionalCollection collection:
                {
                    _writer.Write('(');
                    foreach (var item in collection)
                        WriteCondition(item);
                    _writer.Write(')');
                    break;
                }
                case VpcConditional conditional:
                    _writer.Write($"${conditional.Value}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (value.Operator)
            {
                case VpcOperator.None:
                    break;
                case VpcOperator.And:
                    _writer.Write(" && ");
                    break;
                case VpcOperator.Or:
                    _writer.Write(" || ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteIndentation()
        {
            // Writes tab characters for indentation.
            if (_indentation == 0) return;
            _writer.Write(new string('\t', _indentation));
        }
    }
}
