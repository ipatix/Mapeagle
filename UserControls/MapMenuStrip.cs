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
    /// Modifies the existing menustrip
    /// and adds a fresh style to it.
    /// </summary>
    public class MapMenuStrip : MenuStrip {
        /// <summary>
        /// Enables double-buffering and
        /// sets our custom renderer.
        /// </summary>
        public MapMenuStrip() {
            DoubleBuffered = true;
            Renderer = new MapMenuRenderer(); 
        }
    }

    /// <summary>
    /// Controls all the rendering of
    /// the custom map-menustrip.
    /// </summary>
    public class MapMenuRenderer : ToolStripRenderer {
        /// <summary>
        /// Renders the menu's main client area.
        /// </summary>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
            Int32 width = e.AffectedBounds.Width, height = e.AffectedBounds.Height;
            Rectangle lowrect = new Rectangle(0,height/2,width,height/2);
            Rectangle highrect = new Rectangle(0,0,width,height/2);
            Point linestart = new Point(width-1,height-1);
            Point lineend = new Point(0,height-1);

            e.Graphics.FillRectangle(MenuPaint.MenuBgHi, highrect);
            e.Graphics.FillRectangle(MenuPaint.MenuBgLo, lowrect);
            e.Graphics.DrawLine(MenuPaint.MenuLine, linestart, lineend);
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
        /// Renders the horizontal separator.
        /// </summary>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            if (!e.Vertical) {
                Point linestart = new Point(e.Item.Bounds.X+21,3);
                Point lineend = new Point(e.Item.Bounds.X+e.Item.Bounds.Width+21,3);
                      e.Graphics.DrawLine(MenuPaint.MenuSuBd, linestart, lineend);
            }
        }

        /// <summary>
        /// Renders the item text depending on if the item
        /// is selected or pressed or on the sub menu.
        /// </summary>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            if (e.Item.IsOnDropDown && e.Item.Enabled) {
                // Item is in the sub menu, which is white.
                // So we have to draw a kinda dark text.
                e.TextColor = Color.FromArgb(10,10,10);
            } else if (e.Item.Enabled) {
                // The item is not on sub menu and
                // was not disabled by the program.
                // Also draws a drop shadow.
                Rectangle shadowrect = new Rectangle(e.TextRectangle.X-1,e.TextRectangle.Y+1,e.TextRectangle.Width,e.TextRectangle.Height);
                TextRenderer.DrawText(e.Graphics,e.Text,e.TextFont,shadowrect,Color.FromArgb(43,112,165));
                e.TextColor = Color.FromArgb(250,250,250);
            } else {
                // The text is appearantly disabled, so
                // we draw it with a bit darker color.
                e.TextColor = Color.FromArgb(100,100,100);
            } base.OnRenderItemText(e);
        }

        /// <summary>
        /// Modifes the arrow color for the submenus.
        /// </summary>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
            if (e.Item.Visible)
                e.ArrowColor = MenuPaint.MenuArrow;
            base.OnRenderArrow(e);
        }

        /// <summary>
        /// Draws different item backgrounds for
        /// normal menu-items and sub menu-items.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            if (e.Item.Enabled) {
                // Only draws a BG in case
                // the item is enabled!
                if (!e.Item.IsOnDropDown && ((ToolStripMenuItem)e.Item).DropDown.Visible) {
                    // If the item is NOT a sub menu-item and pressed,
                    // so that the sub menu appears, then draw it like this:
                    Rectangle backbounds = new Rectangle(3,1,e.Item.Width-6,e.Item.Height-3);
                    Rectangle borderbounds = new Rectangle(2,0,e.Item.Width-5,e.Item.Height-2);

                    e.Graphics.FillRectangle(MenuPaint.MenuClBk, backbounds);
                    e.Graphics.DrawRectangle(MenuPaint.MenuClBd, borderbounds);
                } else if (!e.Item.IsOnDropDown && e.Item.Selected) {
                    // A menu item that is mouse hovered.
                    Rectangle backbounds = new Rectangle(3,1,e.Item.Width-6,e.Item.Height-3);
                    Rectangle borderbounds = new Rectangle(2,0,e.Item.Width-5,e.Item.Height-2);

                    e.Graphics.FillRectangle(MenuPaint.MenuHvBk, backbounds);
                    e.Graphics.DrawRectangle(MenuPaint.MenuHvBd, borderbounds);
                } else if (e.Item.IsOnDropDown && e.Item.Selected) {
                    // A mouse hovered sub menu-item
                    Rectangle backbounds = new Rectangle(1,1,e.Item.Width-3,e.Item.Height-2);
                    Rectangle borderbounds = new Rectangle(2,0,e.Item.Width-4,e.Item.Height-1);

                    e.Graphics.FillRectangle(MenuPaint.MenuSuBk, backbounds);
                    e.Graphics.DrawRectangle(MenuPaint.MenuSuBb, borderbounds);
                }
            }
        }
    }
}