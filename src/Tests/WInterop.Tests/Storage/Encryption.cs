// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Shared.Support;
using Tests.Support;
using WInterop.Errors;
using WInterop.Security;
using WInterop.Storage;
using Xunit;

namespace StorageTests;

public class Encryption
{
    [Fact]
    public void EncryptFile_Basic()
    {
        using (var cleaner = new TestFileCleaner())
        {
            string path = cleaner.CreateTestFile(nameof(EncryptFile_Basic));
            FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_Basic));
            Storage.EncryptFile(path);
            FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_Basic));
            Storage.DecryptFile(path);
            FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_Basic));
        }
    }

    [Fact]
    public void EncryptFile_AsAnonymous()
    {
        ThreadRunner.Run(() =>
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.CreateTestFile(nameof(EncryptFile_AsAnonymous));
                FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_AsAnonymous));
                Storage.EncryptFile(path);

                FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_AsAnonymous));
                Security.ImpersonateAnonymousToken();
                Action action = () => FileHelper.ReadAllText(path);
                action.Should().Throw<UnauthorizedAccessException>();
                Security.RevertToSelf();
            }
        });
    }

    [Fact]
    public void EncryptFile_DisableRightsBeforeEncrypting()
    {
        ThreadRunner.Run((() =>
        {
            using (var cleaner = new TestFileCleaner())
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query | AccessTokenRights.Impersonate))
            {
                    // Create and encrypt the file
                    string path = cleaner.CreateTestFile(nameof(EncryptFile_DisableRightsBeforeEncrypting));

                Action action;

                    // Disable the primary user token (prevents using for granting access)
                    using (var restricted = Security.CreateRestrictedToken(token, Security.GetTokenUserSid(token)))
                {
                    Security.ImpersonateLoggedOnUser(restricted);
                    action = () => Storage.EncryptFile(path);
                    action.Should().Throw<UnauthorizedAccessException>();
                    Security.RevertToSelf();
                }

                action = () => Storage.QueryUsersOnEncryptedFile(path);
                action.Should().Throw<WInteropIOException>()
                    .And.HResult.Should().Be((int)WindowsError.ERROR_FILE_NOT_ENCRYPTED.ToHResult());
            }
        }));
    }

    [Fact]
    public void EncryptFile_DisableRightsAfterEncrypting()
    {
        ThreadRunner.Run((() =>
        {
            using (var cleaner = new TestFileCleaner())
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                    // Create and encrypt the file
                    string path = cleaner.CreateTestFile(nameof(EncryptFile_DisableRightsAfterEncrypting));
                Storage.EncryptFile(path);
                FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_DisableRightsAfterEncrypting));

                    // Add Users group to the DACL
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

                        // The primary user token being disabled doesn't impact the implicit decryption
                        // (We're getting rights to the file through the Users group)
                        FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_DisableRightsAfterEncrypting));
                    Security.RevertToSelf();
                }
            }
        }));
    }

    [Fact]
    public void EncryptFile_AccessAsAnonymousAfterEncrypting()
    {
        ThreadRunner.Run((() =>
        {
            using (var cleaner = new TestFileCleaner())
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                    // Create and encrypt the file
                    string path = cleaner.CreateTestFile(nameof(EncryptFile_AccessAsAnonymousAfterEncrypting));
                Storage.EncryptFile(path);
                FileHelper.ReadAllText(path).Should().Be(nameof(EncryptFile_AccessAsAnonymousAfterEncrypting));

                    // Give anonymous rights
                    using (var handle = Storage.CreateFile(path, CreationDisposition.OpenExisting,
                    DesiredAccess.GenericReadWrite | DesiredAccess.WriteDac))
                {
                    handle.IsInvalid.Should().BeFalse();
                    SID anonymous = Security.CreateWellKnownSid(WellKnownSID.Anonymous);
                    handle.ChangeAccess(anonymous, FileAccessRights.GenericRead, AccessMode.Grant);
                }

                    // Should still have only one encrypted user
                    var sids = Storage.QueryUsersOnEncryptedFile(path);
                sids.Count().Should().Be(1);

                    // Try accessing as anonymous
                    Security.ImpersonateAnonymousToken();
                Action action = () => FileHelper.ReadAllText(path);
                action.Should().Throw<UnauthorizedAccessException>();
                Security.RevertToSelf();
            }
        }));
    }

    [Fact]
    public unsafe void EncryptFile_GetUsers()
    {
        using (var cleaner = new TestFileCleaner())
        {
            string path = cleaner.CreateTestFile(nameof(EncryptFile_GetUsers));
            Storage.EncryptFile(path);
            var sids = Storage.QueryUsersOnEncryptedFile(path);
            sids.Count().Should().Be(1);

            // Check that the SID is our current user
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.GetTokenUserSid().Sid.Equals(sids.First()).Should().BeTrue();
            }
        }
    }

    [Fact]
    public unsafe void EncryptFile_RemoveOnlyUser()
    {
        using (var cleaner = new TestFileCleaner())
        {
            string path = cleaner.CreateTestFile(nameof(EncryptFile_GetUsers));
            Storage.EncryptFile(path);

            var sids = Storage.QueryUsersOnEncryptedFile(path);
            sids.Count().Should().Be(1);

            // Check that the SID is our current user
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                Action action = () => Storage.RemoveUser(token.GetTokenUserSid().Sid, path);

                // You can't remove all users
                action.Should().Throw<UnauthorizedAccessException>();
            }

            sids = Storage.QueryUsersOnEncryptedFile(path);
            sids.Count().Should().Be(1);
        }
    }
}
