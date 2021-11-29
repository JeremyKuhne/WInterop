// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.Storage;

namespace WInterop.Shell;

/// <summary>
///  Known folder definition. [KNOWNFOLDER_DEFINITION]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/desktop/bb773325.aspx
[StructLayout(LayoutKind.Sequential)]
public class KnownFolderDefinition : IDisposable
{
    private readonly KnownFolderCategory _category;
    private IntPtr _pszName;
    private IntPtr _pszDescription;
    private Guid _fidParent;
    private IntPtr _pszRelativePath;
    private IntPtr _pszParsingName;
    private IntPtr _pszTooltip;
    private IntPtr _pszLocalizedName;
    private IntPtr _pszIcon;
    private IntPtr _pszSecurity;
    private readonly AllFileAttributes _dwAttributes;
    private readonly KnownFolderDefinitionFlags _kfdFlags;
    private Guid _ftidType;

    // Can't use a bool here as the class will no longer be blittable
    private int _disposed;

    private bool Disposed
    {
        get { return _disposed != 0; }
    }

    public KnownFolderDefinition()
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Interlocked.CompareExchange(ref _disposed, 0, 1) == 0)
        {
            // This API doesn't care if the pointer is already zero
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszName, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszDescription, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszRelativePath, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszParsingName, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszTooltip, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszLocalizedName, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszIcon, IntPtr.Zero));
            Marshal.FreeCoTaskMem(Interlocked.Exchange(ref _pszSecurity, IntPtr.Zero));
        }
    }

    ~KnownFolderDefinition()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public KnownFolderCategory Category
    {
        get { return Disposed ? 0 : _category; }
    }

    public string? Name
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszName); }
    }

    public string? Description
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszDescription); }
    }

    public Guid Parent
    {
        get { return Disposed ? Guid.Empty : _fidParent; }
    }

    public string? RelativePath
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszRelativePath); }
    }

    public string? ParsingName
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszParsingName); }
    }

    public string? Tooltip
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszTooltip); }
    }

    public string? LocalizedName
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszLocalizedName); }
    }

    public string? Icon
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszIcon); }
    }

    public string? Security
    {
        get { return Disposed ? null : Marshal.PtrToStringUni(_pszSecurity); }
    }

    public AllFileAttributes Attributes
    {
        get { return Disposed ? 0 : _dwAttributes; }
    }

    public KnownFolderDefinitionFlags Flags
    {
        get { return Disposed ? 0 : _kfdFlags; }
    }

    public Guid FolderTypeId
    {
        get { return Disposed ? Guid.Empty : _ftidType; }
    }
}