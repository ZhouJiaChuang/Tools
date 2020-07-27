using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZJCToolkit.UI.Function.Excel
{
    class UIExcelTool_FolderSelctPathTemp : UserControl, IUIBase
    {
        private Button Btn_SVNUpdate;
        private Button Btn_Revert;
        private Button Btn_SVNLog;
        private Label PathName;
        private ComboBox FolderKeyBox;
        private TextBox Input_FolderPath;
        private Button Btn_Remove;
        private Button Btn_Add;
        private Button Btn_SVNCommit;
        private Button Btn_OpenFolder;
        private Dictionary<string, string> FolderDic = new Dictionary<string, string>();

        public UIExcelTool_FolderSelctPathTemp(string name)
        {
            InitializeComponent();
            PathName.Text = name;
        }
        
        private void InitializeComponent()
        {
            this.Btn_SVNUpdate = new System.Windows.Forms.Button();
            this.Btn_Revert = new System.Windows.Forms.Button();
            this.Btn_SVNLog = new System.Windows.Forms.Button();
            this.Btn_SVNCommit = new System.Windows.Forms.Button();
            this.PathName = new System.Windows.Forms.Label();
            this.FolderKeyBox = new System.Windows.Forms.ComboBox();
            this.Input_FolderPath = new System.Windows.Forms.TextBox();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.Btn_OpenFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_SVNUpdate
            // 
            this.Btn_SVNUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SVNUpdate.Location = new System.Drawing.Point(754, 4);
            this.Btn_SVNUpdate.Name = "Btn_SVNUpdate";
            this.Btn_SVNUpdate.Size = new System.Drawing.Size(55, 22);
            this.Btn_SVNUpdate.TabIndex = 2;
            this.Btn_SVNUpdate.Text = "更新";
            this.Btn_SVNUpdate.UseVisualStyleBackColor = true;
            this.Btn_SVNUpdate.Click += new System.EventHandler(this.Btn_SVNUpdate_Click);
            // 
            // Btn_Revert
            // 
            this.Btn_Revert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Revert.Location = new System.Drawing.Point(815, 4);
            this.Btn_Revert.Name = "Btn_Revert";
            this.Btn_Revert.Size = new System.Drawing.Size(55, 22);
            this.Btn_Revert.TabIndex = 3;
            this.Btn_Revert.Text = "还原";
            this.Btn_Revert.UseVisualStyleBackColor = true;
            this.Btn_Revert.Click += new System.EventHandler(this.Btn_Revert_Click);
            // 
            // Btn_SVNLog
            // 
            this.Btn_SVNLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SVNLog.Location = new System.Drawing.Point(876, 4);
            this.Btn_SVNLog.Name = "Btn_SVNLog";
            this.Btn_SVNLog.Size = new System.Drawing.Size(55, 22);
            this.Btn_SVNLog.TabIndex = 4;
            this.Btn_SVNLog.Text = "日志";
            this.Btn_SVNLog.UseVisualStyleBackColor = true;
            this.Btn_SVNLog.Click += new System.EventHandler(this.Btn_SVNLog_Click);
            // 
            // Btn_SVNCommit
            // 
            this.Btn_SVNCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SVNCommit.Location = new System.Drawing.Point(937, 4);
            this.Btn_SVNCommit.Name = "Btn_SVNCommit";
            this.Btn_SVNCommit.Size = new System.Drawing.Size(55, 22);
            this.Btn_SVNCommit.TabIndex = 5;
            this.Btn_SVNCommit.Text = "提交";
            this.Btn_SVNCommit.UseVisualStyleBackColor = true;
            this.Btn_SVNCommit.Click += new System.EventHandler(this.Btn_SVNCommit_Click);
            // 
            // PathName
            // 
            this.PathName.AutoSize = true;
            this.PathName.Location = new System.Drawing.Point(4, 8);
            this.PathName.Name = "PathName";
            this.PathName.Size = new System.Drawing.Size(29, 12);
            this.PathName.TabIndex = 6;
            this.PathName.Text = "描述";
            // 
            // FolderKeyBox
            // 
            this.FolderKeyBox.FormattingEnabled = true;
            this.FolderKeyBox.Location = new System.Drawing.Point(87, 4);
            this.FolderKeyBox.Name = "FolderKeyBox";
            this.FolderKeyBox.Size = new System.Drawing.Size(97, 20);
            this.FolderKeyBox.TabIndex = 7;
            this.FolderKeyBox.SelectedIndexChanged += new System.EventHandler(this.FolderKeyBox_SelectedIndexChanged);
            // 
            // Input_FolderPath
            // 
            this.Input_FolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Input_FolderPath.Location = new System.Drawing.Point(190, 4);
            this.Input_FolderPath.Name = "Input_FolderPath";
            this.Input_FolderPath.Size = new System.Drawing.Size(502, 21);
            this.Input_FolderPath.TabIndex = 8;
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Remove.Location = new System.Drawing.Point(698, 4);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(22, 22);
            this.Btn_Remove.TabIndex = 9;
            this.Btn_Remove.Text = "-";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Add.Location = new System.Drawing.Point(726, 4);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(22, 22);
            this.Btn_Add.TabIndex = 10;
            this.Btn_Add.Text = "+";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_OpenFolder
            // 
            this.Btn_OpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_OpenFolder.Location = new System.Drawing.Point(998, 4);
            this.Btn_OpenFolder.Name = "Btn_OpenFolder";
            this.Btn_OpenFolder.Size = new System.Drawing.Size(55, 22);
            this.Btn_OpenFolder.TabIndex = 11;
            this.Btn_OpenFolder.Text = "打开";
            this.Btn_OpenFolder.UseVisualStyleBackColor = true;
            this.Btn_OpenFolder.Click += new System.EventHandler(this.Btn_OpenFolder_Click);
            // 
            // UIExcelTool_FolderSelctPathTemp
            // 
            this.Controls.Add(this.Btn_OpenFolder);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Input_FolderPath);
            this.Controls.Add(this.FolderKeyBox);
            this.Controls.Add(this.PathName);
            this.Controls.Add(this.Btn_SVNCommit);
            this.Controls.Add(this.Btn_SVNLog);
            this.Controls.Add(this.Btn_Revert);
            this.Controls.Add(this.Btn_SVNUpdate);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UIExcelTool_FolderSelctPathTemp";
            this.Size = new System.Drawing.Size(1056, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// 初始化目录下拉列表
        /// </summary>
        /// <param name="data">格式为:描述(key)#路径(Path)&描述(key)#路径(Path)</param>
        public void InitComboBox(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            Dictionary<string, object> dic = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;

            foreach (var i in dic)
            {
                AddFloderCombox(i.Key, i.Value.ToString());
            }
        }

        public string GetSetting()
        {
            return MiniJSON.Json.Serialize(FolderDic);
        }


        private void AddFloderCombox(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;

            if (string.IsNullOrEmpty(value)) return;

            if (!FolderDic.ContainsKey(key))
                FolderKeyBox.Items.Add(key);

            FolderDic[key] = value;
        }

        public void SetActive(bool active)
        {
            this.Visible = active;
        }

        public void Init(params object[] objs)
        {
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            AddFloderCombox(FolderKeyBox.Text, Input_FolderPath.Text);
            CSNetwork.SendClientEvent(EClientEvent.ExcelTranslateFolderSettingUpdate);
        }

        private void Btn_SVNUpdate_Click(object sender, EventArgs e)
        {
            Utility.SVNUpdate(Input_FolderPath.Text);
        }

        private void Btn_Revert_Click(object sender, EventArgs e)
        {
            Utility.SVNRevert(Input_FolderPath.Text);
        }

        private void Btn_SVNLog_Click(object sender, EventArgs e)
        {
            Utility.SVNLog(Input_FolderPath.Text);
        }

        private void Btn_SVNCommit_Click(object sender, EventArgs e)
        {
            Utility.SVNCommit(Input_FolderPath.Text);
        }

        private void FolderKeyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = FolderKeyBox.SelectedItem.ToString();
            if (FolderDic.ContainsKey(key))
            {
                Input_FolderPath.Text = FolderDic[key];
            }
        }

        private void Btn_OpenFolder_Click(object sender, EventArgs e)
        {
            Utility.OpenFolder(Input_FolderPath.Text);
        }
    }
}
