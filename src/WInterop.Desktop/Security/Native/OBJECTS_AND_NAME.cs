// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Security.Native
{
    /// <summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/accctrl/ns-accctrl-_objects_and_name_w"/>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly unsafe struct OBJECTS_AND_NAME
    {
        public readonly AceTypePresent ObjectsPresent;
        public readonly ObjectType ObjectType;
        public readonly char* ObjectTypeName;
        public readonly char* InheritedObjectTypeName;
        public readonly char* ptstrName;
    }
}