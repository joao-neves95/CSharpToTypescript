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
    public class ClassOrStructConstraintTranslation : TypeParameterConstraintTranslation
    {
        public new ClassOrStructConstraintSyntax Syntax
        {
            get { return (ClassOrStructConstraintSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ClassOrStructConstraintTranslation() { }
        public ClassOrStructConstraintTranslation(ClassOrStructConstraintSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

        }


        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
