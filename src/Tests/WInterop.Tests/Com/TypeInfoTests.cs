// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Collections.Generic;
using WInterop.Com;
using WInterop.Com.Native;
using WInterop.Errors;
using WInterop.Globalization;
using Xunit;

namespace WInterop.Tests.Com
{
    public class TypeInfoTests
    {
        [Fact]
        public void GetTypeInfoForIUnknown()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("IUnknown");
            typeInfo.Should().NotBeNull();
        }

        [Fact]
        public unsafe void GetTypeAttributesForIUnknown()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("IUnknown");

            HResult result = typeInfo.GetTypeAttr(out TYPEATTR* attributes);
            result.Should().Be(HResult.S_OK);
            Assert.True(attributes != null);

            attributes->cFuncs.Should().Be(3);
            attributes->cImplTypes.Should().Be(0);
            attributes->cVars.Should().Be(0);
            attributes->cbAlignment.Should().Be(8);
            attributes->cbSizeInstance.Should().Be(8);
            attributes->cbSizeVft.Should().Be(24);
            attributes->wMajorVerNum.Should().Be(0);
            attributes->wMinorVerNum.Should().Be(0);
            attributes->typekind.Should().Be(TypeKind.Interface);
            attributes->wTypeFlags.Should().Be(TypeFlags.Hidden);
            attributes->idldescType.Flags.Should().Be(IdlFlag.None);
            attributes->guid.Should().Be(new Guid("{00000000-0000-0000-c000-000000000046}"));
            attributes->lcid.Should().Be((LocaleId)0);
            attributes->memidConstructor.Should().Be(MemberId.Nil);
            attributes->memidDestructor.Should().Be(MemberId.Nil);
            attributes->tdescAlias.vt.Should().Be(VariantType.Empty);

