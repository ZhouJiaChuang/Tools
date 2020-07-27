using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CSNetwork : Singleton<CSNetwork>
{
    protected EventHandlerManager ClientEventHandler = new EventHandlerManager(EventHandlerManager.DispatchType.Event);

    public static void SendClientEvent(EClientEvent e, params object[] param)
    {
        Instance.ClientEventHandler.SendEvent(e, param);
    }
}