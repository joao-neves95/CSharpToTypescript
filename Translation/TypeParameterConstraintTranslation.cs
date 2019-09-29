/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class TypeParameterConstraintTranslation : CSharpSyntaxTranslation
    {
        public new TypeParameterConstraintSyntax Syntax
        {
            get { return (TypeParameterConstraintSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public TypeParameterConstraintTranslation() { }
        public TypeParameterConstraintTranslation(TypeParameterConstraintSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

        }


        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
