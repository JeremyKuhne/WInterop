using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using Forms_IDataObject = System.Object;

// Clean up warnings
#pragma warning disable CS0649
#pragma warning disable CA1416

namespace ClipboardRedux
{
    internal static class ClipboardImpl
    {
        public static void Initialize()
        {
            int hr = Ole32.OleInitialize(IntPtr.Zero);
            // S_FALSE is a valid HRESULT
            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
        }

        public static object Get()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new InvalidOperationException();
            }

            IntPtr instance;
            int hr = Ole32.OleGetClipboard(out instance);
            if (hr != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            // Check if the returned value is actually a wrapped managed instance.
            if (CCW_IDataObject.TryGetInstance(instance, out Forms_IDataObject forms_dataObject))
            {
                return forms_dataObject;
            }

            IntPtr agileReference;
            var iid = typeof(IDataObject).GUID;
            hr = Ole32.RoGetAgileReference(
                Ole32.AgileReferenceOptions.Default,
                ref iid,
                instance,
                out agileReference);

            if (hr != 0)
            {
                // Release the clipboard object if agile
                // reference creation failed.
                Marshal.Release(instance);
                Marshal.ThrowExceptionForHR(hr);
            }

            // Wrap the agile reference.
            var agileRef = new RCW_IAgileReference(agileReference);

            // Release the current instance as it is now controlled
            // by the agile reference RCW.
            Marshal.Release(instance);

            return new RCW_IDataObject(agileRef);
        }

