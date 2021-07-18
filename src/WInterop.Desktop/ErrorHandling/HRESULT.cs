// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors
{
    /// <summary>
    ///  HRESULT codes.
    ///  <see cref="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a"/>
    /// </summary>
    /// <remarks>
    ///  SCODE (Status Code) values are equivalent to HRESULT values on 32 and 64 bit platforms. On
    ///  16 bit platforms they need to be converted by doing a logical AND with 0x800FFFFF.
    /// </remarks>
    public enum HResult : int
    {
        S_OK = 0,
        S_FALSE = 1,
        DRAGDROP_S_DROP                                     =                0x00040100,
        DRAGDROP_S_CANCEL                                   =                0x00040101,
        DRAGDROP_S_USEDEFAULTCURSORS                        =                0x00040102,
        DATA_S_SAMEFORMATETC                                =                0x00040130,
        E_NOINTERFACE                                       = unchecked((int)0x80004002),
        E_POINTER                                           = unchecked((int)0x80004003),
        E_FAIL                                              = unchecked((int)0x80004005),
        E_UNEXPECTED                                        = unchecked((int)0x8000FFFF),
        STG_E_INVALIDFUNCTION                               = unchecked((int)0x80030001),
        STG_E_FILENOTFOUND                                  = unchecked((int)0x80030002),
        STG_E_ACCESSDENIED                                  = unchecked((int)0x80030005),
        STG_E_INVALIDPARAMETER                              = unchecked((int)0x80030057),
        STG_E_MEDIUMFULL                                    = unchecked((int)0x80030070),
        STG_E_INVALIDFLAG                                   = unchecked((int)0x800300FF),
        OLE_E_NOTRUNNING                                    = unchecked((int)0x80040005),
        DV_E_FORMATETC                                      = unchecked((int)0x80040064),
        DV_E_LINDEX                                         = unchecked((int)0x80040068),
        DV_E_TYMED                                          = unchecked((int)0x80040069),
        DV_E_DVASPECT                                       = unchecked((int)0x8004006B),
        E_ACCESSDENIED                                      = unchecked((int)0x80070005),
        E_OUTOFMEMORY                                       = unchecked((int)0x8007000E),
        E_INVALIDARG                                        = unchecked((int)0x80070057),
        D2DERR_WRONG_STATE                                  = unchecked((int)0x88990001),
        D2DERR_RECREATE_TARGET                              = unchecked((int)0x8899000C),
        D2DERR_INVALID_PROPERTY                             = unchecked((int)0x88990029),
        WINCODEC_ERR_UNSUPPORTEDPIXELFORMAT                 = unchecked((int)0x88982F80),
    }
}
