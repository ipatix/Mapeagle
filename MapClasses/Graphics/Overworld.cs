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
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;

using Gamecube.Core.IO;
using Gamecube.Pokemon;
using Mapeagle.MapClasses.Safety;
using Mapeagle.MapClasses.Graphics;

namespace Mapeagle.MapClasses.Graphics {
    /// <summary>
    /// Holds some of the NPC data. We only
    /// store one frame to display to the
    /// user, more is not needed for a map editor.
    /// </summary>
    public struct Overworld {
        // Public variables
        public long offset;
        public ushort width;
        public ushort height;
        public byte palette;
        public Bitmap image;

        // Constructor
        /// <summary>
        /// Reads the NPC data. Also checks
        /// if the overworld is valid or not.
        /// </summary>
        public Overworld(Rom romfile, long off) {
            // The first bytes need to be
            // a halfword of value 0xFFFF!
            offset = off;
            romfile.Seek(off);
            if (romfile.ReadHWord() == 0xFFFF) {
                // Reads the palette, width and height.
                palette = romfile.ReadByte();
                romfile.Seek(romfile.Offset+5);
                width = romfile.ReadHWord();
                height = romfile.ReadHWord();
                romfile.Seek(romfile.Offset+16);

                // Checks if the frame table is valid.
                long table = romfile.ReadPointer();
                if (romfile.CheckOffset(table)) {
                    romfile.Seek(table);

                    // Checks if the frame is valid.
                    long frame = romfile.ReadPointer();
                    if (romfile.CheckOffset(frame)) {
                        image = romfile.ReadImage(frame, width*height,
                            true, width, Plain.Color16);
                    } else {
                        image = null;
                        ErrorLog.AddError(GetImageError(frame));
                    }
                } else {
                    image = null;
                }
            } else {
                width = 0;
                height = 0;
                palette = 0;
                image = null;
                ErrorLog.AddError(GetStartError(off));
            }
        }

        /// <summary>
        /// Receives the NPC error which defines
        /// an invalid NPC data for the starting
        /// bytes, what need to be 0xFFFF.
        /// </summary>
        private string GetStartError(long offset) {
            return "Overworld loading failed: Class: " +
                   "Overworld.cs. Details: The starting " +
                   "bytes were not of value 0xFFFF. " +
                   "At offset: 0x" + offset.ToString("X");
        }

        /// <summary>
        /// Receives the NPC error which defines
        /// an invalid image, which can only be
        /// caused by an invalid pointer.
        /// </summary>
        private string GetImageError(long offset) {
            return "Overworld loading failed: Class: " +
                   "Overworld.cs. Details: The pointer " +
                   "to the image was invalid. Resulting " +
                   "offset is therefore: 0x" + offset.ToString("X");
        }
    }
}