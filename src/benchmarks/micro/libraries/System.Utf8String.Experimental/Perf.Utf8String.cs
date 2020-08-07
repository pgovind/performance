﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

using ustring = System.Utf8String;

namespace System.Text.Experimental
{
    [BenchmarkCategory(Categories.Libraries, Categories.Runtime)]
    public class Perf
    {
        public static IEnumerable<string> TranscodingTestData()
        {
            //yield return "This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. This is a big string of words. ";
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 256; i++)
            //{
            //    sb.Append(i);
            //}
            //yield return sb.ToString();
            yield return "Mexicanos, al grito de guerra el acero aprestad y el bridón. Y retiemble en sus centros la Tierra, al sonoro rugir del cañón. Y retiemble en sus centros la Tierra, ¡al sonoro rugir del cañón! Ciña ¡oh Patria! tus sienes de oliva de la paz el arcángel divino, que en el cielo tu eterno destino por el dedo de Dios se escribió. Mas si osare un extraño enemigo profanar con su planta tu suelo, piensa ¡oh Patria querida! que el cielo un soldado en cada hijo te dio.";
        }

        [Benchmark]
        [ArgumentsSource(nameof(TranscodingTestData))]
        public void ToUtf16(string expected)
        {
            Utf8Span span = new Utf8Span(new Utf8String(expected));
            Memory<char> memory = new char[expected.Length];
            Span<char> destination = memory.Span;
            span.ToChars(destination);
        }

        [Benchmark]
        [ArgumentsSource(nameof(TranscodingTestData))]
        public void IsAscii(string expected)
        {
            Utf8Span span = new Utf8Span(new Utf8String(expected));
            span.IsAscii();
        }

        [Benchmark]
        public void IsNormalized_GetIndexOfFirstNonAsciiChar()
        {
            string expected = "";
            expected.IsNormalized();
        }

    }
}
