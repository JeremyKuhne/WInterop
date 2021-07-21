
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WInterop.SourceGenerators;

namespace WInterop.SourceGenerators
{
    [Generator]
    public class InternalsAccessGenerator : ISourceGenerator
    {
        private const string AttributeSource = @"
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AccessInternalsAttribute : Attribute
    {
        public Type Type { get; }

        public AccessInternalsAttribute(Type type)
        {
            Type = type;
        }
    }
";

        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = (SyntaxReceiver)context.SyntaxContextReceiver!;
            foreach (TypeInfo typeInfo in receiver.Types)
            {

            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization((pi) => pi.AddSource("InternalsAccess_MainAttributes__", AttributeSource));

#if DEBUG
            if (!Debugger.IsAttached)
            {
               //  Debugger.Launch();
            }
#endif

            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        private class SyntaxReceiver : ISyntaxContextReceiver
        {
            public HashSet<TypeInfo> Types = new();

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
                        var semanticModel = context.SemanticModel.Compilation.GetSemanticModel(context.SemanticModel.SyntaxTree, ignoreAccessibility: true);
                        var attributeType = semanticModel.GetTypeInfo(attribute).Type;
                        if (attributeType is not null
                            && attributeType.ContainingNamespace.IsGlobalNamespace
                            && attributeType.Name is "AccessInternalsAttribute")
                        {
                            var targetType = semanticModel.GetTypeInfo(((TypeOfExpressionSyntax)attribute.ArgumentList!.Arguments[0].Expression).Type);
                            Types.Add(targetType);

                            //var symbol = context.SemanticModel.GetSymbolInfo(((TypeOfExpressionSyntax)attribute.ArgumentList!.Arguments[0].Expression).Type);
                            //if (symbol.Symbol!.Kind == SymbolKind.NamedType)
                            //{
                            //    // A class or struct
                            //    Symbols.Add(symbol);
                            //}
                            //else
                            //{
                            //}
                        }
                    }
                }
            }
        }
    }
}
