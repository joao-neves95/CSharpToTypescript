﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using RoslynTypeScript.Contract;
using RoslynTypeScript.Patch;

namespace RoslynTypeScript.Translation
{
    public abstract class TypeDeclarationTranslation : BaseTypeDeclarationTranslation, ITypeParameterConstraint
    {
        public new TypeDeclarationSyntax Syntax
        {
            get { return (TypeDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public TypeDeclarationTranslation(TypeDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            TypeParameterList = syntax.TypeParameterList.Get<TypeParameterListTranslation>( this );
            Members = syntax.Members.Get<MemberDeclarationSyntax, MemberDeclarationTranslation>( this );
            ConstraintClauses = syntax.ConstraintClauses.Get<TypeParameterConstraintClauseSyntax, TypeParameterConstraintClauseTranslation>( this );
        }

        public SyntaxListTranslation<MemberDeclarationSyntax, MemberDeclarationTranslation> Members { get; set; }

        public SyntaxListTranslation<TypeParameterConstraintClauseSyntax, TypeParameterConstraintClauseTranslation> ConstraintClauses { get; set; }

        public TypeParameterListTranslation TypeParameterList { get; set; }

        public TokenTranslation Identifier
        {
            get { return Syntax.Identifier.Get( this ); }
        }

        public override void ApplyPatch()
        {
            GenericConstrantsPatch genericConstrantsPatch = new GenericConstrantsPatch();
            genericConstrantsPatch.Apply( this );
            base.ApplyPatch();
            /// OverloadingPatch patch = new OverloadingPatch();
            // patch.Apply(this);
        }

        protected string GetAttributeList()
        {
            return Helper.GetAttributeList( Syntax.AttributeLists );
        }
    }
}
