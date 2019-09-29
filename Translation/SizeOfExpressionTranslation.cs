/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class SizeOfExpressionTranslation : ExpressionTranslation
    {
        public new SizeOfExpressionSyntax Syntax
        {
            get { return (SizeOfExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SizeOfExpressionTranslation() { }
        public SizeOfExpressionTranslation(SizeOfExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<TypeTranslation>( this );
        }

        public TypeTranslation Type { get; set; }

        protected override string InnerTranslate()
        {
            var semanticModel = GetSemanticModel();
            var type = semanticModel.GetTypeInfo( Syntax.Type );
            if (type.Type != null)
            {
                switch (type.Type.SpecialType)
                {
                    case SpecialType.System_Byte:
                    case SpecialType.System_SByte:
                        return "1";
                    case SpecialType.System_Int16:
                    case SpecialType.System_UInt16:
                        return "2";
                    case SpecialType.System_Int32:
                    case SpecialType.System_UInt32:
                        return "4";
                    case SpecialType.System_Int64:
                    case SpecialType.System_UInt64:
                        return "8";

                }
            }

            return $"__sizeof__({Syntax.Type.ToString()})";
        }
    }
}
