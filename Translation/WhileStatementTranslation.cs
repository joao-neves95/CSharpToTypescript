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
    public class WhileStatementTranslation : CSharpSyntaxTranslation
    {
        public new WhileStatementSyntax Syntax
        {
            get { return (WhileStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public WhileStatementTranslation() { }
        public WhileStatementTranslation(WhileStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Condition = syntax.Condition.Get<ExpressionTranslation>( this );
            Statement = syntax.Statement.Get<StatementTranslation>( this );
        }

        public ExpressionTranslation Condition { get; set; }
        public StatementTranslation Statement { get; set; }

        protected override string InnerTranslate()
        {
            return $"while ({Condition.Translate()})"
                + $"\n {Statement.Translate()}";
        }
    }
}
