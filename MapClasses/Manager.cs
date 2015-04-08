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
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

using Gamecube.Core.IO;
using Gamecube.Pokemon;
using Mapeagle.UserInterface;
using Mapeagle.MapClasses.Data;
using Mapeagle.MapClasses.Safety;
using Mapeagle.MapClasses.Graphics;
using Mapeagle.MapClasses.Interface;

namespace Mapeagle.MapClasses {
    /// <summary>
    /// Manages the loading of all structures
    /// from the ROM and stores them as variables.
    /// </summary>
    public sealed class Manager {
        // Pointer offsets
        long bankpointer;
        long tilesetpointer;
        long mapnamespointer;
        long flypospointer;
        long wildpokepointer;
        long pokenamespointer;
        long itemspointer;
        long spritepointer;
        long spritecountoffset;
        long spritepalettepointer;

        // Header offsets and offsets
        long freespace;
        long bankheader;
        long tilesetheader;
        long mapnamesheader;
        long flyposheader;
        long wildpokeheader;
        long pokenamesheader;
        long itemsheader;
        long spritecount;
        long spriteheader;
        long spritepaletteheader;

        // Previous values
        int prevbankcount;
        int[] prevmapcount;
        int prevtilesetcount;
        int prevwildpokecount;

        // Instance variables
        /// <summary>
        /// The reference to the
        /// currently used ROM.
        /// </summary>
        Rom romfile;
        /// <summary>
        /// Holds all the tilesets.
        /// </summary>
        Tileset[] tilesets;
        /// <summary>
        /// Holds all the wildpokemon.
        /// </summary>
        WildPokeTable[] wildpokemon;
        /// <summary>
        /// Holds all the overworlds.
        /// </summary>
        Overworld[] overworlds;
        /// <summary>
        /// Holds all overworld palettes.
        /// </summary>
        ByPalette[] owpalettes;
        /// <summary>
        /// Holds all the pokemon names.
        /// </summary>
        String[] pokenames;
        /// <summary>
        /// The reference to the
        /// bound loading screen.
        /// </summary>
        LoadForm loadscreen;
        /// <summary>
        /// The current manager state.
        /// </summary>
        ClassState state;

        // Public properties
        public Tileset[] Tilesets { get { return tilesets; } }
        public String[] PokeNames { get { return pokenames; } }
        public ClassState ClassState { get { return state; } }
        public WildPokeTable[] WildPokemon { get { return wildpokemon; } }
        
        // Constructor
        /// <summary>
        /// Attempts to load all data.
        /// </summary>
        public Manager(Form main, string path) {
            // Opens the ROM, copies it twice
            // and checks if loaded correctly.
            romfile = new Rom(path);
            ErrorLog.ClearLog();

            if (romfile.State == State.Initialized) {
                if (LoadConfiguration()) {
                    // Begins loading every data from
                    // the ROM, without multithreading
                    // as done in the previous map-editor.
                    // We do not need performance that much
                    // because we have a loading screen now.
                    LoadScreen(main);
                    LoadTilesetData();
                    LoadWildPokemonData();
                    LoadPokemonNameData();
                    LoadOverworldSpriteData();
                    LoadOverworldPaletteData();
                    LoadItemNamesData();
                    LoadMapBankData();
                    LoadTilesetBlocks();
                } else {
                    // An error occured while trying
                    // to load the INI: The INI is
                    // either not existing or the
                    // XML has been destroyed.
                    ErrorLog.AddError(GetConfString());
                    state = ClassState.Error;
                    romfile.Dispose();
                    romfile = null;
                }
            } else {
                // Either the path was incorrect or
                // a System.IO.IOException occured.
                // This is because the ROM might be
                // opened in another application.
                ErrorLog.AddError(GetInitError(path));
                state = ClassState.Error;
                romfile.Dispose();
                romfile = null;
            }
        }

