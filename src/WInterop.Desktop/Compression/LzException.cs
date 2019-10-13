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
        public LzException(LzError error, string? path = null)
            : base(GetMessage(error, path), Errors.HResult.E_FAIL)
        {
            Error = error;
        }

        public LzError Error { get; private set; }

        private static string GetMessage(LzError error, string? path)
        {
            return path == null
                ? $"{GetErrorText(error)}"
                : $"{GetErrorText(error)} '{path}'";
        }

        private static string GetErrorText(LzError error)
        {
            return error switch
            {
                LzError.BadInHandle => "Invalid input handle.",
                LzError.BadOutHandle => "Invalid output handle.",
                LzError.Read => "Corrupt compressed file format.",
                LzError.Write => "Out of space for output file.",
                LzError.GlobalAlloc => "Insufficient memory.",
                LzError.GlobalLock => "Bad global handle.",
                LzError.BadValue => "Input parameter out of acceptable range.",
                LzError.UnknownAlgorithm => "Compression algorithm not recognized.",
                _ => error.ToString(),
            };
        }
    }
}
