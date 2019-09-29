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
    public class CastExpressionTranslation : ExpressionTranslation
    {
        public new CastExpressionSyntax Syntax
        {
            get { return (CastExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public CastExpressionTranslation() { }
        public CastExpressionTranslation(CastExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<TypeTranslation>( this );
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
        }

        public TypeTranslation Type { get; set; }

        public ExpressionTranslation Expression { get; set; }

        protected override string InnerTranslate()
        {
            return $"<{Type.Translate()}>{Expression.Translate()}";
        }
    }
}
