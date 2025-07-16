using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CriterPortal.SiWebControls
{
    public class SiUpdatePanel : UpdatePanel
    {
        public SiUpdatePanel()
        {
            
        }

        protected override void DataBindChildren()
        {
            if (Visible)
                base.DataBindChildren();
        }
    }
}