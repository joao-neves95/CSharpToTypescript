﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Constants;
using RoslynTypeScript.Translation;

namespace RoslynTypeScript.Patch
{
    /// <summary>
    /// try to replace yield statement with array.push, then return array at the end of method.
    /// </summary>
    public class ArrayInitReturnForYieldPatch : Patch
    {
        public void Apply(MethodDeclarationTranslation method)
        {
            if (!method.IsYieldReturn)
            {
                return;
            }

            var arrayCreation = new ArrayCreationExpressionTranslation();
            string typeParemter = string.Empty;
            var genericType = method.ReturnType as GenericNameTranslation;
            if (genericType != null)
            {
                typeParemter = genericType.TypeArgumentList.Translate();
            }
            arrayCreation.SyntaxString = $"var {TC.YieldResultName} = new Array{typeParemter}();";

            var returnStatement = new ReturnStatementTranslation();
            returnStatement.SyntaxString = $"return {TC.YieldResultName};";

            method.Body.Statements.Insert( 0, arrayCreation );
            method.Body.Statements.Add( returnStatement );
        }
    }
}
