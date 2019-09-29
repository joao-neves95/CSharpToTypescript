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
    public class ConditionalExpressionTranslation : ExpressionTranslation
    {
        public new ConditionalExpressionSyntax Syntax
        {
            get { return (ConditionalExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ConditionalExpressionTranslation() { }
        public ConditionalExpressionTranslation(ConditionalExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Condition = syntax.Condition.Get<ExpressionTranslation>( this );
            WhenFalse = syntax.WhenFalse.Get<ExpressionTranslation>( this );
            WhenTrue = syntax.WhenTrue.Get<ExpressionTranslation>( this );
        }

        public ExpressionTranslation Condition { get; set; }
        public ExpressionTranslation WhenFalse { get; set; }
        public ExpressionTranslation WhenTrue { get; set; }

        protected override string InnerTranslate()
        {
            return $"{Condition.Translate()} ? {WhenTrue.Translate()} : {WhenFalse.Translate()}";
        }
    }
}
