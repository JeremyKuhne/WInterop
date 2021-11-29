// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi.Native;

namespace WInterop.Gdi;

public unsafe readonly ref struct MetafileRecord
{
    private readonly ENHMETARECORD* _record;

    public MetafileRecord(ENHMETARECORD* record) => _record = record;

    public MetafileRecordType RecordType => (MetafileRecordType)_record->iType;
}