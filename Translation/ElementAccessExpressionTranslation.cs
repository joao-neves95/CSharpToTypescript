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
    public class ElementAccessExpressionTranslation : ExpressionTranslation
    {
        public new ElementAccessExpressionSyntax Syntax
        {
            get { return (ElementAccessExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ElementAccessExpressionTranslation() { }
        public ElementAccessExpressionTranslation(ElementAccessExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            ArgumentList = syntax.ArgumentList.Get<BracketedArgumentListTranslation>( this );
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
        }

        public BracketedArgumentListTranslation ArgumentList { get; set; }
        public ExpressionTranslation Expression { get; set; }

        protected override string InnerTranslate()
        {
            return NormalTranslate();
        }

        private string NormalTranslate()
        {
            return $"{Expression.Translate()}{ArgumentList.Translate()}";
        }
    }
}
