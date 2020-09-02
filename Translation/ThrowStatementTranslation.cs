﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class ThrowStatementTranslation : StatementTranslation
    {
        public new ThrowStatementSyntax Syntax
        {
            get { return (ThrowStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ThrowStatementTranslation() { }
        public ThrowStatementTranslation(ThrowStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
        }

        public ExpressionTranslation Expression { get; set; }

        protected override string InnerTranslate()
        {

            var err = "err";
            // try to find exception variable from catch clause
            if (Expression == null)
            {
                var tokenText = this.Syntax.Ancestors().OfType<CatchClauseSyntax>().FirstOrDefault()?.Declaration?.Identifier.ValueText;

                err = tokenText ?? err;

            }
            else
            {
                err = Expression.Translate();
            }

            return $"throw {err};";
        }
    }
}
