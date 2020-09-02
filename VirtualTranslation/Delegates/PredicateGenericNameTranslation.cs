/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Translation;

using System.Linq;

namespace RoslynTypeScript.VirtualTranslation
{
    public class PredicateGenericNameTranslation : BaseFunctionGenericNameTranslation
    {
        public PredicateGenericNameTranslation(GenericNameTranslation genericNameTranslation) : base( genericNameTranslation )
        {
        }

        protected override string InnerTranslate()
        {
            var firstParam = genericNameTranslation.TypeArgumentList.Arguments.GetEnumerable().First();
            return $"(_:{firstParam.Translate()})=>boolean";
        }
    }
}
