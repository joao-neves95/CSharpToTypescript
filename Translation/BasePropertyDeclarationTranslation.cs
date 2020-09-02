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
    public class BasePropertyDeclarationTranslation : MemberDeclarationTranslation
    {
        public new BasePropertyDeclarationSyntax Syntax
        {
            get { return (BasePropertyDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public BasePropertyDeclarationTranslation() { }
        public BasePropertyDeclarationTranslation(BasePropertyDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<TypeTranslation>( this );
            AccessorList = syntax.AccessorList.Get<AccessorListTranslation>( this );
            Modifiers = syntax.Modifiers.Get( this );

            AccessorList.SetModifier( Modifiers );
        }

        public AccessorListTranslation AccessorList { get; set; }
        public SyntaxTokenListTranslation Modifiers { get; set; }

        public TypeTranslation Type { get; set; }
        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
