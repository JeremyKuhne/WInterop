// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace WInterop.Console;

public class ConsoleWriter : TextWriter
{
    private readonly Lazy<StringBuilder> _builder = new(() => new StringBuilder(1024));
    private readonly Encoder _encoder;
    private readonly Encoding _encoding;
    private readonly ConsoleStream _stream;
    private readonly object _lock = new();
    private readonly bool _autoFlush;

    private byte[]? _buffer;
    private int _position;

    private ConsoleWriter(ConsoleStream stream, Encoding encoding, bool autoFlush = true)
    {
        _stream = stream;
        _encoding = encoding;
        _encoder = encoding.GetEncoder();
        _buffer = ArrayPool<byte>.Shared.Rent(1024);
        _autoFlush = autoFlush;
    }

    public override Encoding Encoding => _encoding;

    /// <summary>
    ///  Creates a <see cref="TextWriter"/> for the console's standard output or error.
    /// </summary>
    /// <param name="output">True for console stardard output, otherwise standard error.</param>
    public static TextWriter Create(bool output = true, bool autoFlush = true)
    {
        return new ConsoleWriter(
            new ConsoleStream(output ? StandardHandleType.Output : StandardHandleType.Error),
            System.Console.OutputEncoding,
            autoFlush);
    }

    public unsafe override void Write(char value) => InternalWrite(value);
    public unsafe override void WriteLine(char value) => InternalWrite(value, newLine: true);
    public override void Write(int value) => InternalWrite(value);
    public override void WriteLine(int value) => InternalWrite(value, newLine: true);
    public override void Write(uint value) => InternalWrite(value);
    public override void WriteLine(uint value) => InternalWrite(value, newLine: true);
    public override void Write(long value) => InternalWrite(value);
    public override void WriteLine(long value) => InternalWrite(value, newLine: true);
    public override void Write(ulong value) => InternalWrite(value);
    public override void WriteLine(ulong value) => InternalWrite(value, newLine: true);
    public override void Write(float value) => InternalWrite(value);
    public override void WriteLine(float value) => InternalWrite(value, newLine: true);
    public override void Write(double value) => InternalWrite(value);
    public override void WriteLine(double value) => InternalWrite(value, newLine: true);
    public override void Write(decimal value) => InternalWrite(value);
    public override void WriteLine(decimal value) => InternalWrite(value, newLine: true);
    public override void Write(char[]? buffer) => InternalWrite(buffer);
    public override void WriteLine(char[]? buffer) => InternalWrite(buffer, newLine: true);
    public override void Write(char[] buffer, int index, int count)
        => InternalWrite(buffer.AsSpan(index, count));
    public override void WriteLine(char[] buffer, int index, int count)
        => InternalWrite(buffer.AsSpan(index, count), newLine: true);
    public override void Write(string? value) => InternalWrite(value.AsSpan());
    public override void WriteLine(string? value) => InternalWrite(value.AsSpan(), newLine: true);
    public override void Write(string? format, object? arg0) => InternalWrite(format, arg0: arg0);
    public override void WriteLine(string? format, object? arg0) => InternalWrite(format, arg0: arg0, newLine: true);
    public override void Write(string? format, object? arg0, object? arg1)
        => InternalWrite(format, arg0: arg0, arg1: arg1);
    public override void WriteLine(string? format, object? arg0, object? arg1)
        => InternalWrite(format, arg0: arg0, arg1: arg1, newLine: true);
    public override void Write(string? format, object? arg0, object? arg1, object? arg2)
        => InternalWrite(format, arg0: arg0, arg1: arg1, arg2: arg2);
    public override void WriteLine(string? format, object? arg0, object? arg1, object? arg2)
        => InternalWrite(format, arg0: arg0, arg1: arg1, arg2: arg2, newLine: true);
    public override void Write(string format, params object?[] arg) => InternalWrite(format, arg: arg);
    public override void WriteLine(string format, params object?[] arg) => InternalWrite(format, arg: arg, newLine: true);

    private unsafe void InternalWrite(char value, bool newLine = false)
    {
        ReadOnlySpan<char> span = new(&value, 1);
        lock (_lock)
        {
            Encode(span, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(int value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(uint value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(long value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(ulong value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(float value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(double value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void InternalWrite(decimal value, bool newLine = false)
    {
        // StringBuilder is optimized in .NET Core to use the new span TryFormat().
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.Append(value);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private void InternalWrite(ReadOnlySpan<char> span, bool newLine = false)
    {
        lock (_lock)
        {
            Encode(span, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private void InternalWrite(string? format, object? arg0, bool newLine = false)
    {
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.AppendFormat(format ?? string.Empty, arg0);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private void InternalWrite(string? format, object? arg0, object? arg1, bool newLine = false)
    {
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.AppendFormat(format ?? string.Empty, arg0, arg1);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private void InternalWrite(string? format, object? arg0, object? arg1, object? arg2, bool newLine = false)
    {
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.AppendFormat(format ?? string.Empty, arg0, arg1, arg2);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private void InternalWrite(string format, object?[] arg, bool newLine = false)
    {
        StringBuilder builder = _builder.Value;
        lock (_lock)
        {
            builder.AppendFormat(format, arg);
            Encode(builder, newLine);
            if (_autoFlush)
                FlushInternal();
        }
    }

    private unsafe void Encode(StringBuilder builder, bool newLine = false)
    {
        char[] buffer = ArrayPool<char>.Shared.Rent(builder.Length);
        builder.CopyTo(0, buffer, 0, builder.Length);
        Encode(buffer.AsSpan(0, builder.Length), newLine);
        builder.Clear();
        ArrayPool<char>.Shared.Return(buffer);
    }

    private unsafe void Encode(ReadOnlySpan<char> span, bool newLine = false)
    {
        if (_buffer is null)
            return;

        EncodeInternal(span);
        if (newLine)
            EncodeInternal(CoreNewLine);

        void EncodeInternal(ReadOnlySpan<char> internalSpan)
        {
            bool completed = false;
            int count = internalSpan.Length;
            int charsUsed = 0;

            fixed (char* c = &MemoryMarshal.GetReference(internalSpan))
            {
                fixed (byte* b = _buffer)
                {
                    while (!completed)
                    {
                        _encoder.Convert(
                            c + charsUsed,
                            count,
                            b + _position,
                            _buffer!.Length - _position,
                            flush: true,
                            out charsUsed,
                            out int bytesUsed,
                            out completed);

                        _position += bytesUsed;
                        if (!completed)
                        {
                            // Out of space, need to flush the buffer
                            FlushInternal();
                            count -= charsUsed;
                        }
                    }
                }
            }
        }
    }

    private void FlushInternal()
    {
        if (_position != 0 && !(_buffer is null))
        {
            _stream.Write(_buffer, 0, _position);
            _position = 0;
        }
    }

    public override void Flush()
    {
        lock (_lock)
        {
            FlushInternal();
        }
    }

    protected override void Dispose(bool disposing)
    {
        byte[]? buffer = Interlocked.Exchange(ref _buffer, null);
        if (buffer != null)
            ArrayPool<byte>.Shared.Return(buffer);
    }
}