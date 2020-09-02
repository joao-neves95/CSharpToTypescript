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
    public class BaseExpressionTranslation : ExpressionTranslation
    {
        public new BaseExpressionSyntax Syntax
        {
            get { return (BaseExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public BaseExpressionTranslation() { }
        public BaseExpressionTranslation(BaseExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
        }

        protected override string InnerTranslate()
        {
            return "super";
        }
    }
}
