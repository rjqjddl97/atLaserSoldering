using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace atOpticalDecenter
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public string WorkerID { get; set; } = string.Empty;
        public string WorkerName { get; set; } = string.Empty;
        public string JobInformation { get; set; } = string.Empty;
        public LoginForm()
        {
            InitializeComponent();
        }
        public LoginForm(string strWorkID, string strWorkerName, string strJobInformation)
        {
            InitializeComponent();
            textEditWorkerID.Text = strWorkID;
            textEditWorkerName.Text = strWorkerName;
            textEditJobInformation.Text = strJobInformation;
        }

        private void textEditWorkerID_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.WorkerID = Convert.ToString(e.NewValue);
        }

        private void textEditWorkerName_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.WorkerName = Convert.ToString(e.NewValue);
        }

        private void textEditJobInformation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.JobInformation = Convert.ToString(e.NewValue);
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(this.WorkerID))
                {
                    MessageBox.Show("작업자 사번이 입력되지 않았습니다.", "사번 누락", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Cancel = true;

                    return;
                }
                else
                {
                    int value;

                    if (int.TryParse(this.WorkerID, out value))
                    {
                        if (value < 10000000 || value > 11000000)
                        {
                            MessageBox.Show("작업자 사번이 잘 못 입력되었습니다.", "사번 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            e.Cancel = true;

                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("작업자 사번이 잘 못 입력되었습니다.", "사번 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        e.Cancel = true;

                        return;
                    }
                }

                if (string.IsNullOrEmpty(this.WorkerName))
                {
                    MessageBox.Show("작업자 이름이 입력되지 않았습니다.", "이름 누락", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Cancel = true;

                    return;
                }

                if (string.IsNullOrEmpty(this.JobInformation))
                {
                    MessageBox.Show("작업지시서가 입력되지 않았습니다.", "작업지시서 누락", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Cancel = true;

                    return;
                }
            }
        }
    }
}