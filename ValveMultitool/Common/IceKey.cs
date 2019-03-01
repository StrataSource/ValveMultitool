using System;
using System.Collections.Generic;

namespace ValveMultitool.Common
{
    //========= Public Domain Code ================================================
    //
    // Purpose: C# implementation of the ICE encryption algorithm.
    //			Taken from public domain code, as written by Matthew Kwan - July 1996
    //			http://www.darkside.com.au/ice/
    // Ported to C# by Joseph Marsden - October 2018
    //=============================================================================
    public class IceKey
    {
        private readonly int _size;
        private readonly int _rounds;
        private readonly int[][] _keySchedule;

	    private int[,] _spBox;
	    private readonly bool _spBoxInitialised;

        private readonly int[,] _sMod = {
						{333, 313, 505, 369},
						{379, 375, 319, 391},
						{361, 445, 451, 397},
						{397, 425, 395, 505}};

	    private readonly int[,] _sXor = {
	        {0x83, 0x85, 0x9b, 0xcd},
	        {0xcc, 0xa7, 0xad, 0x41},
	        {0x4b, 0x2e, 0xd4, 0x33},
	        {0xea, 0xcb, 0x2e, 0x04}};

	    private readonly int[] _pBox = {
            0x00000001, 0x00000080, 0x00000400, 0x00002000,
            0x00080000, 0x00200000, 0x01000000, 0x40000000,
            0x00000008, 0x00000020, 0x00000100, 0x00004000,
            0x00010000, 0x00800000, 0x04000000, 0x20000000,
            0x00000004, 0x00000010, 0x00000200, 0x00008000,
            0x00020000, 0x00400000, 0x08000000, 0x10000000,
            0x00000002, 0x00000040, 0x00000800, 0x00001000,
            0x00040000, 0x00100000, 0x02000000, unchecked((int)0x80000000)};

        private static readonly int[] KeyRot = {
                        0, 1, 2, 3, 2, 1, 3, 0,
                        1, 3, 2, 0, 3, 1, 0, 2};

        // 8-bit Galois Field multiplication of a by b, modulo m.
        // Just like arithmetic multiplication, except that
        // additions and subtractions are replaced by XOR.
        private static int Gf_mult(int a, int b, int m)
        {
            var res = 0;

            while (b != 0)
            {
                if ((b & 1) != 0)
                    res ^= a;

                a <<= 1;
                b = (int)((uint)b >> 1);

                if (a >= 256)
                    a ^= m;
            }

            return res;
        }

        // 8-bit Galois Field exponentiation.
        // Raise the base to the power of 7, modulo m.
        private static int Gf_exp7(int b, int m)
        {
            if (b == 0)
                return (0);

            var x = Gf_mult(b, b, m);
            x = Gf_mult(b, x, m);
            x = Gf_mult(x, x, m);
            return (Gf_mult(b, x, m));
        }

        // Carry out the ICE 32-bit permutation.
        private int Perm32(int x)
        {
            var res = 0;
            var i = 0;

            while (x != 0)
            {
                if ((x & 1) != 0)
                    res |= _pBox[i];
                i++;
                x = (int)((uint)x >> 1);
            }

            return res;
        }

        // Initialise the substitution/permutation boxes.
        private void SpBoxInit()
        {
            int i;

            _spBox = new int[4, 1024];

            for (i = 0; i < 1024; i++)
            {
                var col = (int)((uint)i >> 1) & 0xff;
                var row = (i & 0x1) | (int)(((uint)i & 0x200) >> 8);

                var x = Gf_exp7(col ^ _sXor[0, row], _sMod[0, row]) << 24;
                _spBox[0, i] = Perm32(x);

                x = Gf_exp7(col ^ _sXor[1, row], _sMod[1, row]) << 16;
                _spBox[1, i] = Perm32(x);

                x = Gf_exp7(col ^ _sXor[2, row], _sMod[2, row]) << 8;
                _spBox[2, i] = Perm32(x);

                x = Gf_exp7(col ^ _sXor[3, row], _sMod[3, row]);
                _spBox[3, i] = Perm32(x);
            }
        }

        // Create a new ICE key with the specified level.
        public IceKey(int level)
        {
            if (!_spBoxInitialised)
            {
                SpBoxInit();
                _spBoxInitialised = true;
            }

            if (level < 1)
            {
                _size = 1;
                _rounds = 8;
            }
            else
            {
                _size = level;
                _rounds = level * 16;
            }

            // initialise the key schedule
            _keySchedule = new int[_rounds][];
            for(var i = 0; i < _rounds; i++)
            {
                var row = new int[3];
                _keySchedule[i] = row;
            }
        }

        // Set 8 rounds [n, n+7] of the key schedule of an ICE key.
        private void ScheduleBuild(IList<int> kb, int n, int krotIdx)
        {
            int i;

            for (i = 0; i < 8; i++)
            {
                int j;
                var kr = KeyRot[krotIdx + i];
                var subkey = _keySchedule[n + i];

                for (j = 0; j < 3; j++)
                    _keySchedule[n + i][j] = 0;

                for (j = 0; j < 15; j++)
                {
                    int k;
                    var currSk = j % 3;

                    for (k = 0; k < 4; k++)
                    {
                        var currKb = kb[(kr + k) & 3];
                        var bit = currKb & 1;

                        subkey[currSk] = (subkey[currSk] << 1) | bit;
                        kb[(kr + k) & 3] = (int)((uint)currKb >> 1) | ((bit ^ 1) << 15);
                    }
                }
            }
        }