        /// <summary>
        /// Tries the load the offsets
        /// from the XML data structure.
        /// </summary>
        private bool LoadConfiguration() {
            // Builds the configuration path.
            // Constants defined for easy portability.
            const string folder = @"\Konfiguration\";
            const string extension = ".xml";
            string code = romfile.Code;
            string app = Environment.CurrentDirectory;
            string path = app+folder+code+extension;

            // Checks if the INI is existing
            // for this version of the ROM.
            if (File.Exists(path)) {
                // Using an xml document avoids
                // creating an XML stream and thus
                // increasing the performance a bit.
                var doc = new XmlDocument();
                    doc.Load(path);
                var first = doc.FirstChild;
                var nodes = first.ChildNodes;

                // Tries to read the offsets
                // from each XML child node.
                try {
                    freespace = GetOffset(nodes[0].InnerText);
                    bankpointer = GetOffset(nodes[1].InnerText);
                    tilesetpointer = GetOffset(nodes[2].InnerText);
                    mapnamespointer = GetOffset(nodes[3].InnerText);
                    flypospointer = GetOffset(nodes[4].InnerText);
                    wildpokepointer = GetOffset(nodes[5].InnerText);
                    pokenamespointer = GetOffset(nodes[6].InnerText);
                    itemspointer = GetOffset(nodes[7].InnerText);
                    spritepointer = GetOffset(nodes[8].InnerText);
                    spritepalettepointer = GetOffset(nodes[9].InnerText);
                    spritecountoffset = GetOffset(nodes[10].InnerText);

                    // Now tries to read some pointers. In case the
                    // reading fails because of invalid or null pointers,
                    // an exception will be caught and sent to the errorlog.
                    romfile.Seek(bankpointer);
                    bankheader = romfile.ReadPointer();
                    romfile.Seek(tilesetpointer);
                    tilesetheader = romfile.ReadPointer();
                    romfile.Seek(mapnamespointer);
                    mapnamesheader = romfile.ReadPointer();
                    romfile.Seek(flypospointer);
                    flyposheader = romfile.ReadPointer();
                    romfile.Seek(wildpokepointer);
                    wildpokeheader = romfile.ReadPointer();
                    romfile.Seek(pokenamespointer);
                    pokenamesheader = romfile.ReadPointer();
                    romfile.Seek(itemspointer);
                    itemsheader = romfile.ReadPointer();
                    romfile.Seek(spritepointer);
                    spriteheader = romfile.ReadPointer();
                    romfile.Seek(spritepalettepointer);
                    spritepaletteheader = romfile.ReadPointer();
                    romfile.Seek(spritecountoffset);
                    spritecount = romfile.ReadByte();
                    return true;
                } catch {
                    // An error occured, someone
                    // destroyed the XML data!
                    return false;
                }
            } else {
                // File does not exist
                // so we end execution.
                return false;
            }
        }

        /// <summary>
        /// Creates the loadscreen which is used
        /// to inform the user about what data
        /// is currently beeing read from ROM.
        /// </summary>
        private void LoadScreen(Form main) {
            loadscreen = new LoadForm(main);
            loadscreen.Location = new Point((main.Width/2)-(loadscreen.
                Width/2),(main.Height/2)-(loadscreen.Height/2));
        }

        /// <summary>
        /// Loads the tilesets dynamically by
        /// recursivly receiving the tileset count.
        /// </summary>
        private void LoadTilesetData() {
            prevtilesetcount = GetTilesetCount();
            if (prevtilesetcount != 0) {
                tilesets = new Tileset[prevtilesetcount];
            } else {
                ErrorLog.AddError(GetTilesetError());
            } SetStep(2);

            // If retrieving the length was successful
            // all tilesets will be loaded in a loop.
            for (int i = 0; i < prevtilesetcount; i++) {
                tilesets[i] = new Tileset(romfile,
                    tilesetheader + i * 24);
            }
        }

        /// <summary>
        /// Loads the wildpokemon dynamically by
        /// retrieving the wildpokemon count first.
        /// </summary>
        private void LoadWildPokemonData() {
            prevwildpokecount = GetWildPokeCount();
            if (prevwildpokecount != 0) {
                wildpokemon = new WildPokeTable[prevwildpokecount];
            } else {
                ErrorLog.AddError(GetWildPokeError());
            } SetStep(4);

            // If retrieving the length was successful
            // all wildpokemon will be loaded in a loop.
            for (int i = 0; i < prevwildpokecount; i++) {
                wildpokemon[i] = new WildPokeTable(romfile,
                    wildpokeheader + i * 20);
            }
        }

