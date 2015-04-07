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
using System.Text;
using System.Security;
using Mapeagle.Properties;
using System.Collections.Generic;

namespace Mapeagle.MapClasses.Safety {
    /// <summary>
    /// Logs every error and displays it
    /// to the user in case he requests it.
    /// Saves the error log file as soon as
    /// the program is terminated.
    /// </summary>
    public static class ErrorLog {
        /// <summary>
        /// The list of error entries. They are simple strings
        /// and shall not contain any newline characters.
        /// </summary>
        private static List<String> errors = new List<String>();

        /// <summary>
        /// Adds an error to the list.
        /// </summary>
        public static void AddError(string message) {
            errors.Add(message);
        }

        /// <summary>
        /// Clears the error log.
        /// </summary>
        public static void ClearLog() {
            errors.Clear();
        }

        /// <summary>
        /// Saves the log in the
        /// working directory.
        /// </summary>
        public static void SaveLog() {
            // Constructs the header of the log
            // and saves it in a stringbuilder.
            var builder = new StringBuilder();
            builder.AppendLine(Resources.ErrorHeader);
            builder.AppendLine("Amount of errors: " + errors.Count);
            builder.AppendLine();
            
            // Outputs each error in the list.
            foreach (var error in errors) {
                builder.AppendLine(error);
            }

            // Saves the stringbuilder object
            // into a text document file.
            File.WriteAllText(Environment.CurrentDirectory +
                        @"\ErrorLog.txt", builder.ToString());
        }
    }
}