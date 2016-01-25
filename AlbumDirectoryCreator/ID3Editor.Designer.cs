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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewPerformers = new System.Windows.Forms.DataGridView();
            this.bindingSourcePerformers = new System.Windows.Forms.BindingSource(this.components);
            this.labelArstits = new System.Windows.Forms.Label();
            this.checkedListBoxGenre = new System.Windows.Forms.CheckedListBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.perfomerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starsBox1 = new AlbumDirectoryCreator.StarsBox();
            this.groupBoxEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPerformers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePerformers)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYear.Location = new System.Drawing.Point(71, 215);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(196, 21);
            this.textBoxYear.TabIndex = 0;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // textBoxAlbum
            // 
            this.textBoxAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlbum.Location = new System.Drawing.Point(71, 134);
            this.textBoxAlbum.Name = "textBoxAlbum";
            this.textBoxAlbum.Size = new System.Drawing.Size(196, 21);
            this.textBoxAlbum.TabIndex = 2;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(71, 161);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(196, 21);
            this.textBoxTitle.TabIndex = 3;
            // 
            // textBoxTitleNr
            // 
            this.textBoxTitleNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitleNr.Location = new System.Drawing.Point(71, 188);
            this.textBoxTitleNr.Name = "textBoxTitleNr";
            this.textBoxTitleNr.Size = new System.Drawing.Size(196, 21);
            this.textBoxTitleNr.TabIndex = 4;
            this.textBoxTitleNr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComment.Location = new System.Drawing.Point(71, 242);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(196, 63);
            this.textBoxComment.TabIndex = 5;
            // 
            // labelAlbum
            // 
            this.labelAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAlbum.AutoSize = true;
            this.labelAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlbum.Location = new System.Drawing.Point(26, 134);
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
            this.labelTitle.Location = new System.Drawing.Point(35, 161);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(30, 13);
            this.labelTitle.TabIndex = 10;
            this.labelTitle.Text = "Title:";
            // 
            // labelTitleNr
            // 
            this.labelTitleNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitleNr.AutoSize = true;
            this.labelTitleNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleNr.Location = new System.Drawing.Point(21, 188);
            this.labelTitleNr.Name = "labelTitleNr";
            this.labelTitleNr.Size = new System.Drawing.Size(44, 13);
            this.labelTitleNr.TabIndex = 11;
            this.labelTitleNr.Text = "Title Nr:";
            // 
            // labelYear
            // 
            this.labelYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelYear.AutoSize = true;
            this.labelYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYear.Location = new System.Drawing.Point(33, 215);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(32, 13);
            this.labelYear.TabIndex = 12;
            this.labelYear.Text = "Year:";
            // 
            // labelGenre
            // 
            this.labelGenre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelGenre.AutoSize = true;
            this.labelGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGenre.Location = new System.Drawing.Point(26, 391);
            this.labelGenre.Name = "labelGenre";
            this.labelGenre.Size = new System.Drawing.Size(39, 13);
            this.labelGenre.TabIndex = 13;
            this.labelGenre.Text = "Genre:";
            // 
            // labelComment
            // 
            this.labelComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelComment.AutoSize = true;
            this.labelComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComment.Location = new System.Drawing.Point(11, 242);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(54, 13);
            this.labelComment.TabIndex = 14;
            this.labelComment.Text = "Comment:";
            // 
            // groupBoxEditor
            // 
            this.groupBoxEditor.Controls.Add(this.starsBox1);
            this.groupBoxEditor.Controls.Add(this.label1);
            this.groupBoxEditor.Controls.Add(this.dataGridViewPerformers);
            this.groupBoxEditor.Controls.Add(this.labelArstits);
            this.groupBoxEditor.Controls.Add(this.labelGenre);
            this.groupBoxEditor.Controls.Add(this.checkedListBoxGenre);
            this.groupBoxEditor.Controls.Add(this.labelPath);
            this.groupBoxEditor.Controls.Add(this.textBoxPath);
            this.groupBoxEditor.Controls.Add(this.buttonCancel);
            this.groupBoxEditor.Controls.Add(this.buttonSave);
            this.groupBoxEditor.Controls.Add(this.labelComment);
            this.groupBoxEditor.Controls.Add(this.textBoxYear);
            this.groupBoxEditor.Controls.Add(this.labelYear);
            this.groupBoxEditor.Controls.Add(this.textBoxAlbum);
            this.groupBoxEditor.Controls.Add(this.labelTitleNr);
            this.groupBoxEditor.Controls.Add(this.textBoxTitle);
            this.groupBoxEditor.Controls.Add(this.labelTitle);
            this.groupBoxEditor.Controls.Add(this.textBoxTitleNr);
            this.groupBoxEditor.Controls.Add(this.labelAlbum);
            this.groupBoxEditor.Controls.Add(this.textBoxComment);
            this.groupBoxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEditor.Location = new System.Drawing.Point(0, 0);
            this.groupBoxEditor.Name = "groupBoxEditor";
            this.groupBoxEditor.Size = new System.Drawing.Size(279, 622);
            this.groupBoxEditor.TabIndex = 15;
            this.groupBoxEditor.TabStop = false;
            this.groupBoxEditor.Text = "ID3 Editor";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Rating:";
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
            this.dataGridViewPerformers.DataSource = this.bindingSourcePerformers;
            this.dataGridViewPerformers.Location = new System.Drawing.Point(71, 20);
            this.dataGridViewPerformers.Name = "dataGridViewPerformers";
            this.dataGridViewPerformers.RowHeadersVisible = false;
            this.dataGridViewPerformers.Size = new System.Drawing.Size(196, 108);
            this.dataGridViewPerformers.TabIndex = 23;
            this.dataGridViewPerformers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPerformers_KeyDown);
            // 
            // bindingSourcePerformers
            // 
            this.bindingSourcePerformers.DataSource = typeof(Logic.DataObjects.Performer);
            // 
            // labelArstits
            // 
            this.labelArstits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelArstits.AutoSize = true;
            this.labelArstits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArstits.Location = new System.Drawing.Point(5, 20);
            this.labelArstits.Name = "labelArstits";
            this.labelArstits.Size = new System.Drawing.Size(60, 13);
            this.labelArstits.TabIndex = 21;
            this.labelArstits.Text = "Performers:";
            // 
            // checkedListBoxGenre
            // 
            this.checkedListBoxGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxGenre.FormattingEnabled = true;
            this.checkedListBoxGenre.Location = new System.Drawing.Point(71, 391);
            this.checkedListBoxGenre.Name = "checkedListBoxGenre";
            this.checkedListBoxGenre.Size = new System.Drawing.Size(196, 196);
            this.checkedListBoxGenre.TabIndex = 19;
            // 
            // labelPath
            // 
            this.labelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPath.AutoSize = true;
            this.labelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPath.Location = new System.Drawing.Point(33, 316);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(32, 13);
            this.labelPath.TabIndex = 17;
            this.labelPath.Text = "Path:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(71, 311);
            this.textBoxPath.Multiline = true;
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(196, 46);
            this.textBoxPath.TabIndex = 18;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(192, 593);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(111, 593);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // perfomerDataGridViewTextBoxColumn
            // 
            this.perfomerDataGridViewTextBoxColumn.DataPropertyName = "Perfomer";
            this.perfomerDataGridViewTextBoxColumn.HeaderText = "Perfomer";
            this.perfomerDataGridViewTextBoxColumn.Name = "perfomerDataGridViewTextBoxColumn";
            // 
            // starsBox1
            // 
            this.starsBox1.Location = new System.Drawing.Point(71, 363);
            this.starsBox1.MaximumSize = new System.Drawing.Size(175, 22);
            this.starsBox1.MinimumSize = new System.Drawing.Size(175, 22);
            this.starsBox1.Name = "starsBox1";
            this.starsBox1.Size = new System.Drawing.Size(175, 22);
            this.starsBox1.TabIndex = 26;
            // 
            // Id3Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEditor);
            this.Name = "Id3Editor";
            this.Size = new System.Drawing.Size(279, 622);
            this.groupBoxEditor.ResumeLayout(false);
            this.groupBoxEditor.PerformLayout();
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
        private System.Windows.Forms.Label labelArstits;
        private System.Windows.Forms.DataGridView dataGridViewPerformers;
        private System.Windows.Forms.BindingSource bindingSourcePerformers;
        private System.Windows.Forms.Label label1;
        private StarsBox starsBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn perfomerDataGridViewTextBoxColumn;
    }
}
