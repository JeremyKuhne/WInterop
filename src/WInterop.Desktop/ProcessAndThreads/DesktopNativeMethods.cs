// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.Handles.Desktop;

namespace WInterop.ProcessAndThreads
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class DesktopNativeMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683188.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetEnvironmentVariableW(
                string lpName,
                SafeHandle lpBuffer,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms686206.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetEnvironmentVariableW(
                string lpName,
                string lpValue);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683187.aspx
            // Note that this API does not document that it sets GetLastError
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern SafeEnvironmentStringsHandle GetEnvironmentStringsW();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683151.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeEnvironmentStringsW(
                IntPtr lpszEnvironmentBlock);
        }

        /// <summary>
        /// Set the given enivronment variable.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static void SetEnvironmentVariable(string name, string value)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (!Direct.SetEnvironmentVariableW(name, value))
                throw ErrorHelper.GetIoExceptionForLastError(name);
        }

        /// <summary>
        /// Get the given enivronment variable.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static string GetEnvironmentVariable(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return StringBufferCache.BufferInvoke(
                buffer => Direct.GetEnvironmentVariableW(name, buffer, buffer.CharCapacity),
                name,
                error => error != WinErrors.ERROR_ENVVAR_NOT_FOUND);
        }

        /// <summary>
        /// GetEnvironmentStrings split into key value pairs.
        /// </summary>
        public static IDictionary<string, string> GetEnvironmentVariables()
        {
            var variables = new Dictionary<string, string>();
            using (var buffer = Direct.GetEnvironmentStringsW())
            {
                if (buffer.IsInvalid) return variables;

                foreach (var entry in BufferHelper.SplitNullTerminatedStringList(buffer.DangerousGetHandle()))
                {
                    // Hidden environment variables start with an equals
                    int separator = entry.IndexOf('=', startIndex: 1);
                    if (separator == -1) throw new InvalidOperationException("There should never be a string given back from Windows without an equals sign");

                    string key = entry.Substring(startIndex: 0, length: separator);
                    string value = entry.Substring(startIndex: separator + 1);
                    variables.Add(key, value);
                }
            }

            return variables;
        }

        /// <summary>
        /// Gets the raw set of environment strings as name/value pairs separated by an equals character.
        /// </summary>
        /// <remarks>Names can have an equals character as the first character. Be cautious when splitting or use GetEnvironmentVariables().</remarks>
        public static IEnumerable<string> GetEnvironmentStrings()
        {
            using (var buffer = Direct.GetEnvironmentStringsW())
            {
                if (buffer.IsInvalid) return Enumerable.Empty<string>();

                return BufferHelper.SplitNullTerminatedStringList(buffer.DangerousGetHandle());
            }
        }
    }
}