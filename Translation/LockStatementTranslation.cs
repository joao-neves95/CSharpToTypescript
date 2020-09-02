/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class LockStatementTranslation : StatementTranslation
    {
        public new LockStatementSyntax Syntax
        {
            get { return (LockStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public LockStatementTranslation() { }
        public LockStatementTranslation(LockStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
            Statement = syntax.Statement.Get<StatementTranslation>( this );
        }

        public ExpressionTranslation Expression { get; set; }
        public StatementTranslation Statement { get; set; }

        protected override string InnerTranslate()
        {
            return $@"lock({Expression.Translate()})
                {Statement.Translate()}";
        }
    }
}
