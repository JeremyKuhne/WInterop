// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.Errors;
using WInterop.Registry;
using WInterop.Storage;
using WInterop.Windows;

namespace WInterop.Communications;

public static unsafe partial class Communications
{
    public static DeviceControlBlock GetCommunicationsState(SafeFileHandle fileHandle)
    {
        DeviceControlBlock dcb = new()
        {
            DCBlength = (uint)sizeof(DeviceControlBlock)
        };

        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetCommState((HANDLE)fileHandle.DangerousGetHandle(), (DCB*)&dcb));

        return dcb;
    }

    public static void SetCommunicationsState(SafeFileHandle fileHandle, ref DeviceControlBlock dcb)
    {
        dcb.DCBlength = (uint)sizeof(DeviceControlBlock);

        fixed (void* d = &dcb)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetCommState((HANDLE)fileHandle.DangerousGetHandle(), (DCB*)d));
        }
    }

    public static DeviceControlBlock BuildDeviceControlBlock(string definition)
    {
        DeviceControlBlock dcb;

        fixed (void* d = definition)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.BuildCommDCBW((ushort*)d, (DCB*)&dcb));
        }

        return dcb;
    }

    public static CommunicationsProperties GetCommunicationsProperties(SafeFileHandle fileHandle)
    {
        CommunicationsProperties properties;
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetCommProperties((HANDLE)fileHandle.DangerousGetHandle(), (COMMPROP*)&properties));

        return properties;
    }

    public static CommunicationsConfig GetCommunicationsConfig(SafeFileHandle fileHandle)
    {
        CommunicationsConfig config = default;
        uint size = (uint)sizeof(CommunicationsConfig);

        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetCommConfig((HANDLE)fileHandle.DangerousGetHandle(), (COMMCONFIG*)&config, &size));

        return config;
    }

    /// <summary>
    ///  Get the default config values for the given com port.
    /// </summary>
    /// <param name="port">Simple name only (COM1, not \\.\COM1)</param>
    public static CommunicationsConfig GetDefaultCommunicationsConfig(string port)
    {
        CommunicationsConfig config = default;
        uint size = (uint)sizeof(CommunicationsConfig);

        fixed (void* p = port)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetDefaultCommConfigW((ushort*)p, (COMMCONFIG*)&config, &size));
        }

        return config;
    }

    /// <summary>
    ///  Pops the COM port configuration dialog and returns the selected settings.
    /// </summary>
    /// <exception cref="OperationCanceledException">Thrown if the dialog is cancelled.</exception>
    public static CommunicationsConfig CommunicationsConfigDialog(string port, WindowHandle parent)
    {
        CommunicationsConfig config = GetDefaultCommunicationsConfig(port);

        fixed (void* p = port)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.CommConfigDialogW((ushort*)p, parent, (COMMCONFIG*)&config));
        }

        return config;
    }

    /// <summary>
    ///  Simple helper for CreateFile call that sets the expected values when opening a COM port.
    /// </summary>
    public static SafeFileHandle CreateComPortFileHandle(
        string path,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        FileFlags fileFlags = FileFlags.None)
    {
        return Storage.Storage.CreateFile(
            path,
            CreationDisposition.OpenExisting,
            DesiredAccess.GenericReadWrite,
            0,
            fileAttributes,
            fileFlags);
    }

    public static IEnumerable<string> GetAvailableComPorts()
    {
        using var key = Registry.Registry.OpenKey(
            RegistryKeyHandle.HKEY_LOCAL_MACHINE,
            @"HARDWARE\DEVICEMAP\SERIALCOMM");

        return Registry.Registry.GetValueDataDirect(key, RegistryValueType.String).OfType<string>().ToArray();
    }
}