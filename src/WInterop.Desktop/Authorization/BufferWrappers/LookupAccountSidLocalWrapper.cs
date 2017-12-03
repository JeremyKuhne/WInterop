// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Authorization.BufferWrappers
{
    public struct LookupAccountSidLocalWrapper : ITwoBufferFunc<StringBuffer, AccountSidInformation>
    {
        public SID Sid;

        AccountSidInformation ITwoBufferFunc<StringBuffer, AccountSidInformation>.Func(StringBuffer nameBuffer, StringBuffer domainNameBuffer)
        {
            SidNameUse usage;
            uint nameCharCapacity = nameBuffer.CharCapacity;
            uint domainNameCharCapacity = domainNameBuffer.CharCapacity;

            while (!AuthorizationMethods.Imports.LookupAccountSidLocalW(
                ref Sid,
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

            return new AccountSidInformation
            {
                Name = nameBuffer.ToString(),
                DomainName = domainNameBuffer.ToString(),
                Usage = usage
            };
        }
    }
}
