// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Storage.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff547839.aspx
    public enum INTERFACE_TYPE : uint
    {
        Internal,
        Isa,
        Eisa,
        MicroChannel,
        TurboChannel,
        PCIBus,
        VMEBus,
        NuBus,
        PCMCIABus,
        CBus,
        MPIBus,
        MPSABus,
        ProcessorInternal,
        InternalPowerBus,
        PNPISABus,
        PNPBus,
        Vmcs,
        ACPIBus,
        MaximumInterfaceType,
        InterfaceTypeUndefined = unchecked((uint)-1)
    }
}
