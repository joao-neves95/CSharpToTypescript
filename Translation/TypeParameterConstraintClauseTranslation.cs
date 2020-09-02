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
    public class TypeParameterConstraintClauseTranslation : SyntaxTranslation
    {
        public new TypeParameterConstraintClauseSyntax Syntax
        {
            get { return (TypeParameterConstraintClauseSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public TypeParameterConstraintClauseTranslation() { }
        public TypeParameterConstraintClauseTranslation(TypeParameterConstraintClauseSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Constraints = syntax.Constraints.Get<TypeParameterConstraintSyntax, TypeParameterConstraintTranslation>( this );
            Name = syntax.Name.Get<IdentifierNameTranslation>( this );
        }

        public SeparatedSyntaxListTranslation<TypeParameterConstraintSyntax, TypeParameterConstraintTranslation> Constraints { get; set; }
        public IdentifierNameTranslation Name { get; set; }

        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }
    }
}
