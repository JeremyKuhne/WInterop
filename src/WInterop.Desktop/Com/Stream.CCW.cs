// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;
using IOStream = System.IO.Stream;

namespace WInterop.Com;

public readonly unsafe partial struct Stream
{
    private static unsafe class CCW
    {
        private static readonly IStream.Vtbl<IStream>* s_vtable = AllocateVTable();

        private static IStream.Vtbl<IStream>* AllocateVTable()
        {
            // Allocate and create a singular VTable for this type projection.
            var vtable = (IStream.Vtbl<IStream>*)RuntimeHelpers.AllocateTypeAssociatedMemory(
                typeof(CCW),
                sizeof(IStream.Vtbl<IStream>));

            // IUnknown
            vtable->QueryInterface = &QueryInterface;
            vtable->AddRef = &AddRef;
            vtable->Release = &Release;

            vtable->Read = &Read;
            vtable->Write = &Write;
            vtable->Clone = &Clone;
            vtable->Commit = &Commit;
            vtable->CopyTo = &CopyTo;
            vtable->LockRegion = &LockRegion;
            vtable->Revert = &Revert;
            vtable->Seek = &Seek;
            vtable->SetSize = &SetSize;
            vtable->Stat = &Stat;
            vtable->UnlockRegion = &UnlockRegion;

            return vtable;
        }

        public static IStream* CreateInstance(IOStream stream)
            => (IStream*)Lifetime<IStream.Vtbl<IStream>, IOStream>.Allocate(stream, s_vtable);

        private static IOStream? Stream(IStream* @this)
            => Lifetime<IStream.Vtbl<IStream>, IOStream>.GetObject((IUnknown*)@this);

        [UnmanagedCallersOnly]
        private static int QueryInterface(IStream* @this, Guid* iid, void** ppObject)
        {
            if (ppObject is null)
            {
                return (int)HResult.E_POINTER;
            }

            if (*iid == typeof(IUnknown).GUID
                || *iid == typeof(IStream).GUID)
            {
                *ppObject = @this;
            }
            else
            {
                *ppObject = null;
                return (int)HResult.E_NOINTERFACE;
            }

            Lifetime<IStream.Vtbl<IStream>, Stream>.AddRef((IUnknown*)@this);
            return (int)HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static uint AddRef(IStream* @this) => Lifetime<IStream.Vtbl<IStream>, Stream>.AddRef((IUnknown*)@this);

        [UnmanagedCallersOnly]
        private static uint Release(IStream* @this) => Lifetime<IStream.Vtbl<IStream>, Stream>.Release((IUnknown*)@this);

        [UnmanagedCallersOnly]
        private static unsafe int Read(IStream* @this, void* pv, uint cb, uint* pcbRead)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                Span<byte> buffer = new(pv, checked((int)cb));
                int read = stream.Read(buffer);

                if (pcbRead is not null)
                {
                    *pcbRead = (uint)read;
                }

                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int Write(IStream* @this, void* pv, uint cb, uint* pcbWritten)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                var buffer = new ReadOnlySpan<byte>(pv, checked((int)cb));
                stream.Write(buffer);

                if (pcbWritten is not null)
                {
                    *pcbWritten = cb;
                }

                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int Seek(IStream* @this, LARGE_INTEGER dlibMove, uint dwOrigin, ULARGE_INTEGER* plibNewPosition)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                long length = stream.Length;
                switch ((StreamSeek)dwOrigin)
                {
                    case StreamSeek.Set:
                        stream.Position = dlibMove.QuadPart;
                        break;
                    case StreamSeek.End:
                        stream.Position = length + dlibMove.QuadPart;
                        break;
                    case StreamSeek.Current:
                        stream.Position += dlibMove.QuadPart;
                        break;
                }

                if (plibNewPosition is not null)
                {
                    *plibNewPosition = stream.Position.ToULARGE_INTEGER();
                }

                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int SetSize(IStream* @this, ULARGE_INTEGER libNewSize)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                stream.SetLength(checked((long)libNewSize.QuadPart));

                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int CopyTo(IStream* @this, IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);

                ulong remaining = cb.QuadPart;
                ulong totalWritten = 0;
                ulong totalRead = 0;

                fixed (byte* b = buffer)
                {
                    while (remaining > 0)
                    {
                        uint read = remaining < (ulong)buffer.Length ? (uint)remaining : (uint)buffer.Length;
                        read = (uint)stream.Read(buffer, 0, (int)read);
                        remaining -= read;
                        totalRead += read;

                        if (read == 0)
                        {
                            break;
                        }

                        uint written;
                        pstm->Write(b, read, &written);
                        totalWritten += written;
                    }
                }

                ArrayPool<byte>.Shared.Return(buffer);

                if (pcbRead is not null)
                {
                    *pcbRead = totalRead.ToULARGE_INTEGER();
                }

                if (pcbWritten is not null)
                {
                    *pcbWritten = totalWritten.ToULARGE_INTEGER();
                }

                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int Commit(IStream* @this, uint grfCommitFlags)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                stream.Flush();
                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int Revert(void* @this)
        {
            // We never report ourselves as Transacted, so we can just ignore this.
            return (int)HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int LockRegion(void* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            // Documented way to say we don't support locking
            return (int)HResult.STG_E_INVALIDFUNCTION;
        }

        [UnmanagedCallersOnly]
        private static unsafe int UnlockRegion(void* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            // Documented way to say we don't support locking
            return (int)HResult.STG_E_INVALIDFUNCTION;
        }

        [UnmanagedCallersOnly]
        private static unsafe int Stat(IStream* @this, STATSTG* pstatstg, uint grfStatFlag)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                pstatstg->cbSize = stream.Length.ToULARGE_INTEGER();
                pstatstg->type = (uint)StorageType.Stream;

                // Default read/write access is STGM_READ, which == 0
                pstatstg->grfMode = stream.CanWrite
                    ? stream.CanRead
                        ? (uint)StorageMode.ReadWrite
                        : (uint)StorageMode.Write
                    : default;

                if (grfStatFlag != (uint)StatFlag.NoName)
                {
                    // Caller wants a name
                    string name = stream is FileStream fs ? fs.Name : stream.ToString() ?? "IO.Stream";
                    pstatstg->pwcsName = (ushort*)Marshal.StringToCoTaskMemUni(name);
                }

                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe int Clone(IStream* @this, IStream** ppstm)
        {
            try
            {
                var stream = Stream(@this);
                if (stream is null)
                {
                    return (int)HResult.COR_E_OBJECTDISPOSED;
                }

                // Not technically correct, each instance should have their own Position.
                *ppstm = CreateInstance(stream);
                return (int)HResult.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }
    }
}