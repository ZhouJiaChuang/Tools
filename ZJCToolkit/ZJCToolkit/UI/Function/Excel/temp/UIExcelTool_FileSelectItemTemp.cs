using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZJCToolkit.UI.Function.Excel
{
    class UIExcelTool_FileSelectItemTemp : UserControl, IUIBase
    {
        private TextBox FilePath;
        private CheckBox Toggle_Select;

        public UIExcelTool_FileSelectItemTemp()
        {
            InitializeComponent();
        }

        public void SetActive(bool active)
        {
            this.Visible = active;
        }

        public void Init(params object[] objs)
        {
            if (objs == null || objs.Length < 1) return;
            this.FilePath.Text = objs[0].ToString();
            this.Toggle_Select.Checked = false;
        }

        private void InitializeComponent()
        {
            this.Toggle_Select = new System.Windows.Forms.CheckBox();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Toggle_Select
            // 
            this.Toggle_Select.AutoSize = true;
            this.Toggle_Select.Location = new System.Drawing.Point(14, 9);
            this.Toggle_Select.Name = "Toggle_Select";
            this.Toggle_Select.Size = new System.Drawing.Size(15, 14);
            this.Toggle_Select.TabIndex = 0;
            this.Toggle_Select.UseVisualStyleBackColor = true;
            // 
            // FilePath
            // 
            this.FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePath.Location = new System.Drawing.Point(35, 9);
            this.FilePath.Margin = new System.Windows.Forms.Padding(0);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(813, 14);
            this.FilePath.TabIndex = 1;
            this.FilePath.Text = "ww";
            // 
            // UIExcelTool_FileSelectItemTemp
            // 
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.Toggle_Select);
            this.Name = "UIExcelTool_FileSelectItemTemp";
            this.Size = new System.Drawing.Size(865, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
