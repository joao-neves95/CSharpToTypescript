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

        public BlockTranslation(BlockSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Statements = syntax.Statements.Get<StatementSyntax, StatementTranslation>(this);
        }

        public bool IsIgnoreBracket { get; set; }

        protected override string InnerTranslate()
        {
            if(IsIgnoreBracket)
            {
                return Statements.Translate();
            }

            return string.Format(@"{{
{0}
}}", Statements.Translate());
        }
    }
}
