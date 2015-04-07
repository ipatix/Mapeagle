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
using System.Drawing.Drawing2D;

namespace Mapeagle.UserControls {
    public class MapPanel : Panel {
        /// <summary>
        /// The color of the border
        /// around the MapPanel.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Modifies control styles
        /// and the border style.
        /// </summary>
        public MapPanel() {
            SetStyle(GetStyles(), true);
            BorderStyle = BorderStyle.None;
            BorderColor = Color.FromArgb(102,128,155);
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
        /// Paints the border around the panel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) {
            Rectangle rect = new Rectangle(0,0,Width-1,Height-1);
                  Pen pen = new Pen(BorderColor);
            e.Graphics.DrawRectangle(pen, rect); pen.Dispose();
        }

        protected override void OnScroll(ScrollEventArgs se) {
            if (se.Type == ScrollEventType.ThumbTrack) {
                NativeMethods.SendMessage(Handle,0xB,1,0); Refresh();
                NativeMethods.SendMessage(Handle,0xB,0,0);
            } else {
                NativeMethods.SendMessage(Handle,0xB,1,0);
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs eventargs) {
            // base.OnResize(eventargs); DONT REDRAW
        }
    }
}