namespace class14
{
    partial class UserForm
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
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtn = new System.Windows.Forms.TextBox();
            this.txta = new System.Windows.Forms.TextBox();
            this.txte = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(217, 9);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(220, 26);
            this.textBox9.TabIndex = 0;
            this.textBox9.Text = "User Form";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Age";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Email";
            // 
            // txtn
            // 
            this.txtn.Location = new System.Drawing.Point(194, 59);
            this.txtn.Name = "txtn";
            this.txtn.Size = new System.Drawing.Size(290, 26);
            this.txtn.TabIndex = 7;
            // 
            // txta
            // 
            this.txta.Location = new System.Drawing.Point(197, 256);
            this.txta.Name = "txta";
            this.txta.Size = new System.Drawing.Size(290, 26);
            this.txta.TabIndex = 8;
            // 
            // txte
            // 
            this.txte.Location = new System.Drawing.Point(197, 158);
            this.txte.Name = "txte";
            this.txte.Size = new System.Drawing.Size(290, 26);
            this.txte.TabIndex = 9;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(76, 297);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(124, 71);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "Login";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(288, 300);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 62;
            this.dataGridView3.RowTemplate.Height = 28;
            this.dataGridView3.Size = new System.Drawing.Size(392, 271);
            this.dataGridView3.TabIndex = 11;
            // 
            // UserForm
            // 
            this.ClientSize = new System.Drawing.Size(685, 577);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txte);
            this.Controls.Add(this.txta);
            this.Controls.Add(this.txtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox9);
            this.Name = "UserForm";
            this.Load += new System.EventHandler(this.UserForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Label txtAge;
        private System.Windows.Forms.Label txtEmail;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_save_click;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label txtNamw;
        private System.Windows.Forms.Label txtAgw;
        private System.Windows.Forms.Label txtemai;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtn;
        private System.Windows.Forms.TextBox txta;
        private System.Windows.Forms.TextBox txte;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridView dataGridView3;
    }
}