        public static void Set(object obj)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new InvalidOperationException();
            }

            int hr;
            if (obj is null)
            {
                hr = Ole32.OleSetClipboard(IntPtr.Zero);
                if (hr != 0)
                {
                    Marshal.ThrowExceptionForHR(hr);
                }

                return;
            }

            if (obj is RCW_IDataObject com_dataobject)
            {
                hr = Ole32.OleSetClipboard(com_dataobject.GetInstanceForSta());
            }
            else if (obj is Forms_IDataObject forms_dataObject)
            {
                // This approach is less than ideal since a new wrapper is always
                // created. Having an efficient cache would be more effective.
                var ccw = CCW_IDataObject.CreateInstance(forms_dataObject);
                hr = Ole32.OleSetClipboard(ccw);
            }
            else
            {
                // This requires implementing a universal CCW or alternatively
                // leveraging the built-in system. It isn't obvious which one is
                // the best option - both are possible.
                throw new NotImplementedException();
            }

            if (hr != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
        }

        public static int Flush()
        {
            return Ole32.OleFlushClipboard();
        }

        private static class Ole32
        {
            [DllImport(nameof(Ole32), ExactSpelling = true)]
            public static extern int OleInitialize(IntPtr reserved);

            [DllImport(nameof(Ole32), ExactSpelling = true)]
            public static extern int OleGetClipboard(out IntPtr dataObject);

            [DllImport(nameof(Ole32), ExactSpelling = true)]
            public static extern int OleSetClipboard(IntPtr dataObject);

            [DllImport(nameof(Ole32), ExactSpelling = true)]
            public static extern int OleFlushClipboard();

            public enum AgileReferenceOptions
            {
                Default = 0,
                DelayedMarshal = 1,
            }
;

            // The RoGetAgileReference API is supported on Windows 8.1+.
            //   See: https://docs.microsoft.com/windows/win32/api/combaseapi/nf-combaseapi-rogetagilereference
            // For prior OS versions use the Global Interface Table (GIT).
            //   See: https://docs.microsoft.com/windows/win32/com/creating-the-global-interface-table
            [DllImport(nameof(Ole32), ExactSpelling = true)]
            public static extern int RoGetAgileReference(
                AgileReferenceOptions opts,
                ref Guid riid,
                IntPtr instance,
                out IntPtr agileReference);
        }
    }

    internal class RCW_IAgileReference
    {
        private unsafe struct AgileReferenceVTable
        {
            public IUnknownVTable UnknownVTable;

            // IAgileReference
            public delegate* unmanaged[Stdcall]<IntPtr, ref Guid, out IntPtr, int> Resolve;
        }

        private readonly IntPtr instance;
        private readonly unsafe AgileReferenceVTable* vtable;

        public RCW_IAgileReference(IntPtr instance)
        {
            this.instance = instance;
            unsafe
            {
                this.vtable = *(AgileReferenceVTable**)instance;
            }
        }

        // An IAgileReference instance handles release on the correct context.
        ~RCW_IAgileReference()
        {
            if (this.instance != IntPtr.Zero)
            {
                Marshal.Release(this.instance);
            }
        }

        public IntPtr Resolve(Guid iid)
        {
            unsafe
            {
                // Marshal
                IntPtr resolvedInstance;

                // Dispatch
                int hr = this.vtable->Resolve(this.instance, ref iid, out resolvedInstance);
                if (hr != 0)
                {
                    Marshal.ThrowExceptionForHR(hr);
                }

                // Unmarshal
                return resolvedInstance;
            }
        }
    }

    internal struct STGMEDIUM_Blittable
    {
        public TYMED tymed;
        public IntPtr unionmember;
        public IntPtr pUnkForRelease;
    }

    internal unsafe struct IUnknownVTable
    {
        public static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

        // IUnknown
        public delegate* unmanaged[Stdcall]<IntPtr, Guid*, IntPtr*, int> QueryInterface;
        public delegate* unmanaged[Stdcall]<IntPtr, int> AddRef;
        public delegate* unmanaged[Stdcall]<IntPtr, int> Release;
    }

    internal unsafe struct DataObjectVTable
    {
        public static readonly Guid IID_IDataObject = new Guid("0000010e-0000-0000-C000-000000000046");

        public IUnknownVTable UnknownVTable;

        // IDataObject
        public delegate* unmanaged[Stdcall]<IntPtr, FORMATETC*, STGMEDIUM_Blittable*, int> GetData;
        public delegate* unmanaged[Stdcall]<IntPtr, FORMATETC*, STGMEDIUM_Blittable*, int> GetDataHere;
        public delegate* unmanaged[Stdcall]<IntPtr, /*optional*/ FORMATETC*, int> QueryGetData;
        public delegate* unmanaged[Stdcall]<IntPtr, /*optional*/ FORMATETC*, FORMATETC*, int> GetCanonicalFormatEtc;
        public delegate* unmanaged[Stdcall]<IntPtr, FORMATETC*, STGMEDIUM_Blittable*, int, int> SetData;
        public delegate* unmanaged[Stdcall]<IntPtr, int, IntPtr*, int> EnumFormatEtc;
        public delegate* unmanaged[Stdcall]<IntPtr, FORMATETC*, int, IntPtr, int*, int> DAdvise;
        public delegate* unmanaged[Stdcall]<IntPtr, int, int> DUnadvise;
        public delegate* unmanaged[Stdcall]<IntPtr, IntPtr*, int> EnumDAdvise;
    }

    internal class RCW_IDataObject : IDataObject
    {
        private readonly RCW_IAgileReference agileInstance;
        private readonly IntPtr instanceInSta;
        private readonly unsafe DataObjectVTable* vtableInSta;

        public RCW_IDataObject(RCW_IAgileReference agileReference)
        {
            // Use IAgileReference instance to always be in context.
            this.agileInstance = agileReference;

            Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA);

            // Assuming this class is always in context getting it once is possible.
            // See Finalizer for lifetime detail concerns. If the Clipboard instance
            // is considered a process singleton, then it could be leaked.
            (IntPtr instance, IntPtr vtable) = GetContextSafeRef(this.agileInstance);
            this.instanceInSta = instance;
            unsafe
            {
                this.vtableInSta = (DataObjectVTable*)vtable;
            }
        }

        // This Finalizer only works if the IDataObject is free threaded or if code
        // is added to ensure the Release takes place in the correct context.
        //~RCW_IDataObject()
        //{
        //    if (this.instanceInSta != IntPtr.Zero)
        //    {
        //        Marshal.Release(this.instanceInSta);
        //    }
        //}

        private static unsafe (IntPtr inst, IntPtr vtable) GetContextSafeRef(RCW_IAgileReference agileRef)
        {
            IntPtr instSafe = agileRef.Resolve(typeof(IDataObject).GUID);

            // Retain the instance's vtable when in context.
            unsafe
            {
                var vtableSafe = (IntPtr)(*(DataObjectVTable**)instSafe);
                return (instSafe, vtableSafe);
            }
        }

        public IntPtr GetInstanceForSta()
        {
            Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA);
            return this.instanceInSta;
        }

        public void GetData(ref FORMATETC format, out STGMEDIUM medium)
        {
            unsafe
            {
                // Marshal
                var stgmed = default(STGMEDIUM_Blittable);
                medium = default;

                // Dispatch
                int hr;
                (IntPtr instance, IntPtr vtable) = GetContextSafeRef(this.agileInstance);
                fixed (FORMATETC* formatFixed = &format)
                {
                    hr = ((DataObjectVTable*)vtable)->GetData(instance, formatFixed, &stgmed);
                }

                Marshal.Release(instance);

                if (hr != 0)
                {
                    Marshal.ThrowExceptionForHR(hr);
                }

                // Unmarshal
                medium.tymed = stgmed.tymed;
                medium.unionmember = stgmed.unionmember;
                if (stgmed.pUnkForRelease != IntPtr.Zero)
                {
                    medium.pUnkForRelease = Marshal.GetObjectForIUnknown(stgmed.pUnkForRelease);
                }
            }
        }

        public void GetDataHere(ref FORMATETC format, ref STGMEDIUM medium)
        {
            throw new NotImplementedException();
        }

        public int QueryGetData(ref FORMATETC format)
        {
            throw new NotImplementedException();
        }

        public int GetCanonicalFormatEtc(ref FORMATETC formatIn, out FORMATETC formatOut)
        {
            throw new NotImplementedException();
        }

        public void SetData(ref FORMATETC formatIn, ref STGMEDIUM medium, bool release)
        {
            unsafe
            {
                // Marshal
                var pUnk = default(IntPtr);
                if (medium.pUnkForRelease is not null)
                {
                    pUnk = Marshal.GetIUnknownForObject(medium.pUnkForRelease);
                }

                var stgmed = new STGMEDIUM_Blittable()
                {
                    unionmember = medium.unionmember,
                    tymed = medium.tymed,
                    pUnkForRelease = pUnk
                };

                int isRelease = release ? 1 : 0;

                // Dispatch
                int hr;
                (IntPtr instance, IntPtr vtable) = GetContextSafeRef(this.agileInstance);
                fixed (FORMATETC* formatFixed = &formatIn)
                {
                    hr = ((DataObjectVTable*)vtable)->SetData(instance, formatFixed, &stgmed, isRelease);
                }

                Marshal.Release(instance);

                if (hr != 0)
                {
                    Marshal.ThrowExceptionForHR(hr);
                }
            }
        }

        public IEnumFORMATETC EnumFormatEtc(DATADIR direction)
        {
            throw new NotImplementedException();
        }

        public int DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection)
        {
            throw new NotImplementedException();
        }

        public void DUnadvise(int connection)
        {
            throw new NotImplementedException();
        }

        public int EnumDAdvise(out IEnumSTATDATA enumAdvise)
        {
            throw new NotImplementedException();
        }
    }

    internal static class CCW_IDataObject
    {
        private static Lazy<IntPtr> CCWVTable = new Lazy<IntPtr>(AllocateVTable, isThreadSafe: true);

        private unsafe struct Lifetime
        {
            public DataObjectVTable* VTable;
            public IntPtr Handle;
            public int RefCount;
        }

        public static IntPtr CreateInstance(Forms_IDataObject dataObject)
        {
            unsafe
            {
                var wrapper = (Lifetime*)RuntimeHelpers.AllocateTypeAssociatedMemory(dataObject.GetType(), sizeof(Lifetime));

                // Create the wrapper instance.
                wrapper->VTable = (DataObjectVTable*)CCWVTable.Value;
                wrapper->Handle = GCHandle.ToIntPtr(GCHandle.Alloc(dataObject));
                wrapper->RefCount = 1;

                return (IntPtr)wrapper;
            }
        }

        public static bool TryGetInstance(IntPtr instanceMaybe, out Forms_IDataObject forms_dataObject)
        {
            forms_dataObject = null;

            unsafe
            {
                // This is a dangerous cast since it relies on strictly
                // following the COM ABI. If the VTable is ours the rest of
                // the structure is good, otherwise it is unknown.
                var lifetime = (Lifetime*)instanceMaybe;
                if (lifetime->VTable != (DataObjectVTable*)CCWVTable.Value)
                {
                    return false;
                }

                forms_dataObject = GetInstance(instanceMaybe);
                return true;
            }
        }

        private static IntPtr AllocateVTable()
        {
            unsafe
            {
                // Allocate and create a singular VTable for this type projection.
                var vtable = (DataObjectVTable*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(CCW_IDataObject), sizeof(DataObjectVTable));

                // IUnknown
                vtable->UnknownVTable.QueryInterface = &QueryInterface;
                vtable->UnknownVTable.AddRef = &AddRef;
                vtable->UnknownVTable.Release = &Release;

                // IDataObject
                vtable->GetData = &GetData;
                vtable->GetDataHere = &GetDataHere;
                vtable->QueryGetData = &QueryGetData;
                vtable->GetCanonicalFormatEtc = &GetCanonicalFormatEtc;
                vtable->SetData = &SetData;
                vtable->EnumFormatEtc = &EnumFormatEtc;
                vtable->DAdvise = &DAdvise;
                vtable->DUnadvise = &DUnadvise;
                vtable->EnumDAdvise = &EnumDAdvise;

                return (IntPtr)vtable;
            }
        }

        private static Forms_IDataObject GetInstance(IntPtr wrapper)
        {
            unsafe
            {
                var lifetime = (Lifetime*)wrapper;

                Debug.Assert(lifetime->Handle != IntPtr.Zero);
                Debug.Assert(lifetime->RefCount > 0);
                return (Forms_IDataObject)GCHandle.FromIntPtr(lifetime->Handle).Target;
            }
        }

        // IUnknown
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int QueryInterface(IntPtr _this, Guid* iid, IntPtr* ppObject)
        {
            if (*iid == IUnknownVTable.IID_IUnknown || *iid == DataObjectVTable.IID_IDataObject)
            {
                *ppObject = _this;
            }
            else
            {
                *ppObject = IntPtr.Zero;
                const int E_NOINTERFACE = unchecked((int)0x80004002L);
                return E_NOINTERFACE;
            }

            AddRefInternal(_this);
            return 0;
        }

        private static int AddRefInternal(IntPtr _this)
        {
            unsafe
            {
                var lifetime = (Lifetime*)_this;
                Debug.Assert(lifetime->Handle != IntPtr.Zero);
                Debug.Assert(lifetime->RefCount > 0);
                return Interlocked.Increment(ref lifetime->RefCount);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static int AddRef(IntPtr _this)
        {
            return AddRefInternal(_this);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static int Release(IntPtr _this)
        {
            unsafe
            {
                var lifetime = (Lifetime*)_this;
                Debug.Assert(lifetime->Handle != IntPtr.Zero);
                Debug.Assert(lifetime->RefCount > 0);
                int count = Interlocked.Decrement(ref lifetime->RefCount);
                if (count == 0)
                {
                    GCHandle.FromIntPtr(lifetime->Handle).Free();
                    lifetime->Handle = IntPtr.Zero;
                }

                return count;
            }
        }

        private const int E_NOTIMPL = unchecked((int)0x80004001);

        // IDataObject
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int GetData(IntPtr _this, FORMATETC* format, STGMEDIUM_Blittable* medium)
        {
            *medium = default;
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int GetDataHere(IntPtr _this, FORMATETC* format, STGMEDIUM_Blittable* medium)
        {
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int QueryGetData(IntPtr _this, /*optional*/ FORMATETC* format)
        {
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int GetCanonicalFormatEtc(IntPtr _this, /*optional*/ FORMATETC* formatIn, FORMATETC* formatOut)
        {
            formatOut = default;
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int SetData(IntPtr _this, FORMATETC* format, STGMEDIUM_Blittable* medium, int shouldRelease)
        {
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int EnumFormatEtc(IntPtr _this, int direction, IntPtr* enumFORMATETC)
        {
            *enumFORMATETC = default;
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int DAdvise(IntPtr _this, FORMATETC* format, int advf, IntPtr adviseSink, int* connection)
        {
            *connection = default;
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static int DUnadvise(IntPtr _this, int connection)
        {
            return E_NOTIMPL;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static unsafe int EnumDAdvise(IntPtr _this, IntPtr* enumSTATDATA)
        {
            *enumSTATDATA = default;
            return E_NOTIMPL;
        }
    }
}
