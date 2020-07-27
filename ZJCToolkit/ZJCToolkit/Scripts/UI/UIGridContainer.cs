using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJCToolkit.UI;

/// <summary>
/// 
/// </summary>
public class UIGridContainer<T> where T : Control, IUIBase, new()
{
    private int _MaxCount = 0;
    /// <summary>
    /// 当前Grid面板内组件的数量
    /// 每次发生变动的时候,会进行面板的变动
    /// </summary>
    public int MaxCount
    {
        set
        {
            _MaxCount = value;
            UpdateControlList();
        }
        get
        {
            return _MaxCount;
        }
    }

    /// <summary>
    /// 组件的挂载节点
    /// </summary>
    public Control Root = null;
    /// <summary>
    /// 创建出来的所有的组件列表
    /// </summary>
    public List<T> controlList = new List<T>();


    public UIGridContainer(Control root)
    {
        this.Root = root;
    }

    private void UpdateControlList()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            T t = null;
            if (controlList.Count < i + 1)
            {
                t = new T();
                controlList.Add(t);
                Root.Controls.Add(t);
            }
            else
            {
                t = controlList[i];
            }
            t.SetActive(true);
        }
        if (controlList.Count > MaxCount)
        {
            for (int i = MaxCount; i < controlList.Count; i++)
            {
                T t = controlList[i];
                t.SetActive(false);
            }
        }
    }
}
