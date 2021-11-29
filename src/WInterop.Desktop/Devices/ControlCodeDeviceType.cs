// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices;

public enum ControlCodeDeviceType : ushort
{
    // https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/specifying-device-types
    // See devioctl.h

    /// <summary>
    ///  Beep. [FILE_DEVICE_BEEP]
    /// </summary>
    Beep = 0x0001,

    /// <summary>
    ///  CD-ROM. [FILE_DEVICE_CD_ROM]
    /// </summary>
    Cdrom = 0x0002,

    /// <summary>
    ///  CD-ROM file system. [FILE_DEVICE_CD_ROM_FILE_SYSTEM]
    /// </summary>
    CdromFileSystem = 0x0003,

    /// <summary>
    ///  Controller. [FILE_DEVICE_CONTROLLER]
    /// </summary>
    Controller = 0x0004,

    /// <summary>
    ///  Datalink. [FILE_DEVICE_DATALINK]
    /// </summary>
    Datalink = 0x0005,

    /// <summary>
    ///  Distributed file system device. [FILE_DEVICE_DFS]
    /// </summary>
    DistributedFileSystem = 0x0006,

    /// <summary>
    ///  Disk. [FILE_DEVICE_DISK]
    /// </summary>
    Disk = 0x0007,

    /// <summary>
    ///  Disk file system. [FILE_DEVICE_DISK_FILE_SYSTEM]
    /// </summary>
    DiskFileSystem = 0x0008,

    /// <summary>
    ///  File system. [FILE_DEVICE_FILE_SYSTEM]
    /// </summary>
    FileSystem = 0x0009,

    /// <summary>
    ///  [FILE_DEVICE_INPORT_PORT]
    /// </summary>
    InPort = 0x000a,

    /// <summary>
    ///  [FILE_DEVICE_KEYBOARD]
    /// </summary>
    Keyboard = 0x000b,

    /// <summary>
    ///  [FILE_DEVICE_MAILSLOT]
    /// </summary>
    MailSlot = 0x000c,

    /// <summary>
    ///  [FILE_DEVICE_MIDI_IN]
    /// </summary>
    MidiIn = 0x000d,

    /// <summary>
    ///  FILE_DEVICE_MIDI_OUT
    /// </summary>
    MidiOut = 0x000e,

    /// <summary>
    ///  [FILE_DEVICE_MOUSE]
    /// </summary>
    Mouse = 0x000f,

    /// <summary>
    ///  [FILE_DEVICE_MULTI_UNC_PROVIDER]
    /// </summary>
    MultiUncProvider = 0x0010,

    /// <summary>
    ///  [FILE_DEVICE_NAMED_PIPE]
    /// </summary>
    NamedPipe = 0x0011,

    /// <summary>
    ///  [FILE_DEVICE_NETWORK]
    /// </summary>
    Network = 0x0012,

    /// <summary>
    ///  [FILE_DEVICE_NETWORK_BROWSER]
    /// </summary>
    NetworkBrowser = 0x0013,

    /// <summary>
    ///  [FILE_DEVICE_NETWORK_FILE_SYSTEM]
    /// </summary>
    NetworkFileSystem = 0x0014,

    /// <summary>
    ///  [FILE_DEVICE_NULL]
    /// </summary>
    Null = 0x0015,

    /// <summary>
    ///  [FILE_DEVICE_PARALLEL_PORT]
    /// </summary>
    ParallelPort = 0x0016,

    /// <summary>
    ///  [FILE_DEVICE_PHYSICAL_NETCARD]
    /// </summary>
    PhysicalNetcard = 0x0017,

    /// <summary>
    ///  [FILE_DEVICE_PRINTER]
    /// </summary>
    Printer = 0x0018,

    /// <summary>
    ///  [FILE_DEVICE_SCANNER]
    /// </summary>
    Scanner = 0x0019,

    /// <summary>
    ///  [FILE_DEVICE_SERIAL_MOUSE_PORT]
    /// </summary>
    SerialMousePort = 0x001a,

    /// <summary>
    ///  [FILE_DEVICE_SERIAL_PORT]
    /// </summary>
    SerialPort = 0x001b,

    /// <summary>
    ///  [FILE_DEVICE_SCREEN]
    /// </summary>
    Screen = 0x001c,

    /// <summary>
    ///  [FILE_DEVICE_SOUND]
    /// </summary>
    Sound = 0x001d,

    /// <summary>
    ///  [FILE_DEVICE_STREAMS]
    /// </summary>
    Streams = 0x001e,

