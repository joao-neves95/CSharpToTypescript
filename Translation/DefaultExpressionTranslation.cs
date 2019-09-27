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
    public class DefaultExpressionTranslation : ExpressionTranslation
    {
        public new DefaultExpressionSyntax Syntax
        {
            get { return (DefaultExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public DefaultExpressionTranslation() { }
        public DefaultExpressionTranslation(DefaultExpressionSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Type = syntax.Type.Get<TypeTranslation>(this);
        }

        public TypeTranslation Type { get; set; }
        protected override string InnerTranslate()
        {
            return Helper.GetDefaultValue(Type);
        }
    }
}
