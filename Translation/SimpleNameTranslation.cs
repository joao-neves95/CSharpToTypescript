/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public abstract class SimpleNameTranslation : NameTranslation
    {
        public SimpleNameTranslation() { }
        public SimpleNameTranslation(SimpleNameSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
        }


        public bool DetectApplyThis { get; set; } = true;


        protected string HandleApplyStaticOrThis(string syntaxStr)
        {
            SemanticModel semanticModel = GetSemanticModel();
            if (semanticModel == null)
            {
                return null;
            }

            SymbolInfo symbolInfo = semanticModel.GetSymbolInfo( Syntax );

            if (symbolInfo.Symbol != null && (
                symbolInfo.Symbol.Kind == SymbolKind.Field
                || symbolInfo.Symbol.Kind == SymbolKind.Property
                || symbolInfo.Symbol.Kind == SymbolKind.Method))
            {
                var result = Helper.ApplyThis( semanticModel, this, syntaxStr );
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
