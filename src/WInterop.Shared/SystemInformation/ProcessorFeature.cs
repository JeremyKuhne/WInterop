// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724482.aspx
    public enum ProcessorFeature : uint
    {
        PF_FLOATING_POINT_PRECISION_ERRATA  = 0,
        PF_FLOATING_POINT_EMULATED          = 1,
        PF_COMPARE_EXCHANGE_DOUBLE          = 2,
        PF_MMX_INSTRUCTIONS_AVAILABLE       = 3,
        PF_PPC_MOVEMEM_64BIT_OK             = 4,
        PF_ALPHA_BYTE_INSTRUCTIONS          = 5,
        PF_XMMI_INSTRUCTIONS_AVAILABLE      = 6,
        PF_3DNOW_INSTRUCTIONS_AVAILABLE     = 7,
        PF_RDTSC_INSTRUCTION_AVAILABLE      = 8,
        PF_PAE_ENABLED                      = 9,
        PF_XMMI64_INSTRUCTIONS_AVAILABLE    = 10,
        PF_SSE_DAZ_MODE_AVAILABLE           = 11,
        PF_NX_ENABLED                       = 12,
        PF_SSE3_INSTRUCTIONS_AVAILABLE      = 13,
        PF_COMPARE_EXCHANGE128              = 14,
        PF_COMPARE64_EXCHANGE128            = 15,
        PF_CHANNELS_ENABLED                 = 16,
        PF_XSAVE_ENABLED                    = 17,
        PF_ARM_VFP_32_REGISTERS_AVAILABLE   = 18,
        PF_ARM_NEON_INSTRUCTIONS_AVAILABLE  = 19,
        PF_SECOND_LEVEL_ADDRESS_TRANSLATION = 20,
        PF_VIRT_FIRMWARE_ENABLED            = 21,
        PF_RDWRFSGSBASE_AVAILABLE           = 22,
        PF_FASTFAIL_AVAILABLE               = 23,
        PF_ARM_DIVIDE_INSTRUCTION_AVAILABLE = 24,
        PF_ARM_64BIT_LOADSTORE_ATOMIC       = 25,
        PF_ARM_EXTERNAL_CACHE_AVAILABLE     = 26,
        PF_ARM_FMAC_INSTRUCTIONS_AVAILABLE  = 27,
        PF_RDRAND_INSTRUCTION_AVAILABLE     = 28
    }
}
