/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class ArrayTypeTranslation : TypeTranslation
    {
        public new ArrayTypeSyntax Syntax
        {
            get { return (ArrayTypeSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ArrayTypeTranslation() { }
        public ArrayTypeTranslation(ArrayTypeSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

            RankSpecifiers = syntax.RankSpecifiers.Get<ArrayRankSpecifierSyntax, ArrayRankSpecifierTranslation>( this );
            ElementType = syntax.ElementType.Get<TypeTranslation>( this );
        }

        public SyntaxListTranslation<ArrayRankSpecifierSyntax, ArrayRankSpecifierTranslation> RankSpecifiers { get; set; }
        public TypeTranslation ElementType { get; set; }

        protected override string InnerTranslate()
        {
            string elementTypeStr = ElementType.Translate();

            // in javascript for byte array, we will use 
            if (elementTypeStr == "byte")
            {
                return "Int8Array";
            }

            return $"{elementTypeStr}{RankSpecifiers.Translate()}";
        }
    }
}
