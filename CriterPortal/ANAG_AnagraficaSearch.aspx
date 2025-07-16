<%@ Page Title="Criter - Ricerca anagrafica soggetti" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ANAG_AnagraficaSearch.aspx.cs" Inherits="ANAG_AnagraficaSearch" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>
                            <asp:Label runat="server" ID="lblTitoloTipoSoggetto" />
                        </h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowAzienda" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloAzienda" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtAzienda" TabIndex="1" Width="300" MaxLength="100" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowManutentore" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloPersona" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtManutentore" TabIndex="1" Width="300" MaxLength="100" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowFormaGiuridica" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloFormaGiuridica" Text="Forma giuridica" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFormaGiuridica" TabIndex="1" Width="215" runat="server" CssClass="selectClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCodiceSoggetto" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloCodiceSoggetto" Text="Codice soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceSoggetto" TabIndex="1" Width="200" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCodiceFiscale" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloCodiceFiscale" Text="Codice fiscale" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscale" TabIndex="1" Width="200" MaxLength="16" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowPartitaIva" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloPartitaIva" Text="Partita Iva" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIva" TabIndex="1" Width="200" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloEmail" Text="Email" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmail" TabIndex="1" Width="250" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowStatoAccreditamento" runat="server">
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoAccreditamento" AssociatedControlID="rblStatoAccreditamento" Text="Stato accreditamento" />
                    </asp:TableCell>

                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <dx:ASPxRadioButtonList ID="rblStatoAccreditamento" runat="server" RepeatColumns="1" 
                            ClientInstanceName="rblStatoAccreditamento" Border-BorderStyle="None" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataIscrizione" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloDataIscrizione" Text="Data iscrizione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataIscrizioneInizio" />
                        <asp:RegularExpressionValidator
                            ID="revDataIscrizioneInizio" ControlToValidate="txtDataIscrizioneInizio" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataIscrizioneFine" />
                        <asp:RegularExpressionValidator
                            ID="revDataIscrizioneFine" ControlToValidate="txtDataIscrizioneFine" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataAccreditamento" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloDataAccreditamento" Text="Data accreditamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataAccreditamentoInizio" />
                        <asp:RegularExpressionValidator
                            ID="revDataAccreditamentoInizio" ControlToValidate="txtDataAccreditamentoInizio" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataAccreditamentoFine" />
                        <asp:RegularExpressionValidator
                            ID="revDataAccreditamentoFine" ControlToValidate="txtDataAccreditamentoFine" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataRinnovo" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloDataRinnovo" Text="Data rinnovo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataRinnovoInizio" />
                        <asp:RegularExpressionValidator
                            ID="revDataRinnovoInizio" ControlToValidate="txtDataRinnovoInizio" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataRinnovoFine" />
                        <asp:RegularExpressionValidator
                            ID="revDataRinnovoFine" ControlToValidate="txtDataRinnovoFine" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>



                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAnagraficaAttiva" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButton ID="optAttivo" Text="Si" Checked="True" GroupName="optAttivo" runat="server" TabIndex="1" />
                        <asp:RadioButton ID="optNonAttivo" Text="No" Checked="False" GroupName="optAttivo" runat="server" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowIspettoreAttivo" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolIspettoreAttivo" Text="Ispettore attivo su ispezioni?" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                         <asp:RadioButtonList ID="rblStatoIspettoreAttivo" TabIndex="1" runat="server" Width="150" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass">
                            <asp:ListItem Value="" Text="Tutti" Selected="True" />
                            <asp:ListItem Value="0" Text="Si" />
                            <asp:ListItem Value="1" Text="No" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowfIscrizione" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloStatoIscrizione" AssociatedControlID="rblStatoIscrizione" Text="Stato iscrizione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoIscrizione" TabIndex="1" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Value="0" Text="Visualizza solo le iscrizioni completate" Selected="True" />
                            <asp:ListItem Value="1" Text="Visualizza solo le iscrizioni non completate" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="COM_AnagraficaSearch_btnProcess" runat="server" CssClass="buttonClass" Width="260"
                            OnClick="COM_AnagraficaSearch_btnProcess_Click" TabIndex="1" />&nbsp;
                        <asp:Button ID="COM_AnagraficaSearch_btnAdd" runat="server" CssClass="buttonClass" Width="260"
                            OnClick="COM_AnagraficaSearch_btnAdd_Click" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" PageSize="10" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" UseAccessibleHeader="true" DataKeyField="IDSoggetto"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound" OnItemCreated="DataGrid_ItemCreated">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDSoggetto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoDerived" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTipoSoggetto" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <asp:Panel runat="server" ID="pnlAzienda" Visible="false">
                                                    <td width="680">
                                                        <b><%#Eval("Soggetto") %></b><br />
                                                        <%#Eval("IndirizzoSoggetto") %><br />
                                                        <br />
                                                        <b>Codice soggetto:&nbsp;</b><%#Eval("CodiceSoggetto") %><br />
                                                        <br />
                                                        <b>Partita Iva:&nbsp;</b><%#Eval("PartitaIva") %>&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                                        <b>Telefono:&nbsp;</b><%#Eval("Telefono") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>Email:&nbsp;</b><%#Eval("Email") %>&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                                        <b>Email Pec:&nbsp;</b><%#Eval("EmailPec") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        
                                                    </td>
                                                    <td width="150">
                                                        <asp:Image runat="server" ID="imgSpid" ImageUrl="~/images/spid-logo-b-lb.png" Visible="false" BorderStyle="None" AlternateText="Autenticato con SPID" ToolTip="Autenticato con SPID" />
                                                        <asp:Label runat="server" ID="lblFirmaDigitale" Visible="false" />

                                                        <asp:ImageButton runat="server" ID="ImgAccreditamentoAzienda" Visible="false" AlternateText="Visualizza accreditamento Impresa" />

                                                        <asp:ImageButton ID="imgInviaLinkIscrizione" OnCommand="btnInviaLinkIscrizione" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/email.png" runat="server" AlternateText="Invia link di completamento iscrizione al soggetto" ToolTip="Invia link di completamento iscrizione al soggetto" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'iscrizione?');" TabIndex="1" />
                                                        <asp:ImageButton ID="imgInviaLinkAccessoAzienda" OnCommand="btnInviaLinkAccesso" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/mail.png" runat="server" AlternateText="Invia link di completamento accesso al sistema" ToolTip="Invia link di completamento accesso al sistema" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'accesso al sistema?');" TabIndex="1" />
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlPersona" Visible="false">
                                                    <td width="680">
                                                        <b><%#Eval("Soggetto") %></b><br />
                                                        <%#Eval("IndirizzoSoggetto") %><br />
                                                        <br />
                                                        <b>Codice soggetto:&nbsp;</b><%#Eval("CodiceSoggetto") %><br />
                                                        <br />
                                                        <b>Telefono:&nbsp;</b><%#Eval("Telefono") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>Email:&nbsp;</b><%#Eval("Email") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>Codice fiscale:&nbsp;</b><%#Eval("CodiceFiscale") %>
                                                    </td>
                                                    <td width="150">
                                                        <asp:Label runat="server" ID="lblAttivazioneUtenza" Visible="false" />
                                                        <asp:ImageButton ID="imgInviaLinkAccesso" OnCommand="btnInviaLinkAccesso" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/mail.png" runat="server" AlternateText="Invia link di completamento accesso al sistema" ToolTip="Invia link di completamento accesso al sistema" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'accesso al sistema?');" TabIndex="1" />
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlIspettore" Visible="false">
                                                    <td width="680">
                                                        <b><%#Eval("Soggetto") %></b><br />
                                                        <%#Eval("IndirizzoResidenza") %>&nbsp;<%#Eval("CapResidenza") %>&nbsp;<%#Eval("CittaResidenza") %><br />
                                                        <br />
                                                        <b>Codice soggetto:&nbsp;</b><%#Eval("CodiceSoggetto") %>&nbsp;&nbsp;&nbsp;<b>Data iscrizione:&nbsp;</b><%#Eval("DataIscrizione") %><br />
                                                        <br />
                                                        <b>Telefono:&nbsp;</b><%#Eval("Telefono") %>&nbsp;&nbsp;&nbsp;
                                                        <b>Email:&nbsp;</b><%#Eval("Email") %>&nbsp;&nbsp;&nbsp;<br />
                                                        <b>Codice fiscale:&nbsp;</b><%#Eval("CodiceFiscale") %>
                                                    </td>
                                                    <td width="150">
                                                        <asp:Image runat="server" ID="imgSpidIspettore" ImageUrl="~/images/spid-logo-b-lb.png" Visible="false" BorderStyle="None" AlternateText="Autenticato con SPID" ToolTip="Autenticato con SPID" />
                                                        <asp:Label runat="server" ID="lblFirmaDigitaleIspettore" Visible="false" />
                                                        <asp:ImageButton runat="server" ID="ImgAccreditamentoIspettore" AlternateText="Visualizza accreditamento Ispettore" />
                                                        
                                                        <asp:ImageButton OnCommand="ToggleFlagAttivoIspettore" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") +","+ DataBinder.Eval(Container.DataItem,"fAttivoAccreditamento").ToString() %>' runat="server" ID="imgFlagAttivoIspettore" AlternateText="Attiva/Disattiva ispettore su ispezioni" ToolTip="Attiva/Disattiva ispettore su ispezioni" OnClientClick="javascript:return confirm('Confermi di escludere/includere l ispettore dalle verifiche ispettive?');" TabIndex="1" />
                                                        
                                                        <asp:ImageButton ID="imgInviaLinkIscrizioneIspettore" OnCommand="btnInviaLinkIscrizione" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/email.png" runat="server" AlternateText="Invia link di completamento iscrizione al soggetto" ToolTip="Invia link di completamento iscrizione al soggetto" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'iscrizione?');" TabIndex="1" />
                                                        <asp:ImageButton ID="imgInviaLinkAccessoIspettore" OnCommand="btnInviaLinkAccesso" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/mail.png" runat="server" AlternateText="Invia link di completamento accesso al sistema" ToolTip="Invia link di completamento accesso al sistema" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'accesso al sistema?');" TabIndex="1" />
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlCittadino" Visible="false">
                                                    <td width="680">
                                                        <b><%#Eval("Soggetto") %></b><br />
                                                        <br />
                                                        <b>Codice fiscale:&nbsp;</b><%#Eval("CodiceFiscale") %>
                                                    </td>
                                                    <td width="150">
                                                        <asp:Image runat="server" ID="imgSpidCittadino" ImageUrl="~/images/spid-logo-b-lb.png" Visible="false" BorderStyle="None" AlternateText="Autenticato con SPID" ToolTip="Autenticato con SPID" />
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlEnteLocale" Visible="false">
                                                    <td width="680">
                                                        <b><%#Eval("Soggetto") %></b><br />
                                                        <%#Eval("IndirizzoSoggetto") %><br />
                                                        <br />
                                                        <b>Partita Iva:&nbsp;</b><%#Eval("PartitaIva") %>&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                                        <b>Telefono:&nbsp;</b><%#Eval("Telefono") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>Email:&nbsp;</b><%#Eval("Email") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>Fax:&nbsp;</b><%#Eval("Fax") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        
                                                    </td>
                                                    <td width="150">
                                                        <asp:Label runat="server" ID="Label1" Visible="false" />
                                                        <asp:ImageButton ID="imgInviaLinkIscrizioneEnteLocale" OnCommand="btnInviaLinkIscrizione" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/email.png" runat="server" AlternateText="Invia link di completamento iscrizione al soggetto" ToolTip="Invia link di completamento iscrizione al soggetto" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'iscrizione?');" TabIndex="1" />
                                                        <asp:ImageButton ID="imgInviaLinkAccessoEnteLocale" OnCommand="btnInviaLinkAccesso" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") %>' ImageUrl="~/images/buttons/mail.png" runat="server" AlternateText="Invia link di completamento accesso al sistema" ToolTip="Invia link di completamento accesso al sistema" OnClientClick="javascript:return confirm('Confermi di inviare il link al soggetto per completare l\'accesso al sistema?');" TabIndex="1" />
                                                    </td>
                                                </asp:Panel>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <HeaderTemplate>
                                        Modifica
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgEdit" ToolTip="Modifica anagrafica soggetto" AlternateText="Modifica anagrafica soggetto" ImageUrl="~/images/Buttons/edit.png" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <HeaderTemplate>
                                        Stato
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton OnCommand="ToggleFlagAttivo" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDSoggetto") +","+ DataBinder.Eval(Container.DataItem,"fAttivo").ToString() %>' runat="server" ID="imgFlagAttivo" AlternateText="Attiva/Disattiva il soggetto" ToolTip="Attiva/Disattiva il soggetto" OnClientClick="javascript:return confirm('Confermi il cambio di stato attivo/non attivo');" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TipoFirma" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerQualifier" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerName" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerSurname" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerIdentifier" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerFullName" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerAuthority" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerCertificationAuthority" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="SignerSerialNumber" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="fAttivo" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="fIscrizione" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceSoggetto" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" Visible="false" ItemStyle-Width="30">
                                    <HeaderTemplate>
                                        Albo
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgEditAlbo" ToolTip="Modifica anagrafica soggetto su Albo" AlternateText="Modifica anagrafica soggetto su Albo" ImageUrl="~/images/Buttons/albo.png" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ImageUrlStatoAccreditamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="StatoAccreditamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="fAttivoAccreditamento" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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