// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using WInterop.Console.Types;

namespace WInterop.Console
{
    public class ConsoleReader
    {
        private ConsoleReader()
        {
        }

        public static TextReader Create()
        {
            return new StreamReader(
                new ConsoleStream(StandardHandleType.Input),
                System.Console.InputEncoding,
                detectEncodingFromByteOrderMarks: false);
        }
    }
}
