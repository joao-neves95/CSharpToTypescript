﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class OperatorDeclarationTranslation : BaseMethodDeclarationTranslation
    {
        public new OperatorDeclarationSyntax Syntax
        {
            get { return (OperatorDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public OperatorDeclarationTranslation() { }
        public OperatorDeclarationTranslation(OperatorDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            ReturnType = syntax.ReturnType.Get<TypeTranslation>( this );
            Identifier = new TokenTranslation { SyntaxString = Helper.OperatorToMethod( syntax.OperatorToken.ToString() ) };
        }

        //public ArrowExpressionClauseSyntax ExpressionBody { get; set; }

        public TypeTranslation ReturnType { get; set; }


        protected override string InnerTranslate()
        {

            // currently, I only support == != > < >= <=
            string originalOpeartor = Syntax.OperatorToken.ToString();
            if (!Helper.IsSupportOperator( originalOpeartor ))
            {
                throw new NotSupportedException();
            }

            // this is static method -> to normal method
            var firstParam = ParameterList.Parameters.GetEnumerable().First();
            string firstParamStr = firstParam.Identifier.Translate();
            ParameterList.Parameters.Remove( firstParam );
            return $@" public {Identifier} {ParameterList}: {ReturnType}
                    {{
                    var {firstParamStr} = this;
                    {Body.Statements.Translate()}
                    }}
                ";
        }
    }
}
