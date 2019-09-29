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
    public class ParenthesizedExpressionTranslation : ExpressionTranslation
    {
        public new ParenthesizedExpressionSyntax Syntax
        {
            get { return (ParenthesizedExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ParenthesizedExpressionTranslation() { }
        public ParenthesizedExpressionTranslation(ParenthesizedExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
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
