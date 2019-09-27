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
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynTypeScript.Translation
{
    public class GotoStatementTranslation : StatementTranslation
    {
        public new GotoStatementSyntax Syntax
        {
            get { return (GotoStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public GotoStatementTranslation() { }
        public GotoStatementTranslation(GotoStatementSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            
        }

        public ExpressionTranslation Expression { get; set; }

        protected override string InnerTranslate()
        {
 
            return Syntax.ToString();
        }
    }
}
