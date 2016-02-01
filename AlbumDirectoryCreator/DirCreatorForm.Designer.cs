using AlbumDirectoryCreator.Components;

namespace AlbumDirectoryCreator
{
    partial class DirCreatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirCreatorForm));
            this.textBoxPathOrigins = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxPathDestiny = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.advancedDataGridView1 = new ADGV.AdvancedDataGridView();
            this.joinedPerformersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstPerformerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.albumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newBasePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceFiles = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.linkLabelLog = new System.Windows.Forms.LinkLabel();
            this.iD3Editor = new AlbumDirectoryCreator.Components.Id3Editor();
            this.folderDialogOrigins = new System.Windows.Forms.FolderBrowserDialog();
            this.folderDialogDestiny = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSearchDestinyPath = new System.Windows.Forms.Button();
            this.buttonSearchOriginPath = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPathOrigins
            // 
            this.textBoxPathOrigins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathOrigins.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPathOrigins.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxPathOrigins.Location = new System.Drawing.Point(13, 12);
            this.textBoxPathOrigins.Name = "textBoxPathOrigins";
            this.textBoxPathOrigins.Size = new System.Drawing.Size(911, 20);
            this.textBoxPathOrigins.TabIndex = 2;
            this.textBoxPathOrigins.Text = "Music Incoming Path";
            this.textBoxPathOrigins.MouseHover += new System.EventHandler(this.textBoxPathOrigins_Enter);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(971, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(103, 23);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Read Files In";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxPathDestiny
            // 
            this.textBoxPathDestiny.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathDestiny.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPathDestiny.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxPathDestiny.Location = new System.Drawing.Point(13, 38);
            this.textBoxPathDestiny.Name = "textBoxPathDestiny";
            this.textBoxPathDestiny.Size = new System.Drawing.Size(911, 20);
            this.textBoxPathDestiny.TabIndex = 8;
            this.textBoxPathDestiny.Text = "Music Outgoing Path";
            this.textBoxPathDestiny.MouseHover += new System.EventHandler(this.textBoxPathOrigins_Enter);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreate.Enabled = false;
            this.buttonCreate.Location = new System.Drawing.Point(971, 36);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(103, 23);
            this.buttonCreate.TabIndex = 9;
            this.buttonCreate.Text = "Create Directories";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(13, 65);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.progressBar);
            this.splitContainer.Panel1.Controls.Add(this.advancedDataGridView1);
            this.splitContainer.Panel1.Controls.Add(this.bindingNavigator);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.linkLabelLog);
            this.splitContainer.Panel2.Controls.Add(this.iD3Editor);
            this.splitContainer.Size = new System.Drawing.Size(1061, 578);
            this.splitContainer.SplitterDistance = 778;
            this.splitContainer.TabIndex = 13;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ForeColor = System.Drawing.Color.Lime;
            this.progressBar.Location = new System.Drawing.Point(428, 555);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(350, 20);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 3;
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.AllowUserToAddRows = false;
            this.advancedDataGridView1.AllowUserToDeleteRows = false;
            this.advancedDataGridView1.AllowUserToResizeRows = false;
            this.advancedDataGridView1.AutoGenerateColumns = false;
            this.advancedDataGridView1.AutoGenerateContextFilters = true;
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.joinedPerformersDataGridViewTextBoxColumn,
            this.firstPerformerDataGridViewTextBoxColumn,
            this.albumDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.fileInfoDataGridViewTextBoxColumn,
            this.newBasePathDataGridViewTextBoxColumn});
            this.advancedDataGridView1.DataSource = this.bindingSourceFiles;
            this.advancedDataGridView1.DateWithTime = false;
            this.advancedDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.RowHeadersVisible = false;
            this.advancedDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.advancedDataGridView1.Size = new System.Drawing.Size(778, 553);
            this.advancedDataGridView1.TabIndex = 0;
            this.advancedDataGridView1.TimeFilter = false;
            this.advancedDataGridView1.SortStringChanged += new System.EventHandler(this.advancedDataGridView1_SortStringChanged);
            this.advancedDataGridView1.FilterStringChanged += new System.EventHandler(this.advancedDataGridView1_FilterStringChanged);
            // 
            // joinedPerformersDataGridViewTextBoxColumn
            // 
            this.joinedPerformersDataGridViewTextBoxColumn.DataPropertyName = "JoinedPerformers";
            this.joinedPerformersDataGridViewTextBoxColumn.HeaderText = "Performer";
            this.joinedPerformersDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.joinedPerformersDataGridViewTextBoxColumn.Name = "joinedPerformersDataGridViewTextBoxColumn";
            this.joinedPerformersDataGridViewTextBoxColumn.ReadOnly = true;
            this.joinedPerformersDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.joinedPerformersDataGridViewTextBoxColumn.Width = 150;
            // 
            // firstPerformerDataGridViewTextBoxColumn
            // 
            this.firstPerformerDataGridViewTextBoxColumn.DataPropertyName = "FirstPerformer";
            this.firstPerformerDataGridViewTextBoxColumn.HeaderText = "Performers";
            this.firstPerformerDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.firstPerformerDataGridViewTextBoxColumn.Name = "firstPerformerDataGridViewTextBoxColumn";
            this.firstPerformerDataGridViewTextBoxColumn.ReadOnly = true;
            this.firstPerformerDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.firstPerformerDataGridViewTextBoxColumn.Visible = false;
            // 
            // albumDataGridViewTextBoxColumn
            // 
            this.albumDataGridViewTextBoxColumn.DataPropertyName = "Album";
            this.albumDataGridViewTextBoxColumn.HeaderText = "Album";
            this.albumDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.albumDataGridViewTextBoxColumn.Name = "albumDataGridViewTextBoxColumn";
            this.albumDataGridViewTextBoxColumn.ReadOnly = true;
            this.albumDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.albumDataGridViewTextBoxColumn.Width = 150;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            this.titleDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.titleDataGridViewTextBoxColumn.Width = 150;
            // 
            // fileInfoDataGridViewTextBoxColumn
            // 
            this.fileInfoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileInfoDataGridViewTextBoxColumn.DataPropertyName = "FileInfo";
            this.fileInfoDataGridViewTextBoxColumn.HeaderText = "FileInfo";
            this.fileInfoDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.fileInfoDataGridViewTextBoxColumn.Name = "fileInfoDataGridViewTextBoxColumn";
            this.fileInfoDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileInfoDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // newBasePathDataGridViewTextBoxColumn
            // 
            this.newBasePathDataGridViewTextBoxColumn.DataPropertyName = "NewBasePath";
            this.newBasePathDataGridViewTextBoxColumn.HeaderText = "NewBasePath";
            this.newBasePathDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.newBasePathDataGridViewTextBoxColumn.Name = "newBasePathDataGridViewTextBoxColumn";
            this.newBasePathDataGridViewTextBoxColumn.ReadOnly = true;
            this.newBasePathDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.newBasePathDataGridViewTextBoxColumn.Visible = false;
            // 
            // bindingSourceFiles
            // 
            this.bindingSourceFiles.DataSource = typeof(Logic.DataObjects.BaseInfoTag);
            this.bindingSourceFiles.CurrentChanged += new System.EventHandler(this.bindingSourceFiles_CurrentChanged);
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BindingSource = this.bindingSourceFiles;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 553);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(778, 25);
            this.bindingNavigator.TabIndex = 1;
            this.bindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // linkLabelLog
            // 
            this.linkLabelLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelLog.AutoSize = true;
            this.linkLabelLog.Location = new System.Drawing.Point(212, 0);
            this.linkLabelLog.Name = "linkLabelLog";
            this.linkLabelLog.Size = new System.Drawing.Size(67, 13);
            this.linkLabelLog.TabIndex = 3;
            this.linkLabelLog.TabStop = true;
            this.linkLabelLog.Text = "Open Logfile";
            this.linkLabelLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLog_LinkClicked);
            // 
            // iD3Editor
            // 
            this.iD3Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iD3Editor.Enabled = false;
            this.iD3Editor.Location = new System.Drawing.Point(0, 0);
            this.iD3Editor.Name = "iD3Editor";
            this.iD3Editor.Size = new System.Drawing.Size(279, 578);
            this.iD3Editor.TabIndex = 12;
            this.iD3Editor.ItemSaved += new System.EventHandler(this.iD3Editor_ItemSaved);
            this.iD3Editor.Leave += new System.EventHandler(this.iD3Editor_Leave);
            // 
            // folderDialogOrigins
            // 
            this.folderDialogOrigins.Description = "Please select a folder from where the music files should be  loaded (Subfolders w" +
    "ill be included)";
            // 
            // folderDialogDestiny
            // 
            this.folderDialogDestiny.Description = "Please select a folder where the music with the new structure shoud be copied (Em" +
    "pty folder is recommend)";
            // 
            // buttonSearchDestinyPath
            // 
            this.buttonSearchDestinyPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearchDestinyPath.Image = global::AlbumDirectoryCreator.Properties.Resources.Binocular;
            this.buttonSearchDestinyPath.Location = new System.Drawing.Point(930, 36);
            this.buttonSearchDestinyPath.Name = "buttonSearchDestinyPath";
            this.buttonSearchDestinyPath.Size = new System.Drawing.Size(35, 23);
            this.buttonSearchDestinyPath.TabIndex = 15;
            this.buttonSearchDestinyPath.UseVisualStyleBackColor = true;
            this.buttonSearchDestinyPath.Click += new System.EventHandler(this.buttonSearchOriginPath_Click);
            // 
            // buttonSearchOriginPath
            // 
            this.buttonSearchOriginPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearchOriginPath.Image = global::AlbumDirectoryCreator.Properties.Resources.Binocular;
            this.buttonSearchOriginPath.Location = new System.Drawing.Point(930, 10);
            this.buttonSearchOriginPath.Name = "buttonSearchOriginPath";
            this.buttonSearchOriginPath.Size = new System.Drawing.Size(35, 23);
            this.buttonSearchOriginPath.TabIndex = 14;
            this.buttonSearchOriginPath.UseVisualStyleBackColor = true;
            this.buttonSearchOriginPath.Click += new System.EventHandler(this.buttonSearchOriginPath_Click);
            // 
            // DirCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1086, 655);
            this.Controls.Add(this.buttonSearchDestinyPath);
            this.Controls.Add(this.buttonSearchOriginPath);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxPathDestiny);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxPathOrigins);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DirCreatorForm";
            this.Text = "Directory Kreator";
            this.Load += new System.EventHandler(this.DirCreatorForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPathOrigins;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxPathDestiny;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.BindingSource bindingSourceFiles;
        private Id3Editor iD3Editor;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonSearchOriginPath;
        private System.Windows.Forms.Button buttonSearchDestinyPath;
        private System.Windows.Forms.FolderBrowserDialog folderDialogOrigins;
        private System.Windows.Forms.FolderBrowserDialog folderDialogDestiny;
        private System.Windows.Forms.LinkLabel linkLabelLog;
        private ADGV.AdvancedDataGridView advancedDataGridView1;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn newBasePathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn albumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstPerformerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn joinedPerformersDataGridViewTextBoxColumn;
    }
}

