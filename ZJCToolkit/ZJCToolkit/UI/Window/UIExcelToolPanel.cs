using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJCToolkit.UI.Excel;

namespace ZJCToolkit.UI.Window
{
    class UIExcelToolPanel : Form, IUIBase
    {
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel ExcelToolContainerPanel;
        private Button button1;

        public UIExcelToolPanel()
        {
            InitializeComponent();

            UIManager.Instance.AddPanel<UIExcel_TranslateToolPanel>(this.ExcelToolContainerPanel);
        }

        public void Init(params object[] objs)
        {
        }

        public void SetActive(bool active)
        {
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.ExcelToolContainerPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(874, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Excel转换";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ExcelToolContainerPanel
            // 
            this.ExcelToolContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcelToolContainerPanel.Location = new System.Drawing.Point(0, 33);
            this.ExcelToolContainerPanel.Name = "ExcelToolContainerPanel";
            this.ExcelToolContainerPanel.Size = new System.Drawing.Size(874, 439);
            this.ExcelToolContainerPanel.TabIndex = 1;
            // 
            // UIExcelToolPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(874, 471);
            this.Controls.Add(this.ExcelToolContainerPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UIExcelToolPanel";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
