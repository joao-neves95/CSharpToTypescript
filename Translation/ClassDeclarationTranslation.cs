/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using RoslynTypeScript.Contract;

using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class ClassDeclarationTranslation : ClassStructDeclarationTranslation, IBaseExtended
    {
        public new ClassDeclarationSyntax Syntax
        {
            get { return (ClassDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ClassDeclarationTranslation(ClassDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            var constructor = Members.GetEnumerable<ConstructorDeclarationTranslation>().FirstOrDefault();
            if (constructor == null)
            {
                return;
            }
        }

        public override void ApplyPatch()
        {
            base.ApplyPatch();
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
