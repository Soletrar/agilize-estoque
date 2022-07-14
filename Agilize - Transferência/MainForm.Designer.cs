namespace Agilize___Transferência
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cbboxFrom = new System.Windows.Forms.ComboBox();
            this.cbboxTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbboxFromStock = new System.Windows.Forms.ComboBox();
            this.cbboxToStock = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStartStock = new System.Windows.Forms.Button();
            this.cbboxFranchiseStock = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStartBaixa = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbboxBaixaStock = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbboxBaixaFranchise = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbboxFrom
            // 
            this.cbboxFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxFrom.FormattingEnabled = true;
            this.cbboxFrom.Location = new System.Drawing.Point(16, 47);
            this.cbboxFrom.Name = "cbboxFrom";
            this.cbboxFrom.Size = new System.Drawing.Size(202, 23);
            this.cbboxFrom.TabIndex = 0;
            // 
            // cbboxTo
            // 
            this.cbboxTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxTo.FormattingEnabled = true;
            this.cbboxTo.Location = new System.Drawing.Point(16, 100);
            this.cbboxTo.Name = "cbboxTo";
            this.cbboxTo.Size = new System.Drawing.Size(202, 23);
            this.cbboxTo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "DE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "PARA";
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Location = new System.Drawing.Point(16, 129);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(202, 36);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "INICIAR";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbboxFrom);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.cbboxTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 177);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transferência entre franquias";
            // 
            // cbboxFromStock
            // 
            this.cbboxFromStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxFromStock.FormattingEnabled = true;
            this.cbboxFromStock.Location = new System.Drawing.Point(234, 47);
            this.cbboxFromStock.Name = "cbboxFromStock";
            this.cbboxFromStock.Size = new System.Drawing.Size(184, 23);
            this.cbboxFromStock.TabIndex = 6;
            // 
            // cbboxToStock
            // 
            this.cbboxToStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxToStock.FormattingEnabled = true;
            this.cbboxToStock.Location = new System.Drawing.Point(234, 100);
            this.cbboxToStock.Name = "cbboxToStock";
            this.cbboxToStock.Size = new System.Drawing.Size(184, 23);
            this.cbboxToStock.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "DE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "PARA";
            // 
            // btnStartStock
            // 
            this.btnStartStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartStock.Location = new System.Drawing.Point(16, 129);
            this.btnStartStock.Name = "btnStartStock";
            this.btnStartStock.Size = new System.Drawing.Size(402, 36);
            this.btnStartStock.TabIndex = 5;
            this.btnStartStock.Text = "INICIAR";
            this.btnStartStock.UseVisualStyleBackColor = true;
            this.btnStartStock.Click += new System.EventHandler(this.btnStartStock_Click);
            // 
            // cbboxFranchiseStock
            // 
            this.cbboxFranchiseStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxFranchiseStock.FormattingEnabled = true;
            this.cbboxFranchiseStock.Location = new System.Drawing.Point(16, 47);
            this.cbboxFranchiseStock.Name = "cbboxFranchiseStock";
            this.cbboxFranchiseStock.Size = new System.Drawing.Size(184, 23);
            this.cbboxFranchiseStock.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Franquia";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStartStock);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbboxFromStock);
            this.groupBox2.Controls.Add(this.cbboxFranchiseStock);
            this.groupBox2.Controls.Add(this.cbboxToStock);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(261, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 177);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transferência entre depósitos";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStartBaixa);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbboxBaixaStock);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbboxBaixaFranchise);
            this.groupBox3.Location = new System.Drawing.Point(12, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(430, 143);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Baixa de estoque";
            // 
            // btnStartBaixa
            // 
            this.btnStartBaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartBaixa.Location = new System.Drawing.Point(16, 91);
            this.btnStartBaixa.Name = "btnStartBaixa";
            this.btnStartBaixa.Size = new System.Drawing.Size(394, 36);
            this.btnStartBaixa.TabIndex = 5;
            this.btnStartBaixa.Text = "INICIAR";
            this.btnStartBaixa.UseVisualStyleBackColor = true;
            this.btnStartBaixa.Click += new System.EventHandler(this.btnStartBaixa_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(226, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Estoque";
            // 
            // cbboxBaixaStock
            // 
            this.cbboxBaixaStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxBaixaStock.FormattingEnabled = true;
            this.cbboxBaixaStock.Location = new System.Drawing.Point(226, 51);
            this.cbboxBaixaStock.Name = "cbboxBaixaStock";
            this.cbboxBaixaStock.Size = new System.Drawing.Size(184, 23);
            this.cbboxBaixaStock.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Franquia";
            // 
            // cbboxBaixaFranchise
            // 
            this.cbboxBaixaFranchise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxBaixaFranchise.FormattingEnabled = true;
            this.cbboxBaixaFranchise.Location = new System.Drawing.Point(16, 51);
            this.cbboxBaixaFranchise.Name = "cbboxBaixaFranchise";
            this.cbboxBaixaFranchise.Size = new System.Drawing.Size(184, 23);
            this.cbboxBaixaFranchise.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(708, 346);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agilize - Transferência";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cbboxFrom;
        private ComboBox cbboxTo;
        private Label label1;
        private Label label2;
        private Button btnStart;
        private GroupBox groupBox1;
        private ComboBox cbboxFromStock;
        private ComboBox cbboxToStock;
        private Label label3;
        private Label label4;
        private Button btnStartStock;
        private ComboBox cbboxFranchiseStock;
        private Label label5;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button btnStartBaixa;
        private Label label7;
        private ComboBox cbboxBaixaStock;
        private Label label6;
        private ComboBox cbboxBaixaFranchise;
    }
}