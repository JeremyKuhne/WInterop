// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com.Types;

namespace WInterop.Shell.Types
{
    [ComImport,
        Guid(InterfaceIds.IID_IPropertyDescription),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyDescription
    {
        PROPERTYKEY GetPropertyKey();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetCanonicalName();

        VARENUM GetPropertyType();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetDisplayName();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetEditInvitation();

        PROPDESC_TYPE_FLAGS GetTypeFlags(PROPDESC_TYPE_FLAGS mask);

        PROPDESC_VIEW_FLAGS GetViewFlags();

        uint GetDefaultColumnWidth();

        PROPDESC_DISPLAYTYPE GetDisplayType();

        SHCOLSTATE GetColumnState();

        PROPDESC_GROUPING_RANGE GetGroupingRange();

        PROPDESC_RELATIVEDESCRIPTION_TYPE GetRelativeDescriptionType();

        void GetRelativeDescription(
            PROPVARIANT propvar1,
            PROPVARIANT propvar2,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc1,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc2);

        PROPDESC_SORTDESCRIPTION GetSortDescription();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetSortDescriptionLabel(
            [MarshalAs(UnmanagedType.Bool)] bool fDescending);

        PROPDESC_AGGREGATION_TYPE GetAggregationType();

        CONDITION_OPERATION GetConditionType(
            out PROPDESC_CONDITION_TYPE pcontype);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetEnumTypeList(
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        void CoerceToCanonicalValue(
            PROPVARIANT ppropvar);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string FormatForDisplay(
            PROPVARIANT propvar,
            PROPDESC_FORMAT_FLAGS pdfFlags);

        void IsValueCanonical(
            PROPVARIANT provar);
    }
}
