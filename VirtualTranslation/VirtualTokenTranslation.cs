/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTypeScript.VirtualTranslation
{
    public class VirtualTokenTranslation :TokenTranslation
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
