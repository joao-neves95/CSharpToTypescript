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
    public abstract class SwitchLabelTranslation : CSharpSyntaxTranslation
    {
        public new SwitchLabelSyntax Syntax
        {
            get { return (SwitchLabelSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SwitchLabelTranslation() { }
        public SwitchLabelTranslation(SwitchLabelSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

        }

        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
