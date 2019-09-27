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
    public class TypeParameterTranslation : CSharpSyntaxTranslation
    {
        public new TypeParameterSyntax Syntax
        {
            get { return (TypeParameterSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public TypeParameterTranslation(TypeParameterSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
        }

        public TypeTranslation TypeConstraint { get; set; }

        public bool IsExcludeConstraint { get; set; }

        protected override string InnerTranslate()
        {
            if(TypeConstraint!=null && !IsExcludeConstraint)
            {
                return $"{Syntax.Identifier} extends {TypeConstraint.Translate()}";
            }

            return Syntax.Identifier.ToString();
        }
    }
}
