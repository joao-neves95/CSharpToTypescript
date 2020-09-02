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
    public abstract class BaseMethodDeclarationTranslation : MemberDeclarationTranslation
    {
        public new BaseMethodDeclarationSyntax Syntax
        {
            get { return (BaseMethodDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public BaseMethodDeclarationTranslation() { }
        public BaseMethodDeclarationTranslation(BaseMethodDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            ParameterList = syntax.ParameterList.Get<ParameterListTranslation>( this );
            Modifiers = syntax.Modifiers.Get( this );
            Body = syntax.Body.Get<BlockTranslation>( this );
            SemicolonToken = syntax.SemicolonToken.Get( this );
        }


        public ParameterListTranslation ParameterList { get; set; }
        public SyntaxTokenListTranslation Modifiers { get; set; }
        public BlockTranslation Body { get; set; }
        public TokenTranslation SemicolonToken { get; set; }
        public TokenTranslation Identifier { get; set; }

        public bool IsTheSameOverloading(BaseMethodDeclarationTranslation method)
        {
            var sameToken = this.Identifier.TokenEquals( method.Identifier );
            if (!sameToken) return false;
            return method.Modifiers.IsStatic == this.Modifiers.IsStatic;
        }

        public override void ApplyPatch()
        {
            base.ApplyPatch();
        }

        protected override string InnerTranslate()
        {
            return Syntax.ToString();
        }

        protected string GetAttributeList()
        {
            return Helper.GetAttributeList( Syntax.AttributeLists );
        }
    }
}
