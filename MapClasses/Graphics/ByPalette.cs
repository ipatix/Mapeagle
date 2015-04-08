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
    /// An overworld palette which
    /// contains sixteen color entries
    /// and a byte as index number.
    /// </summary>
    public struct ByPalette {
        // Public variables
        public long offset;
        public byte index;
        public Color[] colors;

        // Constructor
        /// <summary>
        /// Reads the palette index and offset
        /// and loads the palette from this offset.
        /// </summary>
        public ByPalette(Rom romfile, long off) {
            offset = off;
            romfile.Seek(off);

            // Checks if the pointer to
            // the palette is valid.
            long data = romfile.ReadPointer();
            if (romfile.CheckOffset(data)) {
                index = romfile.ReadByte();
                colors = romfile.ReadPalette(data,16);
            } else {
                index = 0;
                colors = null;
                ErrorLog.AddError(GetPalError(off));
            }
        }

        /// <summary>
        /// Receives the error which occurs
        /// when the palette offset is invalid.
        /// </summary>
        private string GetPalError(long offset) {
            return "Ow-Palette loading failed. Class: " +
                   "ByPalette.cs. Details: The palette " +
                   "offset 0x" + offset.ToString("X") + " is invalid.";
        }
    }
}