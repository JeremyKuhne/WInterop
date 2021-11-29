// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Modules;
using WInterop.Multimedia.Native;

namespace WInterop.Multimedia;

public static partial class Multimedia
{
    public static bool PlaySound(PlaySoundAlias alias, PlaySoundOptions options)
    {
        options |= PlaySoundOptions.AliasId;
        return Imports.PlaySoundW((IntPtr)alias, ModuleInstance.Null, options);
    }
}