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
using Mapeagle.Properties;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Mapeagle.UserControls {
    /// <summary>
    /// Extends functionality and appearance
    /// of the default treeview control by MS.
    /// </summary>
    public class MapTreeView : TreeView {
        /// <summary>
        /// Contains data which tells the user
        /// which map and/or what bank it belongs to.
        /// </summary>
        public struct EditorNode {
            public Int32 Bank;
            public Int32 Map;

            /// <summary>
            /// Initializes a new EditorNode with
            /// the specified bank and map.
            /// </summary>
            public EditorNode(Int32 bk, Int32 mp) {
                Bank = bk;
                Map = mp;
            }
        }

        /// <summary>
        /// The currently selected map and/or bank.
        /// </summary>
        private EditorNode editorNode;
        /// <summary>
        /// The event which is used to fire the actions.
        /// </summary>
        private event EditorNodeEventHandler editorNodeChanged;

        // Images for the plus/minus replacing.
        private Image arrowEx = Resources.Expanded;
        private Image arrowCo = Resources.Collapsed;

        /// <summary>
        /// Defines a public event which we can use
        /// and declare in our main editor form.
        /// </summary>
        public event EditorNodeEventHandler EditorNodeChanged {
            add {
                editorNodeChanged += value;
            } remove {
                editorNodeChanged -= value;
            }
        }

        /// <summary>
        /// Modifies the control styles and sets
        /// the draw mode to owner in order to
        /// modify the drawing in this control.
        /// </summary>
        public MapTreeView() {
            SetStyle(GetStyles(), true);
            BorderStyle = BorderStyle.None;
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            editorNode = new EditorNode(-1,-1);
        }

        /// <summary>
        /// Receives optimal settings
        /// for a custom user control.
        /// </summary>
        private ControlStyles GetStyles() {
            return ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw;
        }

        /// <summary>
        /// Draws a modified selection-background and
        /// displays WPF-treeview arrows instead of
        /// plus-minus and lines like in WinForms.
        /// Also handles the EditorNode events.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e) {
            Rectangle bounds = e.Node.Bounds;
            Boolean hasnodes = e.Node.Nodes.Count != 0;
            Rectangle clear = new Rectangle(0,bounds.Y,Width,bounds.Height);
            SolidBrush brush = new SolidBrush(BackColor); {
                e.Graphics.FillRectangle(brush, clear);
            } brush.Dispose();

            Point arrow = new Point(bounds.X-12,bounds.Y+2); bounds.Width+=32;
            Boolean selected = e.State.HasFlag(TreeNodeStates.Selected);
            Rectangle selection = new Rectangle(0,bounds.Y,Width,bounds.Height);
                  

            if (selected) {
                // Node is beeing pressed by the user
                // thus drawing a selection around it.
                e.Graphics.FillRectangle(MenuPaint.ToolBgLo, selection);

                if (e.Node.Parent != null) {
                    if (e.Node.Parent.Parent != null) {
                        // Two parents -> is a map.
                        editorNode.Map = e.Node.Index;
                    } else {
                        // Only one parent -> is a bank.
                        editorNode.Bank = e.Node.Index;
                        editorNode.Map = -1; // no map!
                    }
                }

                // Throws the node-changed event
                // so we can perform a map-switch.
                if (editorNodeChanged != null)
                    editorNodeChanged(new EditorNodeEventArgs(editorNode));
            } if (hasnodes) {
                // If the parent node is not empty,
                // then we draw an arrow, depending
                // on if its extended or not.
                if (e.Node.IsExpanded) {
                    e.Graphics.DrawImage(arrowEx, arrow);
                } else {
                    e.Graphics.DrawImage(arrowCo, arrow);
                }
            }

            // Finally, draws the text of the node via fast GDI.
            TextRenderer.DrawText(e.Graphics, e.Node.Text, Font, bounds, ForeColor, TextFormatFlags.Default);
        }


        protected override void OnResize(EventArgs e) {
            Invalidate();
        }
    }

    /// <summary>
    /// Defines an eventhandler which contains
    /// the EditorNodeEventArgs, which is what
    /// we only need in that context.
    /// </summary>
    public delegate void EditorNodeEventHandler(EditorNodeEventArgs e);

    /// <summary>
    /// Contains event data which makes
    /// the loading of maps much easier.
    /// </summary>
    public class EditorNodeEventArgs : EventArgs {
        public Boolean IsBankSelected { get; set; }
        public Boolean IsMapSelected { get; set; }
        public Int32 SelectedBank { get; set; }
        public Int32 SelectedMap { get; set; }

        /// <summary>
        /// Constructs a new EditorNodeEvent and
        /// precalculates useful properties.
        /// </summary>
        /// <param name="node"></param>
        public EditorNodeEventArgs(MapTreeView.EditorNode node) {
            SelectedBank = node.Bank;
            SelectedMap = node.Map;
            
            // Checks if any of the 
            // parameters is under 0.
            if (SelectedBank != -1) {
                IsBankSelected = true;
            } if (SelectedMap != -1) {
                IsMapSelected = true;
            }
        }
    }
}