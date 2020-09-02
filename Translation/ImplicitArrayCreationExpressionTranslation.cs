/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class ImplicitArrayCreationExpressionTranslation : ExpressionTranslation
    {
        public new ImplicitArrayCreationExpressionSyntax Syntax
        {
            get { return (ImplicitArrayCreationExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ImplicitArrayCreationExpressionTranslation() { }
        public ImplicitArrayCreationExpressionTranslation(ImplicitArrayCreationExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Initializer = syntax.Initializer.Get<InitializerExpressionTranslation>( this );
        }

        public InitializerExpressionTranslation Initializer { get; set; }

        protected override string InnerTranslate()
        {
            return $"new Array({Initializer.Expressions.Translate()})";
        }
    }
}
