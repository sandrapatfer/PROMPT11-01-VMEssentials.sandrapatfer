using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Sessao3
{
    internal class RecorderEventInfo
    {
        public EventInfo Info { get; set; }
        public Control RecControl { get; set; }
        public void EventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("Control: {0}; EventName: {1}; EventDate: {2}; ArgType: {3}", sender.ToString(), Info.Name, 
                DateTime.Now.ToString(), args.GetType().ToString());
        }
    }

    public class SessionRecorder
    {
        private Form m_form;
        private List<RecorderEventInfo> m_eventRecorderList = new List<RecorderEventInfo>();

        // Recebe na construção o Form de que se pretende gravar os
        // eventos gerados durante um período de utilização
        public SessionRecorder(Form form)
        {
            m_form = form;
        }

        // Inicia o período de gravação de eventos
        public void StartRecorder(string eventFilter)
        {
            foreach (var c in m_form.Controls)
            {
                Type controlType = c.GetType();
                foreach (var ev in controlType.GetEvents())
                {
                    if (string.IsNullOrEmpty(eventFilter) || ev.Name.Contains(eventFilter))
                    {
                        var r = new RecorderEventInfo() { Info = ev, RecControl = (Control)c };
                        m_eventRecorderList.Add(r);
                        MethodInfo mi = r.GetType().GetMethod("EventHandler");
                        ev.AddEventHandler(c, Delegate.CreateDelegate(ev.EventHandlerType, r, mi));
                    }
                }
            }
        }

        // Termina o período de gravação de eventos
        public void StopRecorder()
        {
            foreach (var rec in m_eventRecorderList)
            {
                MethodInfo mi = rec.GetType().GetMethod("EventHandler");
                rec.Info.RemoveEventHandler(rec.RecControl, Delegate.CreateDelegate(rec.Info.EventHandlerType, rec, mi));
            }
        }

        // Guarda a informação sobre os eventos no ficheiro fileName
        public void SaveEvents(string fileName)
        { }

    }
}
