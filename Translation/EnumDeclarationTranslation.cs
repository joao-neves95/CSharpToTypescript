﻿/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class EnumDeclarationTranslation : BaseTypeDeclarationTranslation
    {
        public new EnumDeclarationSyntax Syntax
        {
            get { return (EnumDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public EnumDeclarationTranslation() { }
        public EnumDeclarationTranslation(EnumDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Members = syntax.Members.Get<EnumMemberDeclarationSyntax, EnumMemberDeclarationTranslation>( this );
            Members.IsNewLine = true;
        }

        public SeparatedSyntaxListTranslation<EnumMemberDeclarationSyntax, EnumMemberDeclarationTranslation> Members { get; set; }


        protected override string InnerTranslate()
        {
            return $@"export enum {Syntax.Identifier} {{
                 {Members.Translate()}
                 }}";
        }
    }
}
