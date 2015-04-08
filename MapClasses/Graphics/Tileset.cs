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
    /// Holds the tileset data such as image,
    /// palettes, block data and animation.
    /// </summary>
    public class Tileset : IUndoable {
        // Header offsets and offsets
        long offset;
        byte encoded;
        byte number;
        long imageoffset;
        long paletteoffset;
        long blockoffset;
        long animoffset;
        long taskoffset;
        int previmagesize;

        // Interface implementation
        Stack<Bitmap> undostack;
        Stack<Bitmap> redostack;

        // Instance variables
        /// <summary>
        /// The actual tileset image.
        /// </summary>
        Bitmap image;
        /// <summary>
        /// Contains the tileset palettes which
        /// can be six or seven, depending on the
        /// game and tileset identifier number.
        /// </summary>
        Palette[] palettes;
        /// <summary>
        /// Contains the tileset blocks which
        /// each are 2x2 tiles wide => 16x16px
        /// </summary>
        Block[] blocks;

        // Public properties
        public Bitmap Image { get { return image; } }
        public Block[] Blocks { get { return blocks; } }
        public Palette[] Palettes { get { return palettes; } }

        // Constructor
        /// <summary>
        /// Loads all tileset data 
        /// from a specific offset.
        /// </summary>
        public Tileset(Rom romfile, long off) {
            // Reads the determination bytes
            // which are responsible for
            // compression and tileset-#.
            offset = off;
            romfile.Seek(off);
            encoded = romfile.ReadByte();
            number = romfile.ReadByte();
            romfile.Seek(romfile.Offset+2);

            // Reads the pointers, the may
            // not be negative or above size.
            imageoffset = romfile.ReadPointer();
            paletteoffset = romfile.ReadPointer();
            blockoffset = romfile.ReadPointer();

            // Tasks and animations are
            // swapped in FR/LG and RSE!
            if (romfile.IsFR) {
                animoffset = romfile.ReadPointer();
                taskoffset = romfile.ReadPointer();
            } else {
                taskoffset = romfile.ReadPointer();
                animoffset = romfile.ReadPointer();
            }

            // Checks if the tileset is valid.
            if (!romfile.CheckOffset(imageoffset)   ||
                !romfile.CheckOffset(paletteoffset) ||
                !romfile.CheckOffset(blockoffset)   ||
                !romfile.CheckOffset(taskoffset))   {
                ErrorLog.AddError(GetOffsetError());
                return;
            }

            // Constructs the tileset image
            // based on the encoding (LZ77).
            if (encoded == 0) {
                // Unfortunately, the block count
                // for each game is not unique, FR
                // tilesets have different lengths.
                // Total size is always 0xFFFF!
                if (romfile.IsFR) {
                    if (number == 0) {
                        image = romfile.ReadImage(imageoffset,
                            40960, true, 128, Plain.Color16);
                    } else {
                        image = romfile.ReadImage(imageoffset,
                            24576, true, 128, Plain.Color16);
                    }
                } else {
                    image = romfile.ReadImage(imageoffset,
                        32768, true, 128, Plain.Color16);
                }
            } else {
                // Compressed image have a predefined
                // size, defined in the LZ77 header!
                image = romfile.ReadImage(imageoffset,
                    true, 128, out previmagesize, Plain.Color16);
            }

            // Strangely enough, FR has a different
            // amount of palettes for tileset1 than
            // RSE. But they also differ for tileset2!
            if (romfile.IsFR) {
                if (number == 0) {
                    palettes = new Palette[7];
                } else {
                    palettes = new Palette[6];
                }
            } else {
                if (number == 0) {
                    palettes = new Palette[6];
                } else {
                    palettes = new Palette[7];
                }
            }

            int i;
            // Now reads each palette. They are uncompressed
            // and have sixteen color entries, thus 32 bytes.
            for (i = 0; i < palettes.GetLength(0); i++) {
                palettes[i] = new Palette(romfile,
                    paletteoffset + i * 32);
            }

            // Reads the block data. Again, FR has a
            // different amount of blocks than RSE.
            if (romfile.IsFR) {
                if (number == 0) {
                    blocks = new Block[0x280];
                } else {
                    blocks = new Block[0x180];
                }
            } else {
                blocks = new Block[0x200];
            }

            // All blocks are stored one by one
            // so we only need to seek once.
            romfile.Seek(blockoffset);
            for (i = 0; i < blocks.GetLength(0); i++) {
                blocks[i] = new Block(
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()),
                    new Tile(romfile.ReadHWord()));
            }
        }

        /// <summary>
        /// Pops the last element of
        /// the undostack and sets it
        /// as the new tileset image.
        /// </summary>
        public void Undo() {

        }

        /// <summary>
        /// Pops the last element of
        /// the redostack and sets it
        /// as the new tileset image.
        /// </summary>
        public void Redo() {

        }

        /// <summary>
        /// Receives the error log string which
        /// contains all offsets regardingless
        /// of if they are valid or invalid.
        /// </summary>
        private string GetOffsetError() {
            return "Tileset loading failed. Class: " +
                   "Tileset.cs. Offset: " + offset.ToString("X") +
                   " Image: " + imageoffset.ToString("X") +
                   " Palette: " + paletteoffset.ToString("X") +
                   " Blocks: " + blockoffset.ToString("X") +
                   " Tasks: " + taskoffset.ToString("X");
        }
    }
}