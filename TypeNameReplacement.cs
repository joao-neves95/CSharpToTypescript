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
using CSharpToTypescript.VSIX;

namespace CSharpToTypescript
{
    public class TypeNameReplacement
    {
        public static CSharpSyntaxNode Replace(TypeNameReplacementData[] replacedTypeNameArray, CSharpSyntaxNode syntaxNode)
        {
            var typeNodes = syntaxNode.DescendantNodes()
                .OfType<TypeSyntax>()
                .Where( f => replacedTypeNameArray.Any( r => r.OldTypeName == f.ToString() ) );


            return syntaxNode.ReplaceNodes( typeNodes, (n1, n2) =>
            {
                var name = n1.ToString();
                var newName = replacedTypeNameArray.First( f => f.OldTypeName == name ).NewTypeName;
                var newType = SyntaxFactory.ParseTypeName( newName );

                return newType;
            } );

        }

    }
}
