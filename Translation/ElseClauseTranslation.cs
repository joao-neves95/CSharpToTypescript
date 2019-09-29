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
    public class ElseClauseTranslation : CSharpSyntaxTranslation
    {
        public new ElseClauseSyntax Syntax
        {
            get { return (ElseClauseSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ElseClauseTranslation() { }
        public ElseClauseTranslation(ElseClauseSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Statement = syntax.Statement.Get<StatementTranslation>( this );
        }

        public StatementTranslation Statement { get; set; }

        protected override string InnerTranslate()
        {
            return $"else {Statement.Translate()}";
        }
    }
}
