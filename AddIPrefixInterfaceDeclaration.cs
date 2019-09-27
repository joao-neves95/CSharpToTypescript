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
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToTypescript
{
    public class AddIPrefixInterfaceDeclaration
    {
        class AddIPrefixInterfaceDeclarationRewriter: CSharpSyntaxRewriter
        {
            public override SyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
            {
                var name = node.Identifier.ValueText;
                if (name.StartsWith("I"))
                {
                    return base.VisitInterfaceDeclaration(node);
                }

                return node.ReplaceToken(node.Identifier, SyntaxFactory.ParseToken("I" + name));
            }
        }

        public static CSharpSyntaxNode AddIPrefix(CSharpSyntaxNode syntaxNode)
        {
            return (CSharpSyntaxNode)new AddIPrefixInterfaceDeclarationRewriter().Visit(syntaxNode);
        }
    }
}
