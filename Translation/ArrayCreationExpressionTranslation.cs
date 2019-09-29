/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class ArrayCreationExpressionTranslation : ExpressionTranslation
    {
        public new ArrayCreationExpressionSyntax Syntax
        {
            get { return (ArrayCreationExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ArrayCreationExpressionTranslation() { }
        public ArrayCreationExpressionTranslation(ArrayCreationExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<ArrayTypeTranslation>( this );
            Initializer = syntax.Initializer.Get<InitializerExpressionTranslation>( this );
        }

        public ArrayTypeTranslation Type { get; set; }
        public InitializerExpressionTranslation Initializer { get; set; }

        protected override string InnerTranslate()
        {
            // now only support 1-dimension 

            if (Initializer == null)
            {
                var semantic = GetSemanticModel();
                var typeInfo = semantic.GetTypeInfo( Type.ElementType.Syntax );

                string size = Type.RankSpecifiers.GetEnumerable().First().Sizes.Translate();
                return $"new Array({size})";
            }
            else
            {
                return $"{Initializer.Expressions.Translate()}";
            }
        }
    }
}
