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
    public class EmptyStatementTranslation : StatementTranslation
    {
        public new EmptyStatementSyntax Syntax
        {
            get { return (EmptyStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public EmptyStatementTranslation() { }
        public EmptyStatementTranslation(EmptyStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

        }

        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
