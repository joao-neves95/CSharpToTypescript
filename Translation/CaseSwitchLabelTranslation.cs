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
    public class CaseSwitchLabelTranslation : SwitchLabelTranslation
    {
        public new CaseSwitchLabelSyntax Syntax
        {
            get { return (CaseSwitchLabelSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public CaseSwitchLabelTranslation() { }
        public CaseSwitchLabelTranslation(CaseSwitchLabelSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Value = syntax.Value.Get<ExpressionTranslation>( this );
        }


        public ExpressionTranslation Value { get; set; }

        protected override string InnerTranslate()
        {
            return $"case {Value.Translate()}:";
        }
    }
}
