// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BenchmarkDotNet.Attributes;
using MicroBenchmarks;
using System.IO;

namespace System.Text.Tests
{
    [BenchmarkCategory(Categories.Libraries, Categories.Runtime, Categories.NoWASM)]
    public class Perf_AsciiEncoding : Perf_TextBase
    {
        private ASCIIEncoding _enc;
        private string _toEncode;
        private byte[] _bytes;
        private char[] _chars;

        [GlobalSetup]
        public void Setup()
        {
            _toEncode = File.ReadAllText(Path.Combine(TextFilesRootPath, $"{Perf_TextBase.InputFile.EnglishAllAscii}.txt"));
            _enc = new ASCIIEncoding();
            _bytes = _enc.GetBytes(_toEncode);
        }

        [Benchmark]
        public byte[] GetBytes_NarrowFourUtf16CharsToAsciiAndWriteToBuffer() => _enc.GetBytes(_toEncode);

        [Benchmark]
        public char[] GetChars_WidenAsciiToUtf16() => _enc.GetChars(_bytes);

        [Benchmark]
        public int GetCharCount_GetIndexOfFirstNonAsciiByte()
        {
            return _enc.GetCharCount(_enc.GetBytes(_toEncode));
        }
    }
}
