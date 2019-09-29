/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Translation;
using System.Linq;

namespace RoslynTypeScript.VirtualTranslation.Delegates
{
    public class ComparisonGenericNameTranslation : BaseFunctionGenericNameTranslation
    {
        public ComparisonGenericNameTranslation(GenericNameTranslation genericNameTranslation) : base( genericNameTranslation )
        {
        }

        protected override string InnerTranslate()
        {
            var firstParam = genericNameTranslation.TypeArgumentList.Arguments.GetEnumerable().First();
            string firstParamStr = firstParam.Translate();
            return $"(_:{firstParamStr}, __:{firstParamStr})=> number";
        }
    }
}