        // Set the key schedule of an ICE key.
        public void Set(byte[] key)
        {
            int i;
            var kb = new int[4];

            if (_rounds == 8)
            {
                for (i = 0; i < 4; i++)
                    kb[3 - i] = ((key[i * 2] & 0xff) << 8)
                                | (key[i * 2 + 1] & 0xff);

                ScheduleBuild(kb, 0, 0);
                return;
            }

            for (i = 0; i < _size; i++)
            {
                int j;

                for (j = 0; j < 4; j++)
                    kb[3 - j] = ((key[i * 8 + j * 2] & 0xff) << 8)
                                | (key[i * 8 + j * 2 + 1] & 0xff);

                ScheduleBuild(kb, i * 8, 0);
                ScheduleBuild(kb, _rounds - 8 - i * 8, 8);
            }
        }

        // Clear the key schedule to prevent memory snooping.
        public void Clear()
        {
            int i, j;

            for (i = 0; i < _rounds; i++)
                for (j = 0; j < 3; j++)
                    _keySchedule[i][j] = 0;
        }

        // The single round ICE f function.
        private int RoundFunc(int p, IReadOnlyList<int> subkey)
        {
            var tl = ((int)((uint)p >> 16) & 0x3ff) | ((int)(((uint)p >> 14) | ((uint)p << 18)) & 0xffc00);
            var tr = (p & 0x3ff) | ((p << 2) & 0xffc00);

            // al = (tr & subkey[2]) | (tl & ~subkey[2]);
            // ar = (tl & subkey[2]) | (tr & ~subkey[2]);
            var al = subkey[2] & (tl ^ tr);
            var ar = al ^ tr;
            al ^= tl;

            al ^= subkey[0];
            ar ^= subkey[1];

            return (_spBox[0, (int)((uint)al >> 10)] | _spBox[1, al & 0x3ff]
                    | _spBox[2, (int)((uint)ar >> 10)] | _spBox[3, ar & 0x3ff]);
        }

        // Encrypt a block of 8 bytes of data.
        public void Encrypt(byte[] plaintext, ref byte[] ciphertext)
        {
            int i;
            int l = 0, r = 0;

            for (i = 0; i < 4; i++)
            {
                l |= (plaintext[i] & 0xff) << (24 - i * 8);
                r |= (plaintext[i + 4] & 0xff) << (24 - i * 8);
            }

            for (i = 0; i < _rounds; i += 2)
            {
                l ^= RoundFunc(r, (int[]) _keySchedule.GetValue(i));
                r ^= RoundFunc(l, (int[]) _keySchedule.GetValue(i + 1));
            }

            for (i = 0; i < 4; i++)
            {
                ciphertext[3 - i] = (byte)(r & 0xff);
                ciphertext[7 - i] = (byte)(l & 0xff);

                r = (int)((uint)r >> 8);
                l = (int)((uint)l >> 8);
            }
        }

        // Decrypt a block of 8 bytes of data.
        public void Decrypt(byte[] ciphertext, ref byte[] plaintext)
        {
            int i;
            int l = 0, r = 0;

            for (i = 0; i < 4; i++)
            {
                l |= (ciphertext[i] & 0xff) << (24 - i * 8);
                r |= (ciphertext[i + 4] & 0xff) << (24 - i * 8);
            }

            for (i = _rounds - 1; i > 0; i -= 2)
            {
                l ^= RoundFunc(r, (int[]) _keySchedule.GetValue(i));
                r ^= RoundFunc(l, (int[]) _keySchedule.GetValue(i - 1));
            }

            for (i = 0; i < 4; i++)
            {
                plaintext[3 - i] = (byte)(r & 0xff);
                plaintext[7 - i] = (byte)(l & 0xff);

                r = (int)((uint)r >> 8);
                l = (int)((uint)l >> 8);
            }
        }

        /// <summary>
        /// Encrypts an entire buffer of text.
        /// Buffer size must be a multiple of 8!
        /// </summary>
        public void EncryptBuffer(byte[] plainText, ref byte[] cipherText)
        {
            var position = 0;
            while (position < plainText.Length)
            {
                var plainBlock = new byte[8];
                var cipherBlock = new byte[8];
                Buffer.BlockCopy(plainText, position, plainBlock, 0, 8);
                Encrypt(plainBlock, ref cipherBlock);
                Buffer.BlockCopy(cipherBlock, 0, cipherText, position, 8);
                position += 8;
            }
        }

        /// <summary>
        /// Decrypts an entire buffer of text.
        /// Buffer size must be a multiple of 8!
        /// </summary>
        public void DecryptBuffer(byte[] cipherText, ref byte[] plainText)
        {
            var position = 0;
            while (position < cipherText.Length)
            {
                var cipherBlock = new byte[8];
                var plainBlock = new byte[8];
                Buffer.BlockCopy(cipherText, position, cipherBlock, 0, 8);

                Decrypt(cipherBlock, ref plainBlock);
                Buffer.BlockCopy(plainBlock, 0, plainText, position, 8);
                position += 8;
            }
        }

        // Return the key size, in bytes.
        public int KeySize() => _size * 8;

        // Return the block size, in bytes.
        public int BlockSize() => 8;
    }
}
