// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using WInterop.ErrorHandling.Types;
using WInterop.ProcessAndThreads.BufferWrappers;
using WInterop.ProcessAndThreads.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.ProcessAndThreads
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static partial class ProcessMethods
    {
        /// <summary>
        /// Set the given enivronment variable.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static void SetEnvironmentVariable(string name, string value)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (!Imports.SetEnvironmentVariableW(name, value))
                throw Errors.GetIoExceptionForLastError(name);
        }

        /// <summary>
        /// Get the given enivronment variable.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static string GetEnvironmentVariable(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var wrapper = new EnvironmentVariableWrapper { Name = name };

            return BufferHelper.ApiInvoke(
                ref wrapper,
                name,
                error => error != WindowsError.ERROR_ENVVAR_NOT_FOUND);
        }

        /// <summary>
        /// GetEnvironmentStrings split into key value pairs.
        /// </summary>
        public static IDictionary<string, string> GetEnvironmentVariables()
        {
            var variables = new Dictionary<string, string>();
            using (var buffer = Imports.GetEnvironmentStringsW())
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
            using (var buffer = Imports.GetEnvironmentStringsW())
            {
                if (buffer.IsInvalid) return Enumerable.Empty<string>();

                return BufferHelper.SplitNullTerminatedStringList(buffer.DangerousGetHandle());
            }
        }

        /// <summary>
        /// Gets the specified process memory counters.
        /// </summary>
        /// <param name="process">The process to get memory info for for or null for the current process.</param>
        public unsafe static PROCESS_MEMORY_COUNTERS_EX GetProcessMemoryInfo(SafeProcessHandle process = null)
        {
            if (process == null) process = GetCurrentProcess();

            if (!Imports.K32GetProcessMemoryInfo(process, out var info, (uint)sizeof(PROCESS_MEMORY_COUNTERS_EX)))
                throw Errors.GetIoExceptionForLastError();

            return info;
        }

        /// <summary>
        /// Get the process id for the given process handle.
        /// </summary>
        public static uint GetProcessId(SafeProcessHandle processHandle)
        {
            return Imports.GetProcessId(processHandle);
        }
    }
}