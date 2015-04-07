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
    /// <summary>
    /// Modifies the existing toolstrip
    /// and adds a fresh style to it.
    /// </summary>
    public class MapToolStrip : ToolStrip {
        /// <summary>
        /// Enables double-buffering and
        /// sets our custom renderer.
        /// </summary>
        public MapToolStrip() {
            DoubleBuffered = true;
            Renderer = new MapToolRenderer(); 
        }
    }

    /// <summary>
    /// Controls all the rendering of
    /// the custom map-toolstrip.
    /// </summary>
    public class MapToolRenderer : ToolStripRenderer {
        /// <summary>
        /// Renders the strip's main client area.
        /// </summary>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
            Int32 width = e.AffectedBounds.Width, height = e.AffectedBounds.Height;
            Rectangle lowrect = new Rectangle(0,height/2,width,height/2);
            Rectangle highrect = new Rectangle(0,0,width,height/2);
            Point linestart = new Point(width-1,height-1);
            Point lineend = new Point(0,height-1);

            e.Graphics.FillRectangle(MenuPaint.ToolBgHi, highrect);
            e.Graphics.FillRectangle(MenuPaint.ToolBgLo, lowrect);
            e.Graphics.DrawLine(MenuPaint.ToolLine, linestart, lineend);
        }

        /// <summary>
        /// Renders the background of a button
        /// item on the toolstrip control.
        /// </summary>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            if (e.Item.Pressed) {
                // Occurs when the user is pressing
                // the left mouse button on the item.
                Rectangle backbounds = new Rectangle(2,2,e.Item.Width-4,e.Item.Height-4);
                Rectangle borderbounds = new Rectangle(1,1,e.Item.Width-3,e.Item.Height-3);

                e.Graphics.FillRectangle(MenuPaint.MenuClBk, backbounds);
                e.Graphics.DrawRectangle(MenuPaint.MenuClBd, borderbounds);
            } else if (e.Item.Selected) {
                // Occurs when the user is hovering
                // the mouse over the item bounds.
                Rectangle backbounds = new Rectangle(2,2,e.Item.Width-4,e.Item.Height-4);
                Rectangle borderbounds = new Rectangle(1,1,e.Item.Width-3,e.Item.Height-3);

                e.Graphics.FillRectangle(MenuPaint.MenuHvBk, backbounds);
                e.Graphics.DrawRectangle(MenuPaint.MenuHvBd, borderbounds);
            }
        }

        /// <summary>
        /// Renders the background of a
        /// drop down button item.
        /// </summary>
        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
            if (e.Item.Pressed) {
                // Occurs when the user is pressing
                // the left mouse button on the item.
                Rectangle backbounds = new Rectangle(1,2,e.Item.Width-2,e.Item.Height-4);
                Rectangle borderbounds = new Rectangle(0,1,e.Item.Width-1,e.Item.Height-3);

                e.Graphics.FillRectangle(MenuPaint.MenuClBk, backbounds);
                e.Graphics.DrawRectangle(MenuPaint.MenuClBd, borderbounds);
            } else if (e.Item.Selected) {
                // Occurs when the user is hovering
                // the mouse over the item bounds.
                Rectangle backbounds = new Rectangle(1,2,e.Item.Width-2,e.Item.Height-4);
                Rectangle borderbounds = new Rectangle(0,1,e.Item.Width-1,e.Item.Height-3);

                e.Graphics.FillRectangle(MenuPaint.MenuHvBk, backbounds);
                e.Graphics.DrawRectangle(MenuPaint.MenuHvBd, borderbounds);
            }
        }

        /// <summary>
        /// Renders the submenu's background.
        /// </summary>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) {
            Int32 width = e.ToolStrip.Width, height = e.ToolStrip.Height;
            Rectangle borderbounds = new Rectangle(0,0,width-1,height-1);
            Rectangle backbounds = new Rectangle(1,1,width-2,height-2);
            LinearGradientBrush  brush = new LinearGradientBrush(e.AffectedBounds,
                MenuPaint.GradientS, MenuPaint.GradientE, LinearGradientMode.Horizontal);
            Point linestart = new Point(e.AffectedBounds.Width, e.AffectedBounds.Height);
            Point lineend = new Point(e.AffectedBounds.Width, 1);

            e.Graphics.FillRectangle(MenuPaint.MenuSuBg, backbounds);
            e.Graphics.DrawRectangle(MenuPaint.MenuSuBd, borderbounds); 
            e.Graphics.DrawLine(MenuPaint.MenuSuBd, linestart, lineend);
            e.Graphics.FillRectangle(brush, e.AffectedBounds); brush.Dispose();
        }

        /// <summary>
        /// Renders the vertical and the horizontal separator.
        /// </summary>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            if (!e.Vertical) {
                Point linestart = new Point(e.Item.Bounds.X,3);
                Point lineend = new Point(e.Item.Bounds.X+e.Item.Bounds.Width,3);
                      e.Graphics.DrawLine(MenuPaint.MenuSuBd, linestart, lineend);
            } else {
                base.OnRenderSeparator(e);
            }
        }

        /// <summary>
        /// Draws the sub menu-item background.
        /// </summary>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            if (e.Item.Enabled) {
                if (e.Item.IsOnDropDown && e.Item.Selected) {
                    Rectangle backbounds = new Rectangle(1,1,e.Item.Width-3,e.Item.Height-2);
                    Rectangle borderbounds = new Rectangle(2,0,e.Item.Width-4,e.Item.Height-1);
                    e.Graphics.FillRectangle(MenuPaint.MenuSuBk, backbounds);
                    e.Graphics.DrawRectangle(MenuPaint.MenuSuBb, borderbounds);
                }
            }
        }
    }
}