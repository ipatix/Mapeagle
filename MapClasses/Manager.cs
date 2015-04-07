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
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

using Gamecube.Core.IO;
using Gamecube.Pokemon;
using Mapeagle.MapClasses.Safety;
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
        /// The current manager state.
        /// </summary>
        ClassState state;

        // Public properties
        public List<String> ItemNames { get { return null; } }
        public ClassState ClassState { get { return state; } }

        /// <summary>
        /// Attempts to load all data.
        /// </summary>
        public Manager(string path) {
            // Opens the ROM, copies it twice
            // and checks if loaded correctly.
            romfile = new Rom(path);
            ErrorLog.ClearLog();

            if (romfile.State == State.Initialized) {
                if (LoadConfiguration()) {

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