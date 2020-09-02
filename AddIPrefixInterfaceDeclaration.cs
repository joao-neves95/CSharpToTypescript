/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypescript
{
    public class AddIPrefixInterfaceDeclaration
    {
        class AddIPrefixInterfaceDeclarationRewriter : CSharpSyntaxRewriter
        {
            public override SyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
            {
                var name = node.Identifier.ValueText;
                if (name.StartsWith( "I" ))
                {
                    return base.VisitInterfaceDeclaration( node );
                }

                return node.ReplaceToken( node.Identifier, SyntaxFactory.ParseToken( "I" + name ) );
            }
        }

        public static CSharpSyntaxNode AddIPrefix(CSharpSyntaxNode syntaxNode)
        {
            return (CSharpSyntaxNode)new AddIPrefixInterfaceDeclarationRewriter().Visit( syntaxNode );
        }
    }
}
