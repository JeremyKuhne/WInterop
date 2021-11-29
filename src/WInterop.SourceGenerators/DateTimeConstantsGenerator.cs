﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WInterop.SourceGenerators;

[Generator]
public class DateTimeConstantsGenerator : ISourceGenerator
{
    private const string AttributeSource =
@"using System;

[AttributeUsage(AttributeTargets.Assembly)]
internal class DateTimeConstantsAttribute : Attribute
{
    public DateTimeConstantsAttribute(string @namespace)
    {
        Namespace = @namespace;
    }

    public string Namespace { get; }
}";

    public void Execute(GeneratorExecutionContext context)
    {
        var receiver = (SyntaxReceiver)context.SyntaxContextReceiver!;
        foreach (string @namespace in receiver.Namespaces)
        {
            context.AddSource(
                $"__DateTimeConstants.{@namespace}.generated",
                SourceText.From(GenerateConstantsSoruce(@namespace), Encoding.UTF8));
        }

        static string GenerateConstantsSoruce(string @namespace)
        {
            DateTime now = DateTime.Now;

            StringBuilder sb = new(1000);
            sb.IndentAppendLine(0, "namespace ", @namespace);
            sb.AppendLine("{");

            sb.IndentAppendLine(1, "internal static partial class AutoGeneratedConstants");
            sb.IndentAppendLine(1, "{");

            sb.IndentAppendLine(2, "public static partial class Integers");
            sb.IndentAppendLine(2, "{");
            sb.IndentAppendXmlComment(3, "Current year.");
            sb.IndentAppendLine(3, "public const int CurrentYear = ", now.Year.ToString(), ";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current month from 1..12.");
            sb.IndentAppendLine(3, "public const int CurrentMonth = ", now.Month.ToString(), ";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current day of the month from 1..31.");
            sb.IndentAppendLine(3, "public const int CurrentDayOfMonth = ", now.Day.ToString(), ";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current hour from 0..23.");
            sb.IndentAppendLine(3, "public const int CurrentHour = ", now.Hour.ToString(), ";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current minute from 0..59.");
            sb.IndentAppendLine(3, "public const int CurrentMinute = ", now.Minute.ToString(), ";");
            sb.IndentAppendLine(2, "}");
            sb.AppendLine();

            sb.IndentAppendLine(2, "public static partial class Strings");
            sb.IndentAppendLine(2, "{");
            sb.IndentAppendXmlComment(3, "Current year.");
            sb.IndentAppendLine(3, "public const string CurrentYear = \"", now.Year.ToString(), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current month from 1..12.");
            sb.IndentAppendLine(3, "public const string CurrentMonth = \"", now.Month.ToString(), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current two digit month from 01..12.");
            sb.IndentAppendLine(3, "public const string CurrentTwoDigitMonth = \"", now.Month.ToString("00"), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current day of the month from 1..31.");
            sb.IndentAppendLine(3, "public const string CurrentDayOfMonth = \"", now.Day.ToString(), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current two digit day of the month from 01..31.");
            sb.IndentAppendLine(3, "public const string CurrentTwoDigitDayOfMonth = \"", now.Day.ToString("00"), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current hour from 0..23.");
            sb.IndentAppendLine(3, "public const string CurrentHour = \"", now.Hour.ToString(), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current two digit hour from 00..23.");
            sb.IndentAppendLine(3, "public const string CurrentTwoDigitHour = \"", now.Hour.ToString("00"), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current minute from 0..59.");
            sb.IndentAppendLine(3, "public const string CurrentMinute = \"", now.Minute.ToString(), "\";");
            sb.AppendLine();
            sb.IndentAppendXmlComment(3, "Current two digit minute from 00..59.");
            sb.IndentAppendLine(3, "public const string CurrentTwoDigitMinute = \"", now.Minute.ToString("00"), "\";");
            sb.IndentAppendLine(2, "}");
            sb.IndentAppendLine(1, "}");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization((pi) => pi.AddSource("__DateTimeConstants._Attribute.generated", AttributeSource));

#if DEBUG
        if (!Debugger.IsAttached)
        {
            // Debugger.Launch();
        }
#endif

        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    private class SyntaxReceiver : ISyntaxContextReceiver
    {
        public HashSet<string> Namespaces = new();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is not CompilationUnitSyntax compilation)
            {
                return;
            }

            foreach (var attrbuteList in compilation.AttributeLists)
            {
                foreach (var attribute in attrbuteList.Attributes)
                {
                    var semanticModel = context.SemanticModel;
                    var attributeType = semanticModel.GetTypeInfo(attribute).Type;
                    if (attributeType is not null
                        && attributeType.ContainingNamespace.IsGlobalNamespace
                        && attributeType.Name is "DateTimeConstantsAttribute")
                    {
                        Namespaces.Add(((LiteralExpressionSyntax)attribute.ArgumentList!.Arguments[0].Expression).Token.Value!.ToString());
                    }
                }
            }
        }
    }
}
