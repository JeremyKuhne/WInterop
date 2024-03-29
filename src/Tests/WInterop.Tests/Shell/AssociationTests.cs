﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Registry;
using WInterop.Errors;
using WInterop.Shell;

namespace ShellTests;

public class AssociationTests
{
    [Fact]
    public void GetTextAssociation_ProgID()
    {
        string value = ShellMethods.AssocQueryString(
            AssociationFlags.NoUserSettings,
            AssociationString.ProgId,
            ".txt",
            null);

        if (!string.Equals(value, "txtfile") && value.StartsWith("Appl"))
        {
            // Example: Applications\notepad++.exe
            value.Should().StartWith("Applications\\").And.EndWith(".exe");
        }
    }

    [Fact]
    public void GetTextAssociation_OpenCommand()
    {
        string value = ShellMethods.AssocQueryString(AssociationFlags.None, AssociationString.Command, ".txt", "open");

        // Example: "C:\Program Files (x86)\Notepad++\notepad++.exe" "%1"
        value.Should().Contain(".exe");
    }

    [Fact]
    public void GetTextAssociation_OpenCommandExecutable()
    {
        string value = ShellMethods.AssocQueryString(AssociationFlags.None, AssociationString.Executable, ".txt", "open");
        // Example: C:\Program Files (x86)\Notepad++\notepad++.exe
        value.Should().EndWithEquivalent(".exe");
    }

    [Fact]
    public void GetTextAssociation_FriendlyDocName()
    {
        string value = ShellMethods.AssocQueryString(AssociationFlags.None, AssociationString.FriendlyDocName, ".txt", null);
        value.Should().BeOneOf("TXT File", "Text Document");
    }

    [Fact]
    public void GetTextAssociation_FriendlyAppName()
    {
        string value = ShellMethods.AssocQueryString(AssociationFlags.None, AssociationString.FriendlyAppName, ".txt", null);
        // Example: Notepad++ : a free (GNU) source code editor
        value.Should().StartWith("Notepad");
    }

    [Fact]
    public void GetTextAssociation_AppId()
    {
        try
        {
            string association = ShellMethods.AssocQueryString(
                AssociationFlags.NoUserSettings,
                AssociationString.AppId,
                ".txt",
                null);

            association.Should().NotBeNullOrEmpty();
        }
        catch (Exception e)
        {
            // No application is associated with the specified file for this operation.
            e.HResult.Should().Be((int)WindowsError.ERROR_NO_ASSOCIATION.ToHResult());
        }
    }

    [Fact]
    public void GetTextAssociation_ContentType()
    {
        string value = ShellMethods.AssocQueryString(AssociationFlags.None, AssociationString.ContentType, ".txt", null);
        value.Should().Be(@"text/plain");
    }

    [Fact]
    public void GetTextAssociation_QuickTip()
    {
        string value = ShellMethods.AssocQueryString(AssociationFlags.None, AssociationString.QuickTip, ".txt", null);
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
        RegistryKeyHandle key = ShellMethods.AssocQueryKey(AssociationFlags.None, AssociationKey.App, ".txt", null);

        string name = Registry.QueryKeyName(key);

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
        RegistryKeyHandle key = ShellMethods.AssocQueryKey(AssociationFlags.None, AssociationKey.BaseClass, ".txt", null);

        string name = Registry.QueryKeyName(key);

        // \REGISTRY\USER\S-1-5-21-2477298427-4111324449-2912218533-1001_Classes\*
        name.Should().StartWith(@"\REGISTRY\USER\S-").And.EndWith(@"_Classes\*");
    }

    [Fact]
    public void GetTextAssociation_ClassKey()
    {
        RegistryKeyHandle key = ShellMethods.AssocQueryKey(AssociationFlags.None, AssociationKey.Class, ".txt", null);

        // \REGISTRY\USER\S-1-5-21-2477298427-4111324449-2912218533-1001_Classes\Applications\notepad++.exe
        string name = Registry.QueryKeyName(key);

        if (name.StartsWith(@"\REGISTRY\MACHINE"))
        {
            // No user overrides.
            name.Should().StartWith(@"\REGISTRY\MACHINE\SOFTWARE\Classes\txtfile");
        }
        else
        {
            // Has a user setting, should be something like:
            name.Should().StartWith(@"\REGISTRY\USER\S-");
        }
    }

    [Fact]
    public void GetTextAssociation_ShellExecClassKey()
    {
        RegistryKeyHandle key = ShellMethods.AssocQueryKey(AssociationFlags.None, AssociationKey.ShellExecClass, ".txt", null);

        string name = Registry.QueryKeyName(key);

        if (name.StartsWith(@"\REGISTRY\MACHINE"))
        {
            // No user overrides.
            name.Should().StartWith(@"\REGISTRY\MACHINE\SOFTWARE\Classes\txtfile");
        }
        else
        {
            // Has a user setting, should be something like:
            name.Should().StartWith(@"\REGISTRY\USER\S-");
        }
    }
}
