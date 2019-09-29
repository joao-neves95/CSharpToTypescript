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
    public class VariableDeclaratorTranslation : CSharpSyntaxTranslation
    {
        public new VariableDeclaratorSyntax Syntax
        {
            get { return (VariableDeclaratorSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public TokenTranslation Identifier { get; set; }
        public EqualsValueClauseTranslation Initializer { get; set; }

        public VariableDeclaratorTranslation(VariableDeclaratorSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Identifier = syntax.Identifier.Get( this );
            Initializer = syntax.Initializer.Get<EqualsValueClauseTranslation>( this );
        }

        public TypeTranslation KnownType { get; set; }
        public TypeTranslation FirstType { get; set; }
        public bool IsIgnoreInitialize { get; set; }

        public string GetInitializerStr()
        {
            string initializerStr;
            if (Initializer != null)
            {
                initializerStr = Initializer.Translate();
            }
            else
            {
                if (KnownType != null)
                {

                    var defaultTYpe = Helper.GetDefaultValue( KnownType );
                    // null is the same with undefined, don't care :)
                    if (defaultTYpe == "null")
                    {
                        initializerStr = "";
                    }
                    else
                    {
                        initializerStr = $" = {defaultTYpe}";
                    }

                }
                else
                {
                    initializerStr = " ";
                }
            }

            return initializerStr;
        }

        protected override string InnerTranslate()
        {
            string initializerStr = "";

            if (!IsIgnoreInitialize)
            {
                initializerStr = GetInitializerStr();
            }

            if (FirstType == null)
            {
                return string.Format( "{0} {1}", Identifier.Translate(), initializerStr );
            }
            else
            {
                return string.Format( "{0}:{1} {2}", Identifier.Translate(), FirstType.Translate(), initializerStr );
            }
        }
    }
}
