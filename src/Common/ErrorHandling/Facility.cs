﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling
{
    /// <summary>
    /// Windows error facility codes
    /// </summary>
    public enum Facility : uint
    {
        XPS = 82,
        XAML = 43,
        USN = 129,
        BLBUI = 128,
        SPP = 256,
        WSB_ONLINE = 133,
        DLS = 153,
        BLB_CLI = 121,
        BLB = 120,
        WSBAPP = 122,
        WPN = 62,
        WMAAECMA = 1996,
        WINRM = 51,
        WINPE = 61,
        WINDOWSUPDATE = 36,
        WINDOWS_STORE = 63,
        WINDOWS_SETUP = 48,
        WINDOWS_DEFENDER = 80,
        WINDOWS_CE = 24,
        WINDOWS = 8,
        WINCODEC_DWRITE_DWM = 2200,
        WIA = 33,
        WER = 27,
        WEP = 2049,
        WEB_SOCKET = 886,
        WEB = 885,
        USERMODE_VOLSNAP = 130,
        USERMODE_VOLMGR = 56,
        VISUALCPP = 109,
        USERMODE_VIRTUALIZATION = 55,
        USERMODE_VHD = 58,
        URT = 19,
        UMI = 22,
        UI = 42,
        TPM_SOFTWARE = 41,
        TPM_SERVICES = 40,
        TIERING = 131,
        SYNCENGINE = 2050,
        SXS = 23,
        STORAGE = 3,
        STATE_MANAGEMENT = 34,
        SSPI = 9,
        USERMODE_SPACES = 231,
        SOS = 160,
        SCARD = 16,
        SHELL = 39,
        SETUPAPI = 15,
        SECURITY = 9,
        SDIAG = 60,
        USERMODE_SDBUS = 2305,
        RPC = 1,
        RESTORE = 256,
        SCRIPT = 112,
        PARSE = 113,
        RAS = 83,
        POWERSHELL = 84,
        PLA = 48,
        PIDGENX = 2561,
        P2P_INT = 98,
        P2P = 99,
        OPC = 81,
        ONLINE_ID = 134,
        WIN32 = 7,
        CONTROL = 10,
        WEBSERVICES = 61,
        NULL = 0,
        NDIS = 52,
        NAP = 39,
        MOBILE = 1793,
        METADIRECTORY = 35,
        MSMQ = 14,
        MEDIASERVER = 13,
        MBN = 84,
        LINGUISTIC_SERVICES = 305,
        LEAP = 2184,
        JSCRIPT = 2306,
        INTERNET = 12,
        ITF = 4,
        INPUT = 64,
        USERMODE_HYPERVISOR = 53,
        ACCELERATOR = 1536,
        HTTP = 25,
        GRAPHICS = 38,
        FWP = 50,
        FVE = 49,
        USERMODE_FILTER_MANAGER = 31,
        EAS = 85,
        EAP = 66,
        DXGI_DDI = 2171,
        DXGI = 2170,
        DPLAY = 21,
        DMSERVER = 256,
        DISPATCH = 2,
        DIRECTORYSERVICE = 37,
        DIRECTMUSIC = 2168,
        DIRECT3D11 = 2172,
        DIRECT3D10 = 2169,
        DIRECT2D = 2201,
        DAF = 100,
        DEPLOYMENT_SERVICES_UTIL = 260,
        DEPLOYMENT_SERVICES_TRANSPORT_MANAGEMENT = 272,
        DEPLOYMENT_SERVICES_TFTP = 264,
        DEPLOYMENT_SERVICES_PXE = 263,
        DEPLOYMENT_SERVICES_MULTICAST_SERVER = 289,
        DEPLOYMENT_SERVICES_MULTICAST_CLIENT = 290,
        DEPLOYMENT_SERVICES_MANAGEMENT = 259,
        DEPLOYMENT_SERVICES_IMAGING = 258,
        DEPLOYMENT_SERVICES_DRIVER_PROVISIONING = 278,
        DEPLOYMENT_SERVICES_SERVER = 257,
        DEPLOYMENT_SERVICES_CONTENT_PROVIDER = 293,
        DEPLOYMENT_SERVICES_BINLSVC = 261,
        DEFRAG = 2304,
        DEBUGGERS = 176,
        CONFIGURATION = 33,
        COMPLUS = 17,
        USERMODE_COMMONLOG = 26,
        CMI = 54,
        CERT = 11,
        BLUETOOTH_ATT = 101,
        BCD = 57,
        BACKGROUNDCOPY = 32,
        AUDIOSTREAMING = 1094,
        AUDCLNT = 2185,
        AUDIO = 102,
        ACTION_QUEUE = 44,
        ACS = 20,
        AAF = 18
    }
}