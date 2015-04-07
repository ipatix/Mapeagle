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
    /// A banner which is styled in the toolstrip
    /// colors. It defines a heading or title.
    /// </summary>
    public class MapBanner : Control {
        /// <summary>
        /// Modifies control styles and font.
        /// </summary>
        public MapBanner() {
            SetStyle(GetStyles(), true);
            Font = GetEditorFont(14.0f);
        }

        /// <summary>
        /// Receives optimal settings
        /// for a custom user control.
        /// </summary>
        private ControlStyles GetStyles() {
            return ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.UserPaint;
        }

        /// <summary>
        /// Receives an universal font with
        /// the specified size for this editor.
        /// </summary>
        private Font GetEditorFont(float size) {
            return new Font("Segoe UI Semibold",
                size, FontStyle.Regular);
        }

        /// <summary>
        /// Paints the two-colored background. Each
        /// side has the same height and is drawn
        /// with the same style as the toolstrip's.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e) {
            Rectangle lowrect = new Rectangle(0,Height/2,Width,Height/2);
            Rectangle highrect = new Rectangle(0,0,Width,Height/2);
            Point linestart = new Point(Width-1,Height-1);
            Point lineend = new Point(0,Height-1);

            e.Graphics.FillRectangle(MenuPaint.ToolBgHi, highrect);
            e.Graphics.FillRectangle(MenuPaint.ToolBgLo, lowrect);
            e.Graphics.DrawLine(MenuPaint.ToolLine, linestart, lineend);
        }

        /// <summary>
        /// Paints the overlaying text and its
        /// shadow with an offset of (-1, 1).
        /// </summary>
        protected override void OnPaint(PaintEventArgs e) {
            TextFormatFlags flags = 
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.HorizontalCenter;

            Rectangle shadow = new Rectangle(-1,1,Width,Height-1);
            Color shadowcolor = Color.FromArgb(111,140,168);
            Color normalcolor = Color.FromArgb(250,250,250);

            TextRenderer.DrawText(e.Graphics,Text,Font,shadow,shadowcolor,flags);
            TextRenderer.DrawText(e.Graphics,Text,Font,ClientRectangle,normalcolor,flags);
        }
    }
}