        /// <summary>
        /// Loads the pokemon names dynamically by
        /// retrieving the pokemon names count first.
        /// </summary>
        private void LoadPokemonNameData() {
            int namecount = GetPokemonNamesCount();
            if (namecount != 0) {
                pokenames = new String[namecount];
            } else { 
                ErrorLog.AddError(GetPokeNameError());
            } SetStep(5);

            // If retrieving the length was successful
            // all pokemon names will be loaded in a loop.
            for (int i = 0; i < namecount; i++) {
                romfile.Seek(pokenamesheader+i*0xB);
                pokenames[i] = romfile.ReadString();
            }
        }

        /// <summary>
        /// Loads the overworld data. As we have
        /// a limiting byte in the ROM, it is not
        /// needed to retrieve the count recursivly.
        /// </summary>
        private void LoadOverworldSpriteData() {
            SetStep(6);
            overworlds = new Overworld[spritecount];
            for (int i = 0; i < spritecount; i++) {
                // Reads the offset and checks
                // if the offset is valid or not.
                romfile.Seek(spriteheader+i*4);
                long header = romfile.ReadPointer();
                if (romfile.CheckOffset(header)) {
                    overworlds[i] = new Overworld(romfile,header);
                } else {
                    ErrorLog.AddError(GetOwHeaderError(i));
                }
            }
        }

        /// <summary>
        /// Loads the overworld palette data dynamically
        /// by retrieving the palette count recursivly.
        /// </summary>
        private void LoadOverworldPaletteData() {
            int palcount = GetOwPaletteCount();
            if (palcount != 0) {
                owpalettes = new ByPalette[palcount];
            } else {
                ErrorLog.AddError(GetOwPaletteError());
            } SetStep(8);
        }

        /// <summary>
        /// The overworld palette structure
        /// ends with two words of value 0x0.
        /// </summary>
        private int GetOwPaletteCount() {
            romfile.Seek(spritepaletteheader);
            SetStep(7);

            int count = 0;
            while (true) {
                if (romfile.ReadWord() == 0 &&
                    romfile.ReadWord() == 0) {
                    break;
                } count++;
            }

            return count;
        }

        /// <summary>
        /// Receives the pokemon names count
        /// by checking when the given con-
        /// ditions are given. An entry is
        /// a total of 0xB bytes and MUST
        /// contain a 0xFF byte within this
        /// range. It should also be a latin
        /// letter and should not be zero.
        /// </summary>
        private int GetPokemonNamesCount() {
            SetStep(4);
            int count = 0;
            while (true) {
                romfile.Seek(pokenamesheader+count*0xB);
                byte first = romfile.ReadByte();
                if (first == 0xFF || first < 0xA0 ||
                    !HasFF(romfile.Read(0xB))) {
                    break;
                } count++;
            }

            return count;
        }

