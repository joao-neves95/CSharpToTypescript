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
    public class NullableTypeTranslation : TypeTranslation
    {
        public new NullableTypeSyntax Syntax
        {
            get { return (NullableTypeSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public NullableTypeTranslation() { }
        public NullableTypeTranslation(NullableTypeSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            ElementType = syntax.ElementType.Get<TypeTranslation>( this );
        }

        public TypeTranslation ElementType { get; set; }

        protected override string InnerTranslate()
        {
            return ElementType.Translate();
        }
    }
}
