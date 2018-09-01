// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Multimedia
{
    // https://msdn.microsoft.com/en-us/library/dd743680.aspx
    [Flags]
    public enum PlaySoundOptions : uint
    {
        /// <summary>
        /// Play the sound synchronously (default). [SND_SYNC]
        /// </summary>
        Sync = 0x00000000,

        /// <summary>
        /// Play the sound asynchronously. [SND_ASYNC]
        /// </summary>
        Async = 0x00000001,

        /// <summary>
        /// Don't play the default sound if the specified sound cannot be found. [SND_NODEFAULT]
        /// </summary>
        NoDefault = 0x00000002,

        /// <summary>
        /// The specified sound is a resource handle. [SND_MEMORY]
        /// </summary>
        Memory = 0x00000004,

        /// <summary>
        /// Loop the sound until PlaySound is called again with null. Requires <see cref="Async"/> flag. [SND_LOOP]
        /// </summary>
        Loop = 0x00000008,

        /// <summary>
        /// Doesn't stop currently playing sounds, even if it can't play the specified one. [SND_NOSTOP]
        /// </summary>
        NoStop = 0x00000010,

        /// <summary>
        /// The sound is an application specific alias as defined in the registry. [SND_APPLICATION]
        /// </summary>
        Application = 0x00000080,

        /// <summary>
        /// System event alias in the registry or WIN.INI. Don't use with <see cref="FileName"/> or <see cref="Resource"/>. [SND_ALIAS]
        /// </summary>
        Alias = 0x00010000,

        /// <summary>
        /// The specified sound is a filename. [SND_FILENAME]
        /// </summary>
        FileName = 0x00020000,

        /// <summary>
        /// The specified sound is a resource identifier in the specified module. [SND_RESOURCE]
        /// </summary>
        Resource = 0x00040004,

        /// <summary>
        /// Generates a visual cue if the SoundSentry accessibility feature is enabled. [SND_SENTRY]
        /// </summary>
        Sentry = 0x00080000,

        /// <summary>
        /// The specified sound is a predefined id (PlaySoundAlias). [SND_ALIAS_ID]
        /// </summary>
        AliasId = 0x00110000,

        /// <summary>
        /// Plays the sound in the system notification sounds session. (Plays at the "System Sounds" volume.) [SND_SYSTEM]
        /// </summary>
        System = 0x00200000
    }
}
