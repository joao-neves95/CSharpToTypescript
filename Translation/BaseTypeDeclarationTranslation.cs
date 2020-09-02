/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using RoslynTypeScript.Patch;

namespace RoslynTypeScript.Translation
{
    public abstract class BaseTypeDeclarationTranslation : MemberDeclarationTranslation
    {
        public BaseTypeDeclarationTranslation() { }
        public BaseTypeDeclarationTranslation(BaseTypeDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Modifiers = syntax.Modifiers.Get( this );
            AttributeList = syntax.AttributeLists.Get<AttributeListSyntax, AttributeListTranslation>( this );
        }

        public SyntaxTokenListTranslation Modifiers { get; set; }
        public SyntaxListTranslation<AttributeListSyntax, AttributeListTranslation> AttributeList { get; set; }

        public override void ApplyPatch()
        {
            InnerTypeDeclarationPatch innerTypeDeclarationPatch = new InnerTypeDeclarationPatch();
            innerTypeDeclarationPatch.Apply( this );
            base.ApplyPatch();

        }


    }
}
