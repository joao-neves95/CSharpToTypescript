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
    public class TypeParameterTranslation : CSharpSyntaxTranslation
    {
        public new TypeParameterSyntax Syntax
        {
            get { return (TypeParameterSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public TypeParameterTranslation(TypeParameterSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
        }

        public TypeTranslation TypeConstraint { get; set; }

        public bool IsExcludeConstraint { get; set; }

        protected override string InnerTranslate()
        {
            if (TypeConstraint != null && !IsExcludeConstraint)
            {
                return $"{Syntax.Identifier} extends {TypeConstraint.Translate()}";
            }

            return Syntax.Identifier.ToString();
        }
    }
}
