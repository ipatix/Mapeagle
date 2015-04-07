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
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace Mapeagle.MapClasses.Interface {
    /// <summary>
    /// Defines an interface which makes
    /// a class or structure undoable. This
    /// means that an old value is put on a
    /// list and can be restored when using
    /// the undo function in the main form.
    /// The advantage is that we don't need
    /// any slow methods in the rom class.
    /// </summary>
    public interface IUndoable {
        void Undo();
        void Redo();
    }
}