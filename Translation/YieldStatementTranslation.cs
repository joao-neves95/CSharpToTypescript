/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using RoslynTypeScript.Constants;

namespace RoslynTypeScript.Translation
{
    public class YieldStatementTranslation : StatementTranslation
    {
        public new YieldStatementSyntax Syntax
        {
            get { return (YieldStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public YieldStatementTranslation() { }
        public YieldStatementTranslation(YieldStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
        }

        public override void ApplyPatch()
        {
            if (Syntax.IsKind( SyntaxKind.YieldReturnStatement ))
            {
                var method = this.GetAncestor<MethodDeclarationTranslation>();
                if (method != null)
                {
                    method.IsYieldReturn = true;
                }
            }

            base.ApplyPatch();
        }

        public ExpressionTranslation Expression { get; set; }

        protected override string InnerTranslate()
        {
            if (Syntax.IsKind( SyntaxKind.YieldReturnStatement ))
            {
                var expr = Expression.Translate();

                return $@"{TC.YieldResultName}.push({expr});
                    /*{Syntax.ToString()}*/";
            }

            return Syntax.ToString();
        }
    }
}
