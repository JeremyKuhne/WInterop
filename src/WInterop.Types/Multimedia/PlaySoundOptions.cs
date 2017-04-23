// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Multimedia.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/dd743680.aspx
    public enum PlaySoundOptions : uint
    {
        /// <summary>
        /// Play the sound synchronously (default).
        /// </summary>
        SND_SYNC            = 0x00000000,

        /// <summary>
        /// Play the sound asynchronously.
        /// </summary>
        SND_ASYNC           = 0x00000001,

        /// <summary>
        /// Don't play the default sound if the specified sound cannot be found.
        /// </summary>
        SND_NODEFAULT       = 0x00000002,

        /// <summary>
        /// The specified sound is a resource handle.
        /// </summary>
        SND_MEMORY          = 0x00000004,

        /// <summary>
        /// Loop the sound until PlaySound is called again with null. Requires SND_ASYNC flag.
        /// </summary>
        SND_LOOP            = 0x00000008,

        /// <summary>
        /// Doesn't stop currently playing sounds, even if it can't play the specified one.
        /// </summary>
        SND_NOSTOP          = 0x00000010,

        /// <summary>
        /// The sound is an application specific alias as defined in the registry.
        /// </summary>
        SND_APPLICATION     = 0x00000080,

        /// <summary>
        /// System event alias in the registry or WIN.INI. Don't use with SND_FILENAME or SND_RESOURCE.
        /// </summary>
        SND_ALIAS           = 0x00010000,

        /// <summary>
        /// The specified sound is a filename.
        /// </summary>
        SND_FILENAME        = 0x00020000,

        /// <summary>
        /// The specified sound is a resource identifier in the specified module.
        /// </summary>
        SND_RESOURCE        = 0x00040004,

        /// <summary>
        /// Generates a visual cue if the SoundSentry accessibility feature is enabled.
        /// </summary>
        SND_SENTRY          = 0x00080000,

        /// <summary>
        /// The specified sound is a predefined id (PlaySoundAlias).
        /// </summary>
        SND_ALIAS_ID        = 0x00110000,

        /// <summary>
        /// Plays the sound in the system notification sounds session. (Plays at the "System Sounds" volume.)
        /// </summary>
        SND_SYSTEM          = 0x00200000
    }
}
