/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Translation;

namespace RoslynTypeScript.VirtualTranslation
{
    public class VirtualTokenTranslation : TokenTranslation
    {
        public string TokenStr { get; set; }

        protected override string InnerTranslate()
        {
            return TokenStr;
        }

        public override bool IsEmpty
        {
            get
            {
                return TokenStr == string.Empty;
            }
        }

        public static VirtualTokenTranslation SemicolonToken
        {
            get { return new VirtualTokenTranslation { TokenStr = ";" }; }
        }
    }
}
