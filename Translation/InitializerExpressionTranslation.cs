/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class InitializerExpressionTranslation : ExpressionTranslation
    {
        public new InitializerExpressionSyntax Syntax
        {
            get { return (InitializerExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public InitializerExpressionTranslation() { }
        public InitializerExpressionTranslation(InitializerExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expressions = syntax.Expressions.Get<ExpressionSyntax, ExpressionTranslation>( this );
        }


        /* Unmerged change from project 'CSharpToTypescript (net472)'
        Before:
                public SeparatedSyntaxListTranslation<ExpressionSyntax,ExpressionTranslation> Expressions { get; set; }


                public override void ApplyPatch()
        After:
                public SeparatedSyntaxListTranslation<ExpressionSyntax,ExpressionTranslation> Expressions { get; set; }


                public override void ApplyPatch()
        */
        public SeparatedSyntaxListTranslation<ExpressionSyntax, ExpressionTranslation> Expressions { get; set; }


        public override void ApplyPatch()
        {
            base.ApplyPatch();
            if (Syntax.IsKind( SyntaxKind.ArrayInitializerExpression ))
            {
                return;
            }

            foreach (var item in Expressions.GetEnumerable())
            {
                var exp = item as AssignmentExpressionTranslation;
                if (exp == null)
                {
                    continue;
                }

                var identifierName = exp.Left as IdentifierNameTranslation;
                if (identifierName == null)
                {
                    continue;
                }

                identifierName.DetectApplyThis = false;

                exp.OverrideOpeartor = ":";
            }
        }

        protected override string InnerTranslate()
        {
            if (Syntax.IsKind( SyntaxKind.ArrayInitializerExpression ))
            {
                return $"[ {Expressions.Translate()} ]"; ;
            }
            return $"{{ {Expressions.Translate()} }}";
        }
    }
}
