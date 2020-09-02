/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using RoslynTypeScript.Translation;

using System.Collections.Generic;
using System.Linq;

namespace RoslynTypeScript.VirtualTranslation
{
    public class FunctionTypeGenericNameTranslation : BaseFunctionGenericNameTranslation
    {
        public FunctionTypeGenericNameTranslation(GenericNameTranslation genericNameTranslation) : base( genericNameTranslation )
        {
            ReturnType = genericNameTranslation.TypeArgumentList.Arguments.GetEnumerable().Last();
            Arguments = new SeparatedSyntaxListTranslation<TypeSyntax, TypeTranslation>();
            Arguments.Add( genericNameTranslation.TypeArgumentList.Arguments.GetEnumerable().Where( f => f != ReturnType ) );
            this.Attach();
        }

        protected override string InnerTranslate()
        {
            List<string> list = new List<string>();
            string name = "";
            list = Arguments.GetEnumerable().Select( f => $"{name = GetFakeParamName( name )}:{f.Translate()}" ).ToList();

            return $"({string.Join( ",", list )}) => {ReturnType.Translate()}";
        }
    }
}
