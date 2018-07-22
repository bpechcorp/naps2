using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NAPS2_Alfresco.utils
{
    public class FormUtils
    {
        public static void HideForm(Form form)
        {
            if (form != null)
            {
                form.Hide();
            }
        }

        public static void ShowForm(Form form)
        {
            if (form != null)
            {
                form.Show();
            }
        }
    }
}
