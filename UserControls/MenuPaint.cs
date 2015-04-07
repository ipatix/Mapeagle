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

namespace Mapeagle.UserControls {
    /// <summary>
    /// Holds painting equipment which eats a bit
    /// more memory but increases performance!
    /// </summary>
    public static class MenuPaint {
        #region brush declarations

        public static SolidBrush MenuBgHi = new SolidBrush(Color.FromArgb(0xFF, 0x53, 0xA9, 0xFD));
        public static SolidBrush MenuBgLo = new SolidBrush(Color.FromArgb(0xFF, 0x3B, 0x9D, 0xFD));
        public static SolidBrush ToolBgHi = new SolidBrush(Color.FromArgb(0xFF, 0xA7, 0xD3, 0xFF));
        public static SolidBrush ToolBgLo = new SolidBrush(Color.FromArgb(0xFF, 0x9D, 0xC7, 0xF0));
        public static SolidBrush MenuHvBk = new SolidBrush(Color.FromArgb(0xA0, 0x4D, 0x9E, 0xEA));
        public static SolidBrush MenuClBk = new SolidBrush(Color.FromArgb(0xA0, 0x37, 0x91, 0xE5));
        public static SolidBrush MenuSuBg = new SolidBrush(Color.FromArgb(0xFF, 0xF8, 0xF8, 0xF8));
        public static SolidBrush MenuSuBk = new SolidBrush(Color.FromArgb(0x90, 0x53, 0xA9, 0xFD));
        public static SolidBrush TreeNdSl = new SolidBrush(Color.FromArgb(0x80, 0x80, 0x80, 0x80));

        #endregion
        #region color declarations

        public static Color GradientS = Color.FromArgb(0xA0, 0xA7, 0xD3, 0xFF);
        public static Color GradientE = Color.FromArgb(0x60, 0xA7, 0xD3, 0xFF);
        public static Color MenuArrow = Color.FromArgb(0xFF, 0x24, 0x5F, 0x9A);

        #endregion
        #region pen declarations

        public static Pen ToolLine = new Pen(Color.FromArgb(0xFF, 0x66, 0x80, 0x9B));
        public static Pen MenuLine = new Pen(Color.FromArgb(0xFF, 0x24, 0x5F, 0x9A));
        public static Pen MenuHvBd = new Pen(Color.FromArgb(0xA0, 0x24, 0x60, 0x99));
        public static Pen MenuClBd = new Pen(Color.FromArgb(0xA0, 0x1D, 0x4D, 0x7A));
        public static Pen MenuSuBd = new Pen(Color.FromArgb(0xFF, 0x66, 0x80, 0x9B));
        public static Pen MenuSuBb = new Pen(Color.FromArgb(0xFF, 0x30, 0x81, 0xCC));
        public static Pen TreeNdBd = new Pen(Color.FromArgb(0x80, 0x40, 0x40, 0x40));

        #endregion
    }
}