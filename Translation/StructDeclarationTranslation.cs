/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynTypeScript.Contract;

namespace RoslynTypeScript.Translation
{
    public class StructDeclarationTranslation : ClassStructDeclarationTranslation, IBaseExtended
    {
        public new StructDeclarationSyntax Syntax
        {
            get { return (StructDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        //public StructDeclarationTranslation() { }
        public StructDeclarationTranslation(StructDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            if (BaseList == null)
            {
                BaseList = new BaseListTranslation();
                BaseList.Parent = this;
                BaseList.Types = new SeparatedSyntaxListTranslation<BaseTypeSyntax, BaseTypeTranslation>();
                BaseList.Types.Parent = BaseList;
            }

            //BaseList.Types.Add(new BaseTypeTranslation() { SyntaxString = TC.IStruct });
        }


        public override void ApplyPatch()
        {
            base.ApplyPatch();
            //StructPatch structPatch = new StructPatch();
            //structPatch.Apply(this);
        }

        protected override string InnerTranslate()
        {
            string baseTranslation = BaseList?.Translate();

            return $@"{GetAttributeList()}export class {Syntax.Identifier}{TypeParameterList?.Translate()} {baseTranslation}
                {{
                {Members.Translate()} 
                }}";
        }
    }
}
