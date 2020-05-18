namespace CourseProject
{
    partial class Balloons
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.startgame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.easy = new System.Windows.Forms.Button();
            this.normal = new System.Windows.Forms.Button();
            this.hard = new System.Windows.Forms.Button();
            this.Shop = new System.Windows.Forms.GroupBox();
            this.score = new System.Windows.Forms.TextBox();
            this.Exit = new System.Windows.Forms.Button();
            this.Bucket = new System.Windows.Forms.Button();
            this.Bomb = new System.Windows.Forms.Button();
            this.Pickaxe = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.moves = new System.Windows.Forms.TextBox();
            this.Shop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(426, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.TabIndex = 101;
            this.label1.Text = "Счёт";
            this.label1.Visible = false;
            // 
            // startgame
            // 
            this.startgame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.startgame.Cursor = System.Windows.Forms.Cursors.Default;
            this.startgame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.startgame.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startgame.ForeColor = System.Drawing.Color.Lime;
            this.startgame.Location = new System.Drawing.Point(12, 113);
            this.startgame.Name = "startgame";
            this.startgame.Size = new System.Drawing.Size(236, 236);
            this.startgame.TabIndex = 102;
            this.startgame.Text = "Начать Игру";
            this.startgame.UseVisualStyleBackColor = false;
            this.startgame.Click += new System.EventHandler(this.startgame_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 20);
            this.label2.TabIndex = 103;
            this.label2.Text = "Выберите уровень сложности";
            // 
            // easy
            // 
            this.easy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.easy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.easy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.easy.ForeColor = System.Drawing.Color.Lime;
            this.easy.Location = new System.Drawing.Point(12, 32);
            this.easy.Name = "easy";
            this.easy.Size = new System.Drawing.Size(75, 75);
            this.easy.TabIndex = 104;
            this.easy.Text = "Легко";
            this.easy.UseVisualStyleBackColor = false;
            this.easy.Click += new System.EventHandler(this.easy_Click);
            // 
            // normal
            // 
            this.normal.BackColor = System.Drawing.Color.Green;
            this.normal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.normal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.normal.ForeColor = System.Drawing.Color.Lime;
            this.normal.Location = new System.Drawing.Point(93, 32);
            this.normal.Name = "normal";
            this.normal.Size = new System.Drawing.Size(75, 75);
            this.normal.TabIndex = 105;
            this.normal.Text = "Средне";
            this.normal.UseVisualStyleBackColor = false;
            this.normal.Click += new System.EventHandler(this.normal_Click);
            // 
            // hard
            // 
            this.hard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.hard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.hard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hard.ForeColor = System.Drawing.Color.Lime;
            this.hard.Location = new System.Drawing.Point(174, 32);
            this.hard.Name = "hard";
            this.hard.Size = new System.Drawing.Size(75, 75);
            this.hard.TabIndex = 106;
            this.hard.Text = "Тяжело";
            this.hard.UseVisualStyleBackColor = false;
            this.hard.Click += new System.EventHandler(this.hard_Click);
            // 
            // Shop
            // 
            this.Shop.Controls.Add(this.Bucket);
            this.Shop.Controls.Add(this.Bomb);
            this.Shop.Controls.Add(this.Pickaxe);
            this.Shop.Location = new System.Drawing.Point(422, 79);
            this.Shop.Name = "Shop";
            this.Shop.Size = new System.Drawing.Size(66, 202);
            this.Shop.TabIndex = 107;
            this.Shop.TabStop = false;
            this.Shop.Text = "Магазин";
            this.Shop.Visible = false;
            // 
            // score
            // 
            this.score.BackColor = System.Drawing.Color.Lime;
            this.score.Enabled = false;
            this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.score.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.score.Location = new System.Drawing.Point(422, 36);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(66, 22);
            this.score.TabIndex = 108;
            this.score.Text = "5000";
            this.score.Visible = false;
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Green;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Exit.Location = new System.Drawing.Point(420, 411);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(70, 70);
            this.Exit.TabIndex = 109;
            this.Exit.Text = "Выход";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Bucket
            // 
            this.Bucket.BackColor = System.Drawing.Color.Green;
            this.Bucket.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Bucket.Image = global::CourseProject.Properties.Resources.Bucket;
            this.Bucket.Location = new System.Drawing.Point(6, 141);
            this.Bucket.Name = "Bucket";
            this.Bucket.Size = new System.Drawing.Size(55, 55);
            this.Bucket.TabIndex = 2;
            this.Bucket.UseVisualStyleBackColor = false;
            this.Bucket.Click += new System.EventHandler(this.Bucket_Click);
            // 
            // Bomb
            // 
            this.Bomb.BackColor = System.Drawing.Color.Green;
            this.Bomb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Bomb.Image = global::CourseProject.Properties.Resources.Bomb;
            this.Bomb.Location = new System.Drawing.Point(6, 80);
            this.Bomb.Name = "Bomb";
            this.Bomb.Size = new System.Drawing.Size(55, 55);
            this.Bomb.TabIndex = 1;
            this.Bomb.UseVisualStyleBackColor = false;
            this.Bomb.Click += new System.EventHandler(this.Bomb_Click);
            // 
            // Pickaxe
            // 
            this.Pickaxe.BackColor = System.Drawing.Color.Green;
            this.Pickaxe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Pickaxe.Image = global::CourseProject.Properties.Resources.Pickaxe;
            this.Pickaxe.Location = new System.Drawing.Point(6, 19);
            this.Pickaxe.Name = "Pickaxe";
            this.Pickaxe.Size = new System.Drawing.Size(55, 55);
            this.Pickaxe.TabIndex = 0;
            this.Pickaxe.UseVisualStyleBackColor = false;
            this.Pickaxe.Click += new System.EventHandler(this.Pickaxe_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(420, 481);
            this.pictureBox1.TabIndex = 100;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(422, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 24);
            this.label3.TabIndex = 110;
            this.label3.Text = "Ходов";
            this.label3.Visible = false;
            // 
            // moves
            // 
            this.moves.BackColor = System.Drawing.Color.Lime;
            this.moves.Enabled = false;
            this.moves.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moves.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.moves.Location = new System.Drawing.Point(422, 320);
            this.moves.Name = "moves";
            this.moves.Size = new System.Drawing.Size(66, 22);
            this.moves.TabIndex = 111;
            this.moves.Text = "0";
            this.moves.Visible = false;
            // 
            // Balloons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(489, 481);
            this.Controls.Add(this.moves);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.score);
            this.Controls.Add(this.Shop);
            this.Controls.Add(this.hard);
            this.Controls.Add(this.normal);
            this.Controls.Add(this.easy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startgame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Balloons";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "li";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startgame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button easy;
        private System.Windows.Forms.Button normal;
        private System.Windows.Forms.Button hard;
        private System.Windows.Forms.GroupBox Shop;
        private System.Windows.Forms.Button Bucket;
        private System.Windows.Forms.Button Bomb;
        private System.Windows.Forms.Button Pickaxe;
        private System.Windows.Forms.TextBox score;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox moves;
    }
}

