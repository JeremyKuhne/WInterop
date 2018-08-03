// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using WInterop.Authorization;
using WInterop.Authorization.Types;
using WInterop.Shell;
using WInterop.Shell.Types;
using Xunit;

namespace DesktopTests.ShellTests
{
    public class ShellTests
    {
        [Fact]
        public void GetKnownFolderPath_Basic()
        {
            string windowsFolder = ShellMethods.GetKnownFolderPath(KNOWNFOLDERID.Windows);
            windowsFolder.Should().EndWithEquivalent("Windows");
        }

        [Fact]
        public void GetKnownFolderId_Basic()
        {
            using (var id = ShellMethods.GetKnownFolderId(KNOWNFOLDERID.ProgramData))
            {
                id.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetIdName_Basic()
        {
            using (var id = ShellMethods.GetKnownFolderId(KNOWNFOLDERID.Windows))
            {
                id.IsInvalid.Should().BeFalse();
                ShellMethods.GetNameFromId(id, SIGDN.PARENTRELATIVE).Should().Be("Windows");
            }
        }

        [Fact]
        public void KnownFolderManager_Create()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            manager.Should().NotBeNull();
        }

        [Fact]
        public void KnownFolderManager_RoundTripId()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            int csidl = manager.FolderIdToCsidl(KNOWNFOLDERID.Windows);
            csidl.Should().Be(36);
            Guid id = manager.FolderIdFromCsidl(csidl);
            id.Should().Be(KNOWNFOLDERID.Windows);
        }

        [Fact]
        public void KnownFolderManager_GetKnownFolder()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            folder.Should().NotBeNull();
        }

        [Fact]
        public void KnownFolder_GetId()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            folder.GetId().Should().Be(KNOWNFOLDERID.Windows);
        }

        [Fact]
        public void KnownFolder_GetCategory()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            folder.GetCategory().Should().Be(KF_CATEGORY.FIXED);
        }

        [Fact]
        public void KnownFolder_GetIShellItem()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            IShellItem item = folder.GetShellItem(0, new Guid(InterfaceIds.IID_IShellItem));
            item.Should().NotBeNull();
            IShellItem2 item2 = (IShellItem2)folder.GetShellItem(0, new Guid(InterfaceIds.IID_IShellItem2));
            item2.Should().NotBeNull();
        }

        [Fact]
        public void KnownFolder_GetPath()
        {
            using (var id = ShellMethods.GetKnownFolderId(KNOWNFOLDERID.Windows))
            {
                id.IsInvalid.Should().BeFalse();
                ShellMethods.GetNameFromId(id, SIGDN.PARENTRELATIVE).Should().BeEquivalentTo("Windows");

                IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
                IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
                folder.GetPath(0).Should().BeEquivalentTo(ShellMethods.GetNameFromId(id, SIGDN.FILESYSPATH));
            }
        }

        [Fact]
        public void KnownFolder_GetIdList()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            using (var id = folder.GetIDList(0))
            {
                id.IsInvalid.Should().BeFalse();
                ShellMethods.GetNameFromId(id, SIGDN.PARENTRELATIVE).Should().Be("Windows");
            }
        }

        [Fact]
        public void KnownFolder_GetFolderType()
        {
            // Most known folders don't have a FolderType associated and will throw a COMException with E_FAIL.
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Contacts);
            folder.GetFolderType().Should().Be(FOLDERTYPEID.Contacts);
        }

        [Fact]
        public void KnownFolder_GetRedirectionCaps()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Libraries);
            folder.GetRedirectionCapabilities().Should().Be(KF_REDIRECTION_CAPABILITIES.REDIRECTABLE);
        }

        [Fact]
        public void KnownFolder_GetDefinition()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.LocalAppData);
            using (KNOWNFOLDER_DEFINITION definition = new KNOWNFOLDER_DEFINITION())
            {
                folder.GetFolderDefinition(definition);
                definition.FolderTypeId.Should().Be(Guid.Empty);
                definition.Category.Should().Be(KF_CATEGORY.PERUSER);
                definition.Name.Should().Be("Local AppData");
                definition.RelativePath.Should().Be(@"AppData\Local");
                definition.Parent.Should().Be(KNOWNFOLDERID.Profile);
            }
        }

        [Fact]
        public void ShellItem_GetDisplayName()
        {
            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            IShellItem item = folder.GetShellItem(0, new Guid(InterfaceIds.IID_IShellItem));
            item.GetDisplayName(SIGDN.NORMALDISPLAY).Should().Be("Windows");
        }

        [Fact]
        public void GetKnownFolderIds_Basic()
        {
            var ids = ShellMethods.GetKnownFolderIds();
            ids.Should().NotBeEmpty();

            IKnownFolderManager manager = ShellMethods.GetKnownFolderManager();

            // Check a few 
            foreach (var id in ids.Take(5))
            {
                manager.GetFolder(id).Should().NotBeNull();
            }
        }

        [Fact]
        public void UnexpandEnvironmentVariables()
        {
            ShellMethods.UnexpandEnvironmentStrings(Environment.GetEnvironmentVariable("USERPROFILE"))
                .Should().Be("%USERPROFILE%");
        }


        [Fact]
        public void ExpandEnvironmentVariablesForUser()
        {
            ShellMethods.ExpandEnvironmentVariablesForUser(
                Authorization.OpenProcessToken(AccessTokenRights.Impersonate | AccessTokenRights.Query | AccessTokenRights.Duplicate),
                @"%USERNAME%").
                Should().Be(Environment.GetEnvironmentVariable("USERNAME"));
        }
    }
}
