<%@ Page Title="" Language="C#" MasterPageFile="~/BootstrapItalia.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="IscrizioneCriter.aspx.cs" Inherits="IscrizioneCriter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <div class="immaginelogo">
                <%--<img src="images/LogoCriter.png" alt="Logo Criter" title="Immagine Logo Criter" />--%>
                <h1>Accesso alla piattaforma CRITER</h1>
            </div>
            <span id="home">In questa pagina potrà trovare tutte le tipologie di soggetti che possono accedere alla piattaforma CRITER, le possibili funzionalità, la modalità di iscrizione/accreditamento e la possibilità di accedere alla piattaforma.
               
            </span>

            <div class="tab1" runat="server" visible="true">
                <h2 id="tipo1" class="grande"><u>IMPRESA DI INSTALLAZIONE, MANUTENZIONE o TERZO RESPONSABILE</u></h2>
                <h3>COSA PUOI FARE</h3>
                Accedendo al catasto come impresa di installazione o manutenzione potrai:
                <ul>
                    <li>Acquisire i codici di targatura di impianto</li>
                    <li>Procedere all’inserimento del libretto di impianto dei tuoi clienti, o modificarlo quando necessario</li>
                    <li>Scaricare e stampare il libretto di impianto</li>
                    <li>Acquisire i “bollini calore pulito” da assegnare ai clienti</li>
                    <li>Procedere all’inserimento dei rapporti di controllo tecnico di efficienza energetica</li>
                </ul>
                <h3>COSA DEVI AVERE PER ACCEDERE</h3>
                Per accedere al catasto regionale CRITER occorre preliminarmente effettuare la registrazione dell’impresa. La registrazione deve essere effettuata dal legale rappresentante, con le modalità indicate nell’apposita guida (clicca <a href="http://energia.regione.emilia-romagna.it/servizi-on-line/allegati/manualeimpresa_guidaallaregistrazionepreliminare_rev_01.pdf/at_download/file/manuale impresa_guida alla registrazione preliminare_rev_02_12_6.pdf" target="_blank">qui</a> per scaricare la “guida alla registrazione dell’impresa di installazione e/o manutenzione”)
                Sia per effettuare la registrazione che per accedere al catasto regionale CRITER devi essere in possesso:
                <ul>
                    <li>Di una identità digitale rilasciata dal sistema SPID (con credenziali di 2° livello) relativa al legale rappresentante (<a href="http://www.agid.gov.it/agenda-digitale/infrastrutture-architetture/spid " target="_blank">clicca qui per avere maggiori informazioni sul sistema SPID e sulle modalità di acquisizione dell’identità digitale</a>), oppure</li>
                    <li>Di credenziali di accesso rilasciate direttamente dal sistema CRITER: in questo caso, devi essere in possesso della firma digitale del legale rappresentante (<a href="http://www.agid.gov.it/agenda-digitale/infrastrutture-architetture/firme-elettroniche" target="_blank">clicca qui per avere maggiori informazioni sulla firma digitale</a>)</li>
                </ul>
                <div style="text-align: center !important">
                    <asp:Button runat="server" ID="lbIscrizioneImpresa" Width="720" OnClick="lbIscrizioneImpresa_Click" CssClass="buttonClass" Text="REGISTRATI COME IMPRESA O TERZO RESPONSABILE TRAMITE FIRMA DIGITALE" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="lbIscrizioneImpresaSpid" Width="720" OnClick="lbIscrizioneImpresaSpid_Click" CssClass="buttonClass" Text="REGISTRATI COME IMPRESA O TERZO RESPONSABILE TRAMITE SISTEMA SPID" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="lbAccessoImpresa" Width="720" OnClick="lbAccesso_Click" CssClass="buttonClass" Text="ACCEDI COME IMPRESA DI MANUTENZIONE O TERZO RESPONSABILE" />
                </div>
                <br />
            </div>

            <div class="tab3" runat="server" visible="true">
                <h2 id="tipo3" class="grande"><u>ISPETTORE</u></h2>
                <h3>COSA PUOI FARE</h3>
                Accedendo al catasto regionale CRITER come ispettore qualificato potrai:
                <ul>
                    <li>Visionare e prendere in carico le ispezioni da effettuare</li>
                    <li>Scaricare e stampare la documentazione tecnica necessaria per la effettuazione della ispezione</li>
                    <li>Visionare e prendere in carico le ispezioni da effettuare</li>
                    <li>Procedere all’inserimento dei rapporti di ispezione compilati</li>
                </ul>
                <h3>COSA DEVI AVERE PER ACCEDERE</h3>
                Per accedere al catasto regionale CRITER occorre preliminarmente effettuare la registrazione e la richiesta di accreditamento, ed espletare la relativa procedura come indicato nel disciplinare predisposto dall’Organismo di Accreditamento ed Ispezione. L’accreditamento è riservato ai soggetti in possesso dei requisiti richiesti (clicca qui per scaricare il “Disciplinare Accreditamento Ispettori”).<br />
                Sia per effettuare la registrazione che per accedere al catasto regionale CRITER devi essere in possesso di firma digitale (clicca qui per avere maggiori informazioni sulla firma digitale)<br /><br />
                <div style="text-align: center !important">
                    <asp:Button runat="server" ID="lbIscrizioneIspettore" Width="720" OnClick="lbIscrizioneIspettore_Click" CssClass="buttonClass" Text="REGISTRATI E RICHIEDI L’ACCREDITAMENTO COME ISPETTORE QUALIFICATO CRITER" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="lbAccessoIspettore" Width="720" OnClick="lbAccesso_Click" CssClass="buttonClass" Text="ACCEDI COME ISPETTORE QUALIFICATO CRITER" />
                </div>
                <br />
            </div>
            
            <div class="tab5" runat="server" visible="true">
                <h2 id="tipo5" class="grande"><u>DISTRIBUTORI DI ENERGIA PER GLI IMPIANTI TERMICI DEGLI EDIFICI</u></h2>
                <h3>COSA PUOI FARE</h3>
                Accedendo al catasto regionale CRITER come distributore di energia potrai inviare le informazioni ed i dati relativi all’ubicazione, alla titolarità e ai consumi degli impianti, assolvendo agli obblighi di legge:
                <ul>
                    <li>Trasmettere mediante file .xml i consumi annuali delle utenze asservite</li>
                </ul>
                <h3>COSA DEVI AVERE PER ACCEDERE</h3>
                Per accedere al catasto regionale CRITER occorre preliminarmente effettuare la registrazione e la richiesta di accreditamento.<br /><br />
                <div style="text-align: center !important">
                    <asp:Button runat="server" ID="lbIscrizioneDistributore" Width="720" OnClick="lbIscrizioneDistributore_Click" CssClass="buttonClass" Text="REGISTRATI COME DISTRIBUTORE DI COMBUSTIBILE" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="lbAccessoDistributore" Width="720" OnClick="lbAccesso_Click" CssClass="buttonClass" Text="ACCEDI COME DISTRIBUTORE DI COMBUSTIBILE" />
                </div>
                <br />
            </div>

            <div class="tab6" runat="server" visible="true">
                <h2 id="tipo6" class="grande"><u>CITTADINO O AMMINISTRATORE CONDOMINIALE RESPONSABILE DI IMPIANTO</u></h2>
                <h3>COSA PUOI FARE</h3>
                Accedendo al catasto regionale degli impianti termici CRITER potrai:
                <ul>
                    <li>Verificare se nel catasto è presente un impianto termico registrato per il quale sei stato indicato come responsabile</li>
                    <li>Consultare e scaricare il relativo libretto di impianto, e modificare i dati di tua competenza, quali
                        <li>punto 1.2 – Ubicazione dell’edificio: potrai quindi inserire i codici POD e PDR e/o i riferimenti catastali dell’immobile, qualora tali dati non siano già stati registrati dall’installatore o manutentore
                        </li>
                        <li>punto 1.6 – Anagrafica del Responsabile di Impianto: potrai quindi modificare tutti i dati identificativi (tranne il codice fiscale, che è la chiave di collegamento con le credenziali di accesso SPID)
                        </li>
                    </li>
                    <li>Consultare e scaricare i Rapporti di controllo che sono stati registrati dal manutentore a seguito degli interventi periodici effettuati</li>
                    <li>Comunicare la dismissione del generatore dell’impianto termico, o la sua successiva rimessa in servizio</li>
                </ul>
                <h3>COSA DEVI AVERE PER ACCEDERE</h3>
                Per accedere al catasto regionale CRITER è necessario essere in possesso di una identità digitale rilasciata dal sistema SPID (<a href="http://www.agid.gov.it/agenda-digitale/infrastrutture-architetture/spid " target="_blank">clicca qui per avere maggiori informazioni sul sistema SPID e sulle modalità di acquisizione dell’identità digitale</a>)<br /><br />           
                <div style="text-align: center !important">
                    <asp:Button runat="server" ID="lbAccessoCittadino" Width="720" OnClick="lbAccessoCittadino_Click" CssClass="buttonClass" Text="ACCEDI COME CITTADINO TRAMITE SISTEMA SPID" />
                </div>
                <br />
            </div>

            <div class="tab7" runat="server" visible="true">
                <h2 id="tipo7" class="grande"><u>ENTI LOCALI</u></h2>
                <h3>COSA PUOI FARE</h3>
                Accedendo al catasto regionale CRITER come Ente Locale potrai visionare la documentazione (libretti di impianto e rapporti di controllo tecnico) relativa agli impianti siti nel territorio di competenza.
                Potrai inoltre ottenere dei report complessivi con la principali caratteristiche di tali impianti.
                <h3>COSA DEVI AVERE PER ACCEDERE</h3>
                Per accedere al catasto regionale CRITER occorre preliminarmente effettuare la registrazione e la richiesta di accreditamento.<br /><br />
                <div style="text-align: center !important">
                    <asp:Button runat="server" ID="lbIscrizioneEnteLocale" Width="720" OnClick="lbIscrizioneEnteLocale_Click" CssClass="buttonClass" Text="REGISTRATI COME ENTE LOCALE" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="lbAccessoEnteLocale" Width="720" OnClick="lbAccesso_Click" CssClass="buttonClass" Text="ACCEDI COME ENTE LOCALE" />
                </div>
            </div>
            <!-- -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
