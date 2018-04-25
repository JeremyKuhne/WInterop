// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;
using WInterop.ProcessAndThreads;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Authorization
{
    public static partial class AuthorizationMethods
    {
        /// <summary>
        /// Get information about the given security identifier.
        /// </summary>
        /// <param name="systemName">
        /// The target computer to look up the SID on. When null will look on the local machine
        /// then trusted domain controllers.
        /// </param>
        public static AccountSidInformation LookupAccountSid(SID sid, string systemName = null)
        {
            return BufferHelper.TwoBufferInvoke((StringBuffer nameBuffer, StringBuffer domainNameBuffer) =>
            {
                SidNameUse usage;
                uint nameCharCapacity = nameBuffer.CharCapacity;
                uint domainNameCharCapacity = domainNameBuffer.CharCapacity;

                while (!Imports.LookupAccountSidW(
                    systemName,
                    ref sid,
                    nameBuffer,
                    ref nameCharCapacity,
                    domainNameBuffer,
                    ref domainNameCharCapacity,
                    out usage))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    nameBuffer.EnsureCharCapacity(nameCharCapacity);
                    domainNameBuffer.EnsureCharCapacity(domainNameCharCapacity);
                }

                nameBuffer.SetLengthToFirstNull();
                domainNameBuffer.SetLengthToFirstNull();

                AccountSidInformation info = new AccountSidInformation
                {
                    Name = nameBuffer.ToString(),
                    DomainName = domainNameBuffer.ToString(),
                    Usage = usage
                };

                return info;
            });
        }
    }
}
