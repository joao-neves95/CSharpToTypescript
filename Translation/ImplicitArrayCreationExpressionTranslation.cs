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
    public class ImplicitArrayCreationExpressionTranslation : ExpressionTranslation
    {
        public new ImplicitArrayCreationExpressionSyntax Syntax
        {
            get { return (ImplicitArrayCreationExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ImplicitArrayCreationExpressionTranslation() { }
        public ImplicitArrayCreationExpressionTranslation(ImplicitArrayCreationExpressionSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Initializer = syntax.Initializer.Get<InitializerExpressionTranslation>(this);
        }

        public InitializerExpressionTranslation Initializer { get; set; }

        protected override string InnerTranslate()
        {
            return $"new Array({Initializer.Expressions.Translate()})";
        }
    }
}
