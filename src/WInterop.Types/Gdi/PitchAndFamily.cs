// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd145132.aspx
    // https://msdn.microsoft.com/en-us/library/dd208051.aspx
    public struct PitchAndFamily
    {
        public byte RawValue;

        /// <summary>
        /// Only has meaning when creating a font.
        /// </summary>
        public FontPitch Pitch
        {
            get => (FontPitch)(RawValue & 0x0F);
            set => RawValue = (byte)((byte)Family | (byte)value);
        }

        /// <summary>
        /// Only has meaning when enumerating a font.
        /// </summary>
        public FontPitchTypes PitchTypes
        {
            get => (FontPitchTypes)(RawValue & 0x0F);
        }

        public FontFamily Family
        {
            get => (FontFamily)(RawValue & 0xF0);
            set => RawValue = (byte)((byte)value | (byte)Pitch);
        }
    }
}
