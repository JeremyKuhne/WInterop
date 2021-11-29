// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Shared.Support;
using Tests.Support;
using WInterop.Security;
using WInterop.Storage;
using Xunit;

namespace StorageTests;

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
                SID sid = Storage.GetOwner(handle);
                sid.IdentifierAuthority.Should().Be(IdentifierAuthority.NT);
                string sidString = sid.ConvertSidToString();
                AccountSidInformation info = sid.LookupAccountSid();
                info.Usage.Should().Be(SidNameUse.User);
                info.Name.Should().Be(WInterop.SystemInformation.SystemInformation.GetUserName());
            }
        }
    }

    [Fact]
    public void GetGroupSidForCreatedFile()
    {
        using (var cleaner = new TestFileCleaner())
        {
            using (var handle = Storage.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew))
            {
                handle.IsInvalid.Should().BeFalse();
                SID sid = Storage.GetPrimaryGroup(handle);
                sid.IdentifierAuthority.Should().Be(IdentifierAuthority.NT);
                string sidString = sid.ConvertSidToString();
                AccountSidInformation info = sid.LookupAccountSid();
                info.Usage.Should().Be(SidNameUse.User);
                info.Name.Should().Be(WInterop.SystemInformation.SystemInformation.GetUserName());
            }
        }
    }

    [Fact]
    public void GetDiscrectionaryAclForCreatedFile()
    {
        using (var cleaner = new TestFileCleaner())
        {
            using (var handle = Storage.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew))
            {
                handle.IsInvalid.Should().BeFalse();
                using (SecurityDescriptor descriptor = Storage.GetAccessControlList(handle))
                {
                    List<ExplicitAccess> access = descriptor.GetExplicitEntriesFromAcl().ToList();
                    access.Count.Should().Be(0);
                }
            }
        }
    }

    [Fact]
    public void AddGroup_NoWriteDac()
    {
        using (var cleaner = new TestFileCleaner())
        {
            using (var handle = Storage.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite))
            {
                handle.IsInvalid.Should().BeFalse();
                SID usersGroup = Security.CreateWellKnownSid(WellKnownSID.Users);
                Action action = () => handle.ChangeAccess(usersGroup, FileAccessRights.GenericRead, AccessMode.Grant);
                action.Should().Throw<UnauthorizedAccessException>();
            }
        }
    }

    [Fact]
    public void AddGroup_UsersRead()
    {
        using (var cleaner = new TestFileCleaner())
        {
            using (var handle = Storage.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew,
                DesiredAccess.GenericReadWrite | DesiredAccess.WriteDac))
            {
                handle.IsInvalid.Should().BeFalse();
                SID usersGroup = Security.CreateWellKnownSid(WellKnownSID.Users);
                handle.ChangeAccess(usersGroup, FileAccessRights.GenericRead, AccessMode.Grant);

                using (SecurityDescriptor descriptor = Storage.GetAccessControlList(handle))
                {
                    List<ExplicitAccess> access = descriptor.GetExplicitEntriesFromAcl().ToList();
                    access.Count.Should().Be(1);
                    ExplicitAccess ea = access[0];

                    // Here you can see what the generic rights get translated to
                    ((FileAccessRights)ea.Permissions).Should()
                        .Be(FileAccessRights.ReadData | FileAccessRights.ReadAttributes | FileAccessRights.ReadExtendedAttributes
                            | FileAccessRights.Synchronize | FileAccessRights.ReadControl);

                    ea.Inheritance.Should().Be(Inheritance.NoInheritance);
                    ea.Mode.Should().Be(AccessMode.Grant);
                }
            }
        }
    }

    [Fact]
    public void AddUser_AnonymousRead_CanRead()
    {
        using (var cleaner = new TestFileCleaner())
        {
            string path = cleaner.CreateTestFile(nameof(AddUser_AnonymousRead_CanRead));

            using (var handle = Storage.CreateFile(path, CreationDisposition.OpenExisting,
                DesiredAccess.GenericReadWrite | DesiredAccess.WriteDac))
            {
                handle.IsInvalid.Should().BeFalse();
                SID anonymous = Security.CreateWellKnownSid(WellKnownSID.Anonymous);
                handle.ChangeAccess(anonymous, FileAccessRights.GenericRead, AccessMode.Grant);
            }

            ThreadRunner.Run(() =>
            {
                Security.ImpersonateAnonymousToken();
                Action action = () => FileHelper.ReadAllText(path);
                    // The problem here is that Anonymous does not have bypass traverse checking.
                    // All folders, including the root, would need to have DirectoryAccessRights.Traverse.
                    action.Should().Throw<UnauthorizedAccessException>();
                Security.RevertToSelf();
            });
        }
    }

    [Fact]
    public void Impersonate_DisableUser_FileAccess()
    {
        ThreadRunner.Run(() =>
        {
            using (var cleaner = new TestFileCleaner())
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                string path = cleaner.CreateTestFile(nameof(Impersonate_DisableUser_FileAccess));
                FileHelper.ReadAllText(path).Should().Be(nameof(Impersonate_DisableUser_FileAccess));

                    // Disable the primary user token (prevents using for granting access)
                    using (var restricted = Security.CreateRestrictedToken(token, Security.GetTokenUserSid(token)))
                {
                    Security.ImpersonateLoggedOnUser(restricted);
                    Action action = () => FileHelper.ReadAllText(path);
                    action.Should().Throw<UnauthorizedAccessException>();
                    Security.RevertToSelf();
                    FileHelper.ReadAllText(path).Should().Be(nameof(Impersonate_DisableUser_FileAccess));
                }
            }
        });
    }

    [Fact]
    public void Impersonate_DisableUserAddGroup_FileAccess()
    {
        ThreadRunner.Run((() =>
        {
            using (var cleaner = new TestFileCleaner())
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                string path = cleaner.CreateTestFile(nameof(Impersonate_DisableUser_FileAccess));
                FileHelper.ReadAllText(path).Should().Be(nameof(Impersonate_DisableUser_FileAccess));

                    // Add Users group
                    using (var handle = Storage.CreateFile(path, CreationDisposition.OpenExisting,
                    DesiredAccess.GenericReadWrite | DesiredAccess.WriteDac))
                {
                    handle.IsInvalid.Should().BeFalse();
                    SID usersGroup = Security.CreateWellKnownSid(WellKnownSID.Users);
                    handle.ChangeAccess(usersGroup, FileAccessRights.GenericRead, AccessMode.Grant);
                }

                    // Disable the primary user token (prevents using for granting access)
                    using (var restricted = Security.CreateRestrictedToken(token, Security.GetTokenUserSid(token)))
                {
                    Security.ImpersonateLoggedOnUser(restricted);

                        // We'll get access through the Users group
                        FileHelper.ReadAllText(path).Should().Be(nameof(Impersonate_DisableUser_FileAccess));

                    Security.RevertToSelf();
                    FileHelper.ReadAllText(path).Should().Be(nameof(Impersonate_DisableUser_FileAccess));
                }
            }
        }));
    }
}
