// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Multimedia;

public enum PlaySoundAlias : uint
{
    // #define SND_ALIAS_START 0           /* alias base */
    // #define sndAlias(ch0, ch1)      (SND_ALIAS_START + (DWORD)(BYTE)(ch0) | ((DWORD)(BYTE)(ch1) << 8))

    /// <summary>
    ///  [SND_ALIAS_SYSTEMASTERISK]
    /// </summary>
    SystemAsterisk = 'S' | (((uint)'*') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMQUESTION]
    /// </summary>
    SystemQuestion = 'S' | (((uint)'?') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMHAND]
    /// </summary>
    SystemHand = 'S' | (((uint)'H') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMEXIT]
    /// </summary>
    SystemExit = 'S' | (((uint)'E') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMSTART]
    /// </summary>
    SystemStart = 'S' | (((uint)'S') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMWELCOME]
    /// </summary>
    SystemWelcome = 'S' | (((uint)'W') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMEXCLAMATION]
    /// </summary>
    SystemExclamation = 'S' | (((uint)'!') << 8),

    /// <summary>
    ///  [SND_ALIAS_SYSTEMDEFAULT]
    /// </summary>
    SystemDefault = 'S' | (((uint)'D') << 8),
}