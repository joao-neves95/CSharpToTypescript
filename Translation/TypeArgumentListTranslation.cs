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
    public class TypeArgumentListTranslation : CSharpSyntaxTranslation
    {
        public new TypeArgumentListSyntax Syntax
        {
            get { return (TypeArgumentListSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public TypeArgumentListTranslation() { }
        public TypeArgumentListTranslation(TypeArgumentListSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Arguments = syntax.Arguments.Get<TypeSyntax, TypeTranslation>(this);
        }

        public SeparatedSyntaxListTranslation<TypeSyntax,TypeTranslation> Arguments { get; set; }

        protected override string InnerTranslate()
        {
            return $"<{Arguments.Translate()}> ";
        }
    }
}
