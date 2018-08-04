// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Globalization
{
    public class LocaleInfo
    {
        private LocaleInfo() { }

        public static LocaleInfo Instance { get; } = new LocaleInfo();

        public const string LOCALE_NAME_USER_DEFAULT = null;
        public const string LOCALE_NAME_INVARIANT = "";
        public const string LOCALE_NAME_SYSTEM_DEFAULT = "!x-sys-default-locale";

        /// <summary>
        /// Returns whether or not the specified locale uses a 24 hour clock.
        /// </summary>
        public unsafe bool GetIs24HourClock(string localeName = null)
        {
            char* data = stackalloc char[2];
            int result = GlobalizationMethods.Imports.GetLocaleInfoEx(localeName, (uint)LocaleInfoType.LOCALE_ITIME, data, 2);
            if (result != 2)
                throw Error.GetIoExceptionForLastError();

            return data[0] == '1';
        }

        /// <summary>
        /// Returns whether or not the specified locale has leading zeros for hours.
        /// </summary>
        public unsafe bool GetHoursHaveLeadingZeros(string localeName = null)
        {
            char* data = stackalloc char[2];
            int result = GlobalizationMethods.Imports.GetLocaleInfoEx(localeName, (uint)LocaleInfoType.LOCALE_ITLZERO, data, 2);
            if (result != 2)
                throw Error.GetIoExceptionForLastError();

            return data[0] == '1';
        }
    }
}
