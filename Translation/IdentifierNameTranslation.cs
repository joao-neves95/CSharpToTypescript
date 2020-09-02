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
    public class IdentifierNameTranslation : SimpleNameTranslation
    {
        public new IdentifierNameSyntax Syntax
        {
            get { return (IdentifierNameSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public IdentifierNameTranslation() { }

        public IdentifierNameTranslation(IdentifierNameSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

        }

        public string MethodNeedToBind { get; set; }

        public TypeArgumentListTranslation TypeArgumentList { get; set; }

        public bool IsStaticMethod
        {
            get
            {
                var semantic = GetSemanticModel();
                if (semantic == null)
                {
                    return false;
                }

                var symbolInfo = semantic.GetSymbolInfo( this.Syntax );
                if (symbolInfo.Symbol != null && symbolInfo.Symbol is IMethodSymbol)
                {
                    return symbolInfo.Symbol.IsStatic;
                }

                return false;
            }
        }

        public bool IsStatic
        {
            get
            {
                var semantic = GetSemanticModel();
                if (semantic == null)
                {
                    return false;
                }

                var symbolInfo = semantic.GetSymbolInfo( this.Syntax );
                if (symbolInfo.Symbol != null)
                {
                    return symbolInfo.Symbol.IsStatic;
                }

                return false;
            }
        }

        protected override string InnerTranslate()
        {
            string syntaxStr = Syntax.ToString();
            syntaxStr = Helper.NormalizeVariabeleName( syntaxStr );

            // hopefully we guess right
            if (syntaxStr == "DateTime" && !(Parent is TypeTranslation))
            {
                return "Date";
            }

            if (syntaxStr == "Action")
            {
                return "() => void";
            }

            if (!DetectApplyThis && TypeArgumentList != null)
            {
                return $"{syntaxStr}{TypeArgumentList.Translate()}";
            }

            if (IsArrayType( syntaxStr ))
            {
                return "Array<any>";
            }

            if (IsLengthArrayOrString( syntaxStr ))
            {
                return "length";
            }

            if (!DetectApplyThis)
            {
                return syntaxStr;
            }

            var result = HandleApplyStaticOrThis( syntaxStr );
            if (result != null)
            {
                return result;
            }

            return syntaxStr;
        }

        public bool IsInnerType(ISymbol symbol)
        {
            if (symbol == null)
            {
                return false;
            }

            if (symbol.Kind != SymbolKind.NamedType)
            {
                return false;
            }

            return symbol.ContainingType != null;

        }

        private bool IsLengthArrayOrString(string syntaxStr)
        {
            if (syntaxStr != "Length")
            {
                return false;
            }

            SemanticModel semanticModel = GetSemanticModel();
            if (semanticModel == null)
            {
                return false;
            }

            SymbolInfo symbolInfo = semanticModel.GetSymbolInfo( Syntax );
            if (symbolInfo.Symbol == null)
            {
                return false;
            }

            return symbolInfo.Symbol.ContainingType == GetCompilation().GetTypeByMetadataName( "System.Array" )
                || symbolInfo.Symbol.ContainingType == GetCompilation().GetTypeByMetadataName( "System.String" );
        }

        private bool IsArrayType(string syntaxStr)
        {
            if (syntaxStr != "Array")
            {
                return false;
            }

            SemanticModel semanticModel = GetSemanticModel();
            if (semanticModel == null)
            {
                return false;
            }

            SymbolInfo symbolInfo = semanticModel.GetSymbolInfo( Syntax );

            return symbolInfo.Symbol == GetCompilation().GetTypeByMetadataName( "System.Array" );
        }
    }
}
