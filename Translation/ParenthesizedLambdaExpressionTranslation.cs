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
    public class ParenthesizedLambdaExpressionTranslation : ExpressionTranslation
    {
        public new ParenthesizedLambdaExpressionSyntax Syntax
        {
            get { return (ParenthesizedLambdaExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ParenthesizedLambdaExpressionTranslation() { }
        public ParenthesizedLambdaExpressionTranslation(ParenthesizedLambdaExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Body = syntax.Body.Get<CSharpSyntaxTranslation>( this );
            ParameterList = syntax.ParameterList.Get<ParameterListTranslation>( this );
        }

        public CSharpSyntaxTranslation Body { get; set; }
        public ParameterListTranslation ParameterList { get; set; }

        protected override string InnerTranslate()
        {
            return $"{ParameterList.Translate()} => {Body.Translate()}";
        }
    }
}
