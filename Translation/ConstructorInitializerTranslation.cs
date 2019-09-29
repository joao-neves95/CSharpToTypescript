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
    public class ConstructorInitializerTranslation : CSharpSyntaxTranslation
    {
        public new ConstructorInitializerSyntax Syntax
        {
            get { return (ConstructorInitializerSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ConstructorInitializerTranslation() { }
        public ConstructorInitializerTranslation(ConstructorInitializerSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

            ThisOrBaseKeyword = syntax.ThisOrBaseKeyword.Get( this );
            ArgumentList = syntax.ArgumentList.Get<ArgumentListTranslation>( this );
        }

        public TokenTranslation ThisOrBaseKeyword { get; set; }
        public ArgumentListTranslation ArgumentList { get; set; }

        protected override string InnerTranslate()
        {
            string thisOrBase = Syntax.ThisOrBaseKeyword.ToString() == "this" ? "this" : "super";
            return $"{thisOrBase}{ArgumentList.Translate()};";
        }
    }
}
