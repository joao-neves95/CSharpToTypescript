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
    public class SimpleBaseTypeTranslation : BaseTypeTranslation
    {
        public new SimpleBaseTypeSyntax Syntax
        {
            get { return (SimpleBaseTypeSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public SimpleBaseTypeTranslation() { }
        public SimpleBaseTypeTranslation(SimpleBaseTypeSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            this.Syntax = syntax;
        }
    }
}
