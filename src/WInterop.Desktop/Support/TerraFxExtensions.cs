// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Security;
using WInterop.Support;

namespace TerraFX.Interop.Windows;

public static unsafe class TerraFxExtensions
{
    // COLORREF is 0x00BBGGRR
    internal const int COLORREF_RedShift = 0;
    internal const int COLORREF_GreenShift = 8;
    internal const int COLORREF_BlueShift = 16;

    public static HANDLE ToHANDLE(this SafeHandle handle) => new((void*)handle.DangerousGetHandle());
    public static ULARGE_INTEGER ToULARGE_INTEGER(this ulong value) => *(ULARGE_INTEGER*)(void*)(&value);
    public static ULARGE_INTEGER ToULARGE_INTEGER(this long value) => ToULARGE_INTEGER((ulong)value);
    public static LARGE_INTEGER ToLARGE_INTEGER(this long value) => *(LARGE_INTEGER*)(void*)(&value);

    public static byte R(this COLORREF color) => (byte)((color >> COLORREF_RedShift) & 0xFF);
    public static byte G(this COLORREF color) => (byte)((color >> COLORREF_GreenShift) & 0xFF);
    public static byte B(this COLORREF color) => (byte)((color >> COLORREF_BlueShift) & 0xFF);
    public static Color ToColor(this COLORREF color) => Color.FromArgb(color.R(), color.G(), color.B());
    public static COLORREF ToCOLORREF(this Color color)
        => new((uint)(color.R << COLORREF_RedShift | color.G << COLORREF_GreenShift | color.B << COLORREF_BlueShift));
    public static COLORREF ToCOLORREF(this (byte R, byte G, byte B) color)
        => new((uint)(color.R << COLORREF_RedShift | color.G << COLORREF_GreenShift | color.B << COLORREF_BlueShift));

    public static Rectangle ToRectangle(this RECT rect)
        => Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
    public static RECT ToRECT(this Rectangle rectangle)
        => new(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

    public static bool Equals(this ref SID sid, ref SID other)
        => sid.Revision == other.Revision
            && sid.IdentifierAuthority.Equals(other.IdentifierAuthority)
            && sid.SubAuthorityCount == other.SubAuthorityCount
            && sid.SubAuthorities().SequenceEqual(other.SubAuthorities());

    public static unsafe bool Equals(this ref SID sid, SID* other)
        => sid.Revision == other->Revision
            && sid.IdentifierAuthority.Equals(other->IdentifierAuthority)
            && sid.SubAuthorityCount == other->SubAuthorityCount
            && sid.SubAuthorities().SequenceEqual(other->SubAuthorities());

    public static Span<uint> SubAuthorities(this ref SID sid)
    {
        fixed (uint* sa = sid.SubAuthority)
        {
            return new(sa, sid.SubAuthorityCount);
        }
    }

    public static void ThrowIfFailed(this HRESULT result, string? detail = null)
        => result.ToHResult().ThrowIfFailed(detail);

    public static HResult ToHResult(this HRESULT result) => (HResult)result.Value;
    public static HRESULT ToHRESULT(this HResult result) => (HRESULT)(int)result;

    public static string? TrusteeName(this ref TRUSTEE_W trustee)
    {
        char* name = null;

        switch (trustee.TrusteeForm)
        {
            case TRUSTEE_FORM.TRUSTEE_IS_NAME:
                name = (char*)trustee.ptstrName;
                break;
            case TRUSTEE_FORM.TRUSTEE_IS_OBJECTS_AND_NAME:
                name = (char*)((OBJECTS_AND_NAME_W*)trustee.ptstrName)->ptstrName;
                break;
        }

        return name == null ? null : new string(name);
    }

    public static ObjectType ToObjectType(this ref TRUSTEE_W trustee)
    {
        return trustee.TrusteeForm == TRUSTEE_FORM.TRUSTEE_IS_OBJECTS_AND_NAME
            ? (ObjectType)((OBJECTS_AND_NAME_W*)trustee.ptstrName)->ObjectType
            : ObjectType.Unknown;
    }

    public static DateTime ToDateTimeUTC(this ref FILETIME time)
        => DateTime.FromFileTimeUtc((long)Conversion.HighLowToLong(time.dwHighDateTime, time.dwLowDateTime));
}