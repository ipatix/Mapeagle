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
using System.Security.Permissions;

namespace Mapeagle.UserControls {
    /// <summary>
    /// Changes the appearance of the tabcontrol
    /// completely and also draws its tabpages
    /// with the help of a second backbuffer.
    /// </summary>
    public class MapTabControl : TabControl {
        /// <summary>
        /// Modifies control styles and font.
        /// </summary>
        public MapTabControl() {
            SetStyle(GetStyles(), true);
            Font = GetEditorFont(9.75f);
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
            return new Font("Segoe UI",
                size, FontStyle.Regular);
        }

        /// <summary>
        /// Processes the tabselection.
        /// </summary>
        [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessMnemonic(char charCode) {
            foreach (TabPage page in TabPages) {
                if (IsMnemonic(charCode, page.Text)) {
                    SelectedTab = page;
                    return true;
                }
            } return base.ProcessMnemonic(charCode);
        }

        /// <summary>
        /// Every tabpage that is added will either
        /// be transparent or has THIS backcolor.
        /// </summary>
        protected override void OnControlAdded(ControlEventArgs e) {
            try {
                e.Control.BackColor = Color.Transparent;
            } catch {
                e.Control.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// Modifies the tab rectangle in order
        /// to remove the top padding, which doesn't
        /// fit the other usercontrols styles.
        /// </summary>
        public new Rectangle GetTabRect(int index) {
            Rectangle rect = base.GetTabRect(index); {
                rect.Y -= 2;
                rect.Height += 3;
            } return rect;
        }

        /// <summary>
        /// Overrides OnPaint to hook
        /// our custom drawing.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e) {
            RenderBackground(e.Graphics);
            RenderTabPages(e.Graphics);
        }

        /// <summary>
        /// Renders the background of the tabcontrol.
        /// </summary>
        private void RenderBackground(Graphics g) {
            Int32 width = Width, height = ItemSize.Height+4;
            Rectangle highrect = new Rectangle(0,0,width,height/2);
            Rectangle lowrect = new Rectangle(0,height/2,width,height/2);
            Point linestart = new Point(width-1,height-1);
            Point lineend = new Point(0,height-1);

            g.Clear(SystemColors.Control);
            g.FillRectangle(MenuPaint.ToolBgHi, highrect);
            g.FillRectangle(MenuPaint.ToolBgLo, lowrect);
            g.DrawLine(MenuPaint.ToolLine, linestart, lineend);
        }

        /// <summary>
        /// Renders each visible tabpage and
        /// draws the selected one on top.
        /// </summary>
        private void RenderTabPages(Graphics g) {
            if (TabCount > 0 && TabPages != null) {
                for (int row = 0; row < RowCount; row++) {
                    for (int index = TabCount-1; index >= 0; index--) {
                        RenderTabPage(index, g);
                    }
                }
            } if (SelectedIndex >= 0) {
                // We must render the top-most
                // tabpage in order to display
                // it without any bugs.
                RenderTabPage(SelectedIndex, g);
            }
        }

        /// <summary>
        /// Renders a single tabpage.
        /// </summary>
        private void RenderTabPage(int index, Graphics g) {
            // We render each tabpage and header
            // with the text having a drop shadow.
            Rectangle bounds = GetTabRect(index);
            Color color = Color.FromArgb(111,140,168);
            Color txclr = Color.FromArgb(250,250,250);
            Point linestart = new Point(bounds.X+bounds.Width-1,0);
            Point lineend = new Point(bounds.X+bounds.Width-1,bounds.Height-1);
            Rectangle shadow = new Rectangle(bounds.X-1,bounds.Y+2,bounds.Width,bounds.Height-1);
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;

            if (index == SelectedIndex) {
                // We draw a selection around the tab
                // in case it was selected by the user.
                color = Color.FromArgb(43,112,165);
                RenderSelection(bounds, g);
            }
            
            // Separator between each tabpage.
            g.DrawLine(MenuPaint.ToolLine, linestart, lineend);

            // Renders the shadow and the actual text.
            TextRenderer.DrawText(g,TabPages[index].Text,Font,shadow,color,flags);
            TextRenderer.DrawText(g,TabPages[index].Text,Font,bounds,txclr,flags);
        }

        /// <summary>
        /// Renders a selection around the specified tab.
        /// </summary>
        private void RenderSelection(Rectangle rect, Graphics g) {
            Int32 width = rect.Width, height = rect.Height;
            Rectangle highrect = new Rectangle(rect.X,0,width,height/2);
            Rectangle lowrect = new Rectangle(rect.X,height/2,width,height/2);
            Point linestart = new Point(rect.X+width-1,height);
            Point lineend = new Point(rect.X,height);

            g.FillRectangle(MenuPaint.MenuBgHi, highrect);
            g.FillRectangle(MenuPaint.MenuBgLo, lowrect);
            g.DrawLine(MenuPaint.MenuLine, linestart, lineend);
        }
    }
}
