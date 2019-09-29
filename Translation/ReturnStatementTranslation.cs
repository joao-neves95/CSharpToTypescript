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
    public class ReturnStatementTranslation : StatementTranslation
    {
        public new ReturnStatementSyntax Syntax
        {
            get { return (ReturnStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ExpressionTranslation Expression { get; set; }
        public ReturnStatementTranslation() { }
        public ReturnStatementTranslation(ReturnStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
        }

        public override void ApplyPatch()
        {
            base.ApplyPatch();
            Helper.ApplyFunctionBindToCorrectContext( this.Expression );
        }

        protected override string InnerTranslate()
        {
            if (Expression == null)
            {
                return "return";
            }

            var expr = Expression.Translate();

            return $"return {expr};";
        }
    }
}
