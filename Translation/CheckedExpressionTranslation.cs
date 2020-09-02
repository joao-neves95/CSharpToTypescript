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
    public class CheckedExpressionTranslation : ExpressionTranslation
    {
        public new CheckedExpressionSyntax Syntax
        {
            get { return (CheckedExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public CheckedExpressionTranslation() { }
        public CheckedExpressionTranslation(CheckedExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
        }

        public ExpressionTranslation Expression { get; set; }

        protected override string InnerTranslate()
        {
            return $"({Expression.Translate()})";
        }
    }
}
