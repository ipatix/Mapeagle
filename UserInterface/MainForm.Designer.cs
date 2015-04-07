namespace Mapeagle {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mapMenu = new Mapeagle.UserControls.MapMenuStrip();
            this.mapStrip = new Mapeagle.UserControls.MapToolStrip();
            this.listPanel = new Mapeagle.UserControls.MapPanel();
            this.mapView = new Mapeagle.UserControls.MapTreeView();
            this.headingList = new Mapeagle.UserControls.MapBanner();
            this.mapFunctions = new Mapeagle.UserControls.MapTabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.blockPanel = new Mapeagle.UserControls.MapPanel();
            this.tabMovement = new System.Windows.Forms.TabPage();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.tabPokemon = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabPage();
            this.listPanel.SuspendLayout();
            this.mapFunctions.SuspendLayout();
            this.tabMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapMenu
            // 
            this.mapMenu.Location = new System.Drawing.Point(0, 0);
            this.mapMenu.Name = "mapMenu";
            this.mapMenu.Size = new System.Drawing.Size(808, 24);
            this.mapMenu.TabIndex = 0;
            this.mapMenu.Text = "mapMenuStrip1";
            // 
            // mapStrip
            // 
            this.mapStrip.Enabled = false;
            this.mapStrip.Location = new System.Drawing.Point(0, 24);
            this.mapStrip.Name = "mapStrip";
            this.mapStrip.Size = new System.Drawing.Size(808, 25);
            this.mapStrip.TabIndex = 1;
            this.mapStrip.Text = "mapToolStrip1";
            // 
            // listPanel
            // 
            this.listPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(128)))), ((int)(((byte)(155)))));
            this.listPanel.Controls.Add(this.mapView);
            this.listPanel.Controls.Add(this.headingList);
            this.listPanel.Location = new System.Drawing.Point(-1, 48);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(229, 482);
            this.listPanel.TabIndex = 2;
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.SystemColors.Control;
            this.mapView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mapView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.mapView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mapView.HideSelection = false;
            this.mapView.Indent = 19;
            this.mapView.ItemHeight = 16;
            this.mapView.Location = new System.Drawing.Point(0, 34);
            this.mapView.Name = "mapView";
            this.mapView.Size = new System.Drawing.Size(228, 446);
            this.mapView.TabIndex = 2;
            // 
            // headingList
            // 
            this.headingList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headingList.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.headingList.Location = new System.Drawing.Point(1, 1);
            this.headingList.Name = "headingList";
            this.headingList.Size = new System.Drawing.Size(227, 33);
            this.headingList.TabIndex = 1;
            this.headingList.Text = "Mapliste";
            // 
            // mapFunctions
            // 
            this.mapFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapFunctions.Controls.Add(this.tabMap);
            this.mapFunctions.Controls.Add(this.tabMovement);
            this.mapFunctions.Controls.Add(this.tabEvents);
            this.mapFunctions.Controls.Add(this.tabPokemon);
            this.mapFunctions.Controls.Add(this.tabData);
            this.mapFunctions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapFunctions.ItemSize = new System.Drawing.Size(73, 29);
            this.mapFunctions.Location = new System.Drawing.Point(224, 49);
            this.mapFunctions.Name = "mapFunctions";
            this.mapFunctions.SelectedIndex = 0;
            this.mapFunctions.Size = new System.Drawing.Size(588, 484);
            this.mapFunctions.TabIndex = 4;
            // 
            // tabMap
            // 
            this.tabMap.BackColor = System.Drawing.Color.Transparent;
            this.tabMap.Controls.Add(this.blockPanel);
            this.tabMap.Location = new System.Drawing.Point(4, 33);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(580, 447);
            this.tabMap.TabIndex = 1;
            this.tabMap.Text = "Map";
            // 
            // blockPanel
            // 
            this.blockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blockPanel.AutoScroll = true;
            this.blockPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(128)))), ((int)(((byte)(155)))));
            this.blockPanel.Location = new System.Drawing.Point(308, -1);
            this.blockPanel.Name = "blockPanel";
            this.blockPanel.Size = new System.Drawing.Size(273, 449);
            this.blockPanel.TabIndex = 5;
            // 
            // tabMovement
            // 
            this.tabMovement.BackColor = System.Drawing.Color.Transparent;
            this.tabMovement.Location = new System.Drawing.Point(4, 33);
            this.tabMovement.Name = "tabMovement";
            this.tabMovement.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovement.Size = new System.Drawing.Size(580, 447);
            this.tabMovement.TabIndex = 0;
            this.tabMovement.Text = "Gehdaten";
            // 
            // tabEvents
            // 
            this.tabEvents.BackColor = System.Drawing.Color.Transparent;
            this.tabEvents.Location = new System.Drawing.Point(4, 33);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(580, 447);
            this.tabEvents.TabIndex = 2;
            this.tabEvents.Text = "Events";
            // 
            // tabPokemon
            // 
            this.tabPokemon.BackColor = System.Drawing.Color.Transparent;
            this.tabPokemon.Location = new System.Drawing.Point(4, 33);
            this.tabPokemon.Name = "tabPokemon";
            this.tabPokemon.Padding = new System.Windows.Forms.Padding(3);
            this.tabPokemon.Size = new System.Drawing.Size(580, 447);
            this.tabPokemon.TabIndex = 3;
            this.tabPokemon.Text = "Pokémon";
            // 
            // tabData
            // 
            this.tabData.BackColor = System.Drawing.Color.Transparent;
            this.tabData.Location = new System.Drawing.Point(4, 33);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(580, 447);
            this.tabData.TabIndex = 4;
            this.tabData.Text = "Daten";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 529);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.mapStrip);
            this.Controls.Add(this.mapMenu);
            this.Controls.Add(this.mapFunctions);
            this.MainMenuStrip = this.mapMenu;
            this.MinimumSize = new System.Drawing.Size(824, 568);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapeagle";
            this.listPanel.ResumeLayout(false);
            this.mapFunctions.ResumeLayout(false);
            this.tabMap.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.MapMenuStrip mapMenu;
        private UserControls.MapToolStrip mapStrip;
        private UserControls.MapPanel listPanel;
        private UserControls.MapTreeView mapView;
        private UserControls.MapBanner headingList;
        private UserControls.MapTabControl mapFunctions;
        private System.Windows.Forms.TabPage tabMovement;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.TabPage tabPokemon;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabMap;
        private UserControls.MapPanel blockPanel;
    }
}

