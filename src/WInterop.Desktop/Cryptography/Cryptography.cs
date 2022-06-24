// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Handles;

namespace WInterop.Cryptography;

public static unsafe partial class Cryptography
{
    // Example C Program: Listing System and Physical Stores
    // https://docs.microsoft.com/windows/win32/seccrypto/example-c-program-listing-system-and-physical-stores

    /// <summary>
    ///  Attempts to close the given handle.
    /// </summary>
    public static void CloseStore(HCERTSTORE handle)
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.CertCloseStore(handle, dwFlags: 0));

    private static unsafe CertificateStoreHandle OpenSystemStoreWrapper(StoreName storeName)
    {
        uint flags = (uint)StoreOpenFlags.NoCryptRelease;
        if (storeName == StoreName.SPC)
        {
            flags |= (uint)SystemStoreLocation.LocalMachine;
        }
        else
        {
            flags |= (uint)SystemStoreLocation.CurrentUser;
        }

        fixed (char* name = storeName.ToString())
        {
            CertificateStoreHandle store = TerraFXWindows.CertOpenStore(
                lpszStoreProvider: (sbyte*)(uint)StoreProvider.System,
                dwEncodingType: 0,
                hCryptProv: default,
                dwFlags: flags,
                pvPara: (void*)name);

            return store;
        }
    }

    public static CertificateStoreHandle OpenSystemStore(StoreName storeName)
    {
        return OpenSystemStoreWrapper(storeName);
    }

    [UnmanagedCallersOnly]
    private static BOOL SystemStoreLocationCallback(
        ushort* pvszStoreLocations,
        uint dwFlags,
        void* pvReserved,
        void* pvArg)
    {
        GCHandle handle = GCHandle.FromIntPtr((IntPtr)pvArg);
        var infos = (List<string>)handle.Target!;
        string? result = new((char*)pvszStoreLocations);
        if (!string.IsNullOrEmpty(result))
        {
            infos.Add(result);
        }

        return true;
    }

    public static IEnumerable<string> EnumerateSystemStoreLocations()
    {
        var info = new List<string>();
        var handle = GCHandle.Alloc(info);

        TerraFXWindows.CertEnumSystemStoreLocation(
            dwFlags: 0,
            pvArg: (void*)GCHandle.ToIntPtr(handle),
            pfnEnum: &SystemStoreLocationCallback);

        return info;
    }

    /// <docs>
    ///  https://docs.microsoft.com/windows/win32/api/wincrypt/nc-wincrypt-pfn_cert_enum_system_store
    /// </docs>
    [UnmanagedCallersOnly]
    private static unsafe BOOL CertEnumSystemStore(
        void* pvSystemStore,
        uint dwFlags,
        CERT_SYSTEM_STORE_INFO* pStoreInfo,
        void* pvReserved,
        void* pvArg)
    {
        GCHandle handle = GCHandle.FromIntPtr((IntPtr)pvArg);
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
                TerraFXWindows.CertEnumSystemStore(
                    dwFlags: (uint)location,
                    pvSystemStoreLocationPara: namePointer,
                    pvArg: (void*)GCHandle.ToIntPtr(infoHandle),
                    pfnEnum: &CertEnumSystemStore);
            }
            finally
            {
                infoHandle.Free();
            }
        }

        return info;
    }

    /// <docs>
    ///  https://docs.microsoft.com/windows/win32/api/wincrypt/nc-wincrypt-pfn_cert_enum_physical_store
    /// </docs>
    [UnmanagedCallersOnly]
    private static unsafe BOOL CertEnumPhysicalStore(
        void* pvSystemStore,
        uint dwFlags,
        ushort* pwszStoreName,
        CERT_PHYSICAL_STORE_INFO* pStoreInfo,
        void* pvReserved,
        void* pvArg)
    {
        GCHandle handle = GCHandle.FromIntPtr((IntPtr)pvArg);
        var infos = (List<PhysicalStoreInformation>)(handle.Target ?? throw new InvalidOperationException());

        PhysicalStoreInformation info = new()
        {
            SystemStoreInformation = GetSystemNameAndKey(dwFlags, pvSystemStore),
            PhysicalStoreName = new string((char*)pwszStoreName)
        };

        info.ProviderType = new string((char*)pStoreInfo->pszOpenStoreProvider);
        infos.Add(info);

        return true;
    }

    public static unsafe IEnumerable<PhysicalStoreInformation> EnumeratePhysicalStores(SystemStoreLocation location, string systemStoreName)
    {
        var info = new List<PhysicalStoreInformation>();
        GCHandle infoHandle = GCHandle.Alloc(info);

        fixed (void* s = systemStoreName)
        {
            try
            {
                // To lookup system stores in an alternate location you need to set CERT_SYSTEM_STORE_RELOCATE_FLAG
                // and pass in the name and alternate location (HKEY) in pvSystemStoreLocationPara.
                TerraFXWindows.CertEnumPhysicalStore(
                    pvSystemStore: s,
                    dwFlags: (uint)location,
                    pvArg: (void*)GCHandle.ToIntPtr(infoHandle),
                    pfnEnum: &CertEnumPhysicalStore);
            }
            finally
            {
                infoHandle.Free();
            }
        }

        return info;
    }

    private static unsafe SystemStoreInformation GetSystemNameAndKey(uint dwFlags, void* pvSystemStore)
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
            info.Name = Marshal.PtrToStringUni((IntPtr)pvSystemStore) ?? string.Empty;
        }

        info.Location = (SystemStoreLocation)(dwFlags & CryptoDefines.CERT_SYSTEM_STORE_LOCATION_MASK);
        return info;
    }
}