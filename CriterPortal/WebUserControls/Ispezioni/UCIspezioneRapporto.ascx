<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCIspezioneRapporto.ascx.cs" Inherits="WebUserControls_Ispezioni_UCIspezioneRapporto" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCCheckbox.ascx" TagPrefix="uc1" TagName="UCCheckbox" %>
<%@ Register Src="~/WebUserControls/Ispezioni/UCRaccomandazioniPrescrizioniIspezione.ascx" TagPrefix="uc1" TagName="UCRaccomandazioniPrescrizioniIspezione" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Table Width="100%" runat="server" ID="tblIspezioneRapporto" CssClass="TableClass">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan="2" CssClass="riempimento1">
             RAPPORTO DI ISPEZIONE TIPO 1 (gruppi termici)
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2"> 
             1. DATI GENERALI ISPEZIONI
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
                <asp:Label runat="server" Text="Codice targatura impianto  "/>
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Panel ID="PnlViewCodiceTargatura" runat="server" Visible="false">
                <asp:Label ID="lblCodiceTargatura" runat="server" ForeColor="Green" Visible="true" Font-Bold="true" />
                <br />
                <asp:Label runat="server" ID="lblIDTargaturaImpianto" Visible="false" />
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="lblTitoloStrumenti" Text="Strumentazione utilizzata" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Panel runat="server" ID="pnlInsertStrumenti" CssClass="testo0" HorizontalAlign="Right">
                <asp:LinkButton ID="lnkInsertDatiStrumenti" runat="server" Text="Aggiungi dati strumento"
                    OnClick="lnkInsertDatiStrumenti_Click" CausesValidation="false" TabIndex="1" />
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlDatiStrumentiInsert" Visible="false" runat="server">
                <asp:Label runat="server" ID="lblIDIspezioneRapportoDatiStrumenti" Visible="false" />
                <asp:Table ID="tblDatiStrumenti" Width="500" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="200">
                             Tipologia (*)
                        </asp:TableCell>
                        <asp:TableCell Width="300">
                            <asp:TextBox ID="txtTipologiaUtilizzata" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiStrumento" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvTipologiaUtilizzata" runat="server" ValidationGroup="vgDatiStrumento"
                                CssClass="testoerr" Display="Dynamic"
                                InitialValue="0" ErrorMessage="Tipologia: campo obbligatorio"
                                ControlToValidate="txtTipologiaUtilizzata">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="200">
                             Matricola (*)
                        </asp:TableCell>
                        <asp:TableCell Width="300">
                            <asp:TextBox ID="txtMatricolaUtilizzata" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiStrumento" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvMatricolaUtilizzata" runat="server" ValidationGroup="vgDatiStrumento"
                                CssClass="testoerr" Display="Dynamic"
                                InitialValue="0" ErrorMessage="Matricola: campo obbligatorio"
                                ControlToValidate="txtMatricolaUtilizzata">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="200">
                             Tipologia Misurazione (*)
                        </asp:TableCell>
                        <asp:TableCell Width="300">
                            <asp:TextBox ID="txtTipologiaMisurazione" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiStrumento" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvTipologiaMisurazione" runat="server" CssClass="testoerr"
                                ValidationGroup="vgDatiStrumento" EnableClientScript="true" ErrorMessage="Tipologia Misurazione: campo obbligatorio"
                                ControlToValidate="txtTipologiaMisurazione">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="200">
                             Certificato taratura (*)
                        </asp:TableCell>
                        <asp:TableCell Width="300">
                            <asp:TextBox ID="txtCertificatoTaratura" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiStrumento" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvCertificatoTaratura" runat="server" CssClass="testoerr"
                                ValidationGroup="vgDatiStrumento" EnableClientScript="true" ErrorMessage="Certificato taratura: campo obbligatorio"
                                ControlToValidate="txtCertificatoTaratura">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="200">
                             Data scadenza certificato (*)
                        </asp:TableCell>
                        <asp:TableCell Width="300">
                            <asp:TextBox ID="txtDataScadenzaCertificato" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiStrumento" Width="100" />&nbsp;
                                <asp:RequiredFieldValidator ID="rfvDataScadenzaCertificato" runat="server" CssClass="testoerr"
                                    ValidationGroup="vgDatiStrumento" EnableClientScript="true" ErrorMessage="Data scadenza certificato: campo obbligatorio"
                                    Display="Dynamic" ControlToValidate="txtDataScadenzaCertificato">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revDataScadenzaCertificato" ControlToValidate="txtDataScadenzaCertificato" ForeColor="Red" ErrorMessage="Data scadenza certificato: campo non valido" ValidationGroup="vgDatiStrumento"
                                runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="500" ColumnSpan="2" HorizontalAlign="Center">
                            <asp:Button ID="btnSaveDatiStrumentazioneUtilizzata" runat="server" CssClass="buttonClass" Width="100" CausesValidation="true"
                                OnClick="btnSaveDatiStrumentazioneUtilizzata_Click" ValidationGroup="vgDatiStrumento" Text="inserisci" />&nbsp;
                            <asp:Button ID="btnAnnullaDatiStrumentazioneUtilizzata" runat="server" CssClass="buttonClass" Width="100"
                                OnClick="btnAnnullaDatiStrumentazioneUtilizzata_Click" CausesValidation="false" ValidationGroup="vgDatiStrumento" Text="annulla" />
                            <asp:ValidationSummary ID="vgDatiStrumento" ValidationGroup="vgDatiStrumento" runat="server" EnableClientScript="true"
                                ShowMessageBox="True" ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:Panel ID="pnlStrumentazioneUtilizzataView" Visible="true" runat="server">
                <dx:ASPxGridView ID="DataGridStrumentazioneUtilizzata"
                    ClientInstanceName="DataGridStrumentazioneUtilizzata"
                    EnableCallbackAnimation="true"
                    runat="server"
                    KeyFieldName="IDIspezioneStrumenti"
                    Width="100%"
                    
                    Border-BorderStyle="None"
                    BorderBottom-BorderStyle="None"
                    BorderLeft-BorderStyle="None"
                    BorderRight-BorderStyle="None"
                    BorderTop-BorderStyle="None"
                    EnableCallBacks="False"
                    EnableCallbackCompression="False"
                    Styles-AlternatingRow-BackColor="#ffedad"
                    Styles-Row-CssClass="GridItem"
                    Styles-Header-HorizontalAlign="Center"
                    Styles-Header-Font-Bold="true"
                    Styles-Table-BackColor="#ffcc3d"
                    Styles-Header-BackColor="#ffcc3d"
                    Styles-EmptyDataRow-BackColor="#ffffff"
                    >
                    <SettingsPager PageSize="20"
                        Summary-Visible="false"
                        ShowDefaultImages="false"
                        ShowDisabledButtons="false"
                        ShowNumericButtons="true" />
                    
                    <SettingsBehavior AllowFocusedRow="true"
                        ProcessFocusedRowChangedOnServer="false"
                        AllowSort="false" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="IDIspezioneStrumenti" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="IDIspezione" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="Tipologia" />
                        <dx:GridViewDataColumn FieldName="Matricola" />
                        <dx:GridViewDataColumn FieldName="TipologiaMisurazione" Caption="Tip. misurazione" />
                        <dx:GridViewDataColumn FieldName="CertificatoTaratura" Caption="Certificato" />
                        <dx:GridViewDataDateColumn FieldName="DataScadenzaCertificato" Caption="Scadenza Cert." />

                        <dx:GridViewDataColumn>
                            <DataItemTemplate>
                                <asp:ImageButton runat="server" ID="ImgUpdate" ToolTip="Modifica dato strumento" AlternateText="Modifica Strumento Utilizzato" ImageUrl="~/images/Buttons/editSmall.png" OnClientClick="javascript:return true"
                                    OnCommand="RowCommandStrumento" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneStrumenti") %>' />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn>
                            <DataItemTemplate>
                                <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="Cancella dato strumento" AlternateText="Cancella dato strumento" ImageUrl="~/images/Buttons/deleteSmall.png" OnClientClick="javascript:return confirm('Confermi la cancellazione del dato strumento?')"
                                    OnCommand="RowCommandStrumento" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneStrumenti") %>' />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>

                    </Columns>
                </dx:ASPxGridView>
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             Delegato all’ispezione
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Nominato " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblNominatoDelega" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblNominatoDelega_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowDelegaNome">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Nome" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtNomeDelegato" runat="server" CssClass="txtClass_o" Width="250px" />
            <asp:RequiredFieldValidator ID="rfvtxtNomeDelegato" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Delegato all’ispezione: nome campo obbligatorio"
                ControlToValidate="txtNomeDelegato">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowDelegaCognome">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Cognome" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtCognomeDelegato" runat="server" CssClass="txtClass_o" Width="250px" />
            <asp:RequiredFieldValidator ID="rfvtxtCognomeDelegato" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Delegato all’ispezione: cognome campo obbligatorio"
                ControlToValidate="txtCognomeDelegato">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowDelegaCF">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="C.F." />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtCFDelega" runat="server" CssClass="txtClass_o" Width="250px" MaxLength="16" />
            <asp:CustomValidator ID="cvCFDelega" runat="server" ErrorMessage="Sezione Delegato all’ispezione: c.f. non valido"
                            Display="Dynamic" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                            OnServerValidate="ControllaCfDelega">&nbsp;*</asp:CustomValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCFDelega" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Delegato all’ispezione: c.f. campo obbligatorio"
                ControlToValidate="txtCFDelega">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowDelega">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Delega " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblDelegaIspezionePresente" runat="server" RepeatDirection="Horizontal" Width="200">
                <asp:ListItem Text="Presente " Value="True" />
                <asp:ListItem Text="Assente " Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             Operatore/Conduttore
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Nominato" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblOCNominato" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblOCNominato_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvOCNominato" runat="server" ControlToValidate="rblOCNominato" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="1. Operatore/Conduttore - Nominato: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione0" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCPresente">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Nominato è presente all'ispezione" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblOCNominatoPresente" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblOCNominatoPresente_SelectedIndexChanged">
                <asp:ListItem Text="Presente " Value="True" />
                <asp:ListItem Text="Assente " Value="False" />
            </asp:RadioButtonList>
             <asp:RequiredFieldValidator ID="rfvOCNominatoPresente" runat="server" ControlToValidate="rblOCNominatoPresente" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="1. Operatore/Conduttore - Nominato è presente all'ispezione: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCNome">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Nome" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtOCNome" runat="server" CssClass="txtClass_o" Width="250px" />
            <asp:RequiredFieldValidator ID="rfvtxtOCNome" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Operatore conduttore: nome campo obbligatorio"
                ControlToValidate="txtOCNome">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCCognome">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Cognome" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtOCCOgnome" runat="server" CssClass="txtClass_o" Width="250px" />
            <asp:RequiredFieldValidator ID="rfvtxtOCCOgnome" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Operatore conduttore: cognome campo obbligatorio"
                ControlToValidate="txtOCCOgnome">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCCF">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="C.F." />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtOCCF" runat="server" CssClass="txtClass_o" Width="250px" MaxLength="16" />
            <asp:CustomValidator ID="cvCFOC" runat="server" ErrorMessage="Codice fiscale Operatore/Conduttore: non valido"
                            Display="Dynamic" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                            OnServerValidate="ControllaCfOC">&nbsp;*</asp:CustomValidator>
            <asp:RequiredFieldValidator ID="rfvtxtOCCF" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Operatore conduttore: c.f. campo obbligatorio"
                ControlToValidate="txtOCCF">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCAbilitazione">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Abilitazione" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblOCAbilitato" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblOCAbilitato_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvOCAbilitazione" runat="server" ControlToValidate="rblOCAbilitato" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="1. Operatore/Conduttore - Abilitazione: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione1" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTipoAbilitazione">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipo Abilitazione" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblTipoAbilitazione" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="400">
                <asp:ListItem Text="Patentino I grado" Value="1" Selected="True" />
                <asp:ListItem Text="Patentino II grado" Value="2" />
            </asp:RadioButtonList>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCNumeroPatentino">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Patentino numero" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtOCPatentinoNumero" runat="server" CssClass="txtClass_o" Width="250px" />
            <asp:RequiredFieldValidator ID="rfvtxtOCPatentinoNumero" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Sezione Operatore conduttore: Patentino numero campo obbligatorio"
                ControlToValidate="txtOCPatentinoNumero">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowOCDataRilascio">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Data Rilascio Patentino" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <%--<dx:ASPxDateEdit ID="deOCDataRilascioPatentino" runat="server" EditFormat="Custom" Width="200px" />--%>
            <asp:TextBox ID="txtDataRilascioPatentino" Width="100" runat="server" MaxLength="10" CssClass="txtClass_o" ValidationGroup="vgIspezioneRapporto" />
            <asp:RequiredFieldValidator ID="rfvtxtDataRilascioPatentino" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Sezione Operatore conduttore: Data Rilascio Patentino campo obbligatorio"
                ControlToValidate="txtDataRilascioPatentino">&nbsp;*</asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator
                ID="revtxtDataManutenzioneConsigliata" ValidationGroup="vgIspezioneRapporto" ControlToValidate="txtDataRilascioPatentino" 
                Display="Dynamic" ForeColor="Red" ErrorMessage="Sezione Operatore conduttore: Data Rilascio Patentino inserire la data nel formato gg/mm/aaaa"
                runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             2. DATI GENERALI IMPIANTO
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             Impianto
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="POD" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblPOD" runat="server" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione2" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="PDR" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblPDR" runat="server" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione48" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Impianto registrato in CRITER" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblImpiantoRegistratoCRITER" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblImpiantoRegistratoCRITER_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" Selected="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione3" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Data prima installazione " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblDataPrimaInstalazioneImpianto" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Potenze termiche nominali totali (kW)" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            al focolare&nbsp;<asp:TextBox ID="txtPotenzaTermicaNominaleTotaleFocalore" Width="100px" CssClass="txtClass_o" runat="server" />
            <asp:RegularExpressionValidator ID="revPotenzaNominaliTotaliFocalore" ControlToValidate="txtPotenzaTermicaNominaleTotaleFocalore"
                ErrorMessage="1. Potenze termiche nominali totali (kW) al focalore: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>&nbsp;
            utile&nbsp;<asp:TextBox ID="txtPotenzaTermicaNominaleTotaleUtile" Width="100px" CssClass="txtClass_o" runat="server" />
            <asp:RegularExpressionValidator ID="revPotenzaNominaliTotaliUtile" ControlToValidate="txtPotenzaTermicaNominaleTotaleUtile"
                ErrorMessage="1. Potenze termiche nominali totali (kW) utile: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             Ubicazione
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Comune " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblComuneUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Provincia " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblProvinciaUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Indirizzo " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblIndirizzoUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Numero civico " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblCivicoUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Piano / Interno " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblInternoUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Palazzo " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblPalazzoUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Scala " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblScalaUbicazione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="lblTitoloDatiCatastali" Text="Dati catastali" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Panel ID="pnlDatiCatastaliView" Visible="true" runat="server">
                <dx:ASPxGridView ID="dgDatiCatastali"
                    ClientInstanceName="dgDatiCatastali"
                    EnableCallbackAnimation="true"
                    runat="server"
                    KeyFieldName="IDLibrettoImpiantoDatiCatastali"
                    Width="100%">
                    <SettingsPager PageSize="20"
                        Summary-Visible="false"
                        ShowDefaultImages="false"
                        ShowDisabledButtons="false"
                        ShowNumericButtons="true" />
                    <Styles AlternatingRow-CssClass="GridAlternativeItem"
                        Row-CssClass="GridItem"
                        Header-CssClass="GridHeader"
                        Cell-HorizontalAlign="Center"
                        Header-HorizontalAlign="Center" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="IDLibrettoImpiantoDatiCatastali" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="IDLibrettoImpianto" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="IDCodiceCatastaleSezione" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="Foglio" />
                        <dx:GridViewDataColumn FieldName="Mappale" />
                        <dx:GridViewDataColumn FieldName="Subalterno" />
                        <dx:GridViewDataColumn FieldName="Identificativo" />
                    </Columns>
                </dx:ASPxGridView>
            </asp:Panel>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione47" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             Responsabile
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Titolo di Responsabile " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblTipologiaResponsabile" runat="server" RepeatDirection="Horizontal" Width="100%" CssClass="TableClass" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Cognome" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtCognomeResponsabile" runat="server" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Nome" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtNomeResponsabile" runat="server" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="C.F. " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtCFResponsabile" runat="server" CssClass="txtClass" Width="250px" MaxLength="16" />
             <asp:CustomValidator ID="cvResponsabile" runat="server" ErrorMessage="Codice fiscale Responsabile: non valido"
                            Display="Dynamic" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                            OnServerValidate="ControllaCfResponsabile">&nbsp;*</asp:CustomValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Ragione sociale " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtRagioneSocialeResponsabile" runat="server" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="P.IVA " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtPIVAResponsabile" runat="server" CssClass="txtClass" Width="250px" MaxLength="11" />
            <cc1:FilteredTextBoxExtender ID="ftbPartitaIva" runat="server" TargetControlID="txtPIVAResponsabile" FilterType="Custom, Numbers" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Comune " />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">

            <dx:ASPxComboBox ID="cbComuneResponsabile" runat="server" Theme="Default" TabIndex="1"
                EnableCallbackMode="true"
                CallbackPageSize="30"
                IncrementalFilteringMode="Contains"
                ValueType="System.Int32"
                ValueField="IDCodiceCatastale"
                OnItemsRequestedByFilterCondition="cbComuneResponsabile_OnItemsRequestedByFilterCondition"
                OnItemRequestedByValue="cbComuneResponsabile_OnItemRequestedByValue"
                OnButtonClick="cbComuneResponsabile_ButtonClick"
                TextFormatString="{0}"
                Width="250px"
                AllowMouseWheel="true">
                <ItemStyle Border-BorderStyle="None" />
                <Buttons>
                    <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                </Buttons>
                <Columns>
                    <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                </Columns>
            </dx:ASPxComboBox>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Indirizzo " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtIndirizzoResponsabile" runat="server" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Civico " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtCivicoResponsabile" runat="server" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Telefono" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:TextBox ID="txtTelefonoResponsabile" runat="server" CssClass="txtClass" Width="250px" />
             <asp:RegularExpressionValidator ID="revTelefonoResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Telefono Responsabile: campo non valido"
                ControlToValidate="txtTelefonoResponsabile" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Email" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:TextBox ID="txtEmailResponsabile" runat="server" CssClass="txtClass" Width="250px" />
             <asp:RegularExpressionValidator ID="revEmailResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email Responsabile: campo non valido"
                ControlToValidate="txtEmailResponsabile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Email PEC" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:TextBox ID="txtEmailPECResponsabile" runat="server" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revEmailPECResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email PEC Responsabile: campo non valido"
                ControlToValidate="txtEmailPECResponsabile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Terzo Responsabile (se nominato)
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="lblTitoloTerzoResponsabile" Text="E' stato nominato un terzo responsabile?" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblfTerzoResponsabile" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" Width="200px" OnSelectedIndexChanged="rblfTerzoResponsabile_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespoRagS">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Ragione Sociale" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtRagioneSocialeTerzoResponsabile" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespPIVA">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="P.IVA" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtPartitaIVATerzoResponsabile" CssClass="txtClass" Width="250px" MaxLength="11" />
            <cc1:FilteredTextBoxExtender ID="rtbePIVATerzoResponsabile" runat="server" TargetControlID="txtPartitaIVATerzoResponsabile" FilterType="Custom, Numbers" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespComune">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Comune" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbComuneTerzoResponsabile" runat="server" Theme="Default" TabIndex="1"
                EnableCallbackMode="true"
                CallbackPageSize="30"
                IncrementalFilteringMode="Contains"
                ValueType="System.Int32"
                ValueField="IDCodiceCatastale"
                OnItemsRequestedByFilterCondition="cbComuneTerzoResponsabile_OnItemsRequestedByFilterCondition"
                OnItemRequestedByValue="cbComuneTerzoResponsabile_OnItemRequestedByValue"
                OnButtonClick="cbComuneTerzoResponsabile_ButtonClick"
                TextFormatString="{0}"
                Width="250px"
                AllowMouseWheel="true">
                <ItemStyle Border-BorderStyle="None" />
                <Buttons>
                    <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                </Buttons>
                <Columns>
                    <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                </Columns>
            </dx:ASPxComboBox>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespIndirizzo">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Indirizzo" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtIndirizzoTerzoResponsabile" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespCivico">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Numero civico" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtNumeroCivicoTerzoResponsabile" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespTelefono">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Telefono" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtTelefonoTerzoResponsabile" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revTelefonoTerzoResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Telefono Terzo Responsabile: campo non valido"
                ControlToValidate="txtTelefonoTerzoResponsabile" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespEmail">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Email" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtEmailTerzoResponsabile" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revEmailTerzoResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email Terzo Responsabile: campo non valido"
                ControlToValidate="txtEmailTerzoResponsabile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespEmailPEC">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Email PEC" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtEmailPECTerzoResponsabile" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revEmailPECTerzoResposnabile" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email PEC Terzo Responsabile: campo non valido"
                ControlToValidate="txtEmailPECTerzoResponsabile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespAbilitazione">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Abilitazione lett.c ed e DM.37/08" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblAbilitazioneTerzoResponsabile" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" AutoPostBack="true" OnSelectedIndexChanged="rblAbilitazioneTerzoResponsabile_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione4" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespCert">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Certificazione Iso 9001" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblCertificazioneTerzoResponsabile" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" AutoPostBack="true" OnSelectedIndexChanged="rblCertificazioneTerzoResponsabile_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione5" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespAttestato">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Attestato SOA (OG-011 o OS-28)" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblAttestatoTerzoResponsabile" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" AutoPostBack="true" OnSelectedIndexChanged="rblAttestatoTerzoResponsabile_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione6" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowTRespIncarico">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Attestatazione accettazione incarico" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblAttestazioneIncaricoTerzoResponsabile" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" AutoPostBack="true" OnSelectedIndexChanged="rblAttestazioneIncaricoTerzoResponsabile_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione44" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
            Impresa manutentrice
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Impresa manutentrice che ha effettuato l'ultimo intervento" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblImpresaManutentricePresente" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" Width="200px" OnSelectedIndexChanged="rblImpresaManutentricePresente_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMRagS">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Ragione Sociale" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtRagioneSocialeImpresaManutentrice" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMPI">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="P.IVA" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtPartitaIVAImpresaManutentrice" CssClass="txtClass" Width="250px" MaxLength="11" />
            <cc1:FilteredTextBoxExtender ID="ftbePIVAImpresaManutentrice" runat="server" TargetControlID="txtPartitaIVAImpresaManutentrice" FilterType="Custom, Numbers" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMComune">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Comune" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">

            <dx:ASPxComboBox ID="cbComuneImpresaManutentrice" runat="server" Theme="Default" TabIndex="1"
                EnableCallbackMode="true"
                CallbackPageSize="30"
                IncrementalFilteringMode="Contains"
                ValueType="System.Int32"
                ValueField="IDCodiceCatastale"
                OnItemsRequestedByFilterCondition="cbComuneImpresaManutentrice_OnItemsRequestedByFilterCondition"
                OnItemRequestedByValue="cbComuneImpresaManutentrice_OnItemRequestedByValue"
                OnButtonClick="cbComuneImpresaManutentrice_ButtonClick"
                TextFormatString="{0}"
                Width="250px"
                AllowMouseWheel="true">
                <ItemStyle Border-BorderStyle="None" />
                <Buttons>
                    <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                </Buttons>
                <Columns>
                    <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                </Columns>
            </dx:ASPxComboBox>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMIndirizzo">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Indirizzo" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtIndirizzoImpresaManutentrice" CssClass="txtClass" Width="350px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMCivico">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Numero civico" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtNumeroCivicoImpresaManutentrice" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMTelefono">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Telefono" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtTelefonoImpresaManutentrice" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Telefono Impresa Manutentrice: campo non valido"
                ControlToValidate="txtTelefonoImpresaManutentrice" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMEmail">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Email" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtEmailImpresaManutentrice" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revEmailImpresaManutentrice" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email Impresa Manutentrice: campo non valido"
                ControlToValidate="txtEmailImpresaManutentrice" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMEmailPEC">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Email PEC" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtEmailPECImpresaManutentrice" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revEmailPECImpresaManutentrice" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email PEC Impresa Manutentrice: campo non valido"
                ControlToValidate="txtEmailPECImpresaManutentrice" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMAbilitata">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Abilitazione lett.c ed e DM.37/08" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblAbilitazioneImpresaManutentrice" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" AutoPostBack="true" OnSelectedIndexChanged="rblAbilitazioneImpresaManutentrice_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione7" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="ImpresaMCertificata">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Certificazione Iso 9001" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblCertificazioneImpresaManutentrice" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowImpresaMAttestatoSOA">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Attestato SOA (OG-011 o OS-28)" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblAttestatoSOAImpresaManutentrice" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento1" ColumnSpan="2">
             Operatore ultimo controllo
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Nome" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtNomeOperatoreUltimoControllo" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Cognome" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtCognomeOperatoreUltimoControllo" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Nato" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">

             <dx:ASPxComboBox ID="cbNatoOperatoreUltimoControllo" runat="server" Theme="Default" TabIndex="1"
                EnableCallbackMode="true"
                CallbackPageSize="30"
                IncrementalFilteringMode="Contains"
                ValueType="System.Int32"
                ValueField="IDCodiceCatastale"
                OnItemsRequestedByFilterCondition="cbNatoOperatoreUltimoControllo_OnItemsRequestedByFilterCondition"
                OnItemRequestedByValue="cbNatoOperatoreUltimoControllo_OnItemRequestedByValue"
                OnButtonClick="cbNatoOperatoreUltimoControllo_ButtonClick"
                TextFormatString="{0}"
                Width="250px"
                AllowMouseWheel="true">
                <ItemStyle Border-BorderStyle="None" />
                <Buttons>
                    <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                </Buttons>
                <Columns>
                    <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                </Columns>
            </dx:ASPxComboBox>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Data Nascita" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
             <dx:ASPxDateEdit ID="deDataNascitaOperatoreUltimoControllo" runat="server" EditFormat="Custom" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Comune" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">

            <dx:ASPxComboBox ID="cbComuneOperatoreUltimoControllo" runat="server" Theme="Default" TabIndex="1"
                EnableCallbackMode="true"
                CallbackPageSize="30"
                IncrementalFilteringMode="Contains"
                ValueType="System.Int32"
                ValueField="IDCodiceCatastale"
                OnItemsRequestedByFilterCondition="cbComuneOperatoreUltimoControllo_OnItemsRequestedByFilterCondition"
                OnItemRequestedByValue="cbComuneOperatoreUltimoControllo_OnItemRequestedByValue"
                OnButtonClick="cbComuneOperatoreUltimoControllo_ButtonClick"
                TextFormatString="{0}"
                Width="250px"
                AllowMouseWheel="true">
                <ItemStyle Border-BorderStyle="None" />
                <Buttons>
                    <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                </Buttons>
                <Columns>
                    <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                </Columns>
            </dx:ASPxComboBox>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Indirizzo" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtIndirizzoOperatoreUltimoControllo" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Numero civico" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtNumeroCivicoOperatoreUltimoControllo" CssClass="txtClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Telefono" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtTelefonoOperatoreUltimoControllo" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revTelefonoOperatoreUltimoControllo" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Telefono Operatore Ultimo Controllo: campo non valido"
                ControlToValidate="txtTelefonoOperatoreUltimoControllo" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Email" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtEmailOperatoreUltimoControllo" CssClass="txtClass" Width="250px" />
            <asp:RegularExpressionValidator ID="revEmailOperatoreUltimoControllo" ForeColor="Red" runat="server" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                ErrorMessage="Email Operatore Ultimo Controllo: campo non valido"
                ControlToValidate="txtEmailOperatoreUltimoControllo" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Patentino conduzione impianti termici" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblPatentinoOperatoreUltimoControllo" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rblPatentinoOperatoreUltimoControllo_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvPatentinoOperatoreUltimoControllo" runat="server" ControlToValidate="rblPatentinoOperatoreUltimoControllo" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="Patentino conduzione impianti termici: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione8" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiGenerali" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiGenerali" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiGenerali.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiGenerali" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiGenerali">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             3. DESTINAZIONE USO EDIFICIO O UNITA’ IMMOBILIARE 
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Categoria della destinazioni dell'edificio" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:RadioButtonList ID="rblDestinazioneUso" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Unità immobiliari servite" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxComboBox ID="cbUnitaImmobiliariServite" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbUnitaImmobiliariServite_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Una sola unità dall'origine" Value="1" />
                    <dx:ListEditItem Text="Una sola unità per distacco da centralizzato" Value="2" />
                    <dx:ListEditItem Text="Più unità" Value="3" />
                </Items>               
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbUnitaImmobiliariServite" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Unità immobiliari servite: campo obbligatorio"
                    ControlToValidate="cbUnitaImmobiliariServite">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Servizi serviti dall'impianto" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxComboBox ID="cbServiziServitiImpianto" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="250px">
                <Items>
                    <dx:ListEditItem Text="Solo climatizzazione invernale" Value="1" />
                    <dx:ListEditItem Text="Sola produzione ACS" Value="2" />
                    <dx:ListEditItem Text="Combinato" Value="3" />
                </Items>
            </dx:ASPxComboBox>

            <asp:RequiredFieldValidator ID="rfvcbServiziServitiImpianto" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Servizi serviti dall'impianto: campo obbligatorio"
                    ControlToValidate="cbServiziServitiImpianto">&nbsp;*</asp:RequiredFieldValidator>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="lblTitoloVolumeLordoRiscaldato" Text="Volume lordo riscaldato m<sup>3</sup>" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtVolumeLordoRiscaldato" Width="100" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revVolumeLordoRiscaldato" ControlToValidate="txtVolumeLordoRiscaldato"
                ErrorMessage="2. Volume lordo riscaldato m3: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDestinazione" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDestinazione" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDestinazione.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDestinazione" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDestinazione">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             4. CONTROLLO DELL’IMPIANTO  
        </asp:TableCell>
    </asp:TableRow>

    

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Installazione interna/esterna" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxComboBox ID="cbInstallazioneInternaEsterna" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbInstallazioneInternaEsterna_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Interna" Value="1" Selected="True" />
                    <dx:ListEditItem Text="Esterna" Value="2" />
                </Items>
            </dx:ASPxComboBox>
             <asp:RequiredFieldValidator ID="rfvInstallazioneInternaEsterna" runat="server" ControlToValidate="cbInstallazioneInternaEsterna" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="Installazione interna/esterna: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowInstallazioneInterna">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Per Installazione interna: locale idoneo  " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkLocaleInstallazioneIdoneo" disablena="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkLocaleInstallazioneIdoneo_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='6' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione45" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowInstallazioneEsterna">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text=" Per Installazione esterna: generatori idonei " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkGeneratoriIdonei" disablena="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkGeneratoriIdonei_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='7' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione46" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Aperture di ventilazione/aerazione libere da ostruzioni  " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkApertureLibere" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkApertureLibere_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='8' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione9" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Aperture di ventilazione / aerazione di adeguate dimensioni " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkDimensioniApertureAdeguate" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkDimensioniApertureAdeguate_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='9' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione11" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
           <asp:Label runat="server" Text="Canale da fumo o condotti di scarico idonei (esame visivo) " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkScarichiIdonei" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkScarichiIdonei_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='10' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione12" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Assenza perdita combustibile liquido (esame visivo)  " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkAssenzaPerditeCombustibile" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkAssenzaPerditeCombustibile_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='12' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione13" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Idonea tenuta impianto gas combustibile e raccordi con il generatore (UNI 11137)  " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkTenutaImpianto" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkTenutaImpianto_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='13' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione14" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Modalità di evacuazione fumi" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButton ID="rblEvacuazioneForzata" GroupName="EvacuazioneGroup" OnCheckedChanged="rblEvacuazioneFumi_CheckedChanged" AutoPostBack="true" runat="server" Text="Forzata" />
            <asp:RadioButton ID="rblEvacuazioneNaturale" GroupName="EvacuazioneGroup" OnCheckedChanged="rblEvacuazioneFumi_CheckedChanged" AutoPostBack="true" runat="server" Text="Naturale" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Tiraggio del camino: prova strumentale diretta valore in Pa (UNI 10845)" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtTiraggioDelCamino" Width="100px" CssClass="txtClass" runat="server" AutoPostBack="true" OnTextChanged="txtTiraggioDelCamino_TextChanged" />
            <asp:RequiredFieldValidator ID="rfvtxtTiraggioDelCamino" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Tiraggio del camino: cognome campo obbligatorio"
                ControlToValidate="txtTiraggioDelCamino">&nbsp;*</asp:RequiredFieldValidator>
            
            
            <asp:RangeValidator ID="rvTiraggioDelCamino" runat="server" ControlToValidate="txtTiraggioDelCamino" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="25" MinimumValue="0" Type="Double" Display="None" ErrorMessage="Tiraggio del camino: prova strumentale diretta valore in Pa (UNI 10845): campo dovrebbe essere tra 0 e 25.">  
            &nbsp;*</asp:RangeValidator>
            <asp:RegularExpressionValidator ID="revTiraggioDelCamino" ControlToValidate="txtTiraggioDelCamino"
                ErrorMessage="7. Misurazioni eseguite: inserire un numero" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione15" />
        </asp:TableCell>
    </asp:TableRow>

    <%--<asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Tiraggio del camino: prova indiretta calcolata (UNI 10845)" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblTiraggioDelCamino" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rblTiraggioDelCamino_SelectedIndexChanged">
                <asp:ListItem Text="Ok" Value="1" />
                <asp:ListItem Text="No" Value="2" />
                <asp:ListItem Text="Incerta con: aperture di ventilazione / aerazione libere da ostruzioni (NO)" Value="3" />
                <asp:ListItem Text="Incerta con: aperture di ventilazione / aerazione di adeguate dimensioni (SI)" Value="4" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="revLTiraggioDelCamino" runat="server" ControlToValidate="rblTiraggioDelCamino" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="Tiraggio del camino: prova indiretta calcolata (UNI 10845): campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione16" />
        </asp:TableCell>
    </asp:TableRow>--%>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveControlloImpianto" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveControlloImpianto" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpControlloImpianto.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpControlloImpianto" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpControlloImpianto">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             5. STATO DELLA DOCUMENTAZIONE   
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Libretto di impianto presente " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblPresenteLibretto" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblPresenteLibretto_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
             <asp:RequiredFieldValidator ID="rfvPresenteLibretto" runat="server" ControlToValidate="rblPresenteLibretto" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="5. Libretto di impianto presente: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione17" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Libretto uso/manutenzione presente" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblLibrettoUsoManutenzione" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblLibrettoUsoManutenzione_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
             <asp:RequiredFieldValidator ID="rfvLibrettoUsoManutenzione" runat="server" ControlToValidate="rblLibrettoUsoManutenzione" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="5. Libretto uso/manutenzione presente: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='2' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione18" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Libretto uso/manutenzione compilato correttamente in ogni parte" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblLibrettoCompilatoInTutteLeParte" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblLibrettoCompilatoInTutteLeParte_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvLibrettoCompilato" runat="server" ControlToValidate="rblLibrettoCompilatoInTutteLeParte" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="5. Libretto uso/manutenzione compilato correttamente in ogni parte: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='2' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione19" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Dichiarazione di conformità / rispondenza presente" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblDichiarazione" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblDichiarazione_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvDichiarazione" runat="server" ControlToValidate="rblDichiarazione" ValidationGroup="vgIspezioneRapporto" Display="Dynamic"
                ErrorMessage="5. Dichiarazione di conformità / rispondenza presente: campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='26' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione10" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="CPI o SCIA antincendio e data di protocollo comando VV.FF:" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="250px">
                        <uc1:uccheckbox id="chkSCIAAntincendio" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkSCIAAntincendio_CheckedChanged" />
                    </asp:TableCell>
                    <asp:TableCell Width="250px">
                        <dx:ASPxDateEdit ID="deSCIAAntincendio" runat="server" EditFormat="Custom" Width="155" />                  
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='48' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione22" />
        </asp:TableCell>
    </asp:TableRow>

    <%--<asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Verbale di sopralluogo VV.FF e relativa data" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="250px">
                        <uc1:uccheckbox id="chkVerbaleSopralluogo" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkVerbaleSopralluogo_CheckedChanged" />
                    </asp:TableCell>
                    <asp:TableCell Width="250px">
                        <dx:ASPxDateEdit ID="deVerbaleSopralluogo" runat="server" EditFormat="Custom" Width="155" > 
                            <ValidationSettings ErrorDisplayMode="ImageWithText" ValidationGroup="vgIspezioneRapporto" >
                                <RequiredField ErrorText="Verbale di sopralluogo VV.FF e relativa data: campo obbligatorio" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione23" />
        </asp:TableCell>
    </asp:TableRow>--%>

    <%--<asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Ultimo rinnovo periodico e relativa data" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="250px">
                        <uc1:uccheckbox id="chkUltimoRinnovoPeriodico" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkUltimoRinnovoPeriodico_CheckedChanged" />
                    </asp:TableCell>
                    <asp:TableCell Width="250px">
                        <dx:ASPxDateEdit ID="deUltimoRinnovoPeriodico" runat="server" EditFormat="Custom" Width="155" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione24" />
        </asp:TableCell>
    </asp:TableRow>--%>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Progetto dell'impianto presente" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblPresenteProgettoImpianto" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblPresenteProgettoImpianto_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="True" />
                <asp:ListItem Text="No " Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione25" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Progettista" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox runat="server" ID="txtProgettista" CssClass="txtClass" Width="250px" />
            <%--<asp:RequiredFieldValidator ID="rfvProgettista" runat="server" ValidationGroup="vgIspezioneRapporto"
                CssClass="testoerr" Display="Dynamic" 
                InitialValue="" ErrorMessage="Progettista: campo obbligatorio"
                ControlToValidate="txtProgettista">&nbsp;*</asp:RequiredFieldValidator>--%>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Protocollo deposito Comune" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox ID="txtProtocolloDepositoComune" runat="server" CssClass="txtClass" Width="250" />&nbsp;
            <%--<asp:RequiredFieldValidator ID="rfvProtocolloDepositoComune" runat="server" CssClass="testoerr"
                ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" ErrorMessage="Protocollo deposito Comune: campo obbligatorio"
                ControlToValidate="txtProtocolloDepositoComune">&nbsp;*</asp:RequiredFieldValidator>--%>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Data deposito Comune" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxDateEdit ID="deDataDepositoComune" runat="server" EditFormat="Custom" Width="155px" /> 
            <%--<asp:CustomValidator ID="cvDataDEpositoComune" runat="server" ErrorMessage="Data deposito Comune: campo obbligatorio"
                            Display="Dynamic" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true"
                            OnServerValidate="ControllaDataDepositoComune">&nbsp;*</asp:CustomValidator>--%>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Potenza termica utile prevista dal progetto" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox ID="txtPotenzaTermicaUtileProgetto" runat="server" CssClass="txtClass" Width="155px" />&nbsp;

            <%--<asp:RegularExpressionValidator ID="revtxtPotenzaTermicaUtileProgetto" ControlToValidate="txtPotenzaTermicaUtileProgetto"
                ErrorMessage="Potenza termica utile prevista dal progetto: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>--%>

            <%--<asp:RequiredFieldValidator ID="rfvPotenzaTermicaUtileProgetto" runat="server" CssClass="testoerr"
                ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" ErrorMessage="Potenza termica utile prevista: campo obbligatorio"
                Display="Dynamic" ControlToValidate="txtPotenzaTermicaUtileProgetto">&nbsp;*</asp:RequiredFieldValidator>--%>
        </asp:TableCell>
    </asp:TableRow>

    <%--<asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="AQE - attestato qualificazione energetica" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkAQE" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkAQE_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione26" />
        </asp:TableCell>
    </asp:TableRow>--%>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Diagnosi energetica per sostituzione generatori con P > 100 kW e/o disconnessione da impianto centralizzato" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkDiagnosiEnergetica" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkDiagnosiEnergetica_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione27" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Perizia di non aggravio di costi e/o sbilanciamento in caso di distacco dall’impianto centralizzato" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkPerizia" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkPerizia_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione28" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Omologazione o verifiche per. DM 01/12/1975" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkOmologazioneVerifiche" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkOmologazioneVerifiche_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='49' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione16" />
        </asp:TableCell>
    </asp:TableRow>

 <%--   <asp:TableRow Visible="false">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="lblTitoloDatiProgetto" Text="Dati progetto" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Panel runat="server" ID="pnlInsertDatiProgetto" CssClass="testo0" HorizontalAlign="Right">
                <asp:LinkButton ID="lnkInsertDatiProgetto" runat="server" Text="Aggiungi dati progetto"
                    OnClick="lnkInsertDatiProgetto_Click" CausesValidation="false" TabIndex="1" />
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlDatiProggetoInsert" Visible="false" runat="server">
                <asp:Label runat="server" ID="lblIDIspezioneProgetto" Visible="false" />
                <asp:Table ID="tblDatiProgetto" Width="500" runat="server">
                    <%-- <asp:TableRow>
                        <asp:TableCell Width="100">
                             Progettista (*)
                        </asp:TableCell>
                        <asp:TableCell Width="400">
                            <asp:TextBox ID="txtProgettista" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiProgetto" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvProgettista" runat="server" ValidationGroup="vgDatiProgetto"
                                CssClass="testoerr" Display="Dynamic"
                                InitialValue="" ErrorMessage="Progettista: campo obbligatorio"
                                ControlToValidate="txtProgettista">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                   <%-- <asp:TableRow>
                        <asp:TableCell Width="100">
                             N. Progetto (*)
                        </asp:TableCell>
                        <asp:TableCell Width="400">
                            <asp:TextBox ID="txtNumeroProgetto" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiProgetto" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvNumeroProgetto" runat="server" CssClass="testoerr"
                                ValidationGroup="vgDatiProgetto" EnableClientScript="true" ErrorMessage="N. Progetto: campo obbligatorio"
                                ControlToValidate="txtNumeroProgetto">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="100">
                             Anno (*)
                        </asp:TableCell>
                        <asp:TableCell Width="400">
                            <asp:TextBox ID="txtAnnoProgetto" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiProgetto" Width="100" MaxLength="4" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvAnnoProgetto" runat="server" CssClass="testoerr"
                                ValidationGroup="vgDatiProgetto" EnableClientScript="true" ErrorMessage="Anno: campo obbligatorio"
                                ControlToValidate="txtAnnoProgetto">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="100">
                             Potenza termica utile prevista (*)
                        </asp:TableCell>
                        <asp:TableCell Width="400">
                            <asp:TextBox ID="txtPotenzaProgetto" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiProgetto" Width="100" />&nbsp;
                            <asp:RequiredFieldValidator ID="rfvPotenzaProgetto" runat="server" CssClass="testoerr"
                                ValidationGroup="vgDatiProgetto" EnableClientScript="true" ErrorMessage="Potenza termica utile prevista: campo obbligatorio"
                                Display="Dynamic" ControlToValidate="txtPotenzaProgetto">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="500" ColumnSpan="2" HorizontalAlign="Center">
                            <asp:Button ID="btnSaveDatiProgetto" runat="server" CssClass="buttonClass" Width="100" CausesValidation="true"
                                OnClick="btnSaveDatiProgetto_Click" ValidationGroup="vgDatiProgetto" Text="inserisci" />&nbsp;
                            <asp:Button ID="btnAnullaDatiProgetto" runat="server" CssClass="buttonClass" Width="100"
                                OnClick="btnAnnullaDatiProgetto_Click" CausesValidation="false" ValidationGroup="vgDatiProgetto" Text="annulla" />
                            <asp:ValidationSummary ID="vgDatiProgetto" ValidationGroup="vgDatiProgetto" runat="server" EnableClientScript="true"
                                ShowMessageBox="True" ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:Panel ID="pnlDatiProgettoView" Visible="true" runat="server">
                <dx:ASPxGridView ID="DataGridDatiProgetto"
                    ClientInstanceName="DataGridDatiProgetto"
                    EnableCallbackAnimation="true"
                    runat="server"
                    KeyFieldName="IDIspezioneProgetto"
                    Width="100%">
                    <SettingsPager PageSize="20"
                        Summary-Visible="false"
                        ShowDefaultImages="false"
                        ShowDisabledButtons="false"
                        ShowNumericButtons="true" />
                    <Styles AlternatingRow-CssClass="GridAlternativeItem"
                        Row-CssClass="GridItem"
                        Header-CssClass="GridHeader"
                        Cell-HorizontalAlign="Center"
                        Header-HorizontalAlign="Center" />
                    <SettingsBehavior AllowFocusedRow="True"
                        ProcessFocusedRowChangedOnServer="false"
                        AllowSort="false" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="IDIspezioneProgetto" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="IDIspezione" Visible="false" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="Progettista" />
                        <dx:GridViewDataColumn FieldName="NumeroProgetto" Caption="N. Progetto" />
                        <dx:GridViewDataColumn FieldName="Anno" />
                        <dx:GridViewDataColumn FieldName="PotenzaTermicaUtilePrevista" Caption="Potenza termica utile prevista" />
                        <dx:GridViewDataColumn>
                            <DataItemTemplate>
                                <asp:ImageButton runat="server" ID="ImgUpdate" ToolTip="Modifica dato progetto" AlternateText="Modifica dato progetto" ImageUrl="~/images/Buttons/editSmall.png" OnClientClick="javascript:return true"
                                    OnCommand="RowCommandProgetto" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneProgetto") %>' />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn>
                            <DataItemTemplate>
                                <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="Cancella dato progetto" AlternateText="Cancella dato progetto" ImageUrl="~/images/Buttons/deleteSmall.png" OnClientClick="javascript:return confirm('Confermi la cancellazione del dato progetto?')"
                                    OnCommand="RowCommandProgetto" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneProgetto") %>' />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>--%>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveStatoDocumentazione" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveStatoDocumentazione" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpStatoDocumentazione.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpStatoDocumentazione" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpStatoDocumentazione">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             6. VALUTAZIONE EFFICIENZA ENERGETICA DEL GENERATORE
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             6.1 DATI GENERATORE 
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Gruppo termico GT" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblGruppoTermicoGT" Width="25px" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Data installazione generatore" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxDateEdit ID="deDataInstallazioneGeneratore" runat="server" EditFormat="Custom" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Combustibile" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxComboBox ID="cbCombustibile" runat="server" ItemStyle-Height="20px" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbCombustibile_SelectedIndexChanged" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowAltroCombustibile">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Altro combustibile" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtAltroCombustibile" Width="250px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
             <asp:Label runat="server" Text="Fluido termovettore" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbFluidoTermoVettore" runat="server" ItemStyle-Height="20px" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbFluidoTermoVettore_SelectedIndexChanged" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowAltroFluidoTermoVettore">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Altro fluido termovettore" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtAltroFluidoTermoVettore" Width="250px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>
        
    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Costruttore caldaia" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblCustrutoreCaldaia" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Modello generatore" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblModelloGeneratore" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Matricola generatore" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:Label ID="lblMarticolaGeneratore" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Costruttore bruciatore" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtCostruttoreBruciatore" Width="100px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
             <asp:Label runat="server" Text="Modello bruciatore " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtModelloBruciatore" Width="100px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Matricola bruciatore " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtMatricolaBruciatore" Width="100px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Potenza termica al focolare generatore (kW)" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtPotenzaTermicaFocolareDatiNominali" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revPotenzaTermicaFocaloreDatiNominali" ControlToValidate="txtPotenzaTermicaFocolareDatiNominali"
                ErrorMessage="5.1 Potenza termica al focolare (kW): inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Potenza termica utile generatore (kW)" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtPotenzaTermicaUtileDatiNominali" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revPotenzaTermicaUtileDatiNominali" ControlToValidate="txtPotenzaTermicaUtileDatiNominali"
                ErrorMessage="5.1 Potenza termica utile (kW): inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Campo di lavoro bruciatore (kW)" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            da&nbsp;
            <asp:TextBox ID="txtCampoLavoroBruciatoreDatiNominaliDa" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revCampoDa" ControlToValidate="txtCampoLavoroBruciatoreDatiNominaliDa"
                ErrorMessage="5.1. Campo di lavoro bruciatore (kW) da: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
            a&nbsp;
            <asp:TextBox ID="txtCampoLavoroBruciatoreDatiNominaliA" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revCampoA" ControlToValidate="txtCampoLavoroBruciatoreDatiNominaliA"
                ErrorMessage="5.1. Campo di lavoro bruciatore (kW) a: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Portata di combustibile " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtPortataCombustibileValoriMisuratim3h" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revValoriMisuratim3h" ControlToValidate="txtPortataCombustibileValoriMisuratim3h"
                ErrorMessage="5.1 Portata di combustibile (m3/h): inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
            &nbsp;  (m<sup>3</sup>/h)
           
            <asp:TextBox ID="txtPortataCombustibileValoriMisuratikgh" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revValoriMisuratikgh" ControlToValidate="txtPortataCombustibileValoriMisuratikgh"
                ErrorMessage="5.1 Portata di combustibile (kg/h): inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>&nbsp;  (kg/h)
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Potenza termica al focolare " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtPotenzaTermicaFocolareValoriMisurati" Width="100px" CssClass="txtClass" runat="server" />
            <asp:RegularExpressionValidator ID="revPotenzaTermicaFocaloreValoriMusirati" ControlToValidate="txtPotenzaTermicaFocolareValoriMisurati"
                ErrorMessage="5.1 Potenza termica al focolare: inserire un numero con la virgola" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>&nbsp;  (kW)
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipologia gruppo termico " />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbTipologiaGruppoTermico" runat="server" ItemStyle-Height="20px" AutoPostBack="true" Width="200px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" Text="Classificazione DPR 660/96" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbClassificazioneDPR66096" runat="server" ItemStyle-Height="20px" AutoPostBack="true" Width="200px" />
            <asp:RequiredFieldValidator ID="rfvcbClassificazioneDPR66096" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Classificazione DPR 660/96: campo obbligatorio"
                    ControlToValidate="cbClassificazioneDPR66096">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Corretto dimensionamento del generatore in riferimento al progetto" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:RadioButtonList ID="rblCorrettoDimensionamentoGeneratore" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblCorrettoDimensionamentoGeneratore_SelectedIndexChanged">
                <asp:ListItem Text="Si " Value="1" />
                <asp:ListItem Text="No " Value="0" />
                <asp:ListItem Text="Nc " Value="-1" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='50' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione29" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiGeneratore" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnDatiGeneratore" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiGeneratore.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiGeneratore" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiGeneratore">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             6.2 TRATTAMENTO DELL'ACQUA 
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Trattamento acqua in riscaldamento" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblTrattamentoRiscaldamento" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblTrattamentoRiscaldamento_SelectedIndexChanged">
                <asp:ListItem Text="Presente" Value="1" />
                <asp:ListItem Text="Assente" Value="0" />
                <asp:ListItem Text="Non richiesto" Value="2" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='4' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione30" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowTipoTrattamentoRiscaldamento" Visible="false">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipo Trattamento acqua in riscaldamento" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:CheckBoxList ID="cblTipoTrattamentoRiscaldamento" runat="server"
                AutoPostBack="true" RepeatDirection="Horizontal" Width="100%">
            </asp:CheckBoxList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
           <asp:Label runat="server" Text="Trattamento acqua in produzione ACS " />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblTrattamentoProduzioneACS" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblTrattamentoProduzioneACS_SelectedIndexChanged">
                <asp:ListItem Text="Presente" Value="1" />
                <asp:ListItem Text="Assente" Value="0" />
                <asp:ListItem Text="Non richiesto" Value="2" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='5' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione31" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowTipoTrattamentoACS" Visible="false">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipo Trattamento acqua in produzione ACS " />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:CheckBoxList ID="cblTipoTrattamentoProduzioneACS" runat="server"
                AutoPostBack="true" RepeatDirection="Horizontal" Width="100%">
            </asp:CheckBoxList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiTrattamentoAcqua" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiTrattamentoAcqua" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiTrattamentoAcqua.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiTrattamentoAcqua" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiTrattamentoAcqua">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             6.3 MANUTENZIONE E ANALISI
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Controllo funzionale e manutenzione
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
             <asp:Label runat="server" Text="Frequenza" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbFrequenza" runat="server" ItemStyle-Height="20px" AutoPostBack="true" ValueType="System.Int32" Width="250px" OnSelectedIndexChanged="cbFrequenza_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Altro" Value="1" />
                    <dx:ListEditItem Text="Semestrale" Value="2" />
                    <dx:ListEditItem Text="Annuale" Value="3" />
                    <dx:ListEditItem Text="Biennale" Value="4" />
                </Items>
            </dx:ASPxComboBox>

            <asp:RequiredFieldValidator ID="rfvcbFrequenza" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Sezione Controllo funzionale e manutenzione Frequenza: campo obbligatorio"
                    ControlToValidate="cbFrequenza">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowAltroFrequenza">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Altro frequenza" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtAltroFrequenza" Width="250px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Ultima manutenzione programmata effettuata" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblUltimaManutenzionePrevistaEffettuata" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblUltimaManutenzionePrevistaEffettuata_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowDataUltimaManutenzione">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Data ultima manutenzione" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxDateEdit ID="deDataUltimaManutenzione" runat="server" EditFormat="Custom" Width="250px" AllowNull="true" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Controllo di efficienza energetica
        </asp:TableCell>
    </asp:TableRow>

    
    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
             <asp:Label runat="server" Text="Frequenza" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbFrequenzaEfficienzaEnergetica" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbFrequenzaEfficienzaEnergetica_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Altro" Value="1" />
                    <dx:ListEditItem Text="Annuale" Value="3" />
                    <dx:ListEditItem Text="Biennale" Value="4" />
                </Items>
            </dx:ASPxComboBox>

            <asp:RequiredFieldValidator ID="rfvcbFrequenzaEfficienzaEnergetica" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Sezione Controllo di efficienza energetica Frequenza: campo obbligatorio"
                    ControlToValidate="cbFrequenzaEfficienzaEnergetica">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowAltroFrequenzaEfficienzaEnergetica">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Altro frequenza" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtAltroFrequenzaEfficienzaEnergetica" Width="250px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Ultimo controllo obbligatorio effettuato" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbUltimoControlloPrevistoEffettuato" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="cbUltimoControlloPrevistoEffettuato_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Si in tempo utile" Value="1" />
                    <dx:ListEditItem Text="Si in ritardo" Value="2" />
                    <dx:ListEditItem Text="No" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbUltimoControlloPrevistoEffettuato" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Ultimo controllo obbligatorio effettuato: campo obbligatorio"
                    ControlToValidate="cbUltimoControlloPrevistoEffettuato">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione32" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowUltimoControllo">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Data ultimo controllo" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <dx:ASPxDateEdit ID="deDataUltimoControllo" runat="server" EditFormat="Custom" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="RTCEE relativo all'ultimo controllo obbligatorio presente in copia" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbRTCEEPresenteInCopia" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" Width="250px">
                <Items>
                    <dx:ListEditItem Text="Si con bollino" Value="1" />
                    <dx:ListEditItem Text="Si senza bollino" Value="2" />
                    <dx:ListEditItem Text="No" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbRTCEEPresenteInCopia" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                 InitialValue="" ErrorMessage="RTCEE relativo all'ultimo controllo obbligatorio presente in copia: campo obbligatorio"
                 ControlToValidate="cbRTCEEPresenteInCopia">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
           <asp:Label runat="server" Text="RTCEE relativo all'ultima manutenzione registrato" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblRTCEERegistrato" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Width="200px">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Sono stati realizzati gli interventi previsti" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbRTCEEinterventiPrevisti" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="cbRTCEEinterventiPrevisti_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Si tutti" Value="1" />
                    <dx:ListEditItem Text="Si solo in parte" Value="2" />
                    <dx:ListEditItem Text="No" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbRTCEEinterventiPrevisti" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                 InitialValue="" ErrorMessage="Sono stati realizzati gli interventi previsti: campo obbligatorio"
                 ControlToValidate="cbRTCEEinterventiPrevisti">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione33" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Con" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:CheckBox ID="cbOsservazioniRCTEE" runat="server" Text="Osservazioni" Width="33%" AutoPostBack="true" OnCheckedChanged="cbOsservazioniRCTEE_CheckedChanged" />
            <asp:CheckBox ID="cbRaccomandazioniRCTEE" runat="server" Text="Raccomandazioni" Width="33%" AutoPostBack="true" OnCheckedChanged="cbRaccomandazioniRCTEE_CheckedChanged" />
            <asp:CheckBox ID="cbPrescrizioniRCTEE" runat="server" Text="Prescrizioni" Width="33%" AutoPostBack="true" OnCheckedChanged="cbPrescrizioniRCTEE_CheckedChanged" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowOsservazioni">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Osservazioni" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox Width="500" Height="100" ID="txtOsservazioniRCTEE" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="3" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowRaccomandazioni">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Raccomandazioni" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox Width="500" Height="100" ID="txtRaccomandazioniRCTEE" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="3" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowPrescrizioni">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Prescrizioni" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox Width="500" Height="100" ID="txtPrescrizioniRCTEE" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="3" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiAnalisi" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiAnalisi" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiAnalisi.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiAnalisi" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiAnalisi">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             6.4 MISURA DEL RENDIMENTO DI COMBUSTIONE ( UNI 10389 - 1)
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="CO fumi secchi regolare" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblMonossidoCarbonio" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblMonossidoCarbonio_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='21' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione34" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Indice di Bacharach regolare" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblIndiceFumosita" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblIndiceFumosita_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='20' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione35" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Rendimento combustione regolare" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblRendimentoCombustibileMinimo" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Width="200" OnSelectedIndexChanged="rblRendimentoCombustibileMinimo_SelectedIndexChanged">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:RadioButtonList>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='22' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione36" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
            <h3>VALORI MISURATI</h3>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:Table ID="tblGT" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Modulo Termico" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtModuloTermico" CssClass="txtClass" runat="server" Width="50" />
                    </asp:TableCell>

                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Temperatura fumi (°C)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtTemperaturaFumi" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtTemperaturaFumi" ControlToValidate="txtTemperaturaFumi"
                            ErrorMessage="5.4 Temperatura Fumi: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvTemperaturaFumi" runat="server" ControlToValidate="txtTemperaturaFumi" Type="Double" Display="None" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="300" MinimumValue="0" ErrorMessage="Temperatura fumi (°C): campo dovrebbe essere tra 0 e 300.">  
                        &nbsp;*</asp:RangeValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Temperatura aria comburente (°C)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtTemperaturaComburente" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtTemperaturaComburente" ControlToValidate="txtTemperaturaComburente"
                            ErrorMessage="5.4 Temperatura Aria Comburente: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvTemperaturaComburente" runat="server" ControlToValidate="txtTemperaturaComburente" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="100" MinimumValue="-10" Type="Double" Display="None" ErrorMessage="Temperatura aria comburente (°C): campo dovrebbe essere tra -10 e 100.">  
                        &nbsp;*</asp:RangeValidator>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="O2 valore misurato (%)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtO2" runat="server" CssClass="txtClass" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtO2" ControlToValidate="txtO2"
                            ErrorMessage="5.4 O2 valore misurato (%): inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rbtxtO2" runat="server" ControlToValidate="txtO2" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="100" MinimumValue="0" Type="Double" Display="None" ErrorMessage="O2 valore misurato (%): campo dovrebbe essere tra 0 e 100.">  
                        &nbsp;*</asp:RangeValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="1">
                       <asp:Label runat="server" Text="CO2 valore misurato (%)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtCO2" runat="server" CssClass="txtClass" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtCO2" ControlToValidate="txtCO2"
                            ErrorMessage="5.4 CO2 (%): inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvCO2" runat="server" ControlToValidate="txtCO2" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="100" MinimumValue="0" Type="Double" Display="None" ErrorMessage="CO2 valore misurato (%): campo dovrebbe essere tra 0 e 100.">  
                        &nbsp;*</asp:RangeValidator>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Bacharach" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtBacharach1" CssClass="txtClass" runat="server" MaxLength="5" Width="50" />
                        <asp:RangeValidator ID="rvBacharach1" runat="server" ControlToValidate="txtBacharach1" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="10" MinimumValue="0" Type="Double" Display="None" ErrorMessage="Bacharach: campo dovrebbe essere tra 0 e 10.">  
                        &nbsp;* </asp:RangeValidator>
                        / 
                        <asp:TextBox ID="txtBacharach2" CssClass="txtClass" runat="server" MaxLength="5" Width="50" />
                         <asp:RangeValidator ID="rvBacharach2" runat="server" ControlToValidate="txtBacharach2" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="10" MinimumValue="0" Type="Double" Display="None" ErrorMessage="Bacharach: campo dovrebbe essere tra 0 e 10.">  
                        &nbsp;* </asp:RangeValidator>
                        /
                        <asp:TextBox ID="txtBacharach3" CssClass="txtClass" runat="server" MaxLength="5" Width="50" />
                        <asp:RangeValidator ID="rvBacharach3" runat="server" ControlToValidate="txtBacharach3" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="10" MinimumValue="0" Type="Double" Display="None" ErrorMessage="Bacharach: campo dovrebbe essere tra 0 e 10.">  
                        &nbsp;* </asp:RangeValidator>
                        <asp:RegularExpressionValidator ID="revtxtBacharach1" ControlToValidate="txtBacharach1"
                            ErrorMessage="5.4 Bacharach 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>

                        <asp:RegularExpressionValidator ID="revtxtBacharach2" ControlToValidate="txtBacharach2"
                            ErrorMessage="5.4 Bacharach 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>

                        <asp:RegularExpressionValidator ID="revtxtBacharach3" ControlToValidate="txtBacharach3"
                            ErrorMessage="5.4 Bacharach 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="CO fumi secchi (ppm)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtCoFumiSecchi" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtCoFumiSecchi" ControlToValidate="txtCoFumiSecchi"
                            ErrorMessage="5.4 CO fumi secchi: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvCoFumiSecchi" runat="server" ControlToValidate="txtCoFumiSecchi" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="10000" MinimumValue="0" Type="Double" Display="None" ErrorMessage="CO fumi secchi (ppm): campo dovrebbe essere tra 0 e 10000.">  
                        &nbsp;*</asp:RangeValidator>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="CO corretto (ppm)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtCoCorretto" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtCoCorretto" ControlToValidate="txtCoCorretto"
                            ErrorMessage="5.4 CO corretto: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvCoCorretto" runat="server" ControlToValidate="txtCoCorretto" ValidationGroup="vgIspezioneRapporto" EnableClientScript="true" MaximumValue="10000" MinimumValue="0" Type="Double" Display="None" ErrorMessage="CO corretto (ppm): campo dovrebbe essere tra 0 e 10000.">  
                        &nbsp;*</asp:RangeValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow Visible="false">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Portata combustibile (m3/h)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtPortataCombustibile" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtPortataCombustibile" ControlToValidate="txtPortataCombustibile"
                            ErrorMessage="5.4 Portata combustibile: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Potenza termica effettiva (kW)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtPotenzaTermicaEffettiva" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtPotenzaTermicaEffettiva" ControlToValidate="txtPotenzaTermicaEffettiva"
                            ErrorMessage="5.4 Potenza termica effettiva: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Rendimento di combustione (%)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:TextBox ID="txtRendimentoCombustione" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtRendimentoCombustione" ControlToValidate="txtRendimentoCombustione"
                            ErrorMessage="5.4 Rendimento di combustione: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" Text="Rendimento minimo di legge (%)" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label runat="server" ID="lblRendimentoMinimo" />
                        <asp:TextBox ID="txtRendimentoMinimo" CssClass="txtClass" runat="server" Width="50" />
                        <asp:RegularExpressionValidator ID="revtxtRendimentoMinimo" ControlToValidate="txtRendimentoMinimo"
                            ErrorMessage="5.4 Rendimento minimo di legge: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgIspezioneRapporto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiMisura" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiMisura" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiMisura.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiMisura" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiMisura">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione0">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             7.SISTEMI TERMOREGOLAZIONE E CONTABILIZZAZIONE DEL CALORE
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione1">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipo di distribuzione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbTipoDistribuzione" runat="server" ItemStyle-Height="20px" AutoPostBack="true" Width="200px" ValueType="System.Int32" OnSelectedIndexChanged="cbTipoDistribuzione_SelectedIndexChanged" />
             <asp:RequiredFieldValidator ID="rfvcbTipoDistribuzione" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Tipo di distribuzione: campo obbligatorio"
                    ControlToValidate="cbTipoDistribuzione">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowAltroDistribuzione" Visible="false">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Altro distribuzione" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <asp:TextBox ID="txtAltroDistribuzione" Width="200px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione2">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Contabilizzazione
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione3">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Unità immobiliari contabilizzate" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbUnitaImmobiliariContabilizzate" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="cbUnitaImmobiliariContabilizzate_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Regolare" Value="1" />
                    <dx:ListEditItem Text="Non regolare" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                    <dx:ListEditItem Text="Esenzione" Value="4" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbUnitaImmobiliariContabilizzate" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                    InitialValue="" ErrorMessage="Unità immobiliari contabilizzate: campo obbligatorio"
                    ControlToValidate="cbUnitaImmobiliariContabilizzate">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione37" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione4">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipologia contabilizzazione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbTipologiaContabilizzazione" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" />
            <asp:RequiredFieldValidator ID="rfvcbTipologiaContabilizzazione" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Tipologia contabilizzazione: campo obbligatorio"
                ControlToValidate="cbTipologiaContabilizzazione">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione5">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Termoregolazione
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione6">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Unità immobiliari dotate di sistemi di termoregolazione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbUnitaImmobiliariTermoregolazione" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="cbUnitaImmobiliariTermoregolazione_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Regolare" Value="1" />
                    <dx:ListEditItem Text="Non regolare" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                    <dx:ListEditItem Text="Esenzione" Value="4" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbUnitaImmobiliariTermoregolazione" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Unità immobiliari dotate di sistemi di termoregolazione: campo obbligatorio"
                ControlToValidate="cbUnitaImmobiliariTermoregolazione">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione38" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione7">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Tipologia termoregolazione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbTipologiaTermoregolazione" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="cbTipologiaTermoregolazione_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Altro sistema" Value="1" />
                    <dx:ListEditItem Text="Valvole termostatiche" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbTipologiaTermoregolazione" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Tipologia termoregolazione: campo obbligatorio"
                ControlToValidate="cbTipologiaTermoregolazione">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione39" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowAltroSistemiTermoregolazione">
        <asp:TableCell Width="35%" CssClass="riempimento2">
           <asp:Label runat="server" Text="Altri sistemi di termoregolazione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:TextBox ID="txtAltriSistemiTermoregolazione" Width="250px" CssClass="txtClass" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione8">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Funzionamento sistema di regolazione principale" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbFunzionamentoSistemaRegPrincipale" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="cbFunzionamentoSistemaRegPrincipale_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Regolare" Value="1" />
                    <dx:ListEditItem Text="Non regolare" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbFunzionamentoSistemaRegPrincipale" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Funzionamento sistema di regolazione principale: campo obbligatorio"
                ControlToValidate="cbFunzionamentoSistemaRegPrincipale">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione40" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione9">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Funzionamento sistema di regolazione interno alle singole unità abitative" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbFunzionamentoSistemaRegAbitative" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="cbFunzionamentoSistemaRegAbitative_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Regolare" Value="1" />
                    <dx:ListEditItem Text="Non regolare" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbFunzionamentoSistemaRegAbitative" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Funzionamento sistema di regolazione interno alle singole unità abitative: campo obbligatorio"
                ControlToValidate="cbFunzionamentoSistemaRegAbitative">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione41" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione10">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
            Ripartizione spese
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione11">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Motivazione esenzione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbMotivazioneEsenzione" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px">
                <Items>
                    <dx:ListEditItem Text="Impedimenti natura tecnica" Value="1" />
                    <dx:ListEditItem Text="Inefficienza in termini dei costi" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbMotivazioneEsenzione" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Motivazione esenzione: campo obbligatorio"
                ControlToValidate="cbMotivazioneEsenzione">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione12">
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Presenza relazione tecnica di motivazione dell'esenzione" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <dx:ASPxComboBox ID="cbPresenzaRelazioneTecnica" runat="server" ItemStyle-Height="20px" ValueType="System.Int32" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="cbPresenzaRelazioneTecnica_SelectedIndexChanged">
                <Items>
                    <dx:ListEditItem Text="Regolare" Value="1" />
                    <dx:ListEditItem Text="Non regolare" Value="2" />
                    <dx:ListEditItem Text="Nc" Value="3" />
                </Items>
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="rfvcbPresenzaRelazioneTecnica" ValidationGroup="vgIspezioneRapporto" ForeColor="Red" Display="Dynamic" runat="server" 
                InitialValue="" ErrorMessage="Presenza relazione tecnica di motivazione dell'esenzione: campo obbligatorio"
                ControlToValidate="cbPresenzaRelazioneTecnica">&nbsp;*</asp:RequiredFieldValidator>
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione42" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione13">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Verifica della presenza riferimento documentale adozione " />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            <uc1:uccheckbox id="chkVerificaDocumentaleAdozione" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkVerificaDocumentaleAdozione_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='0' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione43" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowSistemiTermoregolazioneContabilizzazione14">
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiSistemi" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiSistemi" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiSistemi.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiSistemi" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiSistemi">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             8. MISURAZIONE LIVELLI TEMPERATURA AMBIENTE
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Misurazioni eseguite" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento">
            Numero di rilevazioni eseguite&nbsp;
           <asp:TextBox ID="txtMisurazioniEseguite" Width="100px" CssClass="txtClass" runat="server" AutoPostBack="true" OnTextChanged="txtMisurazioniEseguite_TextChanged" />
            <asp:RegularExpressionValidator ID="revMisurazioniEseguiti" ControlToValidate="txtMisurazioniEseguite"
                ErrorMessage="7. Misurazioni eseguite: inserire un numero" runat="server"
                ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                EnableClientScript="true" ValidationGroup="vgIspezioneRapporto">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="rowRispettoValoriVigente">
        <asp:TableCell CssClass="riempimento2">
            <asp:Label runat="server" Text="Rispetto valori normativa vigente" />
        </asp:TableCell>
        <asp:TableCell CssClass="riempimento"> 
            <uc1:uccheckbox id="chkRispettoValoriVigente" disablena="true" runat="server" AutoPostBack="True" OnCheckedChanged="chkRispettoValoriVigente_CheckedChanged" />
            <uc1:UCRaccomandazioniPrescrizioniIspezione iDTipologiaRaccomandazionePrescrizioneIspezione='51' runat="server" ID="UCRaccomandazioniPrescrizioniIspezione20" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiAmbiente" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiAmbiente" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiAmbiante.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiAmbiante" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiAmbiante">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">                   
             9. INTERVENTI DI MIGLIORAMENTO ENERGETICO DELL’IMPIANTO 
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Interventi atti a migliorare il rendimento energetico" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblInterventiAttiMigliorare" runat="server" RepeatColumns="1" RepeatDirection="Horizontal">
                <asp:ListItem Text="Non sono stati individuati interventi economicamente convenienti" Value="1" />
                <asp:ListItem Text="Si rimanda a relazione di dettaglio" Value="2" />
                <asp:ListItem Text="NC" Value="3" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow Visible="false">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:CheckBoxList ID="cblCheckListTipo1" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" />
        </asp:TableCell>
    </asp:TableRow>

    <%-- <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Interventi atti a migliorare il rendimento energetico
        </asp:TableCell>
    </asp:TableRow>--%>

    <asp:TableRow Visible="false">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:CheckBoxList ID="cblCheckListTipo2" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" AutoPostBack="true" />
            &nbsp; 
            <asp:TextBox ID="txtMotivo" Width="880" Height="100px" CssClass="txtClass" runat="server" TextMode="MultiLine" />
        </asp:TableCell>
    </asp:TableRow>

    <%--<asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             Stima del dimensionamento del generatore/i 
        </asp:TableCell>
    </asp:TableRow>--%>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="Stima del dimensionamento del generatore/i" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblStima" RepeatColumns="1" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass">
                <asp:ListItem Text="Dimensionamento corretto" Value="1" />
                <asp:ListItem Text="Dimensionamento non corretto" Value="2" />
                <asp:ListItem Text="Si rimanda a relazione di dettaglio" Value="3" />
                <asp:ListItem Text="NC" Value="4" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiStima" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiStima" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiStima.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiStima" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiStima">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
            10. OSSERVAZIONI 
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:TextBox ID="txtOsservazioni" runat="server" TextMode="MultiLine" Width="880" Height="100" CssClass="txtClass" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             11. RACCOMANDAZIONI 
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:TextBox ID="txtRaccomandazzioni" runat="server" TextMode="MultiLine" Width="880" Height="100" CssClass="txtClass" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             12. PRESCRIZIONI 
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:TextBox ID="txtPrescrizioni" runat="server" TextMode="MultiLine" Width="880" Height="100" CssClass="txtClass" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiORP" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiORP" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiORP.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiORP" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiORP">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             13. ULTERIORI PROVVEDIMENTI
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="35%" CssClass="riempimento2">
            <asp:Label runat="server" Text="l'impianto può funzionare?" />
        </asp:TableCell>
        <asp:TableCell Width="65%" CssClass="riempimento">
            <asp:RadioButtonList ID="rblImpiantoPuoFunzionare" RepeatColumns="1" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass">
                <asp:ListItem Text="Si" Value="True" />
                <asp:ListItem Text="No pertanto l’ispettore ha provveduto alla messa fuori servizio dell’impianto" Value="False" />
            </asp:RadioButtonList>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
             14. DICHIARAZIONI DEL RESPONSABILE DI IMPIANTO   
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
            <asp:TextBox ID="txtDichiarazioniResponsabileImpianto" runat="server" TextMode="MultiLine" Width="880" Height="100" CssClass="txtClass" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
            <dx:ASPxButton runat="server" ID="btnSaveDatiDichiarazioni" Text="Salva dati ispezione rapporto" ValidationGroup="vgIspezioneRapporto"
                CausesValidation="true" ClientInstanceName="btnSaveDatiDichiarazioni" OnClick="btnSaveDatiIspezioneRapporto_Click">
                <ClientSideEvents Click="function(s, e) {var isValid = false; isValid = Page_ClientValidate('vgIspezioneRapporto');
                                                        if (isValid) {lpDatiDichiarazioni.Show();}
                                                        else {vsIspezioneRapporto.visible = false;}}" />
            </dx:ASPxButton>
            <dx:ASPxLoadingPanel ID="lpDatiDichiarazioni" runat="server" Text="Attendere, salvataggio in corso..."
                ClientInstanceName="lpDatiDichiarazioni">
            </dx:ASPxLoadingPanel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell>
            <asp:ValidationSummary ID="vsIspezioneRapporto" ValidationGroup="vgIspezioneRapporto" runat="server" EnableClientScript="true"
                ShowMessageBox="true" ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>