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

namespace CSharpToTypescript
{
    class MemberToCamelCaseRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax propertySyntax)
        {
            var leadingTrivia = propertySyntax.Identifier.LeadingTrivia;
            var trailingTriva = propertySyntax.Identifier.TrailingTrivia;
            return propertySyntax.ReplaceToken( propertySyntax.Identifier,
                SyntaxFactory.Identifier( leadingTrivia,
                ToCamelCase( propertySyntax.Identifier.ValueText ), trailingTriva ) );
        }

        private static string ToCamelCase(string name)
        {
            return name.Substring( 0, 1 ).ToLower() + name.Substring( 1 );
        }
    }

    public class MakeMemberCamelCase
    {
        public static CSharpSyntaxNode Make(CSharpSyntaxNode syntaxNode)
        {

            var rewriter = new MemberToCamelCaseRewriter();
            return (CSharpSyntaxNode)rewriter.Visit( syntaxNode );
        }
    }
}
