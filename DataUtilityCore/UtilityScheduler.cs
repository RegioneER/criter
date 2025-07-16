using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Hangfire;
using Z.EntityFramework.Plus;

namespace DataUtilityCore
{
    public static class UtilityScheduler
    {
        public static string Schedule(Expression<Action> action, DateTime scheduledDate)
        {
            TimeSpan delay = TimeSpan.Zero;

            var currentDate = DateTime.Now;
            if (scheduledDate > currentDate)
                delay = scheduledDate.Subtract(currentDate);

            return BackgroundJob.Schedule(action, delay);
        }

        public static string MinuteInterval(int interval)
        {
            return string.Format("*/{0} * * * *", interval);
        }

        public static string HourInterval(int interval)
        {
            return string.Format("0 */{0} * * *", interval);
        }


        public static void AutomaticDeleteBozzeRevisioniLibretti()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["CancellazioneLibrettiBozzaRevisionatiEnabled"]))
            {
                double DaysCancellazioneLibrettiBozzaRevisionati = Convert.ToDouble(ConfigurationManager.AppSettings["DaysCancellazioneLibrettiBozzaRevisionati"]);
                double DaysEmailNotifyCancellazioneLibrettiBozzaRevisionati = Convert.ToDouble(ConfigurationManager.AppSettings["DaysEmailNotifyCancellazioneLibrettiBozzaRevisionati"]);

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    #region Cancella Libretti in Bozza
                    try
                    {
                        string sqlQueryLibrettiBozza = "SELECT * FROM LIM_LibrettiImpianti WHERE (fAttivo = 1) AND (IDStatoLibrettoImpianto = 1) AND (CONVERT(varchar(10), DATEADD(day, " + DaysCancellazioneLibrettiBozzaRevisionati + ", DataInserimento), 120) <= CONVERT(varchar(10), GETDATE(), 120)) ";
                        var LibrettiBozza = ctx.LIM_LibrettiImpianti.SqlQuery(sqlQueryLibrettiBozza).ToList();

                        foreach (var libretto in LibrettiBozza)
                        {
                            UtilityLibrettiImpianti.DeleteLibrettoImpianto(libretto.IDLibrettoImpianto);
                        }

                        //var ListIDLibrettiInBozza = LibrettiBozza.Select(r => r.IDLibrettoImpianto).ToList();
                        //ctx.LIM_LibrettiImpianti.Where(c => ListIDLibrettiInBozza.Contains(c.IDLibrettoImpianto)).Update(c => new LIM_LibrettiImpianti()
                        //{
                        //    fAttivo = false
                        //});

                        //var LibrettiBozzaToRemove = ctx.LIM_LibrettiImpianti.Where(c => ListIDLibrettiInBozza.Contains(c.IDLibrettoImpianto)).ToList();
                        //ctx.LIM_LibrettiImpianti.RemoveRange(LibrettiBozza);
                        //ctx.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                    #endregion

                    #region Cancellazione e ripristino Libretti in Revisione
                    try
                    {
                        string sqlQueryLibrettiRevisionati = "SELECT * FROM LIM_LibrettiImpianti WHERE (fAttivo = 1) AND (IDStatoLibrettoImpianto = 3) AND (CONVERT(varchar(10), DATEADD(day, " + DaysCancellazioneLibrettiBozzaRevisionati + ", DataRevisione), 120) <= CONVERT(varchar(10), GETDATE(), 120)) ";
                        var LibrettiRevisionati = ctx.LIM_LibrettiImpianti.SqlQuery(sqlQueryLibrettiRevisionati).ToList();

                        foreach (var libretto in LibrettiRevisionati)
                        {
                            UtilityLibrettiImpianti.RipristinaRevisione(libretto.IDLibrettoImpianto);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    #endregion

                    #region Notifiche Email pre-cancellazione
                    try
                    {
                        string sqlQueryLibrettiBozzaNotifiche = "SELECT * FROM LIM_LibrettiImpianti WHERE (fAttivo = 1) AND (IDStatoLibrettoImpianto = 1) AND (CONVERT(varchar(10), DATEADD(day, " + DaysEmailNotifyCancellazioneLibrettiBozzaRevisionati + ", DataInserimento), 120) = CONVERT(varchar(10), GETDATE(), 120)) ";
                        var LibrettiBozzaNotifiche = ctx.LIM_LibrettiImpianti.SqlQuery(sqlQueryLibrettiBozzaNotifiche).ToList();
                        if (LibrettiBozzaNotifiche.Count > 0)
                        {
                            EmailNotify.SendMailCancellazioneLibrettiBozza(LibrettiBozzaNotifiche);
                        }

                        string sqlQueryLibrettiRevisionatiNotifiche = "SELECT * FROM LIM_LibrettiImpianti WHERE (fAttivo = 1) AND (IDStatoLibrettoImpianto = 3) AND (CONVERT(varchar(10), DATEADD(day, " + DaysEmailNotifyCancellazioneLibrettiBozzaRevisionati + ", DataRevisione), 120) = CONVERT(varchar(10), GETDATE(), 120)) ";
                        var LibrettiRevisionatiNotifiche = ctx.LIM_LibrettiImpianti.SqlQuery(sqlQueryLibrettiRevisionatiNotifiche).ToList();

                        if (LibrettiRevisionatiNotifiche.Count > 0)
                        {
                            EmailNotify.SendMailCancellazioneLibrettiRevisionati(LibrettiRevisionatiNotifiche);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    #endregion
                }
            }

            if (bool.Parse(ConfigurationManager.AppSettings["CancellazioneRctBozzaEnabled"]))
            {
                double DaysCancellazioneRctBozza = Convert.ToDouble(ConfigurationManager.AppSettings["DaysCancellazioneRctBozza"]);
                double DaysEmailNotifyCancellazioneRctBozza = Convert.ToDouble(ConfigurationManager.AppSettings["DaysEmailNotifyCancellazioneRctBozza"]);

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    #region Cancellazione RTC in bozza
                    try
                    {
                        string sqlQueryRctBozza = "SELECT * FROM RCT_RapportoDiControlloTecnicoBase WHERE (IDStatoRapportoDiControllo = 1) AND (guidInteroImpianto IS NULL) AND (CONVERT(varchar(10), DATEADD(day, " + DaysCancellazioneRctBozza + ", DataInserimento), 120) <= CONVERT(varchar(10), GETDATE(), 120)) ";
                        var RctBozza = ctx.RCT_RapportoDiControlloTecnicoBase.SqlQuery(sqlQueryRctBozza).ToList();

                        foreach (var rct in RctBozza)
                        {
                            UtilityRapportiControllo.DeleteRct((int)rct.IDRapportoControlloTecnico);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    #endregion

                    #region Notifiche Email pre-cancellazione
                    try
                    {
                        double DayNotificaRct = (DaysCancellazioneRctBozza - DaysEmailNotifyCancellazioneRctBozza);

                        string sqlQueryRctBozzaNotifiche = "SELECT * FROM RCT_RapportoDiControlloTecnicoBase WHERE (IDStatoRapportoDiControllo = 1) AND (guidInteroImpianto IS NULL) AND (CONVERT(varchar(10), DATEADD(day, " + DayNotificaRct + ", DataInserimento), 120) = CONVERT(varchar(10), GETDATE(), 120)) ";
                        var RctBozzaNotifiche = ctx.RCT_RapportoDiControlloTecnicoBase.SqlQuery(sqlQueryRctBozzaNotifiche).ToList();
                        if (RctBozzaNotifiche.Count > 0)
                        {
                            EmailNotify.SendMailCancellazioneRctBozza(RctBozzaNotifiche);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    #endregion

                }
            }

        }

        public static void ControllaSanzioniScadute()
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    #region Sanzione scaduta per scritti difensivi
                    var sanzioniScadutePerScrittiDifensivi = ctx.VER_Accertamento.Where(c => c.IDStatoAccertamentoSanzione == 3
                                                                            && c.DataScadenzaSanzione <= DateTime.Now
                                                                            ).ToList();

                    if (sanzioniScadutePerScrittiDifensivi != null)
                    {
                        foreach (var sanzione in sanzioniScadutePerScrittiDifensivi)
                        {
                            UtilityVerifiche.CambiaStatoSanzione(sanzione.IDAccertamento, 4);
                        }
                    }
                    #endregion

                    #region Sanzione scaduta per scritti difensivi
                    var sanzioniScadutePerPagamentoFormaRidotta = ctx.VER_Accertamento.Where(c => c.IDStatoAccertamentoSanzione == 4
                                                                            && c.DataScadenzaPagamentoRidottoSanzione <= DateTime.Now
                                                                            ).ToList();

                    if (sanzioniScadutePerPagamentoFormaRidotta != null)
                    {
                        foreach (var sanzione in sanzioniScadutePerPagamentoFormaRidotta)
                        {
                            UtilityVerifiche.CambiaStatoSanzione(sanzione.IDAccertamento, 7);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
