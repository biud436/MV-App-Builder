﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using System.Diagnostics;

namespace Cordova.Forms
{
    using Manage;

    public partial class SettingsDialog : DarkForm
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataMan.Instance.Use = darkCheckBoxEnabled.Checked;
            DataMan.Instance.AudioFileFormat = darkCheckBoxOgg.Checked ? "ogg" : "m4a";
            DataMan.Instance.RemainTree = darkCheckBoxTrue.Checked;
            DataMan.Instance.Save();
        }

        private void darkCheckBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (darkCheckBoxEnabled.Checked)
            {
                darkCheckBoxDisabled.Checked = false;
            }
            else
            {
                darkCheckBoxDisabled.Checked = true;
            }
        }

        private void darkCheckBoxDisabled_CheckedChanged(object sender, EventArgs e)
        {
            if (darkCheckBoxDisabled.Checked)
            {
                darkCheckBoxEnabled.Checked = false;
            }
            else
            {
                darkCheckBoxEnabled.Checked = true;
            }
        }

        private void darkCheckBoxOgg_CheckedChanged(object sender, EventArgs e)
        {
            if (darkCheckBoxOgg.Checked)
            {
                darkCheckBoxM4a.Checked = false;
            }
            else
            {
                darkCheckBoxM4a.Checked = true;
            }
        }

        private void darkCheckBoxM4a_CheckedChanged(object sender, EventArgs e)
        {
            if (darkCheckBoxM4a.Checked)
            {
                darkCheckBoxOgg.Checked = false;
            }
            else
            {
                darkCheckBoxOgg.Checked = true;
            }
        }

        private void darkCheckBoxTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (darkCheckBoxTrue.Checked)
            {
                darkCheckBoxFalse.Checked = false;
            }
            else
            {
                darkCheckBoxFalse.Checked = true;
            }
        }

        private void darkCheckBoxFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (darkCheckBoxFalse.Checked)
            {
                darkCheckBoxTrue.Checked = false;
            }
            else
            {
                darkCheckBoxTrue.Checked = true;
            }
        }

        private void darkButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            darkCheckBoxEnabled.Checked = DataMan.Instance.Use ? true : false;
            darkCheckBoxDisabled.Checked = DataMan.Instance.Use ? false : true;
            darkCheckBoxOgg.Checked = DataMan.Instance.AudioFileFormat == "ogg" ? true : false;
            darkCheckBoxM4a.Checked = DataMan.Instance.AudioFileFormat == "ogg" ? false : true;
            darkCheckBoxTrue.Checked = DataMan.Instance.RemainTree ? true : false;
            darkCheckBoxFalse.Checked = DataMan.Instance.RemainTree ? false : true;

            // TODO: 안드로이드 SDK 경로를 출력하는 다크 트리 기능
            // 자동으로 찾아서 설정하지만 찾지 못했을 경우에는 오류가 날 수 있음.
            var parentNode = new DarkUI.Controls.DarkTreeNode("ANDROID_SDK");
            parentNode.Nodes.Add(new DarkUI.Controls.DarkTreeNode(DataManager.Instance.AndroidSDKPath));
            darkPathTree.Nodes.Add(parentNode);

            parentNode = new DarkUI.Controls.DarkTreeNode("JAVA_HOME");
            parentNode.Nodes.Add(new DarkUI.Controls.DarkTreeNode(DataManager.Instance.JDKPath));
            darkPathTree.Nodes.Add(parentNode);

            // 출력 폴더를 바꾼 적이 있다면
            if (DataMan.Instance.IsValidCustomOutputPath)
            {
                darkTextBoxOutputFolderPath.Text = DataMan.Instance.OutputPath;
            } else
            {
                // 출력 폴더를 바꾸지 않았다면
                darkTextBoxOutputFolderPath.Text = DataManager.Instance.GetRootDirectory();
            }


        }

        private void darkButtonSetOutputFolderPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                darkTextBoxOutputFolderPath.Text = folderBrowserDialog1.SelectedPath;
                DataMan.Instance.OutputPath = folderBrowserDialog1.SelectedPath;
                DataMan.Instance.IsValidCustomOutputPath = true;
                DataMan.Instance.Save();
            }
        }
    }
}
