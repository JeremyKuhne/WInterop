// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.Security;
using WInterop.Storage;
using WInterop.SystemInformation;
using Xunit;

namespace Tests.File
{
    public class FileSecurityTests
    {
        [Fact]
        public void GetSidForCreatedFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var handle = Storage.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    SID sid = Storage.QueryOwner(handle);
                    sid.IdentifierAuthority.Should().Be(IdentifierAuthority.NT);
                    string sidString = sid.ConvertSidToString();
                    AccountSidInformation info = sid.LookupAccountSid();
                    info.Usage.Should().Be(SidNameUse.User);
                    info.Name.Should().Be(WInterop.SystemInformation.SystemInformation.GetUserName());
                }
            }
        }
    }
}
