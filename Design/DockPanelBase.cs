using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeGenernate.Design
{
    public abstract class DockPanelBase<T> where T : DockContent, new()
    {
        private static T m_Instance;
        public static T GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
                m_Instance.FormClosing += new System.Windows.Forms.FormClosingEventHandler(m_Instance_FormClosing);
            } 
            return m_Instance;
        }

        static void m_Instance_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            m_Instance = null;
        }
    }
}
