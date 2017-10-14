// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DeviceManagement.Types
{
    public enum ControlCodeDeviceType
    {
        // https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/specifying-device-types
        // See devioctl.h

        /// <summary>
        /// Beep. [FILE_DEVICE_BEEP]
        /// </summary>
        Beep = 0x00000001,

        /// <summary>
        /// CD-ROM. [FILE_DEVICE_CD_ROM]
        /// </summary>
        Cdrom = 0x00000002,

        /// <summary>
        /// CD-ROM file system. [FILE_DEVICE_CD_ROM_FILE_SYSTEM]
        /// </summary>
        CdromFileSystem = 0x00000003,

        /// <summary>
        /// Controller. [FILE_DEVICE_CONTROLLER]
        /// </summary>
        Controller = 0x00000004,

        /// <summary>
        /// Datalink. [FILE_DEVICE_DATALINK]
        /// </summary>
        Datalink = 0x00000005,

        /// <summary>
        /// Distributed file system device. [FILE_DEVICE_DFS]
        /// </summary>
        DistributedFileSystem = 0x00000006,

        /// <summary>
        /// Disk. [FILE_DEVICE_DISK]
        /// </summary>
        Disk = 0x00000007,

        /// <summary>
        /// Disk file system. [FILE_DEVICE_DISK_FILE_SYSTEM]
        /// </summary>
        DiskFileSystem = 0x00000008,

        /// <summary>
        /// File system. [FILE_DEVICE_FILE_SYSTEM]
        /// </summary>
        FileSystem = 0x00000009,

        /// <summary>
        /// [FILE_DEVICE_INPORT_PORT]
        /// </summary>
        InPort = 0x0000000a,

        /// <summary>
        /// [FILE_DEVICE_KEYBOARD]
        /// </summary>
        Keyboard = 0x0000000b,

        /// <summary>
        /// [FILE_DEVICE_MAILSLOT]
        /// </summary>
        MailSlot = 0x0000000c,

        /// <summary>
        /// [FILE_DEVICE_MIDI_IN]
        /// </summary>
        MidiIn = 0x0000000d,

        /// <summary>
        /// FILE_DEVICE_MIDI_OUT
        /// </summary>
        MidiOut = 0x0000000e,

        /// <summary>
        /// [FILE_DEVICE_MOUSE]
        /// </summary>
        Mouse = 0x0000000f,

        /// <summary>
        /// [FILE_DEVICE_MULTI_UNC_PROVIDER]
        /// </summary>
        MultiUncProvider = 0x00000010,

        /// <summary>
        /// [FILE_DEVICE_NAMED_PIPE]
        /// </summary>
        NamedPipe = 0x00000011,

        /// <summary>
        /// [FILE_DEVICE_NETWORK]
        /// </summary>
        Network = 0x00000012,

        /// <summary>
        /// [FILE_DEVICE_NETWORK_BROWSER]
        /// </summary>
        NetworkBrowser = 0x00000013,

        /// <summary>
        /// [FILE_DEVICE_NETWORK_FILE_SYSTEM]
        /// </summary>
        NetworkFileSystem = 0x00000014,

        /// <summary>
        /// [FILE_DEVICE_NULL]
        /// </summary>
        Null = 0x00000015,

        /// <summary>
        /// [FILE_DEVICE_PARALLEL_PORT]
        /// </summary>
        ParallelPort = 0x00000016,

        /// <summary>
        /// [FILE_DEVICE_PHYSICAL_NETCARD]
        /// </summary>
        PhysicalNetcard = 0x00000017,

        /// <summary>
        /// [FILE_DEVICE_PRINTER]
        /// </summary>
        Printer = 0x00000018,

        /// <summary>
        /// [FILE_DEVICE_SCANNER]
        /// </summary>
        Scanner = 0x00000019,

        /// <summary>
        /// [FILE_DEVICE_SERIAL_MOUSE_PORT]
        /// </summary>
        SerialMousePort = 0x0000001a,

        /// <summary>
        /// [FILE_DEVICE_SERIAL_PORT]
        /// </summary>
        SerialPort = 0x0000001b,

        /// <summary>
        /// [FILE_DEVICE_SCREEN]
        /// </summary>
        Screen = 0x0000001c,

        /// <summary>
        /// [FILE_DEVICE_SOUND]
        /// </summary>
        Sound = 0x0000001d,

        /// <summary>
        /// [FILE_DEVICE_STREAMS]
        /// </summary>
        Streams = 0x0000001e,

        /// <summary>
        /// [FILE_DEVICE_TAPE]
        /// </summary>
        Tape = 0x0000001f,

        /// <summary>
        /// [FILE_DEVICE_TAPE_FILE_SYSTEM]
        /// </summary>
        TapeFileSystem = 0x00000020,

        /// <summary>
        /// [FILE_DEVICE_TRANSPORT]
        /// </summary>
        Transport = 0x00000021,

        /// <summary>
        /// Unknown or USB. [FILE_DEVICE_UNKNOWN] [FILE_DEVICE_USB]
        /// </summary>
        Unknown = 0x00000022,

        /// <summary>
        /// Video. [FILE_DEVICE_VIDEO]
        /// </summary>
        Video = 0x00000023,

        /// <summary>
        /// Virtual disk. [FILE_DEVICE_VIRTUAL_DISK]
        /// </summary>
        VirtualDisk = 0x00000024,

        /// <summary>
        /// [FILE_DEVICE_WAVE_IN]
        /// </summary>
        WaveIn = 0x00000025,

        /// <summary>
        /// Wave out. [FILE_DEVICE_WAVE_OUT]
        /// </summary>
        WaveOut = 0x00000026,

        /// <summary>
        /// 8042 port. [FILE_DEVICE_8042_PORT]
        /// </summary>
        Ps2 = 0x00000027,

        /// <summary>
        /// [FILE_DEVICE_NETWORK_REDIRECTOR]
        /// </summary>
        NetworkRedirector = 0x00000028,

        /// <summary>
        /// Battery. [FILE_DEVICE_BATTERY]
        /// </summary>
        Battery = 0x00000029,

        /// <summary>
        /// Bus Extender. [FILE_DEVICE_BUS_EXTENDER]
        /// </summary>
        BusExtender = 0x0000002a,

        /// <summary>
        /// [FILE_DEVICE_MODEM]
        /// </summary>
        Modem = 0x0000002b,

        /// <summary>
        /// Virtual DOS machine. [FILE_DEVICE_VDM]
        /// </summary>
        VirtualDosMachine = 0x0000002c,

        /// <summary>
        /// [FILE_DEVICE_MASS_STORAGE]
        /// </summary>
        MassStorage = 0x0000002d,

        /// <summary>
        /// Server Message Block. [FILE_DEVICE_SMB]
        /// </summary>
        ServerMessageBlock = 0x0000002e,

        /// <summary>
        /// Kernel streaming. [FILE_DEVICE_KS]
        /// </summary>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/stream/kernel-streaming
        /// </remarks>
        KernelStreaming = 0x0000002f,

        /// <summary>
        /// Changer. [FILE_DEVICE_CHANGER]
        /// </summary>
        Changer = 0x00000030,

        /// <summary>
        /// [FILE_DEVICE_SMARTCARD]
        /// </summary>
        Smartcard = 0x00000031,

        /// <summary>
        /// ACPI. [FILE_DEVICE_ACPI]
        /// </summary>
        Acpi = 0x00000032,

        /// <summary>
        /// Dvd. [FILE_DEVICE_DVD]
        /// </summary>
        Dvd = 0x00000033,

        /// <summary>
        /// [FILE_DEVICE_FULLSCREEN_VIDEO]
        /// </summary>
        FullscreenVideo = 0x00000034,

        /// <summary>
        /// DFS file system. [FILE_DEVICE_DFS_FILE_SYSTEM]
        /// </summary>
        DfsFileSystem = 0x00000035,

        /// <summary>
        /// DfsVolume. [FILE_DEVICE_DFS_VOLUME]
        /// </summary>
        DfsVolume = 0x00000036,

        /// <summary>
        /// [FILE_DEVICE_SERENUM]
        /// </summary>
        SerialEnumerator = 0x00000037,

        /// <summary>
        /// [FILE_DEVICE_TERMSRV]
        /// </summary>
        TerminalServer = 0x00000038,

        /// <summary>
        /// [FILE_DEVICE_KSEC]
        /// </summary>
        KernelSecurity = 0x00000039,

        /// <summary>
        /// United States Federal Information Processing Standard (FIPS) [FILE_DEVICE_FIPS]
        /// </summary>
        Fips = 0x0000003a,

        /// <summary>
        /// [FILE_DEVICE_INFINIBAND]
        /// </summary>
        Infiniband = 0x0000003B,

        /// <summary>
        /// [FILE_DEVICE_VMBUS]
        /// </summary>
        VirtualMachineBus = 0x0000003E,

        /// <summary>
        /// [FILE_DEVICE_CRYPT_PROVIDER]
        /// </summary>
        CryptographyProvider = 0x0000003F,

        /// <summary>
        /// [FILE_DEVICE_WPD]
        /// </summary>
        WindowsPortableDevice = 0x00000040,

        /// <summary>
        /// [FILE_DEVICE_BLUETOOTH]
        /// </summary>
        Bluetooth = 0x00000041,

        /// <summary>
        /// Multi-transport composite driver. [FILE_DEVICE_MT_COMPOSITE]
        /// </summary>
        /// <remarks>
        /// https://blogs.msdn.microsoft.com/wpdblog/2009/09/04/multi-transport-devices-in-windows-7/
        /// </remarks>
        MultiTransportComposite = 0x00000042,

        /// <summary>
        /// Multi-transport transport driver.[FILE_DEVICE_MT_TRANSPORT]
        /// </summary>
        MultiTransportTransport = 0x00000043,

        /// <summary>
        /// [FILE_DEVICE_BIOMETRIC]
        /// </summary>
        Biometric = 0x00000044,

        /// <summary>
        /// [FILE_DEVICE_PMI]
        /// </summary>
        PowerMeter = 0x00000045,

        /// <summary>
        /// [FILE_DEVICE_EHSTOR]
        /// </summary>
        EnhancedStorage = 0x00000046,

        /// <summary>
        /// DeviceApi? [FILE_DEVICE_DEVAPI]
        /// </summary>
        DeviceApi = 0x00000047,

        /// <summary>
        /// General-purpose IO. [FILE_DEVICE_GPIO]
        /// </summary>
        GeneralPurposeIo = 0x00000048,

        /// <summary>
        /// USB device. Previously these went [FILE_DEVICE_USBEX]
        /// </summary>
        ExtendedUsb = 0x00000049,

        /// <summary>
        /// [MOUNTDEVCONTROLTYPE]
        /// </summary>
        MountDevice = 0x0000004d,

        /// <summary>
        /// [FILE_DEVICE_CONSOLE]
        /// </summary>
        Console = 0x00000050,

        /// <summary>
        /// [FILE_DEVICE_NFP]
        /// </summary>
        NearFieldProximity = 0x00000051,

        /// <summary>
        /// [FILE_DEVICE_SYSENV]
        /// </summary>
        SystemEnvironment = 0x00000052,

        /// <summary>
        /// [FILE_DEVICE_VIRTUAL_BLOCK]
        /// </summary>
        VirtualBlock = 0x00000053,

        /// <summary>
        /// [FILE_DEVICE_POINT_OF_SERVICE]
        /// </summary>
        PointOfService = 0x00000054,

        /// <summary>
        /// [FILE_DEVICE_STORAGE_REPLICATION]
        /// </summary>
        StorageReplication = 0x00000055,

        /// <summary>
        /// [FILE_DEVICE_TRUST_ENV]
        /// </summary>
        TrustedExecutionEnvironment = 0x00000056,

        /// <summary>
        /// USB Connector Manager. [FILE_DEVICE_UCM]
        /// </summary>
        UsbConnectorManager = 0x00000057,

        /// <summary>
        /// [FILE_DEVICE_UCMTCPCI]
        /// </summary>
        UsbConnectorManagerTypeC = 0x00000058,

        /// <summary>
        /// [FILE_DEVICE_PERSISTENT_MEMORY]
        /// </summary>
        PersistentMemory = 0x00000059,

        /// <summary>
        /// [FILE_DEVICE_NVDIMM]
        /// </summary>
        NonVolatileDimm = 0x0000005a,

        /// <summary>
        /// [FILE_DEVICE_HOLOGRAPHIC]
        /// </summary>
        Holographic = 0x0000005b,

        /// <summary>
        /// Mount manager. [MOUNTMGRCONTROLTYPE]
        /// </summary>
        MountManager = 0x0000006d,

        /// <summary>
        /// Infrared class driver. [FILE_DEVICE_IRCLASS]
        /// </summary>
        Infrared = 0x00000f60
    }
}
