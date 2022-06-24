// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles;

/// <summary>
///  Wrapper for a native certificate store handle.
/// </summary>
public class CertificateStoreHandle : HandleZeroIsInvalid
{
    public CertificateStoreHandle() : base(ownsHandle: true)
    {
    }

    public CertificateStoreHandle(bool ownsHandle) : base(ownsHandle)
    {
    }

    public CertificateStoreHandle(HCERTSTORE handle)
        : base((IntPtr)handle, ownsHandle: true)
    {
    }

    protected override bool ReleaseHandle()
        => TerraFXWindows.CertCloseStore((HCERTSTORE)handle, dwFlags: 0);

    public static implicit operator CertificateStoreHandle(HCERTSTORE handle)
        => new(handle);
}