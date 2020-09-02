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
    public class IfStatementTranslation : StatementTranslation
    {
        public new IfStatementSyntax Syntax
        {
            get { return (IfStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public IfStatementTranslation() { }


        public IfStatementTranslation(IfStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Condition = syntax.Condition.Get<ExpressionTranslation>( this );
            Statement = syntax.Statement.Get<StatementTranslation>( this );
            Else = syntax.Else.Get<ElseClauseTranslation>( this );
        }

        public ExpressionTranslation Condition { get; set; }

        public StatementTranslation Statement { get; }

        public ElseClauseTranslation Else { get; set; }

        public string FullConditionStr { get; set; }

        protected override string InnerTranslate()
        {
            string result = $"if({Condition.Translate()})"
                 + $"\n {Statement.Translate()}";
            if (Else != null)
            {
                result += $"\n {Else.Translate()}";
            }

            return result;
        }
    }
}
