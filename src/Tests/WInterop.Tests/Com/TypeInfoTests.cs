// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Com;
using WInterop.Globalization;
using Xunit;

namespace ComTests;

public class TypeInfoTests
{
    [Fact]
    public void GetTypeInfoForIUnknown()
    {
        using var library = TypeLibTests.LoadStdOle2();
        using var typeInfo = library.FindName("IUnknown", 1, out _)[0].Info;
        typeInfo.IsNull.Should().BeFalse();
    }

    [Fact]
    public unsafe void GetTypeAttributesForIUnknown()
    {
        using var library = TypeLibTests.LoadStdOle2();
        using var typeInfo = library.FindName("IUnknown", 1, out _)[0].Info;

        using var attributes = typeInfo.GetTypeAttributes();

        attributes.FunctionCount.Should().Be(3);
        attributes.InterfaceCount.Should().Be(0);
        attributes.VariableCount.Should().Be(0);
        attributes.Alignment.Should().Be(8);
        attributes.InstanceSize.Should().Be(8);
        attributes.VTableSize.Should().Be(24);
        attributes.MajorVersion.Should().Be(0);
        attributes.MinorVersion.Should().Be(0);
        attributes.TypeKind.Should().Be(TypeKind.Interface);
        attributes.TypeFlags.Should().Be(TypeFlags.Hidden);
        attributes.IdlFlags.Should().Be(IdlFlag.None);
        attributes.Guid.Should().Be(new Guid("{00000000-0000-0000-c000-000000000046}"));
        attributes.Locale.Should().Be(LocaleId.Default);
        attributes.Constructor.Should().Be(MemberId.Nil);
        attributes.Destructor.Should().Be(MemberId.Nil);
        attributes.Alias.Type.Should().Be(VariantType.Empty);
    }

    [Fact]
    public unsafe void GetFunctionDescriptionsForIUnknown()
    {
        using var library = TypeLibTests.LoadStdOle2();
        using var typeInfo = library.FindName("IUnknown", 1, out _)[0].Info;

        // From OleView:
        //
        // [restricted]
        // HRESULT _stdcall QueryInterface(
        //                [in] GUID* riid, 
        //                [out] void** ppvObj);

        var description = typeInfo.GetFunctionDescription(0);

        description.ParameterCount.Should().Be(2);
        description.OptionalParameterCount.Should().Be(0);
        description.ReturnCodeCount.Should().Be(0);
        description.CallConvention.Should().Be(CallConvention.StdCall);
        description.FunctionKind.Should().Be(FunctionKind.PureVirtual);
        description.InvokeKind.Should().Be(InvokeKind.Function);
        description.Flags.Should().Be(FunctionFlags.Restricted);

        // Return type
        description.ReturnType.Should().Be(VariantType.HResult);

        var parameters = description.Parameters;
        parameters.Length.Should().Be(2);

        // First arg GUID*
        var first = parameters[0];
        first.ParameterType.Type.Should().Be(VariantType.Pointer);
        first.Flags.Should().Be(ParameterFlags.In);

        var pointerType = first.ParameterType.PointerType;
        pointerType.Type.Should().Be(VariantType.UserDefined);

        using var userDefined = typeInfo.GetRefTypeInfo(pointerType.TypeHandle);
        userDefined.GetDocumentation(MemberId.Nil, out string name);
        name.Should().Be("GUID");

        // Second arg void**
        var second = parameters[1];
        second.ParameterType.Type.Should().Be(VariantType.Pointer);
        second.Flags.Should().Be(ParameterFlags.Out);
        pointerType = second.ParameterType.PointerType;
        pointerType.Type.Should().Be(VariantType.Pointer);
        pointerType.PointerType.Type.Should().Be(VariantType.Void);
    }

    [Fact]
    public unsafe void GetFunctionNamesForIUnknown()
    {
        using var library = TypeLibTests.LoadStdOle2();
        using var typeInfo = library.FindName("IUnknown", out _).Info;

        var names = typeInfo.GetFunctionNames(0);
        names.Should().BeEquivalentTo("QueryInterface", "riid", "ppvObj");

        names = typeInfo.GetFunctionNames(1);
        names.Should().BeEquivalentTo("AddRef");

        names = typeInfo.GetFunctionNames(2);
        names.Should().BeEquivalentTo("Release");
    }

    [Fact]
    public unsafe void GetTypeAttributesForGUID()
    {
        using var library = TypeLibTests.LoadStdOle2();
        using var typeInfo = library.FindName("GUID").Info;

        using var attributes = typeInfo.GetTypeAttributes();

        attributes.FunctionCount.Should().Be(0);
        attributes.InterfaceCount.Should().Be(0);
        attributes.VariableCount.Should().Be(4);
        attributes.Alignment.Should().Be(4);
        attributes.InstanceSize.Should().Be(16);
        attributes.VTableSize.Should().Be(0);
        attributes.MajorVersion.Should().Be(0);
        attributes.MajorVersion.Should().Be(0);
        attributes.TypeKind.Should().Be(TypeKind.Record);
        attributes.TypeFlags.Should().Be(TypeFlags.Hidden);
        attributes.IdlFlags.Should().Be(IdlFlag.None);
        attributes.Guid.Should().Be(Guid.Empty);
        attributes.Locale.Should().Be((LocaleId)0);
        attributes.Constructor.Should().Be(MemberId.Nil);
        attributes.Destructor.Should().Be(MemberId.Nil);
        attributes.Alias.Type.Should().Be(VariantType.Empty);
    }

    [Fact]
    public unsafe void GetVariableDescriptionsForGUID()
    {
        using var library = TypeLibTests.LoadStdOle2();
        using var typeInfo = library.FindName("GUID").Info;

        using var first = typeInfo.GetVariableDescription(0);

        // uint
        first.Kind.Should().Be(VariableKind.PerInstance);
        first.Flags.Should().Be((VariableFlags)0);
        first.VariableType.Type.Should().Be(VariantType.UInt32);
        typeInfo.GetDocumentation(first.MemberId, out string name);
        name.Should().Be("Data1");

        using var second = typeInfo.GetVariableDescription(1);

        // ushort
        second.Kind.Should().Be(VariableKind.PerInstance);
        second.Flags.Should().Be((VariableFlags)0);
        second.VariableType.Type.Should().Be(VariantType.UInt16);
        typeInfo.GetDocumentation(second.MemberId, out name);
        name.Should().Be("Data2");

        using var third = typeInfo.GetVariableDescription(2);

        // ushort
        third.Kind.Should().Be(VariableKind.PerInstance);
        third.Flags.Should().Be((VariableFlags)0);
        third.VariableType.Type.Should().Be(VariantType.UInt16);
        typeInfo.GetDocumentation(third.MemberId, out name);
        name.Should().Be("Data3");

        using var fourth = typeInfo.GetVariableDescription(3);

        // Fixed 8 byte array
        fourth.Kind.Should().Be(VariableKind.PerInstance);
        fourth.Flags.Should().Be((VariableFlags)0);
        fourth.VariableType.Type.Should().Be(VariantType.CArray);
        typeInfo.GetDocumentation(fourth.MemberId, out name);
        name.Should().Be("Data4");

        fourth.VariableType.ArrayDescription.Dimensions.Should().Be(1);
        var bounds = fourth.VariableType.ArrayDescription.Bounds;
        bounds.Length.Should().Be(1);
        bounds[0].Count.Should().Be(8);
        fourth.VariableType.ArrayDescription.ElementType.Type.Should().Be(VariantType.UnsignedByte);
    }
}
