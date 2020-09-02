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
