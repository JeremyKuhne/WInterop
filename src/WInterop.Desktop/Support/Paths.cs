// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Text;

namespace WInterop.Support
{
    /// <summary>
    ///  Path related helpers.
    /// </summary>
    /// <remarks>
    ///  Code in here should NOT do any physical IO.
    /// </remarks>
    public static class Paths
    {
        /// <summary>
        ///  Legacy maximum path length in Windows (without using extended syntax).
        /// </summary>
        public const int MaxPath = 260;

        /// <summary>
        ///  Maximum path size using extended syntax or path APIs in the FlexFileService (default).
        /// </summary>
        /// <remarks>
        ///  Windows APIs need extended syntax to get past 260 characters (including the null terminator).
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
        private static readonly char[] s_directorySeparatorCharacters
            = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

        private static readonly char[] s_wildCards = new char[] { '?', '*', '\"', '>', '<' };

        /// <summary>
        ///  Return true if the string contains any wildcards.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="startIndex">The index to start looking from.</param>
        public static unsafe bool ContainsWildcards(ReadOnlySpan<char> value, int startIndex = 0)
            => value.Slice(startIndex).IndexOfAny(s_wildCards) != -1;

        /// <summary>
        ///  The default directory separator.
        /// </summary>
        public static readonly char DirectorySeparator = System.IO.Path.DirectorySeparatorChar;

        /// <summary>
        ///  The alternate directory separator.
        /// </summary>
        public static readonly char AltDirectorySeparator = System.IO.Path.AltDirectorySeparatorChar;

        /// <summary>
        ///  Path prefix for DOS device paths.
        /// </summary>
        public const string DosDevicePathPrefix = @"\\.\";

        /// <summary>
        ///  Path prefix for extended DOS device paths.
        /// </summary>
        public const string ExtendedDosDevicePathPrefix = @"\\?\";

        /// <summary>
        ///  Returns true if the path begins with a directory separator.
        /// </summary>
        public static bool BeginsWithDirectorySeparator(ReadOnlySpan<char> path)
            => path.Length != 0 && IsDirectorySeparator(path[0]);

        /// <summary>
        ///  Returns true if the path ends in a directory separator.
        /// </summary>
        public static bool EndsInDirectorySeparator(StringBuilder path)
            => path?.Length > 0 && IsDirectorySeparator(path[path.Length - 1]);

        /// <summary>
        ///  Returns true if the given character is a directory separator.
        /// </summary>
        public static bool IsDirectorySeparator(char value)
            => value == DirectorySeparator || value == AltDirectorySeparator;

        /// <summary>
        ///  Ensures that the specified path ends in a directory separator.
        /// </summary>
        /// <returns>
        ///  <paramref name="path"/> with an appended directory separator if necessary.
        ///  If <paramref name="path"/> is null, returns null.
        /// </returns>
        public static string? AddTrailingSeparator(string? path)
            => path == null || Path.EndsInDirectorySeparator(path)
                ? path
                : path + DirectorySeparator;

        /// <summary>
        ///  Ensures that the specified path does not end in a directory separator.
        /// </summary>
        /// <returns>The path with an appended directory separator if necessary.</returns>
        public static string TrimTrailingSeparators(string path)
            => Path.EndsInDirectorySeparator(path) ? path.TrimEnd(s_directorySeparatorCharacters) : path;

        /// <summary>
        ///  Combines two string builders into the first string builder, adding a directory separator between if needed.
        ///  Does not validate path characters.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path1"/> is null.</exception>
        public static void Join(StringBuilder path1, ReadOnlySpan<char> path2)
        {
            if (path1 == null) throw new ArgumentNullException(nameof(path1));

            // Add nothing to something is something
            if (path2.Length == 0) return;

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
        ///  Returns true if the given path is a device path.
        /// </summary>
        /// <remarks>
        ///  This will return true if the path returns any of the following with either forward or back slashes.
        ///  \\?\  \??\  \\.\
        /// </remarks>
        public static bool IsDosDevicePath(ReadOnlySpan<char> path)
        {
            return path.Length >= DosDevicePathPrefix.Length
                && IsDirectorySeparator(path[0])
                &&
                (
                    (IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?'))
                    || (path[1] == '?' && path[2] == '?')
                )
                && IsDirectorySeparator(path[3]);
        }

        /// <summary>
        ///  Returns true if the given path is extended and will skip normalization and MAX_PATH checks.
        /// </summary>
        public static bool IsExtendedDosDevicePath(ReadOnlySpan<char> path)
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

        /// <summary>
        ///  Chops off the last path segment.
        /// </summary>
        public static string TrimLastSegment(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            path = TrimTrailingSeparators(path);
            int length = path.Length;
            while (((length > 0)
                && (path[--length] != DirectorySeparator))
                && (path[length] != AltDirectorySeparator)) { }
            return path[..length];
        }

        /// <summary>
        ///  Returns the last path segment.
        /// </summary>
        public static string GetLastSegment(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            path = TrimTrailingSeparators(path);
            int lastSeparator = path.Length;
            while (((lastSeparator > 0)
                && (path[--lastSeparator] != DirectorySeparator))
                && (path[lastSeparator] != AltDirectorySeparator)) { }

            // Return everything past the last separator
            return lastSeparator == 0 ? path : path[++lastSeparator..];
        }
    }
}