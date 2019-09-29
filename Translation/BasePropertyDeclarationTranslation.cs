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
