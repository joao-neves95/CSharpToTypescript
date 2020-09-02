/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using RoslynTypeScript.Constants;

namespace RoslynTypeScript.Translation
{
    public class AccessorDeclarationTranslation : CSharpSyntaxTranslation
    {
        public new AccessorDeclarationSyntax Syntax
        {
            get { return (AccessorDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public AccessorDeclarationTranslation(AccessorDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Body = syntax.Body.Get<BlockTranslation>( this );
            Modifiers = syntax.Modifiers.Get( this );
        }


        public BlockTranslation Body { get; set; }
        public SyntaxTokenListTranslation ParentModifiers
        {
            get { return Modifiers; }
            set { Modifiers = value; }
        }

        public SyntaxTokenListTranslation Modifiers { get; set; }

        protected override string InnerTranslate()
        {
            var ancestor = GetAncestor<BasePropertyDeclarationTranslation>();
            if (ancestor is IndexerDeclarationTranslation)
            {
                return BuildIndexerAccessor();
            }

            return BuildPropertyAccessor();
        }

        private string BuildIndexerAccessor()
        {
            var ancestor = GetAncestor<IndexerDeclarationTranslation>();

            string keyword = Syntax.Keyword.ToString();

            string bodyStr = Body?.Translate() ?? "{ throw new System.NotImplementedException();}";

            if (keyword == "get")
            {
                return $@"{Modifiers.Translate()} {TC.IndexerGetName}({ancestor.ParameterList.Parameters.Translate()}) :{ancestor.Type.Translate()}
                    {bodyStr}";
            }
            else
            {
                return $@"{Modifiers.Translate()} {TC.IndexerSetName}({ancestor.ParameterList.Parameters.Translate()}, value :{ancestor.Type.Translate()}):void
                {bodyStr}";
            }
        }

        private string BuildPropertyAccessor()
        {
            var ancestor = GetAncestor<PropertyDeclarationTranslation>();

            string keyword = Syntax.Keyword.ToString();

            if (keyword == "get")
            {
                return string.Format( @"{0} get {1}(): {2}
{3}", Modifiers.Translate(), ancestor.Identifier.Translate(), ancestor.Type.Translate(), Body.Translate() );

            }

            return string.Format( @"{0} set {1}(value: {2})
{3}", Modifiers.Translate(), ancestor.Identifier.Translate(), ancestor.Type.Translate(), Body.Translate() );
        }

        public bool IsShorten()
        {
            return Syntax.SemicolonToken.ToString() == ";";
        }
    }
}
