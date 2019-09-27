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
    public class CaseSwitchLabelTranslation : SwitchLabelTranslation
    {
        public new CaseSwitchLabelSyntax Syntax
        {
            get { return (CaseSwitchLabelSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public CaseSwitchLabelTranslation() { }
        public CaseSwitchLabelTranslation(CaseSwitchLabelSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Value = syntax.Value.Get<ExpressionTranslation>(this);
        }

        
        public ExpressionTranslation Value { get; set; }

        protected override string InnerTranslate()
        {
            return $"case {Value.Translate()}:";
        }
    }
}
