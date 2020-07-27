using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJCToolkit.UI
{
    public interface IUIBase
    {
        void Init(params object[] objs);
        void SetActive(bool active);
    }
}
