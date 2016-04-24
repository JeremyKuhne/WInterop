// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using Cryptography;
    using Handles;
    using Microsoft.Win32;
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;

    public static partial class NativeMethods
    {
        public static class Cryptography
        {
            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static class Direct
            {
                public const uint CERT_SYSTEM_STORE_RELOCATE_FLAG = 0x80000000;
                public const uint CERT_SYSTEM_STORE_LOCATION_MASK = 0x00FF0000;
                public const int CERT_SYSTEM_STORE_LOCATION_SHIFT = 16;

                // System Store Locations
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa388136.aspx

#if DESKTOP
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376560.aspx
                [DllImport(Libraries.Crypt32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                public static extern SafeCertificateStoreHandle CertOpenSystemStoreW(
                    IntPtr hprov,
                    string szSubsystemProtocol);
#endif

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376559.aspx
                [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
                public static extern SafeCertificateStoreHandle CertOpenStore(
                    IntPtr lpszStoreProvider,
                    uint dwMsgAndCertEncodingType,
                    IntPtr hCryptProv,
                    uint dwFlags,
                    IntPtr pvPara);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376026.aspx
                [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CertCloseStore(
                    IntPtr hCertStore,
                    uint dwFlags);

                // Example C Program: Listing System and Physical Stores
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa382362.aspx

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376060.aspx
                [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CertEnumSystemStoreLocation(
                    uint dwFlags,
                    IntPtr pvArg,
                    CertEnumSystemStoreLocationCallback pfnEnum);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376061.aspx
                [return: MarshalAs(UnmanagedType.Bool)]
                public delegate bool CertEnumSystemStoreLocationCallback(
                    IntPtr pvszStoreLocations,
                    uint dwFlags,
                    IntPtr pvReserved,
                    IntPtr pvArg);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376058.aspx
                [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CertEnumSystemStore(
                    uint dwFlags,
                    IntPtr pvSystemStoreLocationPara,
                    IntPtr pvArg,
                    CertEnumSystemStoreCallback pfnEnum);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376059.aspx
                [return: MarshalAs(UnmanagedType.Bool)]
                public delegate bool CertEnumSystemStoreCallback(
                    IntPtr pvSystemStore,
                    uint dwFlags,
                    IntPtr pStoreInfo,
                    IntPtr pvReserved,
                    IntPtr pvArg);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376055.aspx
                [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                internal static extern bool CertEnumPhysicalStore(
                    IntPtr pvSystemStore,
                    uint dwFlags,
                    IntPtr pvArg,
                    CertEnumPhysicalStoreCallback pfnEnum);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376056.aspx
                [return: MarshalAs(UnmanagedType.Bool)]
                public delegate bool CertEnumPhysicalStoreCallback(
                    IntPtr pvSystemStore,
                    uint dwFlags,
                    IntPtr pwszStoreName,
                    IntPtr pStoreInfo,
                    IntPtr pvReserved,
                    IntPtr pvArg);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa377568.aspx
                [StructLayout(LayoutKind.Sequential)]
                public struct CERT_SYSTEM_STORE_INFO
                {
                    public uint cbSize;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct CERT_PHYSICAL_STORE_INFO
                {
                    public uint cbSize;
                    public string pszOpenStoreProvider;
                    public uint dwOpenEncodingType;
                    public uint dwOpenFlags;
                    CRYPT_DATA_BLOB OpenParameters;
                    uint dwFlags;
                    uint dwPriority;
                }

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa381414.aspx
                [StructLayout(LayoutKind.Sequential)]
                public struct CRYPT_DATA_BLOB
                {
                    uint cbData;
                    IntPtr pbData;
                }

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa377575.aspx
                [StructLayout(LayoutKind.Sequential)]
                public struct CERT_SYSTEM_STORE_RELOCATE_PARA
                {
                    /// <summary>
                    /// Can be HKEY hKeyBase
                    /// </summary>
                    public IntPtr pvBase;

                    /// <summary>
                    /// Can be LPCSTR pszSystemStore or LPCWSTR pwszSystemStore
                    /// </summary>
                    public IntPtr pvSystemStore;
                }
            }

            /// <summary>
            /// Attempts to close the given handle.
            /// </summary>
            public static void CloseStore(SafeCertificateStoreHandle handle)
            {
                if (!Direct.CertCloseStore(handle.DangerousGetHandle(), dwFlags: 0))
                {
                    throw ErrorHandling.GetIoExceptionForError((uint)Marshal.GetLastWin32Error());
                }
            }

            unsafe private static SafeCertificateStoreHandle OpenSystemStoreWrapper(StoreName storeName)
            {
                uint flags = (uint)StoreOpenFlags.CERT_STORE_NO_CRYPT_RELEASE_FLAG;
                if (storeName == StoreName.SPC)
                    flags |= (uint)SystemStoreLocation.CERT_SYSTEM_STORE_LOCAL_MACHINE;
                else
                    flags |= (uint)SystemStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER;

                fixed (char* name = storeName.ToString())
                {
                    SafeCertificateStoreHandle store = Direct.CertOpenStore(
                        lpszStoreProvider: (IntPtr)StoreProvider.CERT_STORE_PROV_SYSTEM,
                        dwMsgAndCertEncodingType: 0,
                        hCryptProv: IntPtr.Zero,
                        dwFlags: flags,
                        pvPara: (IntPtr)name);

                    return store;
                }
            }

            public static SafeCertificateStoreHandle OpenSystemStore(StoreName storeName)
            {
                return OpenSystemStoreWrapper(storeName);
            }

            private static bool SystemStoreLocationCallback(
                IntPtr pvszStoreLocations,
                uint dwFlags,
                IntPtr pvReserved,
                IntPtr pvArg)
            {
                GCHandle handle = GCHandle.FromIntPtr(pvArg);
                var infos = (List<string>)handle.Target;
                infos.Add(Marshal.PtrToStringUni(pvszStoreLocations));
                return true;
            }

            public static IEnumerable<string> EnumerateSystemStoreLocations()
            {
                var info = new List<string>();
                GCHandle handle = GCHandle.Alloc(info);

                try
                {
                    var callBack = new Direct.CertEnumSystemStoreLocationCallback(SystemStoreLocationCallback);
                    Direct.CertEnumSystemStoreLocation(
                        dwFlags: 0,
                        pvArg: GCHandle.ToIntPtr(handle),
                        pfnEnum: callBack);
                }
                finally
                {
                    handle.Free();
                }

                return info;
            }

            private static bool SystemStoreEnumeratorCallback(
                IntPtr pvSystemStore,
                uint dwFlags,
                IntPtr pStoreInfo,
                IntPtr pvReserved,
                IntPtr pvArg)
            {
                GCHandle handle = GCHandle.FromIntPtr(pvArg);
                var infos = (List<SystemStoreInformation>)handle.Target;
                infos.Add(GetSystemNameAndKey(dwFlags, pvSystemStore));
                return true;
            }

            public unsafe static IEnumerable<SystemStoreInformation> EnumerateSystemStores(SystemStoreLocation location, string name = null)
            {
                var info = new List<SystemStoreInformation>();
                GCHandle infoHandle = GCHandle.Alloc(info);

                fixed (char* namePointer = string.IsNullOrEmpty(name) ? null : name)
                {
                    try
                    {
                        // To lookup system stores in an alternate location you need to set CERT_SYSTEM_STORE_RELOCATE_FLAG
                        // and pass in the name and alternate location (HKEY) in pvSystemStoreLocationPara.
                        var callBack = new Direct.CertEnumSystemStoreCallback(SystemStoreEnumeratorCallback);
                        Direct.CertEnumSystemStore(
                            dwFlags: (uint)location,
                            pvSystemStoreLocationPara: (IntPtr)namePointer,
                            pvArg: GCHandle.ToIntPtr(infoHandle),
                            pfnEnum: callBack);
                    }
                    finally
                    {
                        infoHandle.Free();
                    }
                }

                return info;
            }

            private static bool PhysicalStoreEnumeratorCallback(
                IntPtr pvSystemStore,
                uint dwFlags,
                IntPtr pwszStoreName,
                IntPtr pStoreInfo,
                IntPtr pvReserved,
                IntPtr pvArg)
            {
                GCHandle handle = GCHandle.FromIntPtr(pvArg);
                var infos = (List<PhysicalStoreInformation>)handle.Target;

                PhysicalStoreInformation info = new PhysicalStoreInformation();
                info.SystemStoreInformation = GetSystemNameAndKey(dwFlags, pvSystemStore);
                info.PhysicalStoreName = Marshal.PtrToStringUni(pwszStoreName);
                var physicalInfo = Marshal.PtrToStructure<Direct.CERT_PHYSICAL_STORE_INFO>(pStoreInfo);
                info.ProviderType = physicalInfo.pszOpenStoreProvider;
                infos.Add(info);

                return true;
            }

            public unsafe static IEnumerable<PhysicalStoreInformation> EnumeratePhysicalStores(SystemStoreLocation location, string systemStoreName)
            {
                var info = new List<PhysicalStoreInformation>();
                GCHandle infoHandle = GCHandle.Alloc(info);

                fixed (char* namePointer = systemStoreName)
                {
                    try
                    {
                        // To lookup system stores in an alternate location you need to set CERT_SYSTEM_STORE_RELOCATE_FLAG
                        // and pass in the name and alternate location (HKEY) in pvSystemStoreLocationPara.
                        var callBack = new Direct.CertEnumPhysicalStoreCallback(PhysicalStoreEnumeratorCallback);
                        Direct.CertEnumPhysicalStore(
                            pvSystemStore: (IntPtr)namePointer,
                            dwFlags: (uint)location,
                            pvArg: GCHandle.ToIntPtr(infoHandle),
                            pfnEnum: callBack);
                    }
                    finally
                    {
                        infoHandle.Free();
                    }
                }

                return info;
            }

            private static SystemStoreInformation GetSystemNameAndKey(uint dwFlags, IntPtr pvSystemStore)
            {
                SystemStoreInformation info = new SystemStoreInformation();

                if ((dwFlags & Direct.CERT_SYSTEM_STORE_RELOCATE_FLAG) == Direct.CERT_SYSTEM_STORE_RELOCATE_FLAG)
                {
#if DESKTOP
                    var relocate = Marshal.PtrToStructure<Direct.CERT_SYSTEM_STORE_RELOCATE_PARA>(pvSystemStore);
                    var registryHandle = new SafeRegistryHandle(relocate.pvBase, ownsHandle: false);

                    info.Key = RegistryKey.FromHandle(registryHandle).Name;

                    // The name is null terminated
                    info.Name = Marshal.PtrToStringUni(relocate.pvSystemStore);
#else
                    // Can't do registry access on WinRT
                    throw new PlatformNotSupportedException();
#endif
                }
                else
                {
                    // The name is null terminated
                    info.Name = Marshal.PtrToStringUni(pvSystemStore);
                }

                info.Location = (SystemStoreLocation)(dwFlags & Direct.CERT_SYSTEM_STORE_LOCATION_MASK);
                return info;
            }
        }
    }
}
