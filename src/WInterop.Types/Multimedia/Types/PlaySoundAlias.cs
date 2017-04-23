// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Multimedia.Types
{
    public enum PlaySoundAlias : uint
    {
        // #define SND_ALIAS_START 0           /* alias base */
        // #define sndAlias(ch0, ch1)      (SND_ALIAS_START + (DWORD)(BYTE)(ch0) | ((DWORD)(BYTE)(ch1) << 8))

        SND_ALIAS_SYSTEMASTERISK        = 'S' | (((uint)'*') << 8),
        SND_ALIAS_SYSTEMQUESTION        = 'S' | (((uint)'?') << 8),
        SND_ALIAS_SYSTEMHAND            = 'S' | (((uint)'H') << 8),
        SND_ALIAS_SYSTEMEXIT            = 'S' | (((uint)'E') << 8),
        SND_ALIAS_SYSTEMSTART           = 'S' | (((uint)'S') << 8),
        SND_ALIAS_SYSTEMWELCOME         = 'S' | (((uint)'W') << 8),
        SND_ALIAS_SYSTEMEXCLAMATION     = 'S' | (((uint)'!') << 8),
        SND_ALIAS_SYSTEMDEFAULT         = 'S' | (((uint)'D') << 8),
    }
}
