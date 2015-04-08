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
    /// Defines a block which is, by definition,
    /// 2x2 tiles wide => that is 16x16 pixels.
    /// </summary>
    public struct Block : IUndoable {
        // Undo/redo implementation
        Stack<Dictionary<int,Tile>> undostack;
        Stack<Dictionary<int,Tile>> redostack;

        // Lower layer
        public Tile lowupperleft;
        public Tile lowupperright;
        public Tile lowlowerleft;
        public Tile lowlowerright;

        // Upper layer
        public Tile upupperleft;
        public Tile upupperright;
        public Tile uplowerleft;
        public Tile uplowerright;

        /// <summary>
        /// Fully constructs a new block. Has a storage
        /// function and is useful for undo/redoing.
        /// </summary>
        public Block(Tile l1, Tile l2, Tile l3, Tile l4,
                     Tile h1, Tile h2, Tile h3, Tile h4) {
            lowupperleft = l1;
            lowupperright = l2;
            lowlowerleft = l3;
            lowlowerright = l4;
            upupperleft = h1;
            upupperright = h2;
            uplowerleft = h3;
            uplowerright = h4;

            undostack = new Stack<Dictionary<int,Tile>>(8);
            redostack = new Stack<Dictionary<int,Tile>>(8);
        }

        // Interface implementation
        /// <summary>
        /// Replaces the private variable at
        /// dictionary index with the dictionary
        /// value and pushes it on the redostack.
        /// </summary>
        public void Undo() {

        }

        /// <summary>
        /// Replaces the private variable at
        /// dictionary index with the dictionary
        /// value and pushes it on the undostack.
        /// </summary>
        public void Redo() {

        }
    }
}