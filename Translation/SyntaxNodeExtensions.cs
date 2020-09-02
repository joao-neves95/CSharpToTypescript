/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public static class SyntaxNodeExtensions
    {
        public static CompilationUnitTranslation GetCompilationUnit(this CompilationUnitSyntax syntax)
        {
            return new CompilationUnitTranslation( syntax, null );
        }

        public static T Get<T>(this SyntaxNode syntaxNode, SyntaxTranslation parent) where T : SyntaxTranslation
        {
            if (syntaxNode == null)
            {
                return null;
            }

            var node = TF.Get<T>( syntaxNode, parent );
            return node;
        }

        public static SyntaxTokenListTranslation Get(this SyntaxTokenList list, SyntaxTranslation parent)
        {
            var newList = new SyntaxTokenListTranslation( list, parent );
            return newList;
        }

        public static SyntaxListTranslation<T, TS> Get<T, TS>(this SyntaxList<T> syntaxList, SyntaxTranslation parent) where T : SyntaxNode where TS : SyntaxTranslation
        {
            var newList = new SyntaxListTranslation<T, TS>( syntaxList, parent );
            return newList;
        }

        public static SeparatedSyntaxListTranslation<T, TS> Get<T, TS>(this SeparatedSyntaxList<T> syntaxList, SyntaxTranslation parent) where T : SyntaxNode where TS : SyntaxTranslation
        {
            var newList = new SeparatedSyntaxListTranslation<T, TS>( syntaxList, parent );
            return newList;
        }

        public static TokenTranslation Get(this SyntaxToken token, SyntaxTranslation parent)
        {
            var newToken = new TokenTranslation( token, parent );
            return newToken;
        }
    }
}
