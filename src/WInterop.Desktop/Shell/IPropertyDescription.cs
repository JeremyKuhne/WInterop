// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com;

namespace WInterop.Shell
{
    [ComImport,
        Guid(InterfaceIds.IID_IPropertyDescription),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyDescription
    {
        PropertyKey GetPropertyKey();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetCanonicalName();

        VariantType GetPropertyType();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetDisplayName();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetEditInvitation();

        PropertyTypeFlags GetTypeFlags(PropertyTypeFlags mask);

        PropertyViewFlags GetViewFlags();

        uint GetDefaultColumnWidth();

        PropertyDisplayType GetDisplayType();

        ColumnState GetColumnState();

        PropertyGroupingRange GetGroupingRange();

        PropertyRelativeDescriptionType GetRelativeDescriptionType();

        void GetRelativeDescription(
            PROPVARIANT propvar1,
            PROPVARIANT propvar2,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc1,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc2);

        PropertySortDescription GetSortDescription();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetSortDescriptionLabel(
            [MarshalAs(UnmanagedType.Bool)] bool fDescending);

        PropertyAggregationType GetAggregationType();

        ConditionOperation GetConditionType(
            out PropertyConditionType pcontype);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetEnumTypeList(
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        void CoerceToCanonicalValue(
            PROPVARIANT ppropvar);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string FormatForDisplay(
            PROPVARIANT propvar,
            PropertyFormatFlags pdfFlags);

        void IsValueCanonical(
            PROPVARIANT provar);
    }
}
