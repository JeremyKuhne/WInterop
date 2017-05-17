// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Modules.Types;
using WInterop.Multimedia.Types;

namespace WInterop.Multimedia
{
    public static partial class MultimediaMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            [DllImport(Libraries.Winmm, ExactSpelling = true)]
            public static extern bool PlaySoundW(
                IntPtr pszSound,
                ModuleInstance hmod,
                PlaySoundOptions fdwSound);
        }

        public static bool PlaySound(PlaySoundAlias alias, PlaySoundOptions options)
        {
            options |= PlaySoundOptions.SND_ALIAS_ID;
            return Imports.PlaySoundW((IntPtr)alias, ModuleInstance.Null, options);
        }
    }
}
