﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class PrefixUnaryExpressionTranslation : ExpressionTranslation
    {
        public new PrefixUnaryExpressionSyntax Syntax
        {
            get { return (PrefixUnaryExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public PrefixUnaryExpressionTranslation() { }
        public PrefixUnaryExpressionTranslation(PrefixUnaryExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Operand = syntax.Operand.Get<ExpressionTranslation>( this );
        }

        public ExpressionTranslation Operand { get; set; }

        protected override string InnerTranslate()
        {
            return $"{Syntax.OperatorToken.ToString()}{Operand.Translate()}";
        }
    }
}
