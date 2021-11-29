// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Registry.Native;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Registry;

public static partial class Registry
{
    public static unsafe string QueryKeyName(RegistryKeyHandle key)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            NTStatus status;
            while ((status = Imports.NtQueryKey(key, KeyInformationClass.NameInformation, buffer.VoidPointer, (uint)buffer.ByteCapacity, out uint resultLength))
                == NTStatus.STATUS_BUFFER_TOO_SMALL || status == NTStatus.STATUS_BUFFER_OVERFLOW)
            {
                buffer.EnsureByteCapacity(resultLength);
            }

            status.ThrowIfFailed();

            return ((KEY_NAME_INFORMATION*)buffer.VoidPointer)->Name.CreateString();
        });
    }

    /// <summary>
    ///  Open the specified subkey.
    /// </summary>
    public static RegistryKeyHandle OpenKey(
        RegistryKeyHandle key,
        string subKeyName,
        RegistryAccessRights rights = RegistryAccessRights.Read)
    {
        Imports.RegOpenKeyExW(key, subKeyName, 0, rights, out RegistryKeyHandle subKey).ThrowIfFailed();

        return subKey;
    }

    /// <summary>
    ///  Returns true if the given value exists.
    /// </summary>
    public static unsafe bool QueryValueExists(RegistryKeyHandle key, string valueName)
    {
        WindowsError result = Imports.RegQueryValueExW(key, valueName, null, null, null, null);
        return result switch
        {
            WindowsError.ERROR_SUCCESS => true,
            WindowsError.ERROR_FILE_NOT_FOUND => false,
            _ => throw result.GetException(),
        };
    }

    /// <summary>
    ///  Returns the type of the given value, or REG_NONE if it doesn't exist.
    /// </summary>
    public static unsafe RegistryValueType QueryValueType(RegistryKeyHandle key, string valueName)
    {
        RegistryValueType valueType = default;
        WindowsError result = Imports.RegQueryValueExW(key, valueName, null, &valueType, null, null);
        return result switch
        {
            WindowsError.ERROR_SUCCESS => valueType,
            WindowsError.ERROR_FILE_NOT_FOUND => RegistryValueType.None,
            _ => throw result.GetException(),
        };
    }

    public static unsafe object? QueryValue(RegistryKeyHandle key, string valueName)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            RegistryValueType valueType = default;

            WindowsError result;
            uint byteCapacity = (uint)buffer.ByteCapacity;
            while ((result = Imports.RegQueryValueExW(
                key,
                valueName,
                null,
                &valueType,
                buffer.VoidPointer,
                &byteCapacity)) != WindowsError.ERROR_SUCCESS)
            {
                switch (result)
                {
                    case WindowsError.ERROR_MORE_DATA:
                            // According to the docs, the byteCapacity given back will
                            // not be correct for HKEY_PERFORMANCE_DATA
                            byteCapacity = byteCapacity > buffer.ByteCapacity ? byteCapacity : checked(byteCapacity + 256);
                        buffer.EnsureByteCapacity(byteCapacity);
                        break;
                    case WindowsError.ERROR_FILE_NOT_FOUND:
                        return null;
                    default:
                        throw result.GetException();
                }
            }

            return ReadValue(buffer.VoidPointer, byteCapacity, valueType);
        });
    }

    /// <summary>
    ///  Gets the key's value names directly from NtEnumerateValueKey. This is slightly faster and uses less
    ///  memory, but only works for keys in the local machine registry and does not work with performance keys
    ///  (such as HKEY_PERFORMANCE_DATA).
    ///
    ///  This also doesn't give the same results for "special" (HKCR) keys that are normally redirected to user
    ///  specific settings by RegEnumValue.
    /// </summary>
    /// <param name="filterTo">
    ///  Only return names that have the given type, or REG_NONE for all types.
    /// </param>
    /// <remarks>
    ///  RegEnumValue doesn't map directly to NtEnumerateValueKey and requires allocating a temporary buffer for
    ///  *each* invocation- making the direct call here avoids this extra buffer.
    /// </remarks>
    public static unsafe IEnumerable<string> GetValueNamesDirect(RegistryKeyHandle key, RegistryValueType filterTo = RegistryValueType.None)
    {
        List<string> names = new();

        BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            NTStatus status;
            while ((status = Imports.NtEnumerateValueKey(
                key,
                (uint)names.Count,
                KeyValueInformationClass.BasicInformation,
                buffer.VoidPointer,
                checked((uint)buffer.ByteCapacity),
                out uint resultLength)) != NTStatus.STATUS_NO_MORE_ENTRIES)
            {
                switch (status)
                {
                    case NTStatus.STATUS_SUCCESS:
                        KEY_VALUE_BASIC_INFORMATION* info = (KEY_VALUE_BASIC_INFORMATION*)buffer.VoidPointer;
                        if (filterTo == RegistryValueType.None || info->Type == filterTo)
                            names.Add(info->Name.CreateString());
                        break;
                    case NTStatus.STATUS_BUFFER_OVERFLOW:
                    case NTStatus.STATUS_BUFFER_TOO_SMALL:
                        buffer.EnsureByteCapacity(resultLength);
                        break;
                    default:
                        throw status.GetException();
                }
            }
        });

        return names;
    }

    /// <summary>
    ///  Gets the key's value data directly from NtEnumerateValueKey. This is slightly faster and uses less memory,
    ///  but only works for keys in the local machine registry and does not work with performance keys
    ///  (such as HKEY_PERFORMANCE_DATA).
    ///
    ///  This also doesn't give the same results for "special" (HKCR) keys that are normally redirected to user
    ///  specific settings by RegEnumValue.
    /// </summary>
    /// <param name="filterTo">Only return data of the given type, or REG_NONE for all types.</param>
    /// <remarks>
    ///  RegEnumValue doesn't map directly to NtEnumerateValueKey and requires allocating a temporary buffer for
    ///  *each* invocation- making the direct call here avoids this extra buffer.
    /// </remarks>
    public static unsafe IEnumerable<object> GetValueDataDirect(RegistryKeyHandle key, RegistryValueType filterTo = RegistryValueType.None)
    {
        List<object> data = new();

        BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            NTStatus status;
            while ((status = Imports.NtEnumerateValueKey(
                key,
                (uint)data.Count,
                KeyValueInformationClass.PartialInformation,
                buffer.VoidPointer,
                checked((uint)buffer.ByteCapacity),
                out uint resultLength)) != NTStatus.STATUS_NO_MORE_ENTRIES)
            {
                switch (status)
                {
                    case NTStatus.STATUS_SUCCESS:
                        KEY_VALUE_PARTIAL_INFORMATION* info = (KEY_VALUE_PARTIAL_INFORMATION*)buffer.VoidPointer;
                        if (filterTo == RegistryValueType.None || (*info).Type == filterTo)
                            data.Add(ReadValue(&(*info).Data, (*info).DataLength, (*info).Type));
                        break;
                    case NTStatus.STATUS_BUFFER_OVERFLOW:
                    case NTStatus.STATUS_BUFFER_TOO_SMALL:
                        buffer.EnsureByteCapacity(resultLength);
                        break;
                    default:
                        throw status.GetException();
                }
            }
        });

        return data;
    }

    /// <summary>
    ///  Gets all value names for the given registry key.
    /// </summary>
    public static unsafe IEnumerable<string> GetValueNames(RegistryKeyHandle key)
    {
        List<string> names = new();

        BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
                // Ensure we have enough space to hold the perf key values
                buffer.EnsureCharCapacity(10);
            uint bufferSize = buffer.CharCapacity;

            WindowsError result;
            while ((result = Imports.RegEnumValueW(key, (uint)names.Count, buffer.VoidPointer, ref bufferSize, null, null, null, null))
                != WindowsError.ERROR_NO_MORE_ITEMS)
            {
                switch (result)
                {
                    case WindowsError.ERROR_SUCCESS:
                        buffer.Length = bufferSize;
                        names.Add(buffer.ToString());
                        break;
                    case WindowsError.ERROR_MORE_DATA:
                        if (key.IsPerfKey)
                        {
                                // Perf keys always report back ERROR_MORE_DATA,
                                // and also does not report the size of the string.
                                buffer.SetLengthToFirstNull();
                            names.Add(buffer.ToString());
                            bufferSize = buffer.CharCapacity;
                        }
                        else
                        {
                                // For some reason the name size isn't reported back,
                                // even though it is known. Why would they not do this?
                                buffer.EnsureCharCapacity(bufferSize + 100);
                            bufferSize = buffer.CharCapacity;
                        }

                        break;
                    default:
                        throw result.GetException();
                }
            }
        });

        return names;
    }

    private static unsafe object ReadValue(void* buffer, uint byteCount, RegistryValueType valueType)
    {
        switch (valueType)
        {
            case RegistryValueType.String:
            case RegistryValueType.ExpandString:
            case RegistryValueType.SymbolicLink:
                // Size includes the null
                return new string((char*)buffer, 0, (int)(byteCount / sizeof(char)) - 1);
            case RegistryValueType.MultiString:
                return Strings.SplitNullTerminatedStringList((IntPtr)buffer);
            case RegistryValueType.Unsigned32BitInteger:
                return *(uint*)buffer;
            case RegistryValueType.Unsigned32BitIntegerBigEndian:
                byte* b = (byte*)buffer;
                return (*b << 24) | (*(b + 1) << 16) | (*(b + 2) << 8) | (*(b + 3));
            case RegistryValueType.Unsigned64BitInteger:
                return *(ulong*)buffer;
            case RegistryValueType.Binary:
            // The next three aren't defined yet, so we'll just return them as binary blobs
            case RegistryValueType.ResourceList:                    // CM_RESOURCE_LIST
            case RegistryValueType.FullResourceDescriptor:          // CM_FULL_RESOURCE_DESCRIPTOR
            case RegistryValueType.ResourceRequirementsList:        // CM_RESOURCE_LIST??
                byte[] outBuffer = new byte[byteCount];
                fixed (void* outPointer = &outBuffer[0])
                {
                    Buffer.MemoryCopy(buffer, outPointer, byteCount, byteCount);
                }

                return outBuffer;
            default:
                throw new NotSupportedException($"No support for {valueType} value types.");
        }
    }
}