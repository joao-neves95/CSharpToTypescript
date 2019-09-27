/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public IfStatementTranslation(IfStatementSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Condition = syntax.Condition.Get<ExpressionTranslation>(this);
            Statement = syntax.Statement.Get<StatementTranslation>(this);
            Else = syntax.Else.Get<ElseClauseTranslation>(this);
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
