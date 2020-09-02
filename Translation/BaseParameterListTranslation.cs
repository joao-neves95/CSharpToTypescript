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
    public abstract class BaseParameterListTranslation : CSharpSyntaxTranslation
    {
        public new BaseParameterListSyntax Syntax
        {
            get { return (BaseParameterListSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public BaseParameterListTranslation() { }
        public BaseParameterListTranslation(BaseParameterListSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Parameters = syntax.Parameters.Get<ParameterSyntax, ParameterTranslation>( this );
        }

        public SeparatedSyntaxListTranslation<ParameterSyntax, ParameterTranslation> Parameters { get; set; }

        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