        /// <summary>
        /// Returns a value that indicates whether
        /// the specified array contains 0xFF bytes.
        /// </summary>
        private bool HasFF(byte[] array) {
            // Not the best performance but more readable.
            // As mentioned before, it is not so important.
            for (int i = 0; i < array.GetLength(0); i++) {
                if (array[i] == 0xFF) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Receives the wildpoke count by
        /// checking when the two ending
        /// words 0xFFFF and 0x0 are given.
        /// </summary>
        private int GetWildPokeCount() {
            SetStep(3);
            int count = 0;
            while (true) {
                romfile.Seek(wildpokeheader+count*20);
                // The value 0xFFFF0000 indicates
                // the end of the wildpoke data.
                if (romfile.ReadWord() == 0xFFFF &&
                    romfile.ReadWord() == 0x0000) {
                    break;
                } count++;
            }
            
            return count;
        }

        /// <summary>
        /// Receives the tileset count by
        /// checking whether the next tile-
        /// set contains valid data or not.
        /// </summary>
        private int GetTilesetCount() {
            SetStep(0);
            int count = 0;
            while (true) {
                romfile.Seek(tilesetheader+count*24);

                // Checks if the determining bytes
                // match the conditions. The first
                // two bytes must be either 0 or 1
                // and the second two bytes are 0.
                if (romfile.ReadByte() > 1  ||
                    romfile.ReadByte() > 1  ||
                    romfile.ReadByte() != 0 ||
                    romfile.ReadByte() != 0) {
                    break;
                } count++;
            }

            return count;
        }

        /// <summary>
        /// Cleans up the manager, mostly
        /// used for reloading a ROM file.
        /// </summary>
        public void Dispose() {
            if (romfile != null) {
                romfile.Dispose();
            }

            // Indicates a disposal.
            state = ClassState.Disposed;
        }

        /// <summary>
        /// Sets the step by passing the current operation
        /// index to the method. Comparison made via switch.
        /// </summary>
        /// <param name="step"></param>
        private void SetStep(int step) {
            // Uses a switch for simplicity.
            switch (step) {
                #region conditional branch
                case 0:
                    loadscreen.SetStep("Ermittelt Tileset-Anzahl");
                    break;
                case 1:
                    loadscreen.SetStep("Lädt Tilesetdaten");
                    break;
                case 2:
                    loadscreen.SetStep("Ermittelt Wilde-Pokémon-Anzahl");
                    break;
                case 3:
                    loadscreen.SetStep("Lädt Wilde-Pokémon-Daten");
                    break;
                case 4:
                    loadscreen.SetStep("Ermittelt Anzahl Pokémon");
                    break;
                case 5:
                    loadscreen.SetStep("Lädt Pokémon-Daten");
                    break;
                case 6:
                    loadscreen.SetStep("Lädt Overworlddaten");
                    break;
                case 7:
                    loadscreen.SetStep("Ermittelt OW-Palettenanzahl");
                    break;
                case 8:
                    loadscreen.SetStep("Lädt OW-Palettendaten");
                    break;
                case 9:
                    loadscreen.SetStep("Lädt Itemnamen");
                    break;
                case 10:
                    loadscreen.SetStep("Ermittelt Bankanzahl");
                    break;
                case 11:
                    loadscreen.SetStep("Lädt Mapdaten");
                    break;
                case 12:
                    loadscreen.SetStep("Lädt Tilesetnummern");
                    break;
                case 13:
                    loadscreen.SetStep("Lädt Wilde-Pokémonnummern");
                    break;
                case 14:
                    loadscreen.SetStep("Lädt alle Tilesetblöcke");
                    break;
                #endregion
            }
        }

        /// <summary>
        /// Reads an offset from
        /// a given XML string.
        /// </summary>
        private long GetOffset(string xml) {
            // We're not using tryparse because
            // we're already used try and catch.
            return long.Parse(xml.Substring(2),
                NumberStyles.HexNumber)-0x08000000;
        }

        /// <summary>
        /// Gets the error when the ow
        /// palette count is zero.
        /// </summary>
        private string GetOwPaletteError() {
            return "Ow-Palette loading failed. Class: " +
                   "Manager.cs. Details: The palette " +
                   "count was negative or zero.";
        }

        /// <summary>
        /// Gets the error when the offset
        /// of the overworld header is invalid.
        /// </summary>
        private string GetOwHeaderError(int i) {
            return "Overworld loading failed. Class: " +
                   "Manager.cs. Details: The overworld " +
                   "header offset at entry " + i + " is invalid.";
        }

        /// <summary>
        /// Gets the error when the initialization
        /// of the tileset array failed due to a
        /// negative or zero count of tilesets.
        /// </summary>
        private string GetTilesetError() {
            return "Tileset loading failed. Class: " +
                   "Manager.cs. Details: The tileset " +
                   "count was negative or zero.";
        }

        /// <summary>
        /// Gets the error when the initialization
        /// of the wildpoke array failed due to a
        /// negative or zero count of wildpokemon.
        /// </summary>
        private string GetWildPokeError() {
            return "Wildpokemon loading failed. Class: " +
                   "Manager.cs. Details: The wildpokemon " +
                   "count was negative or zero.";
        }

        /// <summary>
        /// Gets the error when the initialization
        /// of the pokename array failed due to a
        /// negative or zero count of pokemon names.
        /// </summary>
        private string GetPokeNameError() {
            return "Pokename loading failed. Class: " +
                   "Manager.cs Details: The pokename " +
                   "count was negative or zero.";
        }

        /// <summary>
        /// Gets the error when the initialization
        /// of the configuration file failed.
        /// </summary>
        private string GetConfString() {
            // Retrieves all path components.
            string app = Environment.CurrentDirectory;
            string sub = @"\Konfiguration\";
            string code = romfile.Code;
            string ext = ".xml";

            // Combines the path components.
            return "INI loading failed. Class: " +
                   "Manager.cs. Path: "+app+sub+code+ext;
        }

        /// <summary>
        /// Gets the error when the initialization
        /// of one of the ROM files failed.
        /// </summary>
        private string GetInitError(string path) {
            return "ROM loading failed. Class: " +
                   "Manager.cs. Path: " + path;
        }
    }
}