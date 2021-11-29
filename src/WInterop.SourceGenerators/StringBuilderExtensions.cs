// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace WInterop.SourceGenerators;

public static class StringBuilderExtensions
{
    public static StringBuilder IndentAppendLine(this StringBuilder builder, int indent, string value)
        => builder.Indent(indent).AppendLine(value);

    public static StringBuilder IndentAppendLine(this StringBuilder builder, int indent, string value1, string value2)
        => builder.Indent(indent).Append(value1).AppendLine(value2);

    public static StringBuilder IndentAppendLine(this StringBuilder builder, int indent, string value1, string value2, string value3)
        => builder.Indent(indent).Append(value1).Append(value2).AppendLine(value3);

    public static StringBuilder IndentAppend(this StringBuilder builder, int indent, string value)
        => builder.Indent(indent).Append(value);

    public static void IndentAppendXmlComment(this StringBuilder builder, int indent, string value)
    {
        builder.IndentAppendLine(indent, @"/// <summary>");
        builder.IndentAppendLine(indent, @"///  ", value);
        builder.IndentAppendLine(indent, @"/// </summary>");
    }

    private static StringBuilder Indent(this StringBuilder builder, int indent) => builder.Append(' ', indent * 4);
}
