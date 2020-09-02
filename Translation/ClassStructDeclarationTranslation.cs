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
    public abstract class ClassStructDeclarationTranslation : TypeDeclarationTranslation
    {
        public ClassStructDeclarationTranslation(TypeDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            if (syntax.BaseList != null)
            {
                BaseList = syntax.BaseList.Get<BaseListTranslation>( this );
            }
        }

        public override void ApplyPatch()
        {
            base.ApplyPatch();
            // ConstructorPatch constructorPatch = new ConstructorPatch();
            // constructorPatch.Apply(this);
        }

        public BaseListTranslation BaseList { get; set; }

        public bool HasExplicitBase()
        {
            if (Syntax.BaseList == null)
            {
                return false;
            }

            return BaseList.GetBaseClass() != null;
        }
    }
}
