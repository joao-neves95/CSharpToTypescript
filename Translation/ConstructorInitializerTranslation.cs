/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
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
