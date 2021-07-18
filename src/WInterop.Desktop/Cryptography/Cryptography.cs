// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Cryptography.Native;
using WInterop.Errors;
using WInterop.Handles;

namespace WInterop.Cryptography
{
    public static partial class Cryptography
    {
        /// <summary>
        ///  Attempts to close the given handle.
        /// </summary>
        public static void CloseStore(IntPtr handle)
            => Error.ThrowLastErrorIfFalse(Imports.CertCloseStore(handle, dwFlags: 0));

        private static unsafe CertificateStoreHandle OpenSystemStoreWrapper(StoreName storeName)
        {
            uint flags = (uint)StoreOpenFlags.NoCryptRelease;
            if (storeName == StoreName.SPC)
                flags |= (uint)SystemStoreLocation.LocalMachine;
            else
                flags |= (uint)SystemStoreLocation.CurrentUser;

            fixed (char* name = storeName.ToString())
            {
                CertificateStoreHandle store = Imports.CertOpenStore(
                    lpszStoreProvider: (IntPtr)StoreProvider.System,
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
            var infos = (List<string>)(handle.Target ?? throw new InvalidOperationException());
            string? result = Marshal.PtrToStringUni(pvszStoreLocations);
            if (result != null)
                infos.Add(result);
            return true;
        }

        public static IEnumerable<string> EnumerateSystemStoreLocations()
        {
            var info = new List<string>();
            GCHandle handle = GCHandle.Alloc(info);

            try
            {
                var callBack = new CertEnumSystemStoreLocationCallback(SystemStoreLocationCallback);
                Imports.CertEnumSystemStoreLocation(
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
            var infos = (List<SystemStoreInformation>)(handle.Target ?? throw new InvalidOperationException());
            infos.Add(GetSystemNameAndKey(dwFlags, pvSystemStore));
            return true;
        }

        public static unsafe IEnumerable<SystemStoreInformation> EnumerateSystemStores(SystemStoreLocation location, string? name = null)
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
                    Imports.CertEnumSystemStore(
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
            var infos = (List<PhysicalStoreInformation>)(handle.Target ?? throw new InvalidOperationException());

            PhysicalStoreInformation info = new PhysicalStoreInformation
            {
                SystemStoreInformation = GetSystemNameAndKey(dwFlags, pvSystemStore),
                PhysicalStoreName = Marshal.PtrToStringUni(pwszStoreName) ?? string.Empty
            };
            var physicalInfo = Marshal.PtrToStructure<CERT_PHYSICAL_STORE_INFO>(pStoreInfo);
            info.ProviderType = physicalInfo.pszOpenStoreProvider;
            infos.Add(info);

            return true;
        }

        public static unsafe IEnumerable<PhysicalStoreInformation> EnumeratePhysicalStores(SystemStoreLocation location, string systemStoreName)
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
                    Imports.CertEnumPhysicalStore(
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
            SystemStoreInformation info = default;

            if ((dwFlags & CryptoDefines.CERT_SYSTEM_STORE_RELOCATE_FLAG) == CryptoDefines.CERT_SYSTEM_STORE_RELOCATE_FLAG)
            {
                // TODO: Rewrite with WInterop registry code
                // var relocate = Marshal.PtrToStructure<CERT_SYSTEM_STORE_RELOCATE_PARA>(pvSystemStore);
                // var registryHandle = new SafeRegistryHandle(relocate.pvBase, ownsHandle: false);
                // info.Key = RegistryKey.FromHandle(registryHandle).Name;
                // The name is null terminated
                // info.Name = Marshal.PtrToStringUni(relocate.pvSystemStore);
            }
            else
            {
                // The name is null terminated
                info.Name = Marshal.PtrToStringUni(pvSystemStore) ?? string.Empty;
            }

            info.Location = (SystemStoreLocation)(dwFlags & CryptoDefines.CERT_SYSTEM_STORE_LOCATION_MASK);
            return info;
        }
    }
}