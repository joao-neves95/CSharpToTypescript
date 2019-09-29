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
    public class InterfaceDeclarationTranslation : TypeDeclarationTranslation
    {
        public new InterfaceDeclarationSyntax Syntax
        {
            get { return (InterfaceDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public InterfaceDeclarationTranslation(InterfaceDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

            if (syntax.BaseList != null)
            {
                BaseList = syntax.BaseList.Get<BaseListTranslation>( this );
                BaseList.IsForInterface = true;
            }
        }

        public BaseListTranslation BaseList { get; set; }

        protected override string InnerTranslate()
        {
            string baseTranslation = BaseList?.Translate();
            return $@"{GetAttributeList()}export interface {Syntax.Identifier} {TypeParameterList?.Translate()} {baseTranslation}
                {{
                {Members.Translate()} 
                }}";
        }
    }
}
