﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class TypeOfExpressionTranslation : ExpressionTranslation
    {
        public new TypeOfExpressionSyntax Syntax
        {
            get { return (TypeOfExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public TypeOfExpressionTranslation() { }
        public TypeOfExpressionTranslation(TypeOfExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<TypeTranslation>( this );
        }

        public TypeTranslation Type { get; set; }

        protected override string InnerTranslate()
        {
            var str = Type.Translate();
            switch (str)
            {
                case "number":
                    str = "<any>Number";
                    break;
                case "string":
                    str = "<any>String";
                    break;
                case "boolean":
                    str = "<any>Boolean";
                    break;

            }
            // for typeof, we translate to Object type of primitive type, for example number -> Number

            return $"/*typeof*/{Type.GetTypeIgnoreGeneric()} ";
        }
    }
}
