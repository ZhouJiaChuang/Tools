using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJCToolkit.UI.Window;

namespace ZJCToolkit
{
    public class UIRootPanel : Form
    {
        private Panel UIContentContainer;
        private Panel UIBarContainer;

        public UIRootPanel()
        {
            InitializeComponent();
            UIManager.Instance.Init(UIBarContainer, UIContentContainer);

            UIManager.Instance.SwitchTitle<UIMainTiltleBarPanel>();
            UIManager.Instance.CreatePanel<UIMainPanel>();
        }
        

        private void InitializeComponent()
        {
            this.UIBarContainer = new System.Windows.Forms.Panel();
            this.UIContentContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // UIBarContainer
            // 
            this.UIBarContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UIBarContainer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.UIBarContainer.Location = new System.Drawing.Point(0, 0);
            this.UIBarContainer.Name = "UIBarContainer";
            this.UIBarContainer.Size = new System.Drawing.Size(1110, 30);
            this.UIBarContainer.TabIndex = 1;
            // 
            // UIContentContainer
            // 
            this.UIContentContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UIContentContainer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UIContentContainer.Location = new System.Drawing.Point(0, 27);
            this.UIContentContainer.Name = "UIContentContainer";
            this.UIContentContainer.Size = new System.Drawing.Size(1110, 573);
            this.UIContentContainer.TabIndex = 2;
            // 
            // UIRootPanel
            // 
            this.ClientSize = new System.Drawing.Size(1110, 600);
            this.Controls.Add(this.UIContentContainer);
            this.Controls.Add(this.UIBarContainer);
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIRootPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
