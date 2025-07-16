using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomiager.Service.Parer
{
    public class dtoSendParameters
    {
        public string PecGuid { get; set; }

        public DateTime PecSendDate { get; set; }

        public string PecObject { get; set; }

        public string PecManager { get; set; }

        public string PecBody { get; set; } 

        public string PecFrom { get; set; }

        public string PecTo { get; set; }

        public string PecToName { get; set; }

        public string PecToType { get; set; }

        public string PecToSurname { get; set; }

        public string PecAttributes { get; set; }

        public List<PecAttachment> PecAttachments { get; set; }

        public PecDocumentMain PecDocumentMain { get; set; }

        public dtoSendParameters()
        {
            PecDocumentMain = new PecDocumentMain();
            PecAttachments = new List<PecAttachment>();            
        }
    }

    public class PecDocumentMain
    {
        public string IDDocument { get; set; }
        public Stream StreamDocument { get; set; } //FileStream
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        
    }

    public class PecAttachment
    {
        public string IDAttachment { get; set; }
        public Stream StreamAttachment { get; set; } //FileStream
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        
    }
}
