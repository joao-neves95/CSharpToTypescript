/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTypeScript.Translation
{
    public class NamespaceDeclarationTranslation : CSharpSyntaxTranslation
    {
        public new MemberDeclarationSyntax Syntax
        {
            get { return (MemberDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SyntaxListTranslation<MemberDeclarationSyntax, MemberDeclarationTranslation> Members { get; set; }
        public NameTranslation Name { get; set; }

        public bool IsExport { get; set; }

        public NamespaceDeclarationTranslation()
        {

        }

        public NamespaceDeclarationTranslation(NamespaceDeclarationSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
            Members = syntax.Members.Get<MemberDeclarationSyntax, MemberDeclarationTranslation>(this);
            Name = syntax.Name.Get<NameTranslation>(this);
        }

        protected override string InnerTranslate()
        {
            string exportStr = IsExport ? "export" : "";

            return $@"{exportStr} module {Name.Translate()}
                                {{
                                {Members.Translate()}
                                }}";
        }
    }
}
