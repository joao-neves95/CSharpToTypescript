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
    public class PostfixUnaryExpressionTranslation : ExpressionTranslation
    {
        public new PostfixUnaryExpressionSyntax Syntax
        {
            get { return (PostfixUnaryExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public PostfixUnaryExpressionTranslation() { }
        public PostfixUnaryExpressionTranslation(PostfixUnaryExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Operand = syntax.Operand.Get<ExpressionTranslation>( this );
        }

        public ExpressionTranslation Operand { get; set; }

        protected override string InnerTranslate()
        {
            return $"{Operand.Translate()}{Syntax.OperatorToken.ToString()}";
        }
    }
}
