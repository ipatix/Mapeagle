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

using Gamecube.Core.IO;
using Gamecube.Pokemon;
using Mapeagle.UserInterface;
using Mapeagle.MapClasses.Safety;
using Mapeagle.MapClasses.Interface;

namespace Mapeagle.MapClasses.Data {
    /// <summary>
    /// Contains min/max level and pokémon ID.
    /// </summary>
    public struct WildPokeEntry {
        // Private variables
        public byte min;
        public byte max;
        public ushort id;

        // Constructor
        /// <summary>
        /// Stores the variables
        /// into the strucutre.
        /// </summary>
        public WildPokeEntry(byte lo, byte hi, ushort index) {
            min = lo;
            max = hi;
            id = index;
        }
    }
}