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
    public class ActionTypeGenericNameTranslation : BaseFunctionGenericNameTranslation
    {
        public ActionTypeGenericNameTranslation(GenericNameTranslation genericNameTranslation) : base(genericNameTranslation)
        {
            Arguments = genericNameTranslation.TypeArgumentList.Arguments;
            this.Attach();
        }

        protected override string InnerTranslate()
        {
            List<string> list = new List<string>();
            string name = "";
            list = Arguments.GetEnumerable().Select(f => $"{name = GetFakeParamName(name)}:{f.Translate()}").ToList();

            return $"({string.Join(",", list)}) => void";
        }
    }
}