    /// <summary>
    ///  [FILE_DEVICE_TAPE]
    /// </summary>
    Tape = 0x001f,

    /// <summary>
    ///  [FILE_DEVICE_TAPE_FILE_SYSTEM]
    /// </summary>
    TapeFileSystem = 0x0020,

    /// <summary>
    ///  [FILE_DEVICE_TRANSPORT]
    /// </summary>
    Transport = 0x0021,

    /// <summary>
    ///  Unknown or USB. [FILE_DEVICE_UNKNOWN] [FILE_DEVICE_USB]
    /// </summary>
    Unknown = 0x0022,

    /// <summary>
    ///  Video. [FILE_DEVICE_VIDEO]
    /// </summary>
    Video = 0x0023,

    /// <summary>
    ///  Virtual disk. [FILE_DEVICE_VIRTUAL_DISK]
    /// </summary>
    VirtualDisk = 0x0024,

    /// <summary>
    ///  [FILE_DEVICE_WAVE_IN]
    /// </summary>
    WaveIn = 0x0025,

    /// <summary>
    ///  Wave out. [FILE_DEVICE_WAVE_OUT]
    /// </summary>
    WaveOut = 0x0026,

    /// <summary>
    ///  8042 port. [FILE_DEVICE_8042_PORT]
    /// </summary>
    Ps2 = 0x0027,

    /// <summary>
    ///  [FILE_DEVICE_NETWORK_REDIRECTOR]
    /// </summary>
    NetworkRedirector = 0x0028,

    /// <summary>
    ///  Battery. [FILE_DEVICE_BATTERY]
    /// </summary>
    Battery = 0x0029,

    /// <summary>
    ///  Bus Extender. [FILE_DEVICE_BUS_EXTENDER]
    /// </summary>
    BusExtender = 0x002a,

    /// <summary>
    ///  [FILE_DEVICE_MODEM]
    /// </summary>
    Modem = 0x002b,

    /// <summary>
    ///  Virtual DOS machine. [FILE_DEVICE_VDM]
    /// </summary>
    VirtualDosMachine = 0x002c,

    /// <summary>
    ///  [FILE_DEVICE_MASS_STORAGE]
    /// </summary>
    MassStorage = 0x002d,

    /// <summary>
    ///  Server Message Block. [FILE_DEVICE_SMB]
    /// </summary>
    ServerMessageBlock = 0x002e,

    /// <summary>
    ///  Kernel streaming. [FILE_DEVICE_KS]
    /// </summary>
    /// <remarks>
    ///  https://docs.microsoft.com/en-us/windows-hardware/drivers/stream/kernel-streaming
    /// </remarks>
    KernelStreaming = 0x002f,

    /// <summary>
    ///  Changer. [FILE_DEVICE_CHANGER]
    /// </summary>
    Changer = 0x0030,

    /// <summary>
    ///  [FILE_DEVICE_SMARTCARD]
    /// </summary>
    Smartcard = 0x0031,

    /// <summary>
    ///  ACPI. [FILE_DEVICE_ACPI]
    /// </summary>
    Acpi = 0x0032,

    /// <summary>
    ///  Dvd. [FILE_DEVICE_DVD]
    /// </summary>
    Dvd = 0x0033,

    /// <summary>
    ///  [FILE_DEVICE_FULLSCREEN_VIDEO]
    /// </summary>
    FullscreenVideo = 0x0034,

    /// <summary>
    ///  DFS file system. [FILE_DEVICE_DFS_FILE_SYSTEM]
    /// </summary>
    DfsFileSystem = 0x0035,

    /// <summary>
    ///  DfsVolume. [FILE_DEVICE_DFS_VOLUME]
    /// </summary>
    DfsVolume = 0x0036,

    /// <summary>
    ///  [FILE_DEVICE_SERENUM]
    /// </summary>
    SerialEnumerator = 0x0037,

    /// <summary>
    ///  [FILE_DEVICE_TERMSRV]
    /// </summary>
    TerminalServer = 0x0038,

    /// <summary>
    ///  [FILE_DEVICE_KSEC]
    /// </summary>
    KernelSecurity = 0x0039,

    /// <summary>
    ///  United States Federal Information Processing Standard (FIPS) [FILE_DEVICE_FIPS]
    /// </summary>
    Fips = 0x003a,

    /// <summary>
    ///  [FILE_DEVICE_INFINIBAND]
    /// </summary>
    Infiniband = 0x003B,

