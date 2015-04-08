// Mapeagle: Pokémon Advance Generation Map Editor
// Written in C#.NET using the .NET Framework 4.5.
// Copyright (c) Gamecube/Laz0r, in year 2014/15.
//
// License for romhacking tools/libraries:
// This tool/library is open source. Everyone can
// look up, modify, update or even share the code.
// Everyone can change the code to one's wishes,
// but only under the following license conditions:
//
// You are allowed to implement new classes.
// You are allowed to share and modify code.
// You are forced to release every change made.
// You are forced to commit to www.github.com.
// You are not allowed to delete author's name.
// You are not allowed to destroy existing code.
// You are forced to release every library/tool
// that is fully or partly based on this tool/lib.
// You are not allowed to release this tool/lib
// to any other website than the author specified.
// You are forced to, in case this is a library, to
// put the output file (.dll) into your bin folder.

using System;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Mapeagle.MapClasses.Safety {
    /// <summary>
    /// Defines multiple plain structures which
    /// are placeholders for things which are
    /// implemented later or loaded later.
    /// </summary>
    public static class Plain {
        /// <summary>
        /// Defines a black color array
        /// of sixteen entries. The alpha
        /// value for each entry is 255.
        /// </summary>
        public static Color[] Color16 = new Color[16] {
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
        };
    }
}