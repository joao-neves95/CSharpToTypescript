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
    public class FinallyClauseTranslation : CSharpSyntaxTranslation
    {
        public new FinallyClauseSyntax Syntax
        {
            get { return (FinallyClauseSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public FinallyClauseTranslation() { }
        public FinallyClauseTranslation(FinallyClauseSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Block = syntax.Block.Get<BlockTranslation>( this );
        }

        public BlockTranslation Block { get; set; }

        protected override string InnerTranslate()
        {
            return $@"finally
                {Block.Translate()} ";
        }
    }
}
