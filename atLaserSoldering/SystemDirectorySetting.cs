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
using RecipeManager;

namespace atOpticalDecenter
{
    public partial class SystemDirectorySetting : DevExpress.XtraEditors.XtraForm
    {
        string _strRootFolderPath = string.Empty;
        string _strLogFolderPath = string.Empty;
        string _strRecipeFolderPath = string.Empty;
        string _strResultFolderPath = string.Empty;
        string _strSystemFolderPath = string.Empty;
        public string RootFolderPath
        {
            get { return _strRootFolderPath; }
        }

        public string LogFolderPath
        {
            get { return _strLogFolderPath; }
        }

        public string RecipeFolderPath
        {
            get { return _strRecipeFolderPath; }
        }

        public string ResultFolderPath
        {
            get { return _strResultFolderPath; }
        }

        public string SystemFolderPath
        {
            get { return _strSystemFolderPath; }
        }

        public SystemDirectorySetting()
        {
            InitializeComponent();

            _strRootFolderPath = SystemDirectoryParams.RootFolderPath;
            _strLogFolderPath = SystemDirectoryParams.LogFolderPath;
            _strRecipeFolderPath = SystemDirectoryParams.RecipeFolderPath;
            _strResultFolderPath = SystemDirectoryParams.ResultFolderPath;
            _strSystemFolderPath = SystemDirectoryParams.SystemFolderPath;

            buttonEditRootFolderPath.Text = SystemDirectoryParams.RootFolderPath;
            buttonEditLogFolderPath.Text = SystemDirectoryParams.LogFolderPath;
            buttonEditRecipeFolderPath.Text = SystemDirectoryParams.RecipeFolderPath;
            buttonEditResultDataFolderPath.Text = SystemDirectoryParams.ResultFolderPath;
            buttonEditSystemFolderPath.Text = SystemDirectoryParams.SystemFolderPath;

            buttonEditLogFolderPath.Enabled = false;
            buttonEditRecipeFolderPath.Enabled = false;
            buttonEditResultDataFolderPath.Enabled = false;
            buttonEditSystemFolderPath.Enabled = false;
        }

        private void buttonEditRootFolderPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            xtraFolderBrowserDialog1.Title = "변경할 루트 폴더를 선택해주세요.";

            if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("루트 폴더를 변경하면, 나머지 폴더의 경로가 자동변경됩니다.\r\n모두 변경하시겠습니까?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string strNewRoot = string.Format(@"{0}\Autonics\atOpticalDecenters", xtraFolderBrowserDialog1.SelectedPath);

                    _strRootFolderPath = strNewRoot;
                    _strLogFolderPath = string.Format(@"{0}\Log", strNewRoot);
                    _strRecipeFolderPath = string.Format(@"{0}\Recipe", strNewRoot);
                    _strResultFolderPath = string.Format(@"{0}\Result", strNewRoot);
                    _strSystemFolderPath = string.Format(@"{0}\System", strNewRoot);

                    buttonEditRootFolderPath.Text = _strRootFolderPath;
                    buttonEditLogFolderPath.Text = _strLogFolderPath;
                    buttonEditRecipeFolderPath.Text = _strRecipeFolderPath;
                    buttonEditResultDataFolderPath.Text = _strResultFolderPath;
                    buttonEditSystemFolderPath.Text = _strSystemFolderPath;
                }
            }
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (_strRootFolderPath == SystemDirectoryParams.RootFolderPath)
            {
                if (MessageBox.Show("시스템 루트 폴더 경로가 이전과 동일합니다. 계속하시겠습니까?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }
    }
}