using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJCToolkit.UI.Excel;

namespace ZJCToolkit.UI.Window
{
    class UIMainTiltleBarPanel : Form, IUIBase
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem Btn_MainPanel;
        private ToolStripMenuItem Btn_ExcelTool;
        private ToolStripMenuItem Btn_ExcelTranslate;
        private ToolStripMenuItem Btn_Unity;
        private ToolStripMenuItem Btn_UnityAndroidManager;
        private ToolStripMenuItem Btn_UnityAndoird_RXZG;

        public UIMainTiltleBarPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Btn_MainPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_ExcelTool = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_ExcelTranslate = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Unity = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_UnityAndroidManager = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_UnityAndoird_RXZG = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_MainPanel,
            this.Btn_ExcelTool,
            this.Btn_Unity});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Btn_MainPanel
            // 
            this.Btn_MainPanel.Name = "Btn_MainPanel";
            this.Btn_MainPanel.Size = new System.Drawing.Size(56, 21);
            this.Btn_MainPanel.Text = "主界面";
            this.Btn_MainPanel.Click += new System.EventHandler(this.Btn_MainPanel_Click);
            // 
            // Btn_ExcelTool
            // 
            this.Btn_ExcelTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_ExcelTranslate});
            this.Btn_ExcelTool.Name = "Btn_ExcelTool";
            this.Btn_ExcelTool.Size = new System.Drawing.Size(68, 21);
            this.Btn_ExcelTool.Text = "表格工具";
            // 
            // Btn_ExcelTranslate
            // 
            this.Btn_ExcelTranslate.Name = "Btn_ExcelTranslate";
            this.Btn_ExcelTranslate.Size = new System.Drawing.Size(180, 22);
            this.Btn_ExcelTranslate.Text = "Excel转换";
            this.Btn_ExcelTranslate.Click += new System.EventHandler(this.Btn_ExcelTranslate_Click);
            // 
            // Btn_Unity
            // 
            this.Btn_Unity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_UnityAndroidManager});
            this.Btn_Unity.Name = "Btn_Unity";
            this.Btn_Unity.Size = new System.Drawing.Size(49, 21);
            this.Btn_Unity.Text = "Untiy";
            // 
            // Btn_UnityAndroidManager
            // 
            this.Btn_UnityAndroidManager.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_UnityAndoird_RXZG});
            this.Btn_UnityAndroidManager.Name = "Btn_UnityAndroidManager";
            this.Btn_UnityAndroidManager.Size = new System.Drawing.Size(180, 22);
            this.Btn_UnityAndroidManager.Text = "Android工程管理";
            // 
            // Btn_UnityAndoird_RXZG
            // 
            this.Btn_UnityAndoird_RXZG.Name = "Btn_UnityAndoird_RXZG";
            this.Btn_UnityAndoird_RXZG.Size = new System.Drawing.Size(206, 22);
            this.Btn_UnityAndoird_RXZG.Text = "王者2.5D项目(热血之光)";
            this.Btn_UnityAndoird_RXZG.Click += new System.EventHandler(this.Btn_UnityAndoird_RXZG_Click);
            // 
            // UIMainTiltleBarPanel
            // 
            this.ClientSize = new System.Drawing.Size(980, 71);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UIMainTiltleBarPanel";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void Init(params object[] objs)
        {
        }

        public void SetActive(bool active)
        {
        }

        private void Btn_MainPanel_Click(object sender, EventArgs e)
        {
            UIManager.Instance.CreatePanel<UIMainPanel>();
        }

        private void Btn_ExcelTranslate_Click(object sender, EventArgs e)
        {
            UIManager.Instance.CreatePanel<UIExcel_TranslateToolPanel>();
        }

        private void Btn_UnityAndoird_RXZG_Click(object sender, EventArgs e)
        {
            UIManager.Instance.CreatePanel<UIAndroidPanel>();
        }
    }
}
