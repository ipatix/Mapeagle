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
    /// Holds a color array of sixteen colors.
    /// We need this structure to implement our
    /// IUndoable interface to undo color changes.
    /// </summary>
    public struct Palette : IUndoable {
        // Private variables
        long offset;
        Color[] colors;

        // Undo/redo implementation
        Stack<Dictionary<int,Color>> undostack;
        Stack<Dictionary<int,Color>> redostack;

        // Public properties
        public long Offset { 
            get { return offset; } 
        }

        public Color[] Colors { 
            get { return colors; } 
        }

        // Constructor
        /// <summary>
        /// Reads out the sixteen color
        /// sized, umcompressed palette.
        /// </summary>
        public Palette(Rom romfile, long off) {
            undostack = new Stack<Dictionary<int,Color>>(32);
            redostack = new Stack<Dictionary<int,Color>>(32);
            colors = romfile.ReadPalette(off, 16);
            offset = off;
        }

        // Interface implementation
        /// <summary>
        /// Undoes the latest operation by
        /// replacing the color array entry
        /// at dictionary index with the
        /// dictionary color and pushes the
        /// color on the redo stack.
        /// </summary>
        public void Undo() {
        }

        /// <summary>
        /// Redoes the latest operation by
        /// replacing the color array entry
        /// at dictionary index with the
        /// dictionary color and pushes
        /// the color on the undo stack.
        /// </summary>
        public void Redo() {
        }
    }
}