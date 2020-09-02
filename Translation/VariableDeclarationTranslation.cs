/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class VariableDeclarationTranslation : CSharpSyntaxTranslation
    {
        public new VariableDeclarationSyntax Syntax
        {
            get { return (VariableDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public TypeTranslation Type { get; set; }
        public SeparatedSyntaxListTranslation<VariableDeclaratorSyntax, VariableDeclaratorTranslation> Variables { get; set; }


        public VariableDeclarationTranslation(VariableDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Type = syntax.Type.Get<TypeTranslation>( this );
            Variables = syntax.Variables.Get<VariableDeclaratorSyntax, VariableDeclaratorTranslation>( this );
            if (!syntax.Type.IsVar)
            {
                Variables.GetEnumerable().First().FirstType = Type;
            }

            foreach (var item in Variables.GetEnumerable())
            {
                item.KnownType = Type;
            }
        }

        public bool ExcludeVar { get; set; }

        //private bool isIgnoreInitialize;
        //public bool IsIgnoreInitialize
        //{
        //    get { return isIgnoreInitialize; }
        //    set {
        //        isIgnoreInitialize = value;
        //        foreach (var item in Variables.GetEnumerable())
        //        {                    
        //            item.IsIgnoreInitialize = value;
        //        }
        //    }
        //}

        protected override string InnerTranslate()
        {
            if (ExcludeVar)
                return string.Format( "{0}", Variables.Translate() );
            return string.Format( "var {0}", Variables.Translate() );
        }
    }
}