    /// <summary>
    ///  [FILE_DEVICE_VMBUS]
    /// </summary>
    VirtualMachineBus = 0x003E,

    /// <summary>
    ///  [FILE_DEVICE_CRYPT_PROVIDER]
    /// </summary>
    CryptographyProvider = 0x003F,

    /// <summary>
    ///  [FILE_DEVICE_WPD]
    /// </summary>
    WindowsPortableDevice = 0x0040,

    /// <summary>
    ///  [FILE_DEVICE_BLUETOOTH]
    /// </summary>
    Bluetooth = 0x0041,

    /// <summary>
    ///  Multi-transport composite driver. [FILE_DEVICE_MT_COMPOSITE]
    /// </summary>
    /// <remarks>
    ///  https://blogs.msdn.microsoft.com/wpdblog/2009/09/04/multi-transport-devices-in-windows-7/
    /// </remarks>
    MultiTransportComposite = 0x0042,

    /// <summary>
    ///  Multi-transport transport driver.[FILE_DEVICE_MT_TRANSPORT]
    /// </summary>
    MultiTransportTransport = 0x0043,

    /// <summary>
    ///  [FILE_DEVICE_BIOMETRIC]
    /// </summary>
    Biometric = 0x0044,

    /// <summary>
    ///  [FILE_DEVICE_PMI]
    /// </summary>
    PowerMeter = 0x0045,

    /// <summary>
    ///  [FILE_DEVICE_EHSTOR]
    /// </summary>
    EnhancedStorage = 0x0046,

    /// <summary>
    ///  DeviceApi? [FILE_DEVICE_DEVAPI]
    /// </summary>
    DeviceApi = 0x0047,

    /// <summary>
    ///  General-purpose IO. [FILE_DEVICE_GPIO]
    /// </summary>
    GeneralPurposeIo = 0x0048,

    /// <summary>
    ///  USB device. Previously these went [FILE_DEVICE_USBEX]
    /// </summary>
    ExtendedUsb = 0x0049,

    /// <summary>
    ///  These are messages that mountable devices must respond to to
    ///  work with with the mount point manager. [MOUNTDEVCONTROLTYPE]
    /// </summary>
    /// <remarks>Trivia: This value is upper case 'M'.</remarks>
    MountDevice = 0x004d,

    /// <summary>
    ///  [FILE_DEVICE_CONSOLE]
    /// </summary>
    Console = 0x0050,

    /// <summary>
    ///  [FILE_DEVICE_NFP]
    /// </summary>
    NearFieldProximity = 0x0051,

    /// <summary>
    ///  [FILE_DEVICE_SYSENV]
    /// </summary>
    SystemEnvironment = 0x0052,

    /// <summary>
    ///  [FILE_DEVICE_VIRTUAL_BLOCK]
    /// </summary>
    VirtualBlock = 0x0053,

    /// <summary>
    ///  [FILE_DEVICE_POINT_OF_SERVICE]
    /// </summary>
    PointOfService = 0x0054,

    /// <summary>
    ///  [FILE_DEVICE_STORAGE_REPLICATION]
    /// </summary>
    StorageReplication = 0x0055,

    /// <summary>
    ///  [FILE_DEVICE_TRUST_ENV]
    /// </summary>
    TrustedExecutionEnvironment = 0x0056,

    /// <summary>
    ///  USB Connector Manager. [FILE_DEVICE_UCM]
    /// </summary>
    UsbConnectorManager = 0x0057,

    /// <summary>
    ///  [FILE_DEVICE_UCMTCPCI]
    /// </summary>
    UsbConnectorManagerTypeC = 0x0058,

    /// <summary>
    ///  [FILE_DEVICE_PERSISTENT_MEMORY]
    /// </summary>
    PersistentMemory = 0x0059,

    /// <summary>
    ///  [FILE_DEVICE_NVDIMM]
    /// </summary>
    NonVolatileDimm = 0x005a,

    /// <summary>
    ///  [FILE_DEVICE_HOLOGRAPHIC]
    /// </summary>
    Holographic = 0x005b,

    /// <summary>
    ///  Mount manager. [MOUNTMGRCONTROLTYPE]
    /// </summary>
    /// <remarks>Trivia: This value is lower case 'm'.</remarks>
    MountManager = 0x006d,

    /// <summary>
    ///  Infrared class driver. [FILE_DEVICE_IRCLASS]
    /// </summary>
    Infrared = 0x0f60
}