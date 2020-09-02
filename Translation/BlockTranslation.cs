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
    public class BlockTranslation : StatementTranslation
    {
        public new BlockSyntax Syntax
        {
            get { return (BlockSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public SyntaxListTranslation<StatementSyntax, StatementTranslation> Statements { get; set; }

        public BlockTranslation()
        {
            Statements = new SyntaxListTranslation<StatementSyntax, StatementTranslation>();
        }

        public BlockTranslation(BlockSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Statements = syntax.Statements.Get<StatementSyntax, StatementTranslation>( this );
        }

        public bool IsIgnoreBracket { get; set; }

        protected override string InnerTranslate()
        {
            if (IsIgnoreBracket)
            {
                return Statements.Translate();
            }

            return string.Format( @"{{
{0}
}}", Statements.Translate() );
        }
    }
}
