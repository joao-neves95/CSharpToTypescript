/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynTypeScript.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTypeScript.VirtualTranslation
{
    public class BaseFunctionGenericNameTranslation : GenericNameTranslation
    {
        protected GenericNameTranslation genericNameTranslation;

        public BaseFunctionGenericNameTranslation(GenericNameTranslation genericNameTranslation)
        {
            this.genericNameTranslation = genericNameTranslation;
            this.Parent = genericNameTranslation.Parent;
        }

        public SeparatedSyntaxListTranslation<TypeSyntax, TypeTranslation> Arguments { get; set; }
        public TypeTranslation ReturnType { get; set; }

        protected string GetFakeParamName(string previous)
        {
            return (previous ?? "") + "_";
        }
    }
}
