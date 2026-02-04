namespace atOpticalDecenter
{
    partial class LoginForm
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.textEditWorkerID = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditWorkerName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditJobInformation = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonLogIn = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditWorkerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditWorkerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditJobInformation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButtonExit);
            this.layoutControl1.Controls.Add(this.simpleButtonLogIn);
            this.layoutControl1.Controls.Add(this.textEditJobInformation);
            this.layoutControl1.Controls.Add(this.textEditWorkerName);
            this.layoutControl1.Controls.Add(this.textEditWorkerID);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(315, 116);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(298, 118);
            this.Root.TextVisible = false;
            // 
            // textEditWorkerID
            // 
            this.textEditWorkerID.EditValue = "10000000";
            this.textEditWorkerID.Location = new System.Drawing.Point(70, 12);
            this.textEditWorkerID.Name = "textEditWorkerID";
            this.textEditWorkerID.Size = new System.Drawing.Size(216, 20);
            this.textEditWorkerID.StyleController = this.layoutControl1;
            this.textEditWorkerID.TabIndex = 4;
            this.textEditWorkerID.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.textEditWorkerID_EditValueChanging);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditWorkerID;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(278, 24);
            this.layoutControlItem1.Text = "작업자 사번";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(54, 14);
            // 
            // textEditWorkerName
            // 
            this.textEditWorkerName.EditValue = "김철수";
            this.textEditWorkerName.Location = new System.Drawing.Point(70, 36);
            this.textEditWorkerName.Name = "textEditWorkerName";
            this.textEditWorkerName.Size = new System.Drawing.Size(216, 20);
            this.textEditWorkerName.StyleController = this.layoutControl1;
            this.textEditWorkerName.TabIndex = 5;
            this.textEditWorkerName.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.textEditWorkerName_EditValueChanging);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditWorkerName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(278, 24);
            this.layoutControlItem2.Text = "작업자 이름";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(54, 14);
            // 
            // textEditJobInformation
            // 
            this.textEditJobInformation.EditValue = "00000000-0000";
            this.textEditJobInformation.Location = new System.Drawing.Point(70, 60);
            this.textEditJobInformation.Name = "textEditJobInformation";
            this.textEditJobInformation.Size = new System.Drawing.Size(216, 20);
            this.textEditJobInformation.StyleController = this.layoutControl1;
            this.textEditJobInformation.TabIndex = 6;
            this.textEditJobInformation.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.textEditJobInformation_EditValueChanging);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditJobInformation;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(278, 24);
            this.layoutControlItem3.Text = "작업 지시서";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(54, 14);
            // 
            // simpleButtonLogIn
            // 
            this.simpleButtonLogIn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButtonLogIn.Location = new System.Drawing.Point(12, 84);
            this.simpleButtonLogIn.Name = "simpleButtonLogIn";
            this.simpleButtonLogIn.Size = new System.Drawing.Size(135, 22);
            this.simpleButtonLogIn.StyleController = this.layoutControl1;
            this.simpleButtonLogIn.TabIndex = 7;
            this.simpleButtonLogIn.Text = "로그인";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonLogIn;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(139, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButtonExit
            // 
            this.simpleButtonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonExit.Location = new System.Drawing.Point(151, 84);
            this.simpleButtonExit.Name = "simpleButtonExit";
            this.simpleButtonExit.Size = new System.Drawing.Size(135, 22);
            this.simpleButtonExit.StyleController = this.layoutControl1;
            this.simpleButtonExit.TabIndex = 8;
            this.simpleButtonExit.Text = "종료";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonExit;
            this.layoutControlItem5.Location = new System.Drawing.Point(139, 72);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(139, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 116);
            this.Controls.Add(this.layoutControl1);
            this.Name = "LoginForm";
            this.Text = "로그";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditWorkerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditWorkerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditJobInformation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
        private DevExpress.XtraEditors.SimpleButton simpleButtonLogIn;
        private DevExpress.XtraEditors.TextEdit textEditJobInformation;
        private DevExpress.XtraEditors.TextEdit textEditWorkerName;
        private DevExpress.XtraEditors.TextEdit textEditWorkerID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}