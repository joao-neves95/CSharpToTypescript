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
    public class ArgumentListTranslation : CSharpSyntaxTranslation
    {
        public new ArgumentListSyntax Syntax
        {
            get { return (ArgumentListSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SeparatedSyntaxListTranslation<ArgumentSyntax, ArgumentTranslation> Arguments { get; set; }
        public ArgumentListTranslation() { }
        public ArgumentListTranslation(ArgumentListSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Arguments = syntax.Arguments.Get<ArgumentSyntax, ArgumentTranslation>( this );
        }


        protected override string InnerTranslate()
        {
            return string.Format( "({0})", Arguments.Translate() );
        }
    }
}
