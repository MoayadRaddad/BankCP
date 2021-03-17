
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
            this.getScreen = new System.Windows.Forms.Button();
            this.gv_Screen = new System.Windows.Forms.DataGridView();
            this.buttonTab = new System.Windows.Forms.TabPage();
            this.textBranchId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScreenId = new System.Windows.Forms.TextBox();
            this.getButtons = new System.Windows.Forms.Button();
            this.gv_Button = new System.Windows.Forms.DataGridView();
            this.txtBankId = new System.Windows.Forms.TextBox();
            this.lblBankId = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.textBankId = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.screenTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Screen)).BeginInit();
            this.buttonTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Button)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.screenTab);
            this.tabControl1.Controls.Add(this.buttonTab);
            this.tabControl1.ItemSize = new System.Drawing.Size(400, 25);
            this.tabControl1.Location = new System.Drawing.Point(12, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 439);
            this.tabControl1.TabIndex = 3;
            // 
            // screenTab
            // 
            this.screenTab.Controls.Add(this.getScreen);
            this.screenTab.Controls.Add(this.gv_Screen);
            this.screenTab.Location = new System.Drawing.Point(4, 29);
            this.screenTab.Name = "screenTab";
            this.screenTab.Padding = new System.Windows.Forms.Padding(3);
            this.screenTab.Size = new System.Drawing.Size(768, 406);
            this.screenTab.TabIndex = 0;
            this.screenTab.Text = "Screen";
            this.screenTab.UseVisualStyleBackColor = true;
            // 
            // getScreen
            // 
            this.getScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getScreen.Location = new System.Drawing.Point(666, 18);
            this.getScreen.Name = "getScreen";
            this.getScreen.Size = new System.Drawing.Size(96, 38);
            this.getScreen.TabIndex = 9;
            this.getScreen.Text = "Get Screen";
            this.getScreen.UseVisualStyleBackColor = true;
            this.getScreen.Click += new System.EventHandler(this.getScreen_Click);
            // 
            // gv_Screen
            // 
            this.gv_Screen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Screen.Location = new System.Drawing.Point(0, 62);
            this.gv_Screen.Name = "gv_Screen";
            this.gv_Screen.Size = new System.Drawing.Size(765, 338);
            this.gv_Screen.TabIndex = 0;
            // 
            // buttonTab
            // 
            this.buttonTab.Controls.Add(this.textBranchId);
            this.buttonTab.Controls.Add(this.label3);
            this.buttonTab.Controls.Add(this.label2);
            this.buttonTab.Controls.Add(this.txtScreenId);
            this.buttonTab.Controls.Add(this.getButtons);
            this.buttonTab.Controls.Add(this.gv_Button);
            this.buttonTab.Location = new System.Drawing.Point(4, 29);
            this.buttonTab.Name = "buttonTab";
            this.buttonTab.Padding = new System.Windows.Forms.Padding(3);
            this.buttonTab.Size = new System.Drawing.Size(768, 406);
            this.buttonTab.TabIndex = 1;
            this.buttonTab.Text = "Button";
            this.buttonTab.UseVisualStyleBackColor = true;
            // 
            // textBranchId
            // 
            this.textBranchId.Location = new System.Drawing.Point(100, 28);
            this.textBranchId.Name = "textBranchId";
            this.textBranchId.Size = new System.Drawing.Size(100, 20);
            this.textBranchId.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(239, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Screen Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Branch Id";
            // 
            // txtScreenId
            // 
            this.txtScreenId.Location = new System.Drawing.Point(328, 29);
            this.txtScreenId.Name = "txtScreenId";
            this.txtScreenId.Size = new System.Drawing.Size(100, 20);
            this.txtScreenId.TabIndex = 5;
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
            // gv_Button
            // 
            this.gv_Button.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Button.Location = new System.Drawing.Point(2, 66);
            this.gv_Button.Name = "gv_Button";
            this.gv_Button.Size = new System.Drawing.Size(765, 334);
            this.gv_Button.TabIndex = 1;
            // 
            // txtBankId
            // 
            this.txtBankId.Location = new System.Drawing.Point(0, 0);
            this.txtBankId.Name = "txtBankId";
            this.txtBankId.Size = new System.Drawing.Size(100, 20);
            this.txtBankId.TabIndex = 0;
            // 
            // lblBankId
            // 
            this.lblBankId.AutoSize = true;
            this.lblBankId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankId.Location = new System.Drawing.Point(16, 21);
            this.lblBankId.Name = "lblBankId";
            this.lblBankId.Size = new System.Drawing.Size(57, 18);
            this.lblBankId.TabIndex = 16;
            this.lblBankId.Text = "Bank Id";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(303, 21);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(84, 18);
            this.lblUserName.TabIndex = 18;
            this.lblUserName.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(394, 22);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 20);
            this.txtUserName.TabIndex = 17;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(590, 21);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(75, 18);
            this.lblPassword.TabIndex = 20;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(681, 22);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 19;
            // 
            // textBankId
            // 
            this.textBankId.Location = new System.Drawing.Point(89, 22);
            this.textBankId.Name = "textBankId";
            this.textBankId.Size = new System.Drawing.Size(100, 20);
            this.textBankId.TabIndex = 10;
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 499);
            this.Controls.Add(this.textBankId);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblBankId);
            this.Controls.Add(this.tabControl1);
            this.Name = "Services";
            this.Text = "WCF Bank Services Tester";
            this.tabControl1.ResumeLayout(false);
            this.screenTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv_Screen)).EndInit();
            this.buttonTab.ResumeLayout(false);
            this.buttonTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Button)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage screenTab;
        private System.Windows.Forms.Button getScreen;
        private System.Windows.Forms.DataGridView gv_Screen;
        private System.Windows.Forms.TabPage buttonTab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtScreenId;
        private System.Windows.Forms.TextBox txtBankId;
        private System.Windows.Forms.Button getButtons;
        private System.Windows.Forms.DataGridView gv_Button;
        private System.Windows.Forms.Label lblBankId;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox textBankId;
        private System.Windows.Forms.TextBox textBranchId;
    }
}

