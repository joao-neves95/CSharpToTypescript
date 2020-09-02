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
    public class BaseTypeTranslation : CSharpSyntaxTranslation
    {
        public new BaseTypeSyntax Syntax
        {
            get { return (BaseTypeSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public BaseTypeTranslation() { }
        public BaseTypeTranslation(BaseTypeSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<TypeTranslation>( this );
        }

        public TypeTranslation Type { get; set; }

        protected override string InnerTranslate()
        {
            return Type.ToString();
        }
    }
}
