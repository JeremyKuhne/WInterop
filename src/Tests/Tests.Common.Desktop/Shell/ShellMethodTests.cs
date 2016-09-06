// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.Shell;
using WInterop.Shell.DataTypes;
using System.Linq;

namespace DesktopTests.ShellTests
{
    public class ShellMethodsTests
    {
        [Fact]
        public void GetKnownFolderPath_Basic()
        {
            string windowsFolder = ShellDesktopMethods.GetKnownFolderPath(KNOWNFOLDERID.Windows);
            windowsFolder.Should().EndWith("Windows");
        }

        [Fact]
        public void GetKnownFolderId_Basic()
        {
            using (var id = ShellDesktopMethods.GetKnownFolderId(KNOWNFOLDERID.ProgramData))
            {
                id.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetIdName_Basic()
        {
            using (var id = ShellDesktopMethods.GetKnownFolderId(KNOWNFOLDERID.Windows))
            {
                id.IsInvalid.Should().BeFalse();
                ShellDesktopMethods.GetNameFromId(id, SIGDN.PARENTRELATIVE).Should().Be("Windows");
            }
        }

        [Fact]
        public void KnownFolderManager_Create()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            manager.Should().NotBeNull();
        }

        [Fact]
        public void KnownFolderManager_RoundTripId()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            int csidl = manager.FolderIdToCsidl(KNOWNFOLDERID.Windows);
            csidl.Should().Be(36);
            Guid id = manager.FolderIdFromCsidl(csidl);
            id.Should().Be(KNOWNFOLDERID.Windows);
        }

        [Fact]
        public void KnownFolderManager_GetKnownFolder()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            folder.Should().NotBeNull();
        }

        [Fact]
        public void KnownFolder_GetId()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            folder.GetId().Should().Be(KNOWNFOLDERID.Windows);
        }

        [Fact]
        public void KnownFolder_GetCategory()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            folder.GetCategory().Should().Be(KF_CATEGORY.FIXED);
        }

        [Fact]
        public void KnownFolder_GetIShellItem()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            IShellItem item = folder.GetShellItem(0, new Guid(InterfaceIds.IID_IShellItem));
            item.Should().NotBeNull();
            IShellItem2 item2 = (IShellItem2)folder.GetShellItem(0, new Guid(InterfaceIds.IID_IShellItem2));
            item2.Should().NotBeNull();
        }

        [Fact]
        public void KnownFolder_GetPath()
        {
            using (var id = ShellDesktopMethods.GetKnownFolderId(KNOWNFOLDERID.Windows))
            {
                id.IsInvalid.Should().BeFalse();
                ShellDesktopMethods.GetNameFromId(id, SIGDN.PARENTRELATIVE).Should().Be("Windows");

                IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
                IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
                folder.GetPath(0).Should().Be(ShellDesktopMethods.GetNameFromId(id, SIGDN.FILESYSPATH));
            }
        }

        [Fact]
        public void KnownFolder_GetIdList()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            using (var id = folder.GetIDList(0))
            {
                id.IsInvalid.Should().BeFalse();
                ShellDesktopMethods.GetNameFromId(id, SIGDN.PARENTRELATIVE).Should().Be("Windows");
            }
        }

        [Fact]
        public void KnownFolder_GetFolderType()
        {
            // Most known folders don't have a FolderType associated and will throw a COMException with E_FAIL.
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Contacts);
            folder.GetFolderType().Should().Be(FOLDERTYPEID.Contacts);
        }

        [Fact]
        public void KnownFolder_GetRedirectionCaps()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Libraries);
            folder.GetRedirectionCapabilities().Should().Be(KF_REDIRECTION_CAPABILITIES.REDIRECTABLE);
        }

        [Fact]
        public void KnownFolder_GetDefinition()
        {
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
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
            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();
            IKnownFolder folder = manager.GetFolder(KNOWNFOLDERID.Windows);
            IShellItem item = folder.GetShellItem(0, new Guid(InterfaceIds.IID_IShellItem));
            item.GetDisplayName(SIGDN.NORMALDISPLAY).Should().Be("Windows");
        }

        [Fact]
        public void GetKnownFolderIds_Basic()
        {
            var ids = ShellDesktopMethods.GetKnownFolderIds();
            ids.Should().NotBeEmpty();

            IKnownFolderManager manager = ShellDesktopMethods.GetKnownFolderManager();

            // Check a few 
            foreach (var id in ids.Take(5))
            {
                manager.GetFolder(id).Should().NotBeNull();
            }
        }
    }
}
