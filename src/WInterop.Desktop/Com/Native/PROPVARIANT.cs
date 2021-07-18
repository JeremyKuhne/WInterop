// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com
{
    public class PROPVARIANT : Variant
    {
        public PROPVARIANT() : this(ownsHandle: true)
        {
        }

        public PROPVARIANT(bool ownsHandle) : base(ownsHandle)
        {
        }

        public PROPVARIANT(IntPtr handle, bool ownsHandle)
            : base(handle, ownsHandle)
        {
        }

        // VARENUMs that are VARIANT only (e.g. not valid for PROPVARIANT)
        //
        //  VT_DISPATCH
        //  VT_UNKNOWN
        //  VT_DECIMAL
        //  VT_UINT
        //  VT_ARRAY
        //  VT_BYREF

        public override object? GetData()
        {
            VariantType propertyType = RawVariantType;
            if ((propertyType & VariantType.ByRef) != 0
                || (propertyType & VariantType.Array) != 0)
            {
                // Not legit for PROPVARIANT
                throw new InvalidOleVariantTypeException();
            }

            if ((propertyType & VariantType.Vector) != 0)
            {
                throw new NotImplementedException();
            }

            propertyType &= VariantType.TypeMask;

            switch (propertyType)
            {
                case VariantType.IDispatch:
                case VariantType.IUnknown:
                case VariantType.Decimal:
                case VariantType.UnsignedInteger:
                    throw new InvalidOleVariantTypeException();
            }

            return base.GetData();
        }

        protected override unsafe object? GetCoreType(VariantType propertyType, void* data)
        {
            object? value = base.GetCoreType(propertyType, data);
            if (!ReferenceEquals(value, s_UnsupportedObject))
                return value;

            switch (propertyType)
            {
                case VariantType.Int64:
                    return *((long*)data);
                case VariantType.UInt64:
                    return *((ulong*)data);
                case VariantType.LPSTR:
                    return Marshal.PtrToStringAnsi((IntPtr)data);
                case VariantType.LPWSTR:
                    return Marshal.PtrToStringUni((IntPtr)data);
                case VariantType.ClassId:
                    return Marshal.PtrToStructure<Guid>((IntPtr)(*((void**)data)));
                case VariantType.FileTime:
                case VariantType.Blob:
                case VariantType.Stream:
                case VariantType.Storage:
                case VariantType.StreamedObject:
                case VariantType.StoredObject:
                case VariantType.VersionedStream:
                case VariantType.BlobObject:
                case VariantType.ClipboardFormat:
                case VariantType.Vector:
                default:
                    return s_UnsupportedObject;
            }
        }

        protected override bool ReleaseHandle()
        {
            // PropVariantClear is essentially a superset of VariantClear it calls CoTaskMemFree on the following types:
            //
            //     - VT_LPWSTR, VT_LPSTR, VT_CLSID (psvVal)
            //     - VT_BSTR_BLOB (bstrblobVal.pData)
            //     - VT_CF (pclipdata->pClipData, pclipdata)
            //     - VT_BLOB, VT_BLOB_OBJECT (blob.pData)
            //     - VT_STREAM, VT_STREAMED_OBJECT (pStream)
            //     - VT_VERSIONED_STREAM (pVersionedStream->pStream, pVersionedStream)
            //     - VT_STORAGE, VT_STORED_OBJECT (pStorage)
            //
            // If the VARTYPE is a VT_VECTOR, the contents are cleared as above and CoTaskMemFree is also called on
            // cabstr.pElems.

            IntPtr handle = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
                Imports.VariantClear(handle).ThrowIfFailed();

            return true;
        }
    }
}