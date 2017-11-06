// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.Authorization;
using WInterop.Authorization.Types;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.SystemInformation;
using Xunit;

namespace DesktopTests.FileManagement
{
    public class FileSecurityTests
    {
        [Fact]
        public void GetSidForCreatedFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var handle = FileMethods.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    FileMethods.QueryOwner(handle, out SID sid);
                    sid.IdentifierAuthority.Should().Be(SID_IDENTIFIER_AUTHORITY.NT);
                    AccountSidInfo info = AuthorizationMethods.LookupAccountSidLocal(ref sid);
                    info.Usage.Should().Be(SidNameUse.User);
                    info.Name.Should().Be(SystemInformationMethods.GetUserName());
                }
            }
        }
    }
}
