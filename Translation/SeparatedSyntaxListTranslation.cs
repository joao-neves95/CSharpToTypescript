/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoslynTypeScript.Translation
{
    public class SeparatedSyntaxListTranslation<T, ST> : SyntaxListBaseTranslation<T, ST> where T : SyntaxNode where ST : SyntaxTranslation
    {
        private SeparatedSyntaxList<T> separatedSyntaxList;

        public SeparatedSyntaxListTranslation()
        {
            SyntaxCollection = new List<SyntaxTranslation>();
        }

        public SeparatedSyntaxListTranslation(SeparatedSyntaxList<T> separatedSyntaxList, SyntaxTranslation parent) : base( parent )
        {
            this.separatedSyntaxList = separatedSyntaxList;
            this.Parent = parent;
            SyntaxCollection = separatedSyntaxList.Select( f => f.Get<SyntaxTranslation>( this ) ).ToList();
        }

        public bool IsNewLine { get; set; }
        public string Seperator { get; set; }

        protected override string InnerTranslate()
        {
            if (!SyntaxCollection.Any())
            {
                return string.Empty;
            }

            StringBuilder bd = new StringBuilder();

            //int separatorCount = separatedSyntaxList.GetSeparators().Count();
            int separatorCount = SyntaxCollection.Count - 1;
            for (int i = 0; i < SyntaxCollection.Count; i++)
            {
                bd.Append( SyntaxCollection[i].Translate() );
                if (i < separatorCount)
                {
                    bd.Append( Seperator ?? GetSeparator( i ) );
                    if (IsNewLine)
                    {
                        bd.Append( Environment.NewLine );
                    }
                }

            }

            return bd.ToString();
            //return string.Join(",", SyntaxCollection.Select(f => f.Translate()));
        }

        private string GetSeparator(int idx)
        {
            var separator = separatedSyntaxList.GetSeparator( idx );
            return $"{Helper.GetNewLineIfExist( separator.LeadingTrivia )}{separator.ToString()}{Helper.GetNewLineIfExist( separator.TrailingTrivia )}";
        }
    }
}
