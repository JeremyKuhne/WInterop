// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace WInterop.Support
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
        /// Path prefix for DOS device paths
        /// </summary>
        public const string DosDevicePathPrefix = @"\\.\";

        /// <summary>
        /// Path prefix for extended DOS device paths
        /// </summary>
        public const string ExtendedDosDevicePathPrefix = @"\\?\";

        /// <summary>
        /// Returns true if the path begins with a directory separator.
        /// </summary>
        public static bool BeginsWithDirectorySeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return IsDirectorySeparator(path[0]);
        }

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
        /// Returns true if the path ends in a directory separator.
        /// </summary>
        public static bool EndsInDirectorySeparator(StringBuilder path)
        {
            if (path == null || path.Length == 0)
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

        /// <summary>
        /// Combines two strings, adding a directory separator between if needed.
        /// Does not validate path characters.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path1"/> is null.</exception>
        public static string Combine(string path1, string path2)
        {
            if (path1 == null) throw new ArgumentNullException(nameof(path1));

            // Add nothing to something is something
            if (string.IsNullOrEmpty(path2)) return path1;

            StringBuilder sb = new StringBuilder();
            if (!EndsInDirectorySeparator(path1) && !BeginsWithDirectorySeparator(path2))
            {
                sb.Append(path1);
                sb.Append(DirectorySeparator);
                sb.Append(path2);
            }
            else
            {
                sb.Append(path1);
                sb.Append(path2);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Combines two string builders into the first string builder, adding a directory separator between if needed.
        /// Does not validate path characters.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path1"/> is null.</exception>
        public static void Combine(StringBuilder path1, string path2)
        {
            if (path1 == null) throw new ArgumentNullException(nameof(path1));

            // Add nothing to something is something
            if (path2 == null || path2.Length == 0) return;

            if (!EndsInDirectorySeparator(path1) && !BeginsWithDirectorySeparator(path2))
            {
                path1.Append(DirectorySeparator);
                path1.Append(path2);
            }
            else
            {
                path1.Append(path2);
            }
        }

        /// <summary>
        /// Returns true if the given path is a device path.
        /// </summary>
        /// <remarks>
        /// This will return true if the path returns any of the following with either forward or back slashes.
        ///  \\?\  \??\  \\.\
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDosDevicePath(string path)
        {
            return path != null
                && path.Length >= DosDevicePathPrefix.Length
                && IsDirectorySeparator(path[0])
                &&
                (
                    (IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?'))
                    || (path[1] == '?' && path[2] == '?')
                )
                && IsDirectorySeparator(path[3]);
        }

        /// <summary>
        /// Returns true if the given path is extended and will skip normalization and MAX_PATH checks.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExtendedDosDevicePath(string path)
        {
            // While paths like "//?/C:/" will work, they're treated the same as "\\.\" paths.
            // Skipping of normalization will *only* occur if back slashes ('\') are used.
            return path != null
                && path.Length >= ExtendedDosDevicePathPrefix.Length
                && path[0] == '\\'
                && (path[1] == '\\' || path[1] == '?')
                && path[2] == '?'
                && path[3] == '\\';
        }
    }
}
