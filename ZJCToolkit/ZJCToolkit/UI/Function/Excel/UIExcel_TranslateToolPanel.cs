using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJCToolkit.UI.Function.Excel;

namespace ZJCToolkit.UI.Excel
{
    class UIExcel_TranslateToolPanel : Form, IUIBase
    {
        private FlowLayoutPanel TableGridPanel;
        private FlowLayoutPanel FolderPathPanel;
        private UIGridContainer<UIExcelTool_FileSelectItemTemp> uIGridContainer = null;

        private UIExcelTool_FolderSelctPathTemp SelectFilePath = null;
        private UIExcelTool_FolderSelctPathTemp OutFolderPath = null;

        protected EventHandlerManager ClientEventHandler = new EventHandlerManager(EventHandlerManager.DispatchType.Event);

        public UIExcel_TranslateToolPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.TableGridPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.FolderPathPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // TableGridPanel
            // 
            this.TableGridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableGridPanel.AutoScroll = true;
            this.TableGridPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TableGridPanel.Location = new System.Drawing.Point(0, 80);
            this.TableGridPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TableGridPanel.Name = "TableGridPanel";
            this.TableGridPanel.Size = new System.Drawing.Size(764, 388);
            this.TableGridPanel.TabIndex = 0;
            // 
            // FolderPathPanel
            // 
            this.FolderPathPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderPathPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FolderPathPanel.Location = new System.Drawing.Point(0, 0);
            this.FolderPathPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FolderPathPanel.Name = "FolderPathPanel";
            this.FolderPathPanel.Size = new System.Drawing.Size(764, 80);
            this.FolderPathPanel.TabIndex = 1;
            // 
            // UIExcel_TranslateToolPanel
            // 
            this.ClientSize = new System.Drawing.Size(764, 467);
            this.Controls.Add(this.FolderPathPanel);
            this.Controls.Add(this.TableGridPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UIExcel_TranslateToolPanel";
            this.ResumeLayout(false);

        }


        public void Init(params object[] objs)
        {
            uIGridContainer = new UIGridContainer<UIExcelTool_FileSelectItemTemp>(TableGridPanel);
            uIGridContainer.MaxCount = 20;

            SelectFilePath = new UIExcelTool_FolderSelctPathTemp("表格目录");
            SelectFilePath.Show();
            FolderPathPanel.Controls.Add(SelectFilePath);


            OutFolderPath = new UIExcelTool_FolderSelctPathTemp("输出目录:");
            OutFolderPath.Show();
            FolderPathPanel.Controls.Add(OutFolderPath);

            ClientEventHandler.AddEvent(EClientEvent.ExcelTranslateFolderSettingUpdate, OnExcelTranslateFolderSettingUpdate);

            LoadExcel_TranslateFolderSetting();
        }

        public void SetActive(bool active)
        {
        }

        private void OnExcelTranslateFolderSettingUpdate(uint uiEvtID, object[] data)
        {
            SaveExcel_TranslateFolderSetting();
        }

        private string cfg_file = "G:\\aa.txt";
        public void LoadExcel_TranslateFolderSetting()
        {
            string content = Utility.LoadFileContent(cfg_file);
            if (string.IsNullOrEmpty(content)) return;
            object jsonParsed = MiniJSON.Json.Deserialize(content);

            Dictionary<string, object> jsonMap = jsonParsed as Dictionary<string, object>;

            if (jsonMap == null) return;

            if (jsonMap.ContainsKey("SlectFolderData"))
                SelectFilePath.InitComboBox(jsonMap["SlectFolderData"].ToString());

            if (jsonMap.ContainsKey("OutFolderData"))
                OutFolderPath.InitComboBox(jsonMap["OutFolderData"].ToString());
        }

        public void SaveExcel_TranslateFolderSetting()
        {
            Dictionary<string, object> jsonMap = new Dictionary<string, object>();
            jsonMap["SlectFolderData"] = SelectFilePath.GetSetting();
            jsonMap["OutFolderData"] = OutFolderPath.GetSetting();

            string content = MiniJSON.Json.Serialize(jsonMap);
            Utility.SaveFileContent(cfg_file, content);
        }
    }
}
