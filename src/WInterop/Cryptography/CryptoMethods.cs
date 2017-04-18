// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Cryptography.DataTypes;
using WInterop.ErrorHandling;
using WInterop.Handles.DataTypes;

namespace WInterop.Cryptography
{
    public static class CryptoMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376559.aspx
            [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
            public static extern CertificateStoreHandle CertOpenStore(
                IntPtr lpszStoreProvider,
                uint dwMsgAndCertEncodingType,
                IntPtr hCryptProv,
                uint dwFlags,
                IntPtr pvPara);

            public static bool CertCloseStore(
                IntPtr hCertStore,
                uint dwFlags) => Support.Internal.Imports.CertCloseStore(hCertStore, dwFlags);

            // Example C Program: Listing System and Physical Stores
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa382362.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376060.aspx
            [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertEnumSystemStoreLocation(
                uint dwFlags,
                IntPtr pvArg,
                CertEnumSystemStoreLocationCallback pfnEnum);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376058.aspx
            [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertEnumSystemStore(
                uint dwFlags,
                IntPtr pvSystemStoreLocationPara,
                IntPtr pvArg,
                CertEnumSystemStoreCallback pfnEnum);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376055.aspx
            [DllImport(Libraries.Crypt32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertEnumPhysicalStore(
                IntPtr pvSystemStore,
                uint dwFlags,
                IntPtr pvArg,
                CertEnumPhysicalStoreCallback pfnEnum);
        }

        /// <summary>
        /// Attempts to close the given handle.
        /// </summary>
        public static void CloseStore(IntPtr handle)
        {
            if (!Direct.CertCloseStore(handle, dwFlags: 0))
                throw ErrorHelper.GetIoExceptionForLastError();
        }

        unsafe private static CertificateStoreHandle OpenSystemStoreWrapper(StoreName storeName)
        {
            uint flags = (uint)StoreOpenFlags.CERT_STORE_NO_CRYPT_RELEASE_FLAG;
            if (storeName == StoreName.SPC)
                flags |= (uint)SystemStoreLocation.CERT_SYSTEM_STORE_LOCAL_MACHINE;
            else
                flags |= (uint)SystemStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER;

            fixed (char* name = storeName.ToString())
            {
                CertificateStoreHandle store = Direct.CertOpenStore(
                    lpszStoreProvider: (IntPtr)StoreProvider.CERT_STORE_PROV_SYSTEM,
                    dwMsgAndCertEncodingType: 0,
                    hCryptProv: IntPtr.Zero,
                    dwFlags: flags,
                    pvPara: (IntPtr)name);

                return store;
            }
        }

        public static CertificateStoreHandle OpenSystemStore(StoreName storeName)
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
                var callBack = new CertEnumSystemStoreLocationCallback(SystemStoreLocationCallback);
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
                    var callBack = new CertEnumSystemStoreCallback(SystemStoreEnumeratorCallback);
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
            var physicalInfo = Marshal.PtrToStructure<CERT_PHYSICAL_STORE_INFO>(pStoreInfo);
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
                    var callBack = new CertEnumPhysicalStoreCallback(PhysicalStoreEnumeratorCallback);
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

            if ((dwFlags & CryptoDefines.CERT_SYSTEM_STORE_RELOCATE_FLAG) == CryptoDefines.CERT_SYSTEM_STORE_RELOCATE_FLAG)
            {
#if DESKTOP
                var relocate = Marshal.PtrToStructure<CERT_SYSTEM_STORE_RELOCATE_PARA>(pvSystemStore);
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

            info.Location = (SystemStoreLocation)(dwFlags & CryptoDefines.CERT_SYSTEM_STORE_LOCATION_MASK);
            return info;
        }
    }
}
