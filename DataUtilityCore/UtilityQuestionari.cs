using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Web;
using System.Data.Entity.Migrations;

namespace DataUtilityCore
{
    public static class UtilityQuestionari
    {
        public static int? CreateSaveQuestionari(long iDRapportoControlloTecnico)
        {
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            int? IDQuestionario = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var questionarioCheck = ctx.COM_Questionari.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();

                if (questionarioCheck == null)
                {
                    var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                                                            & c.IDStatoRapportoDiControllo == 2
                                                                           ).FirstOrDefault();

                    bool fRaccomandazioniPrescrizioni = false;
                    if (rapporto.Raccomandazioni != null || rapporto.Prescrizioni != null)
                    {
                        fRaccomandazioniPrescrizioni = true;
                    }

                    COM_Questionari questionario = new COM_Questionari();
                    questionario.IDRapportoControlloTecnico = rapporto.IDRapportoControlloTecnico;
                    questionario.IDStatoQuestionario = 1;
                    questionario.IDUtente = (int)info.IDUtente;
                    questionario.DataInserimento = DateTime.Now;
                    ctx.COM_Questionari.Add(questionario);

                    var domande = ctx.SYS_QuestionarioDomande.Where(a => a.fAttivo == true).ToList();
                    foreach (var domanda in domande)
                    {
                        if (!domanda.fRaccomandazioniPrescrizioni && !fRaccomandazioniPrescrizioni)
                        {
                            COM_QuestionariDomande questionarioDomanda = new COM_QuestionariDomande();
                            questionarioDomanda.COM_Questionari = questionario;
                            questionarioDomanda.Domanda = domanda.Domanda;
                            questionarioDomanda.Note = null;
                            ctx.COM_QuestionariDomande.Add(questionarioDomanda);

                            var risposte = ctx.SYS_QuestionarioRisposte.Where(a => a.fAttivo == true && a.IDQuestionarioDomanda == domanda.IDQuestionarioDomanda).ToList();
                            foreach (var risposta in risposte)
                            {
                                COM_QuestionariRisposte questionarioRisposta = new COM_QuestionariRisposte();
                                questionarioRisposta.COM_QuestionariDomande = questionarioDomanda;
                                questionarioRisposta.IDQuestionarioTipoRisposta = risposta.IDQuestionarioTipoRisposta;
                                questionarioRisposta.Risposta = risposta.Risposta;
                                questionarioRisposta.fRisposta = false;
                                ctx.COM_QuestionariRisposte.Add(questionarioRisposta);
                            }
                        }
                    }

                    ctx.SaveChanges();
                    IDQuestionario = questionario.IDQuestionario;
                }
                else
                {
                    var Questionario = ctx.COM_Questionari.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                    IDQuestionario = Questionario.IDQuestionario;
                    //TODO: qui dovrei salvare
                }
                
                return IDQuestionario;
            }
        }

        public static string GetSqlValoriQuestionariFilter(
            object IDStatoQuestionario
            )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_COM_Questionari ");
            strSql.Append(" WHERE IDStatoRapportoDiControllo=2 ");

            if (IDStatoQuestionario.ToString() != "0")
            {
                strSql.Append(" AND IDStatoQuestionario=");
                strSql.Append(IDStatoQuestionario);
            }
            strSql.Append(" AND convert(varchar(10), DataControllo, 126) <= '");
            DateTime dataFiltro = DateTime.Now.AddMonths(-3);
            string newdataFiltroFormat = dataFiltro.ToString("yyyy") + "-" + dataFiltro.ToString("MM") + "-" + dataFiltro.ToString("dd");
            strSql.Append(newdataFiltroFormat);
            strSql.Append("'");
            
            strSql.Append(" ORDER BY DataControllo DESC");

            return strSql.ToString();
        }

    }
}
