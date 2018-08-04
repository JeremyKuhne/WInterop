// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Compression
{
    public class LzException : WInteropIOException
    {
        public LzException(LzError error, string path = null)
            : base(GetMessage(error, path), HRESULT.E_FAIL)
        {
            Error = error;
        }

        public LzError Error { get; private set; }

        private static string GetMessage(LzError error, string path)
        {
            return path == null
                ? $"{GetErrorText(error)}"
                : $"{GetErrorText(error)} '{path}'";
        }

        private static string GetErrorText(LzError error)
        {
            switch (error)
            {
                case LzError.BadInHandle:
                    return "Invalid input handle.";
                case LzError.BadOutHandle:
                    return "Invalid output handle.";
                case LzError.Read:
                    return "Corrupt compressed file format.";
                case LzError.Write:
                    return "Out of space for output file.";
                case LzError.GlobalAlloc:
                    return "Insufficient memory.";
                case LzError.GlobalLock:
                    return "Bad global handle.";
                case LzError.BadValue:
                    return "Input parameter out of acceptable range.";
                case LzError.UnknownAlgorithm:
                    return "Compression algorithm not recognized.";
                default:
                    return error.ToString();
            }
        }
    }
}
