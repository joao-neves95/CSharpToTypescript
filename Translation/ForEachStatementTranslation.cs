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
    public class ForEachStatementTranslation : StatementTranslation
    {
        public new ForEachStatementSyntax Syntax
        {
            get { return (ForEachStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ForEachStatementTranslation() { }
        public ForEachStatementTranslation(ForEachStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
            Statement = syntax.Statement.Get<StatementTranslation>( this );
            Type = syntax.Type.Get<TypeTranslation>( this );
        }

        public ExpressionTranslation Expression { get; set; }
        //public SyntaxToken Identifier { get; }
        public StatementTranslation Statement { get; set; }
        public TypeTranslation Type { get; set; }

        protected override string InnerTranslate()
        {

            var expression = Statement is BlockTranslation ? Statement.Translate() : "{" + Statement.Translate() + "}";
            var statement = @"{0}.forEach(function({1}){2});";

            return string.Format( statement, Expression.Translate(), Syntax.Identifier, expression );

        }
    }
}
