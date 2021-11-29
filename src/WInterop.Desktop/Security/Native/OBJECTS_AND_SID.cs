// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security.Native;

/// <docs>https://docs.microsoft.com/en-us/windows/desktop/api/accctrl/ns-accctrl-_objects_and_sid</docs>
public readonly unsafe struct OBJECTS_AND_SID
{
    public readonly AceTypePresent ObjectsPresent;
    public readonly Guid ObjectTypeGuid;
    public readonly Guid InheritedObjectTypeGuid;
    public readonly SID* pSid;
}