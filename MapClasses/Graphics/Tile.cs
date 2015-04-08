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

using Gamecube.Core.IO;
using Gamecube.Pokemon;
using Mapeagle.UserInterface;
using Mapeagle.MapClasses.Safety;
using Mapeagle.MapClasses.Interface;

namespace Mapeagle.MapClasses.Graphics {
    /// <summary>
    /// A single 8x8 pixels tile.
    /// </summary>
    public struct Tile {
        // Public variables
        public ushort tile;
        public byte palette;
        public bool xflipped;
        public bool yflipped;

        /// <summary>
        /// Unpacks the clustered data and copies
        /// it to the public class variables.
        /// </summary>
        public Tile(ushort data) {
            tile = (ushort)(data & 0x3FF);
            palette = (byte)((data & 0xF800) >> 0xC);

            // Determines X/Y by comparing
            // which bitfields are applied
            // to the tile. The bitfields are:
            // X: 0x400; Y: 0x800; XY: 0xC00;
            xflipped = ((data & 0x400) == 0x400);
            yflipped = ((data & 0x800) == 0x800);
        }
    }
}