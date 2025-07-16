<%@ Page Language="C#" AutoEventWireup="true" Title="Gestione Pagamento PayER" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" CodeFile="MNG_PagamentoPayER.aspx.cs" Inherits="MNG_PagamentoPayER" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ConfermaInizioPagamento() {
            var importo = $('#<%= lblImporto.ClientID %>').text().replace(',', '.');
            if (confirm("Si vuole avviare una transazione di pagamento online per l'importo di " + importo + " euro? Scegliendo OK verra avviata la procedura di pagamento. Procedere?")) {
                return true;
            }
            return false;
        }

        function RefreshUpdatePanel() {
            __doPostBack('<%= txtQtaBollini.ClientID %>', '');
            document.getElementById(<%= txtQtaBollini.ClientID %>).blur();
            document.getElementById(<%= txtQtaBollini.ClientID %>).focus();
        };
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICARICA PORTAFOGLIO AZIENDA
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}" BorderBottom-BorderStyle="None"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="CodiceSoggetto" Caption="Codice" Width="50" />
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Azienda" Width="200" />
                                <dx:ListBoxColumn FieldName="IndirizzoSoggetto" Caption="Indirizzo" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox1" ForeColor="Red" Display="Dynamic"
                            runat="server" InitialValue="" ErrorMessage="Azienda: campo obbligatorio"
                            ControlToValidate="ASPxComboBox1">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        <div>
                            <div style="text-align: center; display: inline-block; width: 100%;">
                                <div style="border: 1px solid #FFCC3D; background-color: #fff1ca; margin: 10px auto; display: inline-block; padding: 5px; text-align: justify;">
                                    <p style="text-align: center; font-size: 1.3em;"><strong>Ricarica online con PayER</strong></p>
                                    <p>E' possibile effettuare la ricarica del proprio portafoglio elettronico mediante pagamento on-line dell'importo desiderato. Il pagamento on-line avviene tramite il circuito PayER &ndash; PagoPA, ovvero attraverso la piattaforma dei pagamenti della Regione Emilia-Romagna che permette di effettuare transazioni online. La piattaforma PayER &egrave; coordinata con la piattaforma nazionale PagoPA, ovvero il sistema di regole, standard e strumenti definiti dall'Agenzia per l'Italia Digitale per supportare i pagamenti elettronici verso la Pubblica Amministrazione.</p>
                                    <p>Selezionando questa opzione, una volta indicato l'importo da caricare sul portafoglio elettronico l'applicativo Criter re-indirizza il soggetto alle pagine interattive di PayER, che consentono di effettuare il relativo versamento on-line, utilizzando tutte le diverse modalit&agrave; previsti dai canali convenzionati. Occorre quindi seguire la procedura di pagamento che viene di volta in volta proposta, a seconda del circuito di pagamento selezionato: il versamento &egrave; gravato degli oneri di transazione previsti dai diversi soggetti convenzionati.</p>
                                    <p>Una volta terminata la procedura di pagamento, si viene re-indirizzati automaticamente all'applicativo Criter: il credito viene caricato nel portafoglio elettronico dell'azienda in tempi pressoch&eacute; immediati (il sistema entra in stato di attesa per circa 30 secondi, per consentire al circuito di effettuare tutte le verifiche necessarie), rendendolo subito disponibile per le operazioni di registrazione dei Rapporti di Controllo Tecnico.</p>
                                    <p>Si consiglia di leggere attentamente la guida prima di procedere.</p>
                                    <asp:Panel runat="server" ID="pnlPayerServizioAttivo">
                                        <p>Scegliere la quantit&agrave; di Bollini Calore Pulito da acquistare e premere il pulsante per avviare il pagamento.</p>
                                        <asp:TextBox ID="txtQtaBollini" runat="server" onkeyup="RefreshUpdatePanel();" AutoPostBack="true" MaxLength="3" OnTextChanged="txtQtaBollini_TextChanged" ValidationGroup="vgQtaBollini" Style="width: 150px; height: 26px; padding-right: 5px; font-size: 1.3em; text-align: right;" TabIndex="1" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblImporto" />&nbsp;&euro;
                                        <span style="font-size: 1.0em;">
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vgQtaBollini" ControlToValidate="txtQtaBollini" ErrorMessage="<br />Inserire la quantit&agrave; di Bollini Calore Pulito da acquistare." CssClass="validation-error" Display="Dynamic" />
                                            <asp:RegularExpressionValidator ValidationGroup="vgQtaBollini" ID="revQtaBollini" ControlToValidate="txtQtaBollini" runat="server" ErrorMessage="<br />Quantit&agrave; di Bollini Calore Pulito non valida." ValidationExpression="^[0-9]*$" CssClass="validation-error" Display="Dynamic" />
                                            <asp:CustomValidator ID="cvQuantitaBollini" runat="server" EnableClientScript="true" Display="Dynamic"
                                                OnServerValidate="ControllaQuantitaBollini"
                                                CssClass="validation-error"
                                                ValidationGroup="vgQtaBollini"
                                                ErrorMessage="<br />La Quantit&agrave deve essere minimo 1 e massimo di 857 di Bollini Calore Pulito." />
                                        </span>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnPayPayer" runat="server" Text="Procedi con la ricarica on-line"
                                            OnClientClick="return ConfermaInizioPagamento();"
                                            OnClick="btnPayPayer_Click" CssClass="buttonClass" Style="height: 26px;" Width="250"
                                            ValidationGroup="vgQtaBollini" CausesValidation="True" TabIndex="1" />
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlPayerServizioSospeso" CssClass="validation-error">
                                        <p>Il servizio è temporaneamente sospeso per cause tecniche di natura informatica, e sarà riattivato il piu' presto possibile.</p>
                                        <p>Rimane possibile utilizzare le altre modalità di ricarica del portafoglio elettronico.</p>
                                    </asp:Panel>
                                </div>
                                <div style="border: 1px solid #FFCC3D; background-color: #fff1ca; margin: 10px auto; padding: 5px; text-align: justify;">
                                    <p style="text-align: center; font-size: 1.3em;"><strong>Ricarica con Bonifico</strong></p>
                                    <p>E' possibile effettuare la ricarica del proprio portafoglio elettronico dell'importo desiderato mediante bonifico bancario diretto sulle seguenti coordinate IBAN:</p>
                                    <ul>
                                        <li>conto corrente bancario IT47Q0200802480000002853795</li>
                                        <li>conto corrente postale IT52N0760102400000021697404</li>
                                    </ul>
                                    <p>intestati a ART-ER S. cons. p. a., indicando ESCLUSIVAMENTE come causale &ldquo;portafoglio elettronico Criter &ndash; azienda manutentrice codice n. XXXXX&rdquo; (si prega di NON inserire altri codici o testi), e inviando copia del bonifico effettuato in formato .pdf ai seguenti indirizzi di posta elettronica:</p>
                                    <p><a href="mailto:pagamenti.criter@art-er.it">pagamenti.criter@art-er.it</a></p>
                                    <p>Utilizzando tale modalit&agrave; di ricarica, il credito viene reso disponibile solo dopo verifica dell'avvenuto incasso e mediante intervento di un operatore dell&rsquo;Area Amministrazione di ART-ER S. cons. p. a. che provvede al caricamento manuale dell'importo versato sul portafoglio elettronico dell'azienda. In considerazione di ci&ograve;, possono passare 5 giorni lavorativi dal momento in cui si effettua il bonifico viene incassato al momento in cui il credito viene caricato e reso disponibile per le operazioni di registrazione dei Rapporti di Controllo Tecnico. Il versamento pu&ograve; essere gravato degli oneri di transazione previsti dall'istituto bancario presso il quale si effettua il bonifico.</p>
                                    <p>Si precisa che in particolari momenti ed in prossimit&agrave; di scadenze fiscali o societarie tali tempi potrebbero non essere rispettati.</p>
                                    <br />
                                </div>

                                <div style="border: 1px solid #FFCC3D; background-color: #fff1ca; margin: 10px auto; padding: 5px; text-align: justify;">
                                    <p style="text-align: center; font-size: 1.3em;"><strong>Ricarica con versamento in contanti</strong></p>
                                    <p>E' possibile effettuare la ricarica del proprio portafoglio elettronico mediante pagamento in contanti (solo con previo appuntamento), per importi non superiori ai 2.999,99 euro, presso gli uffici amministrativi della societ&agrave; ART-ER S. cons. p. a., siti in Via G.B. Morgagni 6 a Bologna. Non sono applicati oneri di transazione.</p>
                                    <p>Utilizzando tale modalita' di ricarica, il credito viene reso disponibile solo dopo l&rsquo;intervento di un operatore dell&rsquo;Area Amministrazione di ART-ER S. cons. p. a., che provvede al caricamento manuale dell'importo versato sul portafoglio elettronico dell'azienda. In considerazione di ci&ograve;, possono passare 3 giorni lavorativi dal momento in cui si effettua il versamento al momento in cui il credito viene caricato e reso disponibile per le operazioni di registrazione dei Rapporti di Controllo Tecnico.</p>
                                    <p>Il pagamento in contanti e' possibile dal Lunedi' al Venerdi' dalle ore 10,30 alle ore 11,30. Il pagamento in contanti e' possibile solo fissando un appuntamento telefonico e sara' disponibile nella fascia oraria dalle 10.30 alle 11.30 dal Lunedi' al Venerdi'.Tale servizio non viene effettuato nel mese di agosto e nel periodo che intercorre fra il 20 dicembre e il 10 gennaio dell&rsquo;anno successivo.</p>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtQtaBollini" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>