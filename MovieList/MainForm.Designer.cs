namespace MovieList
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstMovieList = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMedia = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.lblLoan = new System.Windows.Forms.Label();
            this.lblGenres = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnLend = new System.Windows.Forms.Button();
            this.btnRemoveLend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstMovieList
            // 
            this.lstMovieList.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.lstMovieList.FormattingEnabled = true;
            this.lstMovieList.Location = new System.Drawing.Point(12, 44);
            this.lstMovieList.Name = "lstMovieList";
            this.lstMovieList.Size = new System.Drawing.Size(1147, 459);
            this.lstMovieList.TabIndex = 0;
            this.lstMovieList.SelectedIndexChanged += new System.EventHandler(this.lstMovieList_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblTitle.Location = new System.Drawing.Point(13, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(34, 16);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblMedia
            // 
            this.lblMedia.AutoSize = true;
            this.lblMedia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblMedia.Location = new System.Drawing.Point(380, 22);
            this.lblMedia.Name = "lblMedia";
            this.lblMedia.Size = new System.Drawing.Size(46, 16);
            this.lblMedia.TabIndex = 2;
            this.lblMedia.Text = "Media";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblYear.Location = new System.Drawing.Point(482, 22);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(37, 16);
            this.lblYear.TabIndex = 3;
            this.lblYear.Text = "Year";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblRating.Location = new System.Drawing.Point(528, 22);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(47, 16);
            this.lblRating.TabIndex = 4;
            this.lblRating.Text = "Rating";
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblRuntime.Location = new System.Drawing.Point(578, 22);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(57, 16);
            this.lblRuntime.TabIndex = 5;
            this.lblRuntime.Text = "Runtime";
            // 
            // lblLoan
            // 
            this.lblLoan.AutoSize = true;
            this.lblLoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblLoan.Location = new System.Drawing.Point(641, 22);
            this.lblLoan.Name = "lblLoan";
            this.lblLoan.Size = new System.Drawing.Size(54, 16);
            this.lblLoan.TabIndex = 6;
            this.lblLoan.Text = "On loan";
            // 
            // lblGenres
            // 
            this.lblGenres.AutoSize = true;
            this.lblGenres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblGenres.Location = new System.Drawing.Point(887, 25);
            this.lblGenres.Name = "lblGenres";
            this.lblGenres.Size = new System.Drawing.Size(52, 16);
            this.lblGenres.TabIndex = 7;
            this.lblGenres.Text = "Genres";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.btnAdd.Location = new System.Drawing.Point(134, 513);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(125, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add movie";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.btnRemove.Location = new System.Drawing.Point(640, 513);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(125, 25);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove movie";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnChange
            // 
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.btnChange.Location = new System.Drawing.Point(387, 513);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(125, 25);
            this.btnChange.TabIndex = 10;
            this.btnChange.Text = "Change media";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnLend
            // 
            this.btnLend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.btnLend.Location = new System.Drawing.Point(885, 513);
            this.btnLend.Name = "btnLend";
            this.btnLend.Size = new System.Drawing.Size(125, 25);
            this.btnLend.TabIndex = 11;
            this.btnLend.Text = "Lend out";
            this.btnLend.UseVisualStyleBackColor = true;
            this.btnLend.Click += new System.EventHandler(this.btnLend_Click);
            // 
            // btnRemoveLend
            // 
            this.btnRemoveLend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.btnRemoveLend.Location = new System.Drawing.Point(885, 513);
            this.btnRemoveLend.Name = "btnRemoveLend";
            this.btnRemoveLend.Size = new System.Drawing.Size(125, 25);
            this.btnRemoveLend.TabIndex = 12;
            this.btnRemoveLend.Text = "Remove lend";
            this.btnRemoveLend.UseVisualStyleBackColor = true;
            this.btnRemoveLend.Click += new System.EventHandler(this.btnRemoveLend_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 548);
            this.Controls.Add(this.btnLend);
            this.Controls.Add(this.btnRemoveLend);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblGenres);
            this.Controls.Add(this.lblLoan);
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblMedia);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstMovieList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstMovieList;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblRuntime;
        private System.Windows.Forms.Label lblLoan;
        private System.Windows.Forms.Label lblGenres;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnLend;
        private System.Windows.Forms.Button btnRemoveLend;
    }
}

