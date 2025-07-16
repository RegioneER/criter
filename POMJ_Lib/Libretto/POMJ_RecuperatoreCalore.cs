using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Si tratta di dispositivi che effettuano il ricambio di aria ambiente recuperando il calore presente in essa tramite uno scambiatore e immettendo dell’aria esterna tramite ventilatori. Tali apparecchiature possono essere montate singolarmente o inserire in Unità di trattamento aria (UTA) o in sistemi di ventilazione meccanica controllata (VMC
    /// </summary>
    public class POMJ_RecuperatoreCalore
    {
        /// <summary>
        /// Data di installazione
        /// </summary>
        public System.DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Indicare la tipologia costruttiva, ovvero se trattasi di recuperatori statici,
        /// a piastre, a flusso incrociato, ecc.
        /// </summary>
        public string Tipologia { get; set; }
        /// <summary>
        /// Indicare se installato all‘interno di un sistema U.T.A. (Unità Trattamento Aria) o 
        /// V.M.C ( Ventilazione Meccanica Controllata) o se sia un'apparecchiatura indipendente
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Installato in U.T.A.o V.M.C.</item>
        /// <item>2	Indipendente</item>
        /// </list>
        /// </summary>
        [Range(1, 2, ErrorMessage = "Modalità installazione recuperatori di calore valori consentiti compresi da 1 a 2")]
        public int ModalitaInstallazioneRecuperatoriCalore { get; set; }
        ///<summary>
        ///Portata Ventilatore Mandata Lts
        ///</summary>
        public decimal? PortataVentilatoreMandataLts { get; set; }
        ///<summary>
        ///Portata Ventilatore Ripresa Lts
        ///</summary>
        public decimal? PortataVentilatoreRipresaLts { get; set; }
        ///<summary>
        ///Potenza Ventilatore Mandata Lts
        ///</summary>
        public decimal? PotenzaVentilatoreMandataKw { get; set; }
        ///<summary>
        ///Potenza Ventilatore Ripresa Lts
        ///</summary>
        public decimal? PotenzaVentilatoreRipresaKw { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }

    }
}