/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class AssignmentExpressionTranslation : ExpressionTranslation
    {
        public new AssignmentExpressionSyntax Syntax
        {
            get { return (AssignmentExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ExpressionTranslation Left { get; set; }
        public ExpressionTranslation Right { get; set; }

        public string OverrideOpeartor { get; set; }

        public AssignmentExpressionTranslation() { }
        public AssignmentExpressionTranslation(AssignmentExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Left = syntax.Left.Get<ExpressionTranslation>( this );
            Right = syntax.Right.Get<ExpressionTranslation>( this );
        }

        public override void ApplyPatch()
        {
            base.ApplyPatch();
            Helper.ApplyFunctionBindToCorrectContext( this.Right );
        }

        protected override string InnerTranslate()
        {
            if (OverrideOpeartor != null)
            {
                return $"{Left.Translate()} {OverrideOpeartor}  {Right.Translate()}";
            }

            var operatorToken = Syntax.OperatorToken.ToString();
            if (Helper.IsInKinds( this.Syntax,
                SyntaxKind.OrAssignmentExpression,
                SyntaxKind.AndAssignmentExpression,
                SyntaxKind.BitwiseOrExpression,
                SyntaxKind.BitwiseAndExpression ))
            {
                switch (this.Syntax.Kind())
                {
                    case SyntaxKind.OrAssignmentExpression:
                        return $"{Left.Translate()} = {Left.Translate()} || {Right.Translate()} ";
                    case SyntaxKind.AndAssignmentExpression:
                        return $"{Left.Translate()} = {Left.Translate()} && {Right.Translate()} ";
                    case SyntaxKind.BitwiseOrExpression:
                        operatorToken = "||";
                        break;
                    case SyntaxKind.BitwiseAndExpression:
                        operatorToken = "&&";
                        break;
                }
            }

            var rightStr = Right.Translate();

            return string.Format( "{0} {1} {2}", Left.Translate(), operatorToken, rightStr );
        }
    }
}
