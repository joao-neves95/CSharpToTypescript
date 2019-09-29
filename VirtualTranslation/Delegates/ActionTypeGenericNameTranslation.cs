/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Translation;
using System.Collections.Generic;
using System.Linq;

namespace RoslynTypeScript.VirtualTranslation
{
    public class ActionTypeGenericNameTranslation : BaseFunctionGenericNameTranslation
    {
        public ActionTypeGenericNameTranslation(GenericNameTranslation genericNameTranslation) : base( genericNameTranslation )
        {
            Arguments = genericNameTranslation.TypeArgumentList.Arguments;
            this.Attach();
        }

        protected override string InnerTranslate()
        {
            List<string> list = new List<string>();
            string name = "";
            list = Arguments.GetEnumerable().Select( f => $"{name = GetFakeParamName( name )}:{f.Translate()}" ).ToList();

            return $"({string.Join( ",", list )}) => void";
        }
    }
}
