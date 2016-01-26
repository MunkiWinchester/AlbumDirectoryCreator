namespace AlbumDirectoryCreator
{
    partial class Id3Editor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.textBoxAlbum = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxTitleNr = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelAlbum = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelTitleNr = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelGenre = new System.Windows.Forms.Label();
            this.labelComment = new System.Windows.Forms.Label();
            this.groupBoxEditor = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelPerformers = new System.Windows.Forms.Label();
            this.dataGridViewPerformers = new System.Windows.Forms.DataGridView();
            this.perfomerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourcePerformers = new System.Windows.Forms.BindingSource(this.components);
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelRating = new System.Windows.Forms.Label();
            this.starsBoxRating = new AlbumDirectoryCreator.StarsBox();
            this.checkedListBoxGenre = new System.Windows.Forms.CheckedListBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxEditor.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPerformers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePerformers)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textBoxYear, 2);
            this.textBoxYear.Location = new System.Drawing.Point(94, 243);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(176, 21);
            this.textBoxYear.TabIndex = 0;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // textBoxAlbum
            // 
            this.textBoxAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textBoxAlbum, 2);
            this.textBoxAlbum.Location = new System.Drawing.Point(94, 63);
            this.textBoxAlbum.Name = "textBoxAlbum";
            this.textBoxAlbum.Size = new System.Drawing.Size(176, 21);
            this.textBoxAlbum.TabIndex = 2;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textBoxTitle, 2);
            this.textBoxTitle.Location = new System.Drawing.Point(94, 123);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(176, 21);
            this.textBoxTitle.TabIndex = 3;
            // 
            // textBoxTitleNr
            // 
            this.textBoxTitleNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textBoxTitleNr, 2);
            this.textBoxTitleNr.Location = new System.Drawing.Point(94, 183);
            this.textBoxTitleNr.Name = "textBoxTitleNr";
            this.textBoxTitleNr.Size = new System.Drawing.Size(176, 21);
            this.textBoxTitleNr.TabIndex = 4;
            this.textBoxTitleNr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // textBoxComment
            // 
            this.tableLayoutPanel.SetColumnSpan(this.textBoxComment, 2);
            this.textBoxComment.Location = new System.Drawing.Point(94, 303);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(176, 52);
            this.textBoxComment.TabIndex = 5;
            // 
            // labelAlbum
            // 
            this.labelAlbum.AutoSize = true;
            this.labelAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlbum.Location = new System.Drawing.Point(3, 60);
            this.labelAlbum.Name = "labelAlbum";
            this.labelAlbum.Size = new System.Drawing.Size(39, 13);
            this.labelAlbum.TabIndex = 9;
            this.labelAlbum.Text = "Album:";
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(3, 120);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(30, 60);
            this.labelTitle.TabIndex = 10;
            this.labelTitle.Text = "Title:";
            // 
            // labelTitleNr
            // 
            this.labelTitleNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitleNr.AutoSize = true;
            this.labelTitleNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleNr.Location = new System.Drawing.Point(3, 180);
            this.labelTitleNr.Name = "labelTitleNr";
            this.labelTitleNr.Size = new System.Drawing.Size(44, 60);
            this.labelTitleNr.TabIndex = 11;
            this.labelTitleNr.Text = "Title Nr:";
            // 
            // labelYear
            // 
            this.labelYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelYear.AutoSize = true;
            this.labelYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYear.Location = new System.Drawing.Point(3, 240);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(32, 60);
            this.labelYear.TabIndex = 12;
            this.labelYear.Text = "Year:";
            // 
            // labelGenre
            // 
            this.labelGenre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelGenre.AutoSize = true;
            this.labelGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGenre.Location = new System.Drawing.Point(3, 480);
            this.labelGenre.Name = "labelGenre";
            this.labelGenre.Size = new System.Drawing.Size(39, 60);
            this.labelGenre.TabIndex = 13;
            this.labelGenre.Text = "Genre:";
            // 
            // labelComment
            // 
            this.labelComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelComment.AutoSize = true;
            this.labelComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComment.Location = new System.Drawing.Point(3, 300);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(54, 60);
            this.labelComment.TabIndex = 14;
            this.labelComment.Text = "Comment:";
            // 
            // groupBoxEditor
            // 
            this.groupBoxEditor.Controls.Add(this.tableLayoutPanel);
            this.groupBoxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEditor.Location = new System.Drawing.Point(0, 0);
            this.groupBoxEditor.Name = "groupBoxEditor";
            this.groupBoxEditor.Size = new System.Drawing.Size(279, 622);
            this.groupBoxEditor.TabIndex = 15;
            this.groupBoxEditor.TabStop = false;
            this.groupBoxEditor.Text = "ID3 Editor";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.Controls.Add(this.labelPerformers, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.dataGridViewPerformers, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelAlbum, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.textBoxAlbum, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelTitle, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.textBoxTitle, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelTitleNr, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxTitleNr, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.labelYear, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.textBoxYear, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.labelComment, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.textBoxComment, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.labelPath, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.textBoxPath, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.labelRating, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.starsBoxRating, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.labelGenre, 0, 8);
            this.tableLayoutPanel.Controls.Add(this.checkedListBoxGenre, 1, 8);
            this.tableLayoutPanel.Controls.Add(this.buttonSave, 1, 9);
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 2, 9);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 10;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(273, 602);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelPerformers
            // 
            this.labelPerformers.AutoSize = true;
            this.labelPerformers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPerformers.Location = new System.Drawing.Point(3, 0);
            this.labelPerformers.Name = "labelPerformers";
            this.labelPerformers.Size = new System.Drawing.Size(60, 13);
            this.labelPerformers.TabIndex = 21;
            this.labelPerformers.Text = "Performers:";
            // 
            // dataGridViewPerformers
            // 
            this.dataGridViewPerformers.AutoGenerateColumns = false;
            this.dataGridViewPerformers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPerformers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewPerformers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPerformers.ColumnHeadersVisible = false;
            this.dataGridViewPerformers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.perfomerDataGridViewTextBoxColumn});
            this.tableLayoutPanel.SetColumnSpan(this.dataGridViewPerformers, 2);
            this.dataGridViewPerformers.DataSource = this.bindingSourcePerformers;
            this.dataGridViewPerformers.Location = new System.Drawing.Point(94, 3);
            this.dataGridViewPerformers.Name = "dataGridViewPerformers";
            this.dataGridViewPerformers.RowHeadersVisible = false;
            this.dataGridViewPerformers.Size = new System.Drawing.Size(176, 52);
            this.dataGridViewPerformers.TabIndex = 23;
            this.dataGridViewPerformers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPerformers_KeyDown);
            // 
            // perfomerDataGridViewTextBoxColumn
            // 
            this.perfomerDataGridViewTextBoxColumn.DataPropertyName = "Perfomer";
            this.perfomerDataGridViewTextBoxColumn.HeaderText = "Perfomer";
            this.perfomerDataGridViewTextBoxColumn.Name = "perfomerDataGridViewTextBoxColumn";
            // 
            // bindingSourcePerformers
            // 
            this.bindingSourcePerformers.DataSource = typeof(Logic.DataObjects.Performer);
            // 
            // labelPath
            // 
            this.labelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPath.AutoSize = true;
            this.labelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPath.Location = new System.Drawing.Point(3, 360);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(32, 60);
            this.labelPath.TabIndex = 17;
            this.labelPath.Text = "Path:";
            // 
            // textBoxPath
            // 
            this.tableLayoutPanel.SetColumnSpan(this.textBoxPath, 2);
            this.textBoxPath.Location = new System.Drawing.Point(94, 363);
            this.textBoxPath.Multiline = true;
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(176, 52);
            this.textBoxPath.TabIndex = 18;
            // 
            // labelRating
            // 
            this.labelRating.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRating.AutoSize = true;
            this.labelRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRating.Location = new System.Drawing.Point(3, 420);
            this.labelRating.Name = "labelRating";
            this.labelRating.Size = new System.Drawing.Size(41, 60);
            this.labelRating.TabIndex = 25;
            this.labelRating.Text = "Rating:";
            // 
            // starsBoxRating
            // 
            this.starsBoxRating.Location = new System.Drawing.Point(94, 423);
            this.starsBoxRating.MaximumSize = new System.Drawing.Size(175, 22);
            this.starsBoxRating.MinimumSize = new System.Drawing.Size(175, 22);
            this.starsBoxRating.Name = "starsBoxRating";
            this.starsBoxRating.Size = new System.Drawing.Size(175, 22);
            this.starsBoxRating.TabIndex = 26;
            // 
            // checkedListBoxGenre
            // 
            this.tableLayoutPanel.SetColumnSpan(this.checkedListBoxGenre, 2);
            this.checkedListBoxGenre.FormattingEnabled = true;
            this.checkedListBoxGenre.Location = new System.Drawing.Point(94, 483);
            this.checkedListBoxGenre.Name = "checkedListBoxGenre";
            this.checkedListBoxGenre.Size = new System.Drawing.Size(176, 52);
            this.checkedListBoxGenre.TabIndex = 19;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(104, 576);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(195, 576);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // Id3Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEditor);
            this.Name = "Id3Editor";
            this.Size = new System.Drawing.Size(279, 622);
            this.groupBoxEditor.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPerformers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePerformers)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.TextBox textBoxAlbum;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxTitleNr;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelAlbum;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelTitleNr;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelGenre;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.GroupBox groupBoxEditor;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.CheckedListBox checkedListBoxGenre;
        private System.Windows.Forms.Label labelPerformers;
        private System.Windows.Forms.DataGridView dataGridViewPerformers;
        private System.Windows.Forms.BindingSource bindingSourcePerformers;
        private System.Windows.Forms.Label labelRating;
        private StarsBox starsBoxRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn perfomerDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}
