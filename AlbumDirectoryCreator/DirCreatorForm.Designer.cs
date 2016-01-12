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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirCreatorForm));
            this.textBoxPathOrigins = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.backgroundWorkerGetFiles = new System.ComponentModel.BackgroundWorker();
            this.textBoxPathDestiny = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.backgroundWorkerCreate = new System.ComponentModel.BackgroundWorker();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTreeview = new System.Windows.Forms.TabPage();
            this.dataTreeListView = new BrightIdeasSoftware.DataTreeListView();
            this.olvColumnArtist = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnParentId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAlbum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPath = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bindingSourceTree = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageDatagridview = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.bindingSourceFiles = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.linkLabelLog = new System.Windows.Forms.LinkLabel();
            this.folderDialogOrigins = new System.Windows.Forms.FolderBrowserDialog();
            this.folderDialogDestiny = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSearchDestinyPath = new System.Windows.Forms.Button();
            this.buttonSearchOriginPath = new System.Windows.Forms.Button();
            this.ColumnArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iD3Editor = new AlbumDirectoryCreator.Id3Editor();
            this.tabControl.SuspendLayout();
            this.tabPageTreeview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTreeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTree)).BeginInit();
            this.tabPageDatagridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
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
            this.textBoxPathOrigins.Text = "G:\\Stuff\\Filme\\Musik";
            this.textBoxPathOrigins.MouseHover += new System.EventHandler(this.textBoxPathOrigins_Enter);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(971, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(100, 23);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Read Files In";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // backgroundWorkerGetFiles
            // 
            this.backgroundWorkerGetFiles.WorkerReportsProgress = true;
            this.backgroundWorkerGetFiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGetFiles_DoWork);
            this.backgroundWorkerGetFiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGetFiles_RunWorkerCompleted);
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
            this.textBoxPathDestiny.Text = "G:\\Stuff\\Filme\\Musik\\Musik Geordnet";
            this.textBoxPathDestiny.MouseHover += new System.EventHandler(this.textBoxPathDestiny_Enter);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreate.Location = new System.Drawing.Point(971, 36);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(103, 23);
            this.buttonCreate.TabIndex = 9;
            this.buttonCreate.Text = "Create Directories";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // backgroundWorkerCreate
            // 
            this.backgroundWorkerCreate.WorkerReportsProgress = true;
            this.backgroundWorkerCreate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCreate_DoWork);
            this.backgroundWorkerCreate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerCreate_RunWorkerCompleted);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageTreeview);
            this.tabControl.Controls.Add(this.tabPageDatagridview);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(778, 578);
            this.tabControl.TabIndex = 10;
            // 
            // tabPageTreeview
            // 
            this.tabPageTreeview.Controls.Add(this.dataTreeListView);
            this.tabPageTreeview.Location = new System.Drawing.Point(4, 22);
            this.tabPageTreeview.Name = "tabPageTreeview";
            this.tabPageTreeview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTreeview.Size = new System.Drawing.Size(770, 552);
            this.tabPageTreeview.TabIndex = 2;
            this.tabPageTreeview.Text = "Treeview";
            this.tabPageTreeview.UseVisualStyleBackColor = true;
            // 
            // dataTreeListView
            // 
            this.dataTreeListView.AllColumns.Add(this.olvColumnArtist);
            this.dataTreeListView.AllColumns.Add(this.olvColumnId);
            this.dataTreeListView.AllColumns.Add(this.olvColumnParentId);
            this.dataTreeListView.AllColumns.Add(this.olvColumnAlbum);
            this.dataTreeListView.AllColumns.Add(this.olvColumnTitle);
            this.dataTreeListView.AllColumns.Add(this.olvColumnPath);
            this.dataTreeListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataTreeListView.BackColor = System.Drawing.SystemColors.Control;
            this.dataTreeListView.CellEditUseWholeCell = false;
            this.dataTreeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnArtist,
            this.olvColumnAlbum,
            this.olvColumnTitle,
            this.olvColumnPath});
            this.dataTreeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataTreeListView.DataMember = null;
            this.dataTreeListView.DataSource = this.bindingSourceTree;
            this.dataTreeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTreeListView.EmptyListMsg = "No Files Read In";
            this.dataTreeListView.FullRowSelect = true;
            this.dataTreeListView.SelectedBackColor = System.Drawing.Color.Empty;
            this.dataTreeListView.SelectedForeColor = System.Drawing.Color.Empty;
            this.dataTreeListView.Location = new System.Drawing.Point(3, 3);
            this.dataTreeListView.Name = "dataTreeListView";
            this.dataTreeListView.RootKeyValueString = "";
            this.dataTreeListView.ShowGroups = false;
            this.dataTreeListView.ShowKeyColumns = false;
            this.dataTreeListView.Size = new System.Drawing.Size(764, 546);
            this.dataTreeListView.TabIndex = 2;
            this.dataTreeListView.UseCompatibleStateImageBehavior = false;
            this.dataTreeListView.View = System.Windows.Forms.View.Details;
            this.dataTreeListView.VirtualMode = true;
            // 
            // olvColumnArtist
            // 
            this.olvColumnArtist.AspectName = "Artist";
            this.olvColumnArtist.Text = "Artist";
            this.olvColumnArtist.Width = 200;
            // 
            // olvColumnId
            // 
            this.olvColumnId.AspectName = "Id";
            this.olvColumnId.DisplayIndex = 3;
            this.olvColumnId.IsVisible = false;
            this.olvColumnId.Tag = "";
            this.olvColumnId.Text = "Id";
            // 
            // olvColumnParentId
            // 
            this.olvColumnParentId.AspectName = "ParentId";
            this.olvColumnParentId.DisplayIndex = 1;
            this.olvColumnParentId.IsVisible = false;
            this.olvColumnParentId.Text = "ParentId";
            // 
            // olvColumnAlbum
            // 
            this.olvColumnAlbum.AspectName = "Album";
            this.olvColumnAlbum.Text = "Album";
            this.olvColumnAlbum.Width = 200;
            // 
            // olvColumnTitle
            // 
            this.olvColumnTitle.AspectName = "Title";
            this.olvColumnTitle.Text = "Title";
            this.olvColumnTitle.Width = 250;
            // 
            // olvColumnPath
            // 
            this.olvColumnPath.AspectName = "Path";
            this.olvColumnPath.Text = "Path";
            this.olvColumnPath.Width = 300;
            // 
            // bindingSourceTree
            // 
            this.bindingSourceTree.CurrentChanged += new System.EventHandler(this.bindingSourceTree_CurrentChanged);
            // 
            // tabPageDatagridview
            // 
            this.tabPageDatagridview.Controls.Add(this.dataGridView);
            this.tabPageDatagridview.Location = new System.Drawing.Point(4, 22);
            this.tabPageDatagridview.Name = "tabPageDatagridview";
            this.tabPageDatagridview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDatagridview.Size = new System.Drawing.Size(770, 552);
            this.tabPageDatagridview.TabIndex = 0;
            this.tabPageDatagridview.Text = "Datagridview";
            this.tabPageDatagridview.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnArtist,
            this.ColumnAlbum,
            this.ColumnTitle,
            this.ColumnPath});
            this.dataGridView.DataSource = this.bindingSourceFiles;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(764, 546);
            this.dataGridView.TabIndex = 0;
            // 
            // bindingSourceFiles
            // 
            this.bindingSourceFiles.CurrentChanged += new System.EventHandler(this.bindingSourceFiles_CurrentChanged);
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
            this.splitContainer.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.linkLabelLog);
            this.splitContainer.Panel2.Controls.Add(this.iD3Editor);
            this.splitContainer.Size = new System.Drawing.Size(1061, 578);
            this.splitContainer.SplitterDistance = 778;
            this.splitContainer.TabIndex = 13;
            // 
            // linkLabelLog
            // 
            this.linkLabelLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelLog.AutoSize = true;
            this.linkLabelLog.Location = new System.Drawing.Point(3, 520);
            this.linkLabelLog.Name = "linkLabelLog";
            this.linkLabelLog.Size = new System.Drawing.Size(67, 13);
            this.linkLabelLog.TabIndex = 3;
            this.linkLabelLog.TabStop = true;
            this.linkLabelLog.Text = "Open Logfile";
            this.linkLabelLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLog_LinkClicked);
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
            this.buttonSearchDestinyPath.Location = new System.Drawing.Point(930, 35);
            this.buttonSearchDestinyPath.Name = "buttonSearchDestinyPath";
            this.buttonSearchDestinyPath.Size = new System.Drawing.Size(35, 23);
            this.buttonSearchDestinyPath.TabIndex = 15;
            this.buttonSearchDestinyPath.UseVisualStyleBackColor = true;
            this.buttonSearchDestinyPath.Click += new System.EventHandler(this.buttonSearchDestinyPath_Click);
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
            // ColumnArtist
            // 
            this.ColumnArtist.DataPropertyName = "Artist";
            this.ColumnArtist.HeaderText = "Artist";
            this.ColumnArtist.Name = "ColumnArtist";
            this.ColumnArtist.ReadOnly = true;
            this.ColumnArtist.Width = 200;
            // 
            // ColumnAlbum
            // 
            this.ColumnAlbum.DataPropertyName = "Album";
            this.ColumnAlbum.HeaderText = "Album";
            this.ColumnAlbum.Name = "ColumnAlbum";
            this.ColumnAlbum.ReadOnly = true;
            this.ColumnAlbum.Width = 200;
            // 
            // ColumnTitle
            // 
            this.ColumnTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnTitle.DataPropertyName = "Title";
            this.ColumnTitle.HeaderText = "Title";
            this.ColumnTitle.Name = "ColumnTitle";
            this.ColumnTitle.ReadOnly = true;
            this.ColumnTitle.Width = 250;
            // 
            // ColumnPath
            // 
            this.ColumnPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPath.DataPropertyName = "Path";
            this.ColumnPath.HeaderText = "Path";
            this.ColumnPath.Name = "ColumnPath";
            this.ColumnPath.ReadOnly = true;
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
            this.tabControl.ResumeLayout(false);
            this.tabPageTreeview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataTreeListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTree)).EndInit();
            this.tabPageDatagridview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFiles)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPathOrigins;
        private System.Windows.Forms.Button buttonSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGetFiles;
        private System.Windows.Forms.TextBox textBoxPathDestiny;
        private System.Windows.Forms.Button buttonCreate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCreate;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDatagridview;
        private System.Windows.Forms.BindingSource bindingSourceFiles;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabPage tabPageTreeview;
        private BrightIdeasSoftware.DataTreeListView dataTreeListView;
        private System.Windows.Forms.BindingSource bindingSourceTree;
        private BrightIdeasSoftware.OLVColumn olvColumnId;
        private BrightIdeasSoftware.OLVColumn olvColumnParentId;
        private BrightIdeasSoftware.OLVColumn olvColumnArtist;
        private BrightIdeasSoftware.OLVColumn olvColumnAlbum;
        private BrightIdeasSoftware.OLVColumn olvColumnTitle;
        private BrightIdeasSoftware.OLVColumn olvColumnPath;
        private Id3Editor iD3Editor;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonSearchOriginPath;
        private System.Windows.Forms.Button buttonSearchDestinyPath;
        private System.Windows.Forms.FolderBrowserDialog folderDialogOrigins;
        private System.Windows.Forms.FolderBrowserDialog folderDialogDestiny;
        private System.Windows.Forms.LinkLabel linkLabelLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAlbum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnArtist;
    }
}

