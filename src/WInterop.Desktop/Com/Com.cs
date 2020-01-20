// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com
{
    public static partial class Com
    {
        public unsafe static object CreateStorage(
            string path,
            Guid riid,
            StorageMode mode = StorageMode.ReadWrite | StorageMode.Create | StorageMode.ShareExclusive,
            StorageFormat format = StorageFormat.DocFile)
        {
            STGOPTIONS options = new STGOPTIONS
            {
                usVersion = 1,

                // If possible, we want the larger 4096 sector size
                ulSectorSize = (mode & StorageMode.Simple) != 0  ? 512u : 4096
            };

            Imports.StgCreateStorageEx(
                path,
                mode,
                format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                ref riid,
                out object created).ThrowIfFailed(path);

            return created;
        }

        public unsafe static object OpenStorage(
            string path,
            Guid riid,
            StorageMode mode = StorageMode.ReadWrite | StorageMode.ShareExclusive,
            StorageFormat format = StorageFormat.Any)
        {
            STGOPTIONS options = new STGOPTIONS
            {
                // Must have version set before using
                usVersion = 1
            };

            Imports.StgOpenStorageEx(
                path,
                mode,
                format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                ref riid,
                out object created).ThrowIfFailed(path);

            return created;
        }

        public static bool IsStorageFile(string path)
        {
            return Imports.StgIsStorageFile(path) == HResult.S_OK;
        }

        public unsafe static ITypeInfo GetTypeInfoByName(this ITypeLib typeLib, string typeName)
        {
            // The find method is case insensitive, and will overwrite the input buffer
            // with the actual found casing.

            char* nameBuffer = stackalloc char[typeName.Length + 1];
            Span<char> nameSpan = new Span<char>(nameBuffer, typeName.Length);
            typeName.AsSpan().CopyTo(nameSpan);
            nameBuffer[typeName.Length] = '\0';

            IntPtr* typeInfos = stackalloc IntPtr[1];
            MemberId* memberIds = stackalloc MemberId[1];
            ushort found = 1;
            typeLib.FindName(
                nameBuffer,
                lHashVal: 0,
                typeInfos,
                memberIds,
                &found).ThrowIfFailed(typeName);

            return (ITypeInfo)Marshal.GetTypedObjectForIUnknown(typeInfos[0], typeof(ITypeInfo));
        }

        public unsafe static ICollection<string?> GetFunctionNames(this ITypeInfo typeInfo, uint functionIndex)
        {
            typeInfo.GetFuncDesc(functionIndex, out FUNCDESC* description)
                .ThrowIfFailed($"Failed to get description for function index: {functionIndex}");

            uint count = (uint)description->cParams + 1;
            MemberId id = description->memid;
            typeInfo.ReleaseFuncDesc(description);
            return GetMemberNames(typeInfo, id, count);
        }

        public unsafe static ICollection<string?> GetMemberNames(this ITypeInfo typeInfo, MemberId id, uint count)
        {
            BasicString* names = stackalloc BasicString[(int)(count)];
            typeInfo.GetNames(id, names, count, out count)
                .ThrowIfFailed($"Failed to get names for member id: {id.Value}");

            var results = new List<string?>((int)count);
            for (int i = 0; i < count; i++)
            {
                results.Add(names[i].ToStringAndFree());
            }

            return results;
        }

        public unsafe static string? GetMemberName(this ITypeInfo typeInfo, MemberId id)
        {
            BasicString name;
            typeInfo.GetDocumentation(id, &name)
                .ThrowIfFailed($"Failed to get documention for member id: {id.Value}");
            return name.ToStringAndFree();
        }

        public unsafe static string? GetVariableName(this ITypeInfo typeInfo, uint variableIndex)
        {
            typeInfo.GetVarDesc(variableIndex, out VARDESC* description)
                .ThrowIfFailed($"Failed to get description for variable index: {variableIndex}");

            MemberId id = description->memid;
            typeInfo.ReleaseVarDesc(description);

            return typeInfo.GetMemberName(id);
        }
    }
}
