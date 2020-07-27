using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJCToolkit;
using ZJCToolkit.UI;

public class UIManager
{
    public UIRootPanel RootPanel;
    public Panel UITilteBarContainer;
    public Panel UIContentContainer;

    private static UIManager m_instance;
    public static UIManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new UIManager();
                m_instance.Init();
            }

            return m_instance;
        }
    }
    private void Init()
    {
        RootPanel = new UIRootPanel();
    }

    public void Init(Panel tilteBarContainer,Panel contentContainer)
    {
        UITilteBarContainer = tilteBarContainer;
        UIContentContainer = contentContainer;
    }

    /// <summary>
    /// 切换标题面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SwitchTitle<T>(params object[] objs) where T : Form, IUIBase, new()
    {
        for(int i=0;i < UITilteBarContainer.Controls.Count; i++)
        {
            Control control = UITilteBarContainer.Controls[0];
            if (control.GetType() == typeof(T))
            {
                return;
            }
            UITilteBarContainer.Controls.Remove(control);
        }
        AddPanel<T>(UITilteBarContainer, objs);
    }

    /// <summary>
    /// 创建面板到主面板中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void CreatePanel<T>(params object[] objs) where T : Form, IUIBase, new()
    {
        // 每次加载子窗口的时候关闭其他窗口
        for (int i = 0; i < UIContentContainer.Controls.Count; i++)
        {
            Control control = UIContentContainer.Controls[0];
            if(control.GetType() == typeof(T))
            {
                return;
            }
            UIContentContainer.Controls.Remove(control);
        }
        AddPanel<T>(UIContentContainer, objs);
    }

    /// <summary>
    /// 添加面板到指定Panel下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    public void AddPanel<T>(Panel parent, params object[] objs) where T : Form, IUIBase, new()
    {
        T t = new T();
        t.TopLevel = false;
        t.FormBorderStyle = FormBorderStyle.None;
        t.Dock = DockStyle.Fill;
        t.Init(objs);
        t.Show();
        parent.Controls.Add(t);
    }
}