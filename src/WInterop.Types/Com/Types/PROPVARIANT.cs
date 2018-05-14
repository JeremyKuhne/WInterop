// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WInterop.Com.Types
{
    public class PROPVARIANT : VARIANT
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

        public override object GetData()
        {
            VARENUM propertyType = RawVariantType;
            if ((propertyType & VARENUM.VT_BYREF) != 0
                || (propertyType & VARENUM.VT_ARRAY) != 0)
            {
                // Not legit for PROPVARIANT
                throw new InvalidOleVariantTypeException();
            }

            if ((propertyType & VARENUM.VT_VECTOR) != 0)
            {
                throw new NotImplementedException();
            }

            propertyType = propertyType & VARENUM.VT_TYPEMASK;

            switch (propertyType)
            {
                case VARENUM.VT_DISPATCH:
                case VARENUM.VT_UNKNOWN:
                case VARENUM.VT_DECIMAL:
                case VARENUM.VT_UINT:
                    throw new InvalidOleVariantTypeException();
            }

            return base.GetData();
        }

        protected override unsafe object GetCoreType(VARENUM propertyType, void* data)
        {
            object value = base.GetCoreType(propertyType, data);
            if (!ReferenceEquals(value, s_UnsupportedObject))
                return value;

            switch (propertyType)
            {
                case VARENUM.VT_I8:
                    return *((long*)data);
                case VARENUM.VT_UI8:
                    return *((ulong*)data);
                case VARENUM.VT_LPSTR:
                    return Marshal.PtrToStringAnsi((IntPtr)data);
                case VARENUM.VT_LPWSTR:
                    return Marshal.PtrToStringUni((IntPtr)data);
                case VARENUM.VT_CLSID:
                    return Marshal.PtrToStructure<Guid>((IntPtr)(*((void**)data)));
                case VARENUM.VT_FILETIME:
                case VARENUM.VT_BLOB:
                case VARENUM.VT_STREAM:
                case VARENUM.VT_STORAGE:
                case VARENUM.VT_STREAMED_OBJECT:
                case VARENUM.VT_STORED_OBJECT:
                case VARENUM.VT_VERSIONED_STREAM:
                case VARENUM.VT_BLOB_OBJECT:
                case VARENUM.VT_CF:
                case VARENUM.VT_VECTOR:
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
                Support.Internal.Imports.PropVariantClear(handle);

            return true;
        }
    }
}
