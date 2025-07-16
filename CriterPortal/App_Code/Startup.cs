using Hangfire;
using Hangfire.SqlServer;
using Owin;
using Microsoft.Owin;
using DataUtilityCore;

[assembly: OwinStartup(typeof(Startup))]
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var options = new SqlServerStorageOptions
        {
            PrepareSchemaIfNecessary = false,
            QueuePollInterval = System.TimeSpan.FromSeconds(60)
        };

        var dashboardoptions = new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthFilter() }
        };

        GlobalConfiguration.Configuration.UseSqlServerStorage("DBConnection", options);
        app.UseHangfireDashboard("/hangfire", dashboardoptions);
        app.UseHangfireServer();

        /*
        //ogni 10 minuti controllo se ci accertamenti in stato sospeso da riportare ad assegnato ad accertatore
        RecurringJob.AddOrUpdate("1", () => UtilityVerifiche.ControllaAccertamentiSospesi(), UtilityScheduler.MinuteInterval(10));

        //Controllo se ci sono file di esito recapiti da scaricare
        //RecurringJob.AddOrUpdate("2", () => UtilityNexive.DownloadFileEsitoFromNexive(), Cron.Daily);

        //ogni 20 minuti controllo se ci interventu su accertamenti che non sono stati completati
        RecurringJob.AddOrUpdate("3", () => UtilityVerifiche.ControllaInterventiScaduti(), UtilityScheduler.MinuteInterval(20));

        //ogni 30 minuti disattivo eventuali terzi responsabili scaduti
        RecurringJob.AddOrUpdate("5", () => UtilityLibrettiImpianti.DisabledExpiredTerziResponsabili(), UtilityScheduler.MinuteInterval(30));

        

        RecurringJob.AddOrUpdate("7", () => UtilityVerifiche.RefreshLibrettiAttiviInVisiteIspettive(), UtilityScheduler.MinuteInterval(50));

        //RecurringJob.AddOrUpdate("4", () => ReportingServices.CreateReportsEnti(), "0 9 * * *"); //Ogni giorno alle ore 3 -->https://crontab.guru/

        RecurringJob.AddOrUpdate("8", () => UtilityPosteItaliane.DownloadEsitiROLFromPosteItaliane(), Cron.Daily);

        RecurringJob.AddOrUpdate("9", () => UtilityVerifiche.RimandaInRicercaIspettoreNoFirmaLAI(), UtilityScheduler.MinuteInterval(60));

        RecurringJob.AddOrUpdate("10", () => UtilityVerifiche.RimandaInRicercaIspettoreNoPianificazione(), UtilityScheduler.MinuteInterval(65));
        
        RecurringJob.AddOrUpdate("11", () => UtilityScheduler.AutomaticDeleteBozzeRevisioniLibretti(), "0 22 * * *"); //Ogni giorno alle ore 22 -->https://crontab.guru/
        
        RecurringJob.AddOrUpdate("12", () => UtilityScheduler.ControllaSanzioniScadute(), "0 21 * * *");  

        RecurringJob.AddOrUpdate("13", () => UtilityVerifiche.NotificaIspettoreMancataChiusuraIspezione(), "0 20 * * *");

        RecurringJob.AddOrUpdate("14", () => UtilityPosteItaliane.DownloadEsitiAGOLFromPosteItaliane(), "0 19 * * *");
       */
    }
}