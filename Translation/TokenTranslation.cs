/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;

namespace RoslynTypeScript.Translation
{
    public class TokenTranslation : SyntaxTranslation
    {
        private SyntaxToken token;
        public TokenTranslation()
        { }

        public TokenTranslation(SyntaxToken token, SyntaxTranslation parent) : base( null, parent )
        {
            this.token = token;

        }

        protected override string InnerTranslate()
        {
            return Helper.NormalizeVariabeleName( token.ToString() );
        }

        public virtual bool IsEmpty
        {
            get { return token.ToString() == string.Empty; }
        }

        public SyntaxToken Token
        {
            get
            {
                return token;
            }
        }

        public bool TokenEquals(TokenTranslation another)
        {
            return this.ToString() == another.ToString();
        }
    }
}
