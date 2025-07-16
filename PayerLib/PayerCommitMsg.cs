using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayerLib
{
    public class PayerCommitMsg
    {
        //<CommitMsg>
        //<PortaleID></PortaleID>
        //<NumeroOperazione></NumeroOperazione>
        //<IDOrdine></IDOrdine>
        //<Commit></Commit>
        //</CommitMsg>

        public string PortaleID { get; set; }
        public  string NumeroOperazione { get; set; }
        public string IDOrdine { get; set; }
        public string Commit { get; set; }

        public string ToXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<CommitMsg>");
            sb.AppendFormat("<PortaleID>{0}</PortaleID>", PortaleID);
            sb.AppendFormat("<NumeroOperazione>{0}</NumeroOperazione>", NumeroOperazione);
            sb.AppendFormat("<IDOrdine>{0}</IDOrdine>", IDOrdine);
            sb.AppendFormat("<Commit>{0}</Commit>", Commit);
            sb.Append("</CommitMsg>");

            return sb.ToString();

        }
    }
}
