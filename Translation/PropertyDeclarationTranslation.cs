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
    public class PropertyDeclarationTranslation : BasePropertyDeclarationTranslation
    {
        public new PropertyDeclarationSyntax Syntax
        {
            get { return (PropertyDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public TokenTranslation Identifier { get; set; }


        public PropertyDeclarationTranslation(PropertyDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Identifier = syntax.Identifier.Get( this );
        }


        protected override string InnerTranslate()
        {
            var found = TravelUp( f => f is ClassDeclarationTranslation || f is InterfaceDeclarationTranslation );

            // following TypeScript with interface, we just need set as Field
            if (found is InterfaceDeclarationTranslation)
            {
                //return string.Format("{0}: {1}", syntax.Identifier,type.Translate());
                return $"{Helper.GetAttributeList( Syntax.AttributeLists )}{Syntax.Identifier}: {Type.Translate()} ;";
            }

            // hmm, if it's in class, much thing to do

            if (AccessorList.IsShorten())
            {

                var defaultStr = Helper.GetDefaultValue( Type );
                if (defaultStr == "null")
                {
                    defaultStr = string.Empty;
                }
                else
                {
                    defaultStr = " = " + defaultStr;
                }
                return $"{Helper.GetAttributeList( Syntax.AttributeLists )} {Modifiers.Translate()} {Syntax.Identifier}: {Type.Translate()}{defaultStr} ;";
            }

            return AccessorList.Translate();
        }
    }
}
