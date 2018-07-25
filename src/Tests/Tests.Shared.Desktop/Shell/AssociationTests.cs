// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Registry;
using WInterop.Registry.Types;
using WInterop.ErrorHandling;
using WInterop.Shell;
using WInterop.Shell.Types;
using Xunit;

namespace DesktopTests.ShellTests
{
    public class AssociationTests
    {
        [Fact]
        public void GetTextAssociation_ProgID()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.NoUserSettings, ASSOCSTR.ProgId, ".txt", null);

            if (!string.Equals(value, "txtfile"))
            {
                // Example: Applications\notepad++.exe
                value.Should().StartWith("Applications\\").And.EndWith(".exe");
            }
        }

        [Fact]
        public void GetTextAssociation_OpenCommand()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.None, ASSOCSTR.Command, ".txt", "open");

            // Example: "C:\Program Files (x86)\Notepad++\notepad++.exe" "%1"
            value.Should().Contain("%1");
        }

        [Fact]
        public void GetTextAssociation_OpenCommandExecutable()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.None, ASSOCSTR.Executable, ".txt", "open");
            // Example: C:\Program Files (x86)\Notepad++\notepad++.exe
            value.Should().EndWithEquivalent(".exe");
        }

        [Fact]
        public void GetTextAssociation_FriendlyDocName()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.None, ASSOCSTR.FriendlyDocName, ".txt", null);
            value.Should().BeOneOf("TXT File", "Text Document");
        }

        [Fact]
        public void GetTextAssociation_FriendlyAppName()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.None, ASSOCSTR.FriendlyAppName, ".txt", null);
            // Example: Notepad++ : a free (GNU) source code editor
            value.Should().StartWith("Notepad");
        }

        [Fact]
        public void GetTextAssociation_AppId()
        {
            Action action = () => ShellMethods.AssocQueryString(ASSOCF.NoUserSettings, ASSOCSTR.AppId, ".txt", null);
            // No application is associated with the specified file for this operation.
            action.Should().Throw<WInteropIOException>().And.HResult.Should().Be(unchecked((int)0x80070483));
        }

        [Fact]
        public void GetTextAssociation_ContentType()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.None, ASSOCSTR.ContentType, ".txt", null);
            value.Should().Be(@"text/plain");
        }

        [Fact]
        public void GetTextAssociation_QuickTip()
        {
            string value = ShellMethods.AssocQueryString(ASSOCF.None, ASSOCSTR.QuickTip, ".txt", null);
            value.Should().Be("prop:System.ItemTypeText;System.Size;System.DateModified");

            IPropertyDescriptionList list = ShellMethods.GetPropertyDescriptionListFromString(value);
            list.GetCount().Should().Be(3);
            IPropertyDescription desc = list.GetAt(0, new Guid(InterfaceIds.IID_IPropertyDescription));
            desc.GetCanonicalName().Should().Be("System.ItemTypeText");
            desc = list.GetAt(1, new Guid(InterfaceIds.IID_IPropertyDescription));
            desc.GetCanonicalName().Should().Be("System.Size");
            desc = list.GetAt(2, new Guid(InterfaceIds.IID_IPropertyDescription));
            desc.GetCanonicalName().Should().Be("System.DateModified");
        }

        [Fact]
        public void GetTextAssociation_AppKey()
        {
            RegistryKeyHandle key = ShellMethods.AssocQueryKey(ASSOCF.None, ASSOCKEY.App, ".txt", null);

            string name = RegistryMethods.QueryKeyName(key);

            if (name.StartsWith(@"\REGISTRY\MACHINE"))
            {
                // No user overrides.
                name.Should().StartWith(@"\REGISTRY\MACHINE\SOFTWARE\Classes\Applications\notepad.exe");
            }
            else
            {
                // Has a user setting, should be something like:
                // \REGISTRY\USER\S-1-5-21-2477298427-4111324449-2912218533-1001_Classes\Applications\notepad++.exe
                name.Should().StartWith(@"\REGISTRY\USER\S-").And.EndWith(".exe");
            }
        }

        [Fact]
        public void GetTextAssociation_BaseClassKey()
        {
            RegistryKeyHandle key = ShellMethods.AssocQueryKey(ASSOCF.None, ASSOCKEY.BaseClass, ".txt", null);

            string name = RegistryMethods.QueryKeyName(key);

            // \REGISTRY\USER\S-1-5-21-2477298427-4111324449-2912218533-1001_Classes\*
            name.Should().StartWith(@"\REGISTRY\USER\S-").And.EndWith(@"_Classes\*");
        }

        [Fact]
        public void GetTextAssociation_ClassKey()
        {
            RegistryKeyHandle key = ShellMethods.AssocQueryKey(ASSOCF.None, ASSOCKEY.Class, ".txt", null);

            // \REGISTRY\USER\S-1-5-21-2477298427-4111324449-2912218533-1001_Classes\Applications\notepad++.exe
            string name = RegistryMethods.QueryKeyName(key);

            if (name.StartsWith(@"\REGISTRY\MACHINE"))
            {
                // No user overrides.
                name.Should().StartWith(@"\REGISTRY\MACHINE\SOFTWARE\Classes\txtfile");
            }
            else
            {
                // Has a user setting, should be something like:
                name.Should().StartWith(@"\REGISTRY\USER\S-").And.EndWith(".exe");
            }
        }

        [Fact]
        public void GetTextAssociation_ShellExecClassKey()
        {
            RegistryKeyHandle key = ShellMethods.AssocQueryKey(ASSOCF.None, ASSOCKEY.ShellExecClass, ".txt", null);

            string name = RegistryMethods.QueryKeyName(key);

            if (name.StartsWith(@"\REGISTRY\MACHINE"))
            {
                // No user overrides.
                name.Should().StartWith(@"\REGISTRY\MACHINE\SOFTWARE\Classes\txtfile");
            }
            else
            {
                // Has a user setting, should be something like:
                name.Should().StartWith(@"\REGISTRY\USER\S-").And.EndWith(".exe");
            }
        }
    }
}
