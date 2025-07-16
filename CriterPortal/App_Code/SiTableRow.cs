using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace CriterPortal.SiWebControls
{
    /// <summary>
    /// Summary description for SiTableRow
    /// </summary>
    public class SiTableRow : TableRow
    {
        public SiTableRow()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void DataBindChildren()
        {
            if (this.Visible)
            {
                base.DataBindChildren();
            }
        }
    }
}