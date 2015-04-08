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
using System.Collections.Generic;

using Gamecube.Core.IO;
using Gamecube.Pokemon;
using Mapeagle.UserInterface;
using Mapeagle.MapClasses.Safety;
using Mapeagle.MapClasses.Interface;

namespace Mapeagle.MapClasses.Data {
    /// <summary>
    /// Holds the wildpokemon areas, their
    /// probability to appear and the bank
    /// and also the map for identifying.
    /// </summary>
    public class WildPokeTable {
        // Private variables
        byte bank;
        byte map;
        long offset;
        byte grassprob;
        byte waterprob;
        byte rockprob;
        byte fishprob;
        long grassoffset;
        long wateroffset;
        long rockoffset;
        long fishoffset;
        long grassdata;
        long waterdata;
        long rockdata;
        long fishdata;

        // Instance variables
        WildPokeEntry[] grass;
        WildPokeEntry[] water;
        WildPokeEntry[] rock;
        WildPokeEntry[] fish;

        // Public properties
        public WildPokeEntry[] Grass { get { return grass; } }
        public WildPokeEntry[] Water { get { return water; } }
        public WildPokeEntry[] Rock { get { return rock; } }
        public WildPokeEntry[] Fish { get { return fish; } }
        public Byte GrassProbability { get { return grassprob; } }
        public Byte WaterProbability { get { return waterprob; } }
        public Byte RockProbability { get { return rockprob; } }
        public Byte FishProbability { get { return fishprob; } }
        public Byte Bank { get { return bank; } }
        public Byte Map { get { return map; } }

        // Constructor
        /// <summary>
        /// Loads the wildpokemon data
        /// of every available area.
        /// </summary>
        public WildPokeTable(Rom romfile, long off) {
            // Reads the main table structure
            // and the pointers to the areas.
            offset = off;
            romfile.Seek(off);
            bank = romfile.ReadByte();
            map = romfile.ReadByte();
            romfile.Seek(romfile.Offset+2);
            grassoffset = romfile.ReadPointer();
            wateroffset = romfile.ReadPointer();
            rockoffset = romfile.ReadPointer();
            fishoffset = romfile.ReadPointer();

            // Initializes the arrays
            // which have fixed sizes!
            grass = new WildPokeEntry[12];
            water = new WildPokeEntry[5];
            rock = new WildPokeEntry[5];
            fish = new WildPokeEntry[10];

            // Now seeks to the areas and
            // loads the actual pokémon data
            // but only if the pointer is not NULL.
            int i;
            if (romfile.CheckOffset(grassoffset)) {
                romfile.Seek(grassoffset);
                grassprob = romfile.ReadByte();
                romfile.Seek(romfile.Offset+3);
                grassdata = romfile.ReadPointer();
                romfile.Seek(grassdata);

                // Checks the offset and
                // then loads the grass data.
                if (romfile.CheckOffset()) {
                    for (i = 0; i < 12; i++) {
                        grass[i] = new WildPokeEntry(romfile.ReadByte(),
                                romfile.ReadByte(), romfile.ReadHWord());
                    }
                } else {
                    ErrorLog.AddError(GetWildPokeError(grassoffset));
                }
            } if (romfile.CheckOffset(wateroffset)) {
                romfile.Seek(wateroffset);
                waterprob = romfile.ReadByte();
                romfile.Seek(romfile.Offset+3);
                waterdata = romfile.ReadPointer();
                romfile.Seek(waterdata);

                // Checks the offset and
                // then loads the water data.
                if (romfile.CheckOffset()) {
                    for (i = 0; i < 5; i++) {
                        water[i] = new WildPokeEntry(romfile.ReadByte(),
                                romfile.ReadByte(), romfile.ReadHWord());
                    }
                } else {
                    ErrorLog.AddError(GetWildPokeError(wateroffset));
                }
            } if (romfile.CheckOffset(rockoffset)) {
                romfile.Seek(rockoffset);
                rockprob = romfile.ReadByte();
                romfile.Seek(romfile.Offset+3);
                rockdata = romfile.ReadPointer();
                romfile.Seek(rockdata);

                // Checks the offset and
                // then loads the water data.
                if (romfile.CheckOffset()) {
                    for (i = 0; i < 5; i++) {
                        rock[i] = new WildPokeEntry(romfile.ReadByte(),
                                romfile.ReadByte(), romfile.ReadHWord());
                    }
                } else {
                    ErrorLog.AddError(GetWildPokeError(rockoffset));
                }
            } if (romfile.CheckOffset(fishoffset)) {
                romfile.Seek(fishoffset);
                fishprob = romfile.ReadByte();
                romfile.Seek(romfile.Offset+3);
                fishdata = romfile.ReadPointer();
                romfile.Seek(fishdata);

                // Checks the offset and
                // then loads the water data.
                if (romfile.CheckOffset()) {
                    for (i = 0; i < 10; i++) {
                        fish[i] = new WildPokeEntry(romfile.ReadByte(),
                                romfile.ReadByte(), romfile.ReadHWord());
                    }
                } else {
                    ErrorLog.AddError(GetWildPokeError(fishoffset));
                }
            }


        }

        /// <summary>
        /// Gets the error when one of
        /// the wildpokemon offsets
        /// contained invalid data.
        /// </summary>
        private string GetWildPokeError(long offset) {
            return "Wildpokemon loading failed. Class: " +
                   "WildPokeTable.cs. Details: The data " +
                   "at 0x" + offset.ToString("X") + "was invalid";
        }
    }
}