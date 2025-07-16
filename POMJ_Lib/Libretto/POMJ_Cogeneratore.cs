using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Cogeneratore. Nel caso di impianti, aventi come sottosistema di generazione, 
    /// più cogeneratori e/o trigeneratori dovranno essere create tante entità quante 
    /// sono le singole apparecchiature
    /// </summary>
    public class POMJ_Cogeneratore
    {
        /// <summary>
        /// Data di installazione
        /// </summary>
        public System.DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Fabbricante
        /// </summary>
        public string Fabbricante { get; set; }
        /// <summary>
        /// Dato desumibile dalla documentazione del produttore
        /// </summary>
        public string Modello { get; set; }
        /// <summary>
        /// Dato desumibile dalla documentazione del produttore
        /// </summary>
        public string Matricola { get; set; }
        /// <summary>
        /// <list type="bullet">
        /// <item> 1 Cogeneratore</item>
        /// <item> 2 Trigeneratore</item>
        /// </list>
        /// </summary>
        [Range(1, 2, ErrorMessage = "Tipologia cogeneratore valori consentiti compresi da 1 a 2")]
        public int TipologiaCogeneratore { get; set; }
        /// <summary>
        /// <list type="bullet">
        /// <item> 1 Altro</item>
        /// <item> 2 Gas naturale</item>
        /// <item> 3 Gpl </item>
        /// <item> 4 Gasolio</item>
        /// <item> 5 Olio combustibile </item>
        /// <item> 6 Pellet</item>
        /// <item> 7 Legna </item>
        /// <item> 8 Cippato</item>
        /// <item> 9 Bricchette </item>
        /// <item> 10 Biogas </item>
        /// <item> 11 Syngas</item>
        /// <item> 12 Kerosene</item>
        /// <item> 13 Biodiesel </item>
        /// <item> 14 Aria propanata</item>
        /// </list>
        /// </summary>
        [Range(1, 14, ErrorMessage = "Tipologia di combustibile valori consentiti compresi da 1 a 14")]
        public int TipologiaCombustibile { get; set; }
        /// <summary>
        /// Altro tipo di combustibile
        /// </summary>
        public string CombustibileAltro { get; set; }
        /// <summary>
        /// Indicare il valore, espresso in kW, definito dal fabbricante in condizioni di massimo recupero
        /// </summary>
        public decimal? PotenzaTermicaNominaleKw { get; set; }
        /// <summary>
        /// Indicare il valore, espresso in kW, definito dal fabbricante ai morsetti del generatore
        /// </summary>
        public decimal? PotenzaElettricaNominaleKw { get; set; }
        /// <summary>
        /// Temperatura minima dell'acqua in uscita, espressa in gradi centigradi (°C),
        /// al sistema di generazione definita dal fabbricante nella documentazione di prodotto o in targa del generatore
        /// </summary>
        public decimal? TemperaturaAcquaUscitaGradiMin { get; set; }
        /// <summary>
        /// Temperatura massima dell'acqua in uscita, espressa in gradi centigradi (°C),
        /// al sistema di generazione definita dal fabbricante nella documentazione di prodotto o in targa del generatore
        /// </summary>
        public decimal? TemperaturaAcquaUscitaGradiMax { get; set; }
        /// <summary>
        /// Temperatura minima dell'acqua in ingresso, espressa in gradi centigradi (°C),
        /// al sistema di generazione definita dal fabbricante nella documentazione di prodotto o in targa del generatore
        /// </summary>
        public decimal? TemperaturaAcquaIngressoGradiMin { get; set; }
        /// <summary>
        /// Temperatura massima dell'acqua in ingresso, espressa in gradi centigradi (°C),
        /// al sistema di generazione definita dal fabbricante nella documentazione di prodotto o in targa del generatore
        /// </summary>
        public decimal? TemperaturaAcquaIngressoGradiMax { get; set; }
        /// <summary>
        /// Temperatura minima dell'acqua all‘interno del motore, espressa in gradi centigradi (°C),
        /// definita dal fabbricante nella documentazione di prodotto o in targa del generatore
        /// </summary>
        public decimal? TemperaturaAcquaMotoreMin { get; set; }
        /// <summary>
        /// Temperatura massima dell'acqua all'interno del motore, espressa in gradi centigradi (°C),
        /// definita dal fabbricante nella documentazione di prodotto o in targa del generatore
        /// </summary>
        public decimal? TemperaturaAcquaMotoreMax { get; set; }
        /// <summary>
        /// Temperatura minima dei fumi di scarico, espresse in gradi centigradi (°C), a 
        /// valle dello scambiatore di calore così come definita dal fabbricante nella 
        /// documentazione di prodotto
        /// </summary>
        public decimal? TemperaturaFumiValleMin { get; set; }
        /// <summary>
        /// Temperatura massima dei fumi di scarico, espresse in gradi centigradi (°C), a 
        /// valle dello scambiatore di calore così come definita dal fabbricante nella 
        /// documentazione di prodotto
        /// </summary>
        public decimal? TemperaturaFumiValleMax { get; set; }
        /// <summary>
        /// Temperatura minima dei fumi di scarico, espresse in gradi centigradi (°C), a 
        /// monte dello scambiatore di calore così come definita dal fabbricante nella 
        /// documentazione di prodotto
        /// </summary>
        public decimal? TemperaturaFumiMonteMin { get; set; }
        /// <summary>
        /// Temperatura massima dei fumi di scarico, espresse in gradi centigradi (°C), a 
        /// monte dello scambiatore di calore così come definita dal fabbricante nella 
        /// documentazione di prodotto
        /// </summary>
        public decimal? TemperaturaFumiMonteMax { get; set; }
        /// <summary>
        /// indicare il valore della concentrazione minima di monossido di carbonio (CO) nei fumi 
        /// in uscita del generatore espresso in mg/Nm3 riportati al valore di riferimento del 5% di 
        /// ossigeno (O2) nei fumi stessi
        /// </summary>
        public decimal? EmissioniCOMin { get; set; }
        /// <summary>
        /// indicare il valore della concentrazione massima di monossido di carbonio (CO) nei fumi 
        /// in uscita del generatore espresso in mg/Nm3 riportati al valore di riferimento del 5% di 
        /// ossigeno (O2) nei fumi stessi
        /// </summary>
        public decimal? EmissioniCOMax { get; set; }
        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }

        /// <summary>
        /// Data di dismissione
        /// </summary>
        public string DataDismissione { get; set; }

        /// <summary>
        /// Data di sostituzione
        /// </summary>
        public string DataSostituzione { get; set; }
    }
}