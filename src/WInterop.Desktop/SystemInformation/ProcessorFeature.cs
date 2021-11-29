// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms724482.aspx
public enum ProcessorFeature : uint
{
    /// <summary>
    ///  Pentium floating point bug present.
    ///  [PF_FLOATING_POINT_PRECISION_ERRATA]
    /// </summary>
    /// <remarks>
    ///  https://en.wikipedia.org/wiki/Pentium_FDIV_bug
    /// </remarks>
    FloatingPointPrecisionErrata = 0,

    /// <summary>
    ///  [PF_FLOATING_POINT_EMULATED]
    /// </summary>
    FloatingPointEmulated = 1,

    /// <summary>
    ///  Atomic compare and exchange (cmpexchg) is available. [PF_COMPARE_EXCHANGE_DOUBLE]
    /// </summary>
    CompareExchangeDouble = 2,

    /// <summary>
    ///  MMX instruction set is available. [PF_MMX_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    MmxInstructionsAvailable = 3,

    // PowerPC only, long gone
    // PF_PPC_MOVEMEM_64BIT_OK = 4,

    // Alpha only, long gone
    // PF_ALPHA_BYTE_INSTRUCTIONS = 5,

    /// <summary>
    ///  SSE instruction set is available. [PF_XMMI_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    XmmiInstructionsAvailable = 6,

    /// <summary>
    ///  3D-Now instruction set is available. [PF_3DNOW_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    Amd3dNowInstructionsAvailable = 7,

    /// <summary>
    ///  The RDTSC instruction is available. [PF_RDTSC_INSTRUCTION_AVAILABLE]
    /// </summary>
    RdtscInstructionAvailable = 8,

    /// <summary>
    ///  Physical Address Extension is available. (Always true for x64.) [PF_PAE_ENABLED]
    /// </summary>
    PaeEnabled = 9,

    /// <summary>
    ///  [PF_XMMI64_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    Xmmi64InstructionsAvailable = 10,

    /// <summary>
    ///  [PF_SSE_DAZ_MODE_AVAILABLE]
    /// </summary>
    SseDazModeAvailable = 11,

    /// <summary>
    ///  [PF_NX_ENABLED]
    /// </summary>
    NoExecuteEnabled = 12,

    /// <summary>
    ///  [PF_SSE3_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    Sse3InstructionsAvailable = 13,

    /// <summary>
    ///  [PF_COMPARE_EXCHANGE128]
    /// </summary>
    CompareExchange128 = 14,

    /// <summary>
    ///  [PF_COMPARE64_EXCHANGE128]
    /// </summary>
    Compare64Exchange128 = 15,

    /// <summary>
    ///  [PF_CHANNELS_ENABLED]
    /// </summary>
    ChannelsEnabled = 16,

    /// <summary>
    ///  [PF_XSAVE_ENABLED]
    /// </summary>
    XSaveEnabled = 17,

    /// <summary>
    ///  [PF_ARM_VFP_32_REGISTERS_AVAILABLE]
    /// </summary>
    ArmVfp32RegistersAvailable = 18,

    /// <summary>
    ///  [PF_ARM_NEON_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    ArmNeonInstrucationsAvailable = 19,

    /// <summary>
    ///  [PF_SECOND_LEVEL_ADDRESS_TRANSLATION]
    /// </summary>
    SecondLevelAddressTranslation = 20,

    /// <summary>
    ///  [PF_VIRT_FIRMWARE_ENABLED]
    /// </summary>
    VirtualizationFirmwareEnabled = 21,

    /// <summary>
    ///  [PF_RDWRFSGSBASE_AVAILABLE]
    /// </summary>
    RdWrFsGsBaseAvailable = 22,

    /// <summary>
    ///  [PF_FASTFAIL_AVAILABLE]
    /// </summary>
    FastFailAvailable = 23,

    /// <summary>
    ///  [PF_ARM_DIVIDE_INSTRUCTION_AVAILABLE]
    /// </summary>
    ArmDivideInstructionAvailable = 24,

    /// <summary>
    ///  [PF_ARM_64BIT_LOADSTORE_ATOMIC]
    /// </summary>
    Arm64BitLoadstoreAtomic = 25,

    /// <summary>
    ///  [PF_ARM_EXTERNAL_CACHE_AVAILABLE]
    /// </summary>
    ArmExternalCacheAvailable = 26,

    /// <summary>
    ///  [PF_ARM_FMAC_INSTRUCTIONS_AVAILABLE]
    /// </summary>
    ArmFmacInstructionsAvailable = 27,

    /// <summary>
    ///  [PF_RDRAND_INSTRUCTION_AVAILABLE]
    /// </summary>
    RdrandInstructionAvailable = 28
}