            typeInfo.ReleaseTypeAttr(attributes);
        }

        [Fact]
        public unsafe void GetFunctionDescriptionsForIUnknown()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("IUnknown");

            // From OleView:
            //
            // [restricted]
            // HRESULT _stdcall QueryInterface(
            //                [in] GUID* riid, 
            //                [out] void** ppvObj);

            HResult result = typeInfo.GetFuncDesc(0, out FUNCDESC* description);
            result.Should().Be(HResult.S_OK);

            description->cParams.Should().Be(2);
            description->cParamsOpt.Should().Be(0);
            description->cScodes.Should().Be(0);
            description->callconv.Should().Be(CallConvention.StdCall);
            description->funckind.Should().Be(FunctionKind.PureVirtual);
            description->invkind.Should().Be(InvokeKind.Function);
            description->wFuncFlags.Should().Be(FunctionFlags.Restricted);

            // Return type
            description->elemdescFunc.tdesc.vt.Should().Be(VariantType.HResult);

            // First arg GUID*
            ELEMDESC* element = description->lprgelemdescParam;

            element->tdesc.vt.Should().Be(VariantType.Pointer);
            element->Union.paramdesc.wParamFlags.Should().Be(ParameterFlags.In);
            element->tdesc.Union.lptdesc->vt.Should().Be(VariantType.UserDefined);
            RefTypeHandle handle = element->tdesc.Union.lptdesc->Union.hreftype;

            result = typeInfo.GetRefTypeInfo(handle, out ITypeInfo userDefined);
            result.Should().Be(HResult.S_OK);
            userDefined.GetMemberName(MemberId.Nil).Should().Be("GUID");

            // Second arg void**
            element++;
            element->tdesc.vt.Should().Be(VariantType.Pointer);
            element->Union.paramdesc.wParamFlags.Should().Be(ParameterFlags.Out);
            element->tdesc.Union.lptdesc->vt.Should().Be(VariantType.Pointer);
            element->tdesc.Union.lptdesc->Union.lptdesc->vt.Should().Be(VariantType.Void);

            typeInfo.ReleaseFuncDesc(description);
        }

        [Fact]
        public unsafe void GetFunctionNamesForIUnknown()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("IUnknown");

            ICollection<string> names = typeInfo.GetFunctionNames(0);
            names.Should().BeEquivalentTo("QueryInterface", "riid", "ppvObj");

            names = typeInfo.GetFunctionNames(1);
            names.Should().BeEquivalentTo("AddRef");

            names = typeInfo.GetFunctionNames(2);
            names.Should().BeEquivalentTo("Release");
        }

        [Fact]
        public unsafe void GetTypeAttributesForGUID()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("GUID");

            HResult result = typeInfo.GetTypeAttr(out TYPEATTR* attributes);
            result.Should().Be(HResult.S_OK);
            Assert.True(attributes != null);

            attributes->cFuncs.Should().Be(0);
            attributes->cImplTypes.Should().Be(0);
            attributes->cVars.Should().Be(4);
            attributes->cbAlignment.Should().Be(4);
            attributes->cbSizeInstance.Should().Be(16);
            attributes->cbSizeVft.Should().Be(0);
            attributes->wMajorVerNum.Should().Be(0);
            attributes->wMinorVerNum.Should().Be(0);
            attributes->typekind.Should().Be(TypeKind.Record);
            attributes->wTypeFlags.Should().Be(TypeFlags.Hidden);
            attributes->idldescType.Flags.Should().Be(IdlFlag.None);
            attributes->guid.Should().Be(Guid.Empty);
            attributes->lcid.Should().Be((LocaleId)0);
            attributes->memidConstructor.Should().Be(MemberId.Nil);
            attributes->memidDestructor.Should().Be(MemberId.Nil);
            attributes->tdescAlias.vt.Should().Be(VariantType.Empty);

            typeInfo.ReleaseTypeAttr(attributes);
        }

        [Fact]
        public unsafe void GetVariableDescriptionsForGUID()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("GUID");

            HResult result = typeInfo.GetVarDesc(0, out VARDESC* description);
            result.Should().Be(HResult.S_OK);

            // uint
            description->varkind.Should().Be(VariableKind.PerInstance);
            description->wVarFlags.Should().Be((VariableFlags)0);
            description->elemdescVar.tdesc.vt.Should().Be(VariantType.UInt32);

            typeInfo.ReleaseVarDesc(description);

            result = typeInfo.GetVarDesc(1, out description);
            result.Should().Be(HResult.S_OK);

            // ushort
            description->varkind.Should().Be(VariableKind.PerInstance);
            description->wVarFlags.Should().Be((VariableFlags)0);
            description->elemdescVar.tdesc.vt.Should().Be(VariantType.UInt16);

            typeInfo.ReleaseVarDesc(description);

            result = typeInfo.GetVarDesc(2, out description);
            result.Should().Be(HResult.S_OK);

            // ushort
            description->varkind.Should().Be(VariableKind.PerInstance);
            description->wVarFlags.Should().Be((VariableFlags)0);
            description->elemdescVar.tdesc.vt.Should().Be(VariantType.UInt16);

            typeInfo.ReleaseVarDesc(description);

            result = typeInfo.GetVarDesc(3, out description);
            result.Should().Be(HResult.S_OK);

            // Fixed 8 byte array
            description->varkind.Should().Be(VariableKind.PerInstance);
            description->wVarFlags.Should().Be((VariableFlags)0);
            description->elemdescVar.tdesc.vt.Should().Be(VariantType.CArray);
            description->elemdescVar.tdesc.Union.llpadesc->cDims.Should().Be(1);
            description->elemdescVar.tdesc.Union.llpadesc->rgbounds[0].Count.Should().Be(8);
            description->elemdescVar.tdesc.Union.llpadesc->tdescElem.vt.Should().Be(VariantType.UnsignedByte);

            typeInfo.ReleaseVarDesc(description);
        }

        [Fact]
        public unsafe void GetVariableNamesForGUID()
        {
            TypeLibTests.LoadStdOle2(out ITypeLib typeLib);
            ITypeInfo typeInfo = typeLib.GetTypeInfoByName("GUID");

            typeInfo.GetVariableName(0).Should().Be("Data1");
            typeInfo.GetVariableName(1).Should().Be("Data2");
            typeInfo.GetVariableName(2).Should().Be("Data3");
            typeInfo.GetVariableName(3).Should().Be("Data4");
        }
    }
}
