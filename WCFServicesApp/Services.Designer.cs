
namespace WCFServicesApp
{
    partial class Services
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.screenTab = new System.Windows.Forms.TabPage();
            this.buttonTab = new System.Windows.Forms.TabPage();
            this.gv_Screen = new System.Windows.Forms.DataGridView();
            this.gv_Button = new System.Windows.Forms.DataGridView();
            this.getButtons = new System.Windows.Forms.Button();
            this.txtBankId = new System.Windows.Forms.TextBox();
            this.txtBranchId = new System.Windows.Forms.TextBox();
            this.txtScreenId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBankId_Screen = new System.Windows.Forms.TextBox();
            this.getScreen = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.screenTab.SuspendLayout();
            this.buttonTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Screen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Button)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.screenTab);
            this.tabControl1.Controls.Add(this.buttonTab);
            this.tabControl1.ItemSize = new System.Drawing.Size(400, 25);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 3;
            // 
            // screenTab
            // 
            this.screenTab.Controls.Add(this.label6);
            this.screenTab.Controls.Add(this.txtBankId_Screen);
            this.screenTab.Controls.Add(this.getScreen);
            this.screenTab.Controls.Add(this.gv_Screen);
            this.screenTab.Location = new System.Drawing.Point(4, 29);
            this.screenTab.Name = "screenTab";
            this.screenTab.Padding = new System.Windows.Forms.Padding(3);
            this.screenTab.Size = new System.Drawing.Size(768, 393);
            this.screenTab.TabIndex = 0;
            this.screenTab.Text = "Screen";
            this.screenTab.UseVisualStyleBackColor = true;
            // 
            // buttonTab
            // 
            this.buttonTab.Controls.Add(this.label3);
            this.buttonTab.Controls.Add(this.label2);
            this.buttonTab.Controls.Add(this.label1);
            this.buttonTab.Controls.Add(this.txtScreenId);
            this.buttonTab.Controls.Add(this.txtBranchId);
            this.buttonTab.Controls.Add(this.txtBankId);
            this.buttonTab.Controls.Add(this.getButtons);
            this.buttonTab.Controls.Add(this.gv_Button);
            this.buttonTab.Location = new System.Drawing.Point(4, 29);
            this.buttonTab.Name = "buttonTab";
            this.buttonTab.Padding = new System.Windows.Forms.Padding(3);
            this.buttonTab.Size = new System.Drawing.Size(768, 393);
            this.buttonTab.TabIndex = 1;
            this.buttonTab.Text = "Button";
            this.buttonTab.UseVisualStyleBackColor = true;
            // 
            // gv_Screen
            // 
            this.gv_Screen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Screen.Location = new System.Drawing.Point(0, 62);
            this.gv_Screen.Name = "gv_Screen";
            this.gv_Screen.Size = new System.Drawing.Size(765, 325);
            this.gv_Screen.TabIndex = 0;
            // 
            // gv_Button
            // 
            this.gv_Button.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Button.Location = new System.Drawing.Point(2, 78);
            this.gv_Button.Name = "gv_Button";
            this.gv_Button.Size = new System.Drawing.Size(765, 310);
            this.gv_Button.TabIndex = 1;
            // 
            // getButtons
            // 
            this.getButtons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getButtons.Location = new System.Drawing.Point(674, 15);
            this.getButtons.Name = "getButtons";
            this.getButtons.Size = new System.Drawing.Size(88, 45);
            this.getButtons.TabIndex = 2;
            this.getButtons.Text = "Get Buttons";
            this.getButtons.UseVisualStyleBackColor = true;
            this.getButtons.Click += new System.EventHandler(this.getButtons_Click);
            // 
            // txtBankId
            // 
            this.txtBankId.Location = new System.Drawing.Point(92, 28);
            this.txtBankId.Name = "txtBankId";
            this.txtBankId.Size = new System.Drawing.Size(100, 20);
            this.txtBankId.TabIndex = 3;
            // 
            // txtBranchId
            // 
            this.txtBranchId.Location = new System.Drawing.Point(314, 28);
            this.txtBranchId.Name = "txtBranchId";
            this.txtBranchId.Size = new System.Drawing.Size(100, 20);
            this.txtBranchId.TabIndex = 4;
            // 
            // txtScreenId
            // 
            this.txtScreenId.Location = new System.Drawing.Point(551, 28);
            this.txtScreenId.Name = "txtScreenId";
            this.txtScreenId.Size = new System.Drawing.Size(100, 20);
            this.txtScreenId.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bank Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(228, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Branch Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(462, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Screen Id";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Bank Id";
            // 
            // txtBankId_Screen
            // 
            this.txtBankId_Screen.Location = new System.Drawing.Point(106, 24);
            this.txtBankId_Screen.Name = "txtBankId_Screen";
            this.txtBankId_Screen.Size = new System.Drawing.Size(100, 20);
            this.txtBankId_Screen.TabIndex = 10;
            // 
            // getScreen
            // 
            this.getScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getScreen.Location = new System.Drawing.Point(254, 14);
            this.getScreen.Name = "getScreen";
            this.getScreen.Size = new System.Drawing.Size(96, 38);
            this.getScreen.TabIndex = 9;
            this.getScreen.Text = "Get Screen";
            this.getScreen.UseVisualStyleBackColor = true;
            this.getScreen.Click += new System.EventHandler(this.getScreen_Click);
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Services";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.screenTab.ResumeLayout(false);
            this.screenTab.PerformLayout();
            this.buttonTab.ResumeLayout(false);
            this.buttonTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Screen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Button)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage screenTab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBankId_Screen;
        private System.Windows.Forms.Button getScreen;
        private System.Windows.Forms.DataGridView gv_Screen;
        private System.Windows.Forms.TabPage buttonTab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScreenId;
        private System.Windows.Forms.TextBox txtBranchId;
        private System.Windows.Forms.TextBox txtBankId;
        private System.Windows.Forms.Button getButtons;
        private System.Windows.Forms.DataGridView gv_Button;
    }
}

