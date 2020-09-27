// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus.EmfPlus
{
    public enum EmfType
    {
        /// <summary>
        ///  No EMF+, just EMF
        /// </summary>
        EmfOnly = 3,

        /// <summary>
        ///  No EMF, just EMF+
        /// </summary>
        EmfPlusOnly,

        /// <summary>
        ///  Both EMF and EMF+
        /// </summary>
        EmfPlusDual
    }
}
