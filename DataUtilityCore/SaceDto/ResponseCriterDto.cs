using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtilityCore.SaceDto
{
    public class ResponseCriterDto<T>
    {
        public bool Success { get; set; } 
        public string ErrorMessage { get; set; } 
        public T Result { get; set; } 

        // Costruttore per successo
        public ResponseCriterDto(T result)
        {
            Success = true;
            Result = result;
            ErrorMessage = string.Empty;
        }

        // Costruttore per errore
        public ResponseCriterDto(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
            Result = default;
        }
    }

    public class ResponseLibrettiImpiantiDto
    {
        public string Azienda { get; set; }

        public string OperatoreAddetto { get; set; }

        public string Responsabile { get; set; }

        public string CFPIvaResponsabile { get; set; }

        public string Indirizzo { get; set; }

        public string Comune { get; set; }

        public string Pod { get; set; }

        public string Pdr { get; set; }

        public string Impianto { get; set; }

        public string CodiceTargatura { get; set; }

        public int IDTargaturaImpianto { get; set; }
        
    }

    public class ResponsePodPdrDto
    {
        /// <summary>
        /// Descrizione response
        /// </summary>
        public string Utenza { get; set; }
                
        /// <summary>
        /// Codice Pod
        /// </summary>
        public string CodicePod { get; set; }

        /// <summary>
        /// Codice Pdr
        /// </summary>
        public string CodicePdr { get; set; }


    }
}
