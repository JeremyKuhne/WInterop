// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop.Utility
{
    /// <summary>
    /// Path related helpers.
    /// </summary>
    /// <remarks>
    /// Code in here should NOT touch actual IO.
    /// </remarks>
    public static class Paths
    {
        /// <summary>
        /// Legacy maximum path length in Windows (without using extended syntax).
        /// </summary>
        public const int MaxPath = 260;

        /// <summary>
        /// Maximum path size using extended syntax or path APIs in the FlexFileService (default).
        /// </summary>
        /// <remarks>
        /// Windows APIs need extended syntax to get past 260 characters (including the null terminator).
        /// </remarks>
        public const int MaxLongPath = short.MaxValue;

        // - Paths are case insensitive (NTFS supports sensitivity, but it is not enabled by default)
        // - Backslash is the "correct" separator for path components. Windows APIs convert forward slashes to backslashes, except for "\\?\"
        //
        // References
        // ==========
        //
        // "Naming Files, Paths, and Namespaces"
        // http://msdn.microsoft.com/en-us/library/windows/desktop/aa365247.aspx
        //
        private static readonly char[] s_DirectorySeparatorCharacters = new char[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar };

        /// <summary>
        /// The default directory separator
        /// </summary>
        public static readonly char DirectorySeparator = System.IO.Path.DirectorySeparatorChar;

        /// <summary>
        /// The alternate directory separator
        /// </summary>
        public static readonly char AltDirectorySeparator = System.IO.Path.AltDirectorySeparatorChar;

        /// <summary>
        /// Returns true if the path ends in a directory separator.
        /// </summary>
        public static bool EndsInDirectorySeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            char lastChar = path[path.Length - 1];
            return IsDirectorySeparator(lastChar);
        }

        /// <summary>
        /// Returns true if the given character is a directory separator.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDirectorySeparator(char character)
        {
            return (character == DirectorySeparator || character == AltDirectorySeparator);
        }

        /// <summary>
        /// Ensures that the specified path ends in a directory separator.
        /// </summary>
        /// <returns>The path with an appended directory separator if necessary.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if path is null.</exception>
        public static string AddTrailingSeparator(string path)
        {
            if (path == null) { throw new ArgumentNullException(nameof(path)); }
            if (EndsInDirectorySeparator(path))
            {
                return path;
            }
            else
            {
                return path + DirectorySeparator;
            }
        }

        /// <summary>
        /// Ensures that the specified path does not end in a directory separator.
        /// </summary>
        /// <returns>The path with an appended directory separator if necessary.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if path is null.</exception>
        public static string RemoveTrailingSeparators(string path)
        {
            if (path == null) { throw new ArgumentNullException(nameof(path)); }
            if (EndsInDirectorySeparator(path))
            {
                return path.TrimEnd(s_DirectorySeparatorCharacters);
            }
            else
            {
                return path;
            }
        }
    }
}
