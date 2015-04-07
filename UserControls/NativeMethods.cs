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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Mapeagle.UserControls {
    /// <summary>
    /// Helps interacting with native code
    /// and/or native structures.
    /// </summary>
    public static class NativeMethods {
        /// <summary>
        /// Hides or shows the specified scroll bars.
        /// </summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        /// <summary>
        /// An enum which controls the
        /// visibility of scoll bars.
        /// </summary>
        public enum ScrollBar : int {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTRL = 2,
            SB_BOTH = 3
        };

        // Defines a bunch of constants which
        // help checking if the scrollbars are visible.
        public const int WS_VSCROLL = 0x200000;
        public const int WS_HSCROLL = 0x100000;

        /// <summary>
        /// Receives the windowstates.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    }
}