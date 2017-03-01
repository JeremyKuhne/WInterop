// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support
{
    public static class Arrays
    {
        /// <summary>
        /// Compares two arrays for equivalency. All indicies must match.
        /// </summary>
        public static bool AreEquivalent<T>(T[] left, T[] right)
        {
            if (ReferenceEquals(left, right)) { return true; }

            bool isLeftNullOrEmpty = (left == null) || (left.Length == 0);
            bool isRightNullOrEmpty = (right == null) || (right.Length == 0);

            if (isLeftNullOrEmpty)
            {
                return isRightNullOrEmpty;
            }
            else if (isRightNullOrEmpty)
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (int i = 0; i < left.Length; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
