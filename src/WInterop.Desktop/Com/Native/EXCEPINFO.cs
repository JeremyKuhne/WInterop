// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com.Native;

public unsafe ref struct EXCEPINFO
{
    public ushort wCode;
    public ushort wReserved;
    public BasicString bstrSource;
    public BasicString bstrDescription;
    public BasicString bstrHelpFile;
    public BasicString dwHelpContext;
    public void* pvReserved;
    // HRESULT(__stdcall* pfnDeferredFillIn)(struct tagEXCEPINFO *);
    public IntPtr pfnDeferredFillIn;
    public HResult scode;
}