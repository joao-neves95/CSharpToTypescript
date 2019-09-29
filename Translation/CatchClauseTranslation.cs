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
    public class CatchClauseTranslation : CSharpSyntaxTranslation
    {
        public new CatchClauseSyntax Syntax
        {
            get { return (CatchClauseSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public CatchClauseTranslation() { }
        public CatchClauseTranslation(CatchClauseSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Block = syntax.Block.Get<BlockTranslation>( this );
            Declaration = syntax.Declaration.Get<CatchDeclarationTranslation>( this );
        }

        public BlockTranslation Block { get; set; }
        public CatchDeclarationTranslation Declaration { get; set; }

        protected override string InnerTranslate()
        {
            string errName = Declaration?.Syntax.Identifier.ToString();
            if (string.IsNullOrEmpty( errName ))
            {
                errName = "err";
            }

            return $@"catch({errName})
                {Block.Translate()}";
        }
    }
}
