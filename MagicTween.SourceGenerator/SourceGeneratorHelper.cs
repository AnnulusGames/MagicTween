using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MagicTween.Generator
{
	internal static class SourceGeneratorHelper
    {
        public static bool HasAttribute(BaseTypeDeclarationSyntax typeSyntax, string attributeName)
        {
            if (typeSyntax.AttributeLists != null)
            {
                foreach (AttributeListSyntax attributeList in typeSyntax.AttributeLists)
                {
                    foreach (AttributeSyntax attribute in attributeList.Attributes)
                    {
                        if (attribute.Name.ToString() == attributeName)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}

