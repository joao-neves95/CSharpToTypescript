/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace CSharpToTypescript
{
    public class OptionalInterfaceProperties
    {
        public static CSharpSyntaxNode AddOptional(CSharpSyntaxNode syntaxNode)
        {
            var interfaces = syntaxNode.DescendantNodesAndSelf().Where( f => f is InterfaceDeclarationSyntax );

            var properties = interfaces.SelectMany( f => f.DescendantNodes().Where( c => c is PropertyDeclarationSyntax ) );
            var methods = interfaces.SelectMany( f => f.DescendantNodes().Where( c => c is MethodDeclarationSyntax ) );

            return syntaxNode.ReplaceNodes( properties.Concat( methods ), (node, node2) =>
               {
                   var property = node as PropertyDeclarationSyntax;
                   var method = node as MethodDeclarationSyntax;
                   if (property != null)
                   {
                       return property.WithIdentifier( SyntaxFactory.Identifier( property.Identifier.ValueText + "?" ) );
                   }

                   return method.WithIdentifier( SyntaxFactory.Identifier( method.Identifier.ValueText + "?" ) );
               } );

        }
    }
}
