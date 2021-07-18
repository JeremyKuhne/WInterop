// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security.Native
{
    /// <docs>https://docs.microsoft.com/windows/desktop/api/accctrl/ns-accctrl-_trustee_w</docs>
    public unsafe readonly struct TRUSTEE
    {
        // Not used, must be IntPtr.Zero
        public readonly IntPtr pMultipleTrustee;

        // Must be 0 [NO_MULTIPLE_TRUSTEE]
        public readonly int MultipleTrusteeOperation;
        public readonly TrusteeForm TrusteeForm;
        public readonly TrusteeType TrusteeType;
        public readonly void* pstrName;

        public TRUSTEE(SID* sid)
        {
            this = default;
            TrusteeForm = TrusteeForm.Sid;
            pstrName = sid;
        }

        public string? TrusteeName
        {
            get
            {
                char* name = null;

                switch (TrusteeForm)
                {
                    case TrusteeForm.Name:
                        name = (char*)pstrName;
                        break;
                    case TrusteeForm.ObjectsAndName:
                        name = ((OBJECTS_AND_NAME*)pstrName)->ptstrName;
                        break;
                }

                return name == null ? null : new string(name);
            }
        }

        public ObjectType ObjectType => TrusteeForm == TrusteeForm.ObjectsAndName
            ? ((OBJECTS_AND_NAME*)pstrName)->ObjectType : ObjectType.Unknown;
    }
}