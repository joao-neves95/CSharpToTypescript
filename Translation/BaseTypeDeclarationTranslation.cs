/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynTypeScript.Patch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTypeScript.Translation
{
    public abstract class BaseTypeDeclarationTranslation : MemberDeclarationTranslation
    {
        public BaseTypeDeclarationTranslation() { }
        public BaseTypeDeclarationTranslation(BaseTypeDeclarationSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Modifiers = syntax.Modifiers.Get(this);
            AttributeList = syntax.AttributeLists.Get<AttributeListSyntax, AttributeListTranslation>(this);
        }

        public SyntaxTokenListTranslation Modifiers { get; set; }
        public SyntaxListTranslation<AttributeListSyntax, AttributeListTranslation> AttributeList { get; set; }

        public override void ApplyPatch()
        {
            InnerTypeDeclarationPatch innerTypeDeclarationPatch = new InnerTypeDeclarationPatch();
            innerTypeDeclarationPatch.Apply(this);
            base.ApplyPatch();

        }

     
    }
}
