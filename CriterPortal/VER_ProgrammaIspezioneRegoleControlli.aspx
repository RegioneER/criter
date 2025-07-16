<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VER_ProgrammaIspezioneRegoleControlli.aspx.cs" Inherits="VER_ProgrammaIspezioneRegoleControlli" %>
<%@ Register Src="~/WebUserControls/WUC_Progress.ascx" TagPrefix="uc1" TagName="WebUSUpdateProgress" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .HelpFixAlignItem {
            display: flex;
            align-items: center;
        }
    </style>
    <script type="text/javascript">        
        var textSeparator = ",";
        function updateText(listbox, args, dropdownedit) {
            var selectedItems = listbox.GetSelectedItems();
            dropdownedit.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args, list) {
            list.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts, list);
            list.SelectValues(values);
            updateText(list, null, dropDown); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts, list) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = list.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Table runat="server" ID="tblPnlRegoleControlli" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Left" CssClass="riempimento1">
                        REGOLE CONTROLLI PER PROGRAMMA ISPEZIONE
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" ColumnSpan="5" Width="600" HorizontalAlign="Left" CssClass="riempimento">
                        Per effettuare un inserimento massivo di generatori nel programma ispezione attivo è possibile scegliere tra due opzioni:<br />
                        <ul>
                            <li><b>NUOVA ANALISI DATI:</b> Nel caso in cui si voglia effettuare una nuova analisi sui generatori da includere nel programma ispezione impostare per ogni regola controllo i filtri desiderati e cliccare sul pulsante NUOVA ANALISI
                            </li>
                            <li><b>ANALISI DATI ESISTENTE:</b> Nel caso in cui si voglia utilizzare l'ultima analisi effettuata il <asp:label runat="server" Font-Bold="true" ID="lblDataUltimaAnalisi" /> cliccare sul pulsante ANALISI ESISTENTE
                            </li>
                        </ul>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:Button ID="btnProgrammaIspezione" runat="server" TabIndex="1" 
                            CssClass="buttonClass" Width="250" 
                            CausesValidation="false" Text="VAI AL PROGRAMMA ISPEZIONI" 
                            OnClick="btnProgrammaIspezione_Click" />
                        &nbsp;
                        <asp:Button ID="btnNuovaAnalisiDati" runat="server" TabIndex="1" CssClass="buttonClass" Width="250"
                            OnClick="btnNuovaAnalisiDati_Click" Text="NUOVA ANALISI DATI" UseSubmitBehavior="false"
                            OnClientClick="if (confirm('Confermi di voler effettuare una nuova analisi dati?Questa operazione richiederà del tempo...')) {disableBtn(this.id, 'ATTENDERE ANALISI DEI DATI IN CORSO...')} else {return false};" />
                        &nbsp;
                        <asp:Button ID="btnAnalisiDatiEsistente" runat="server" TabIndex="1" CssClass="buttonClass" Width="250"
                            OnClick="btnAnalisiDatiEsistente_Click" Text="ANALISI DATI ESISTENTE" UseSubmitBehavior="false"
                            OnClientClick="if (confirm('Confermi di voler utilizzare l analisi dati esistente?')) {disableBtn(this.id, 'ATTENDERE RECUPERO ANALISI DEI DATI IN CORSO...')} else {return false};" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="600" HorizontalAlign="Left" CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblTitolo1" Font-Bold="true" Text="Regole programma ispezione" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="150" HorizontalAlign="Center" CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblTitolo2" Font-Bold="true" Text="N. generatori" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblTitolo3" Font-Bold="true" Text="%" />
                    </asp:TableCell>

                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblTitolo4" Font-Bold="true" Text="" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <%--<asp:Label runat="server" ID="lblTitoloCountRegolaR1" AssociatedControlID="lblRegolaR1Count" Text="<b>GT che hanno anzianità superiore a 15 anni</b><br/><br/>Solo generatori con:<br/>Potenza dei generatori >=10 kW<br/> Combustibili: Gas naturale, Gpl extra rete, Gpl, Gasolio, Kerosene, Olio Combustibile<br/>Tipi di generatori: Gruppi termici singoli, Gruppi termici modulari" />--%>
                        <asp:Label runat="server" ID="lblTitoloCountRegolaR1" AssociatedControlID="lblRegolaR1Count" Text="<b>GT che hanno anzianità superiore a 15 anni</b><br/><br/>Solo generatori con:<br/>" />
                        Potenza dei generatori >=
                        <asp:TextBox CssClass="txtClass_o" ID="txtPotenzaR1" runat="server" Width="50px" />kW
                        <cc1:FilteredTextBoxExtender ID="fbetxtPotenzaR1" runat="server" FilterType="Numbers, Custom" ValidChars="," TargetControlID="txtPotenzaR1" />
                        <br />
                        <div class="HelpFixAlignItem">
                            Combustibili:
                        <dx:ASPxDropDownEdit ClientInstanceName="ddeR1" ID="ddeCombustibileR1" Width="500px" runat="server" AnimationType="None">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbR1" SelectionMode="CheckColumn"
                                    runat="server" Height="200" EnableSelectAll="true">
                                    <Border BorderStyle="None" />
                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                    <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeR1 ); }"
                                        Init="function (s, e) { updateText (s, e, ddeR1 ); }" />
                                </dx:ASPxListBox>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="padding: 4px">
                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                <ClientSideEvents Click="function(s, e){ ddeR1.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DropDownWindowTemplate>
                            <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbR1 ); }"/>
                              <%-- DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbR1); }"--%>  
                        </dx:ASPxDropDownEdit>
                        </div>
                        <div class="HelpFixAlignItem">
                            Tipi di generatori:
                            <dx:ASPxDropDownEdit ClientInstanceName="ddeGr1" ID="ddeTipiGeneratoriR1" Width="400px" runat="server" AnimationType="None">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbGr1" SelectionMode="CheckColumn"
                                        runat="server" Height="200" EnableSelectAll="true">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                        <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeGr1 ); }"
                                            Init="function (s, e) { updateText (s, e, ddeGr1 ); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                    <ClientSideEvents Click="function(s, e){ ddeGr1.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbGr1 ); }" />
                                <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbGr1); }"--%>
                            </dx:ASPxDropDownEdit>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="150" CssClass="riempimento">
                        <asp:Label ID="lblRegolaR1Count" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="50" TabIndex="1" MaxLength="3" Enabled="false" runat="server" ID="txtRegolaR1Percentage" />
                        <cc1:FilteredTextBoxExtender ID="ftbetxtRegolaR1Percentage" runat="server" TargetControlID="txtRegolaR1Percentage"
                            FilterType="Numbers" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkRegolaR1" AutoPostBack="true" OnCheckedChanged="chkRegolaR1_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <%--<asp:Label runat="server" ID="lblTitoloCountRegolaR2" AssociatedControlID="lblRegolaR2Count" Text="<b>GT alimentati a gas con potenza termica utile nominale > a 100 kW</b><br/><br/>Solo generatori con:<br/> Combustibili: Gas naturale, Gpl extra rete, Gpl<br/>Tipi di generatori: Gruppi termici singoli, Gruppi termici modulari" />--%>
                        <asp:Label runat="server" ID="lblTitoloCountRegolaR2" AssociatedControlID="lblRegolaR2Count" Text="<b>GT alimentati a gas</b><br/><br/>Solo generatori con:<br/>" />
                        potenza termica utile nominale > a
                        <asp:TextBox CssClass="txtClass_o" ID="txtPotenzaR2" runat="server" Width="50px" />kW
                        <cc1:FilteredTextBoxExtender ID="fbetxtPotenzaR2" runat="server" FilterType="Numbers, Custom" ValidChars="," TargetControlID="txtPotenzaR2" />
                        <div class="HelpFixAlignItem">
                            Combustibili:
                        <dx:ASPxDropDownEdit ClientInstanceName="ddeR2" ID="ddeCombustibileR2" Width="500px" runat="server" AnimationType="None">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbR2" SelectionMode="CheckColumn"
                                    runat="server" Height="200" EnableSelectAll="true">
                                    <Border BorderStyle="None" />
                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                    <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeR2 ); }"
                                        Init="function (s, e) { updateText (s, e, ddeR2 ); }" />
                                </dx:ASPxListBox>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="padding: 4px">
                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                <ClientSideEvents Click="function(s, e){ ddeR2.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DropDownWindowTemplate>
                            <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbR2 ); }" />
                            <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbR2); }"--%>
                        </dx:ASPxDropDownEdit>
                        </div>
                        <div class="HelpFixAlignItem">
                            Tipi di generatori:
                            <dx:ASPxDropDownEdit ClientInstanceName="ddeGr2" ID="ddeTipiGeneratoriR2" Width="400px" runat="server" AnimationType="None">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbGr2" SelectionMode="CheckColumn"
                                        runat="server" Height="200" EnableSelectAll="true">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                        <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeGr2 ); }"
                                            Init="function (s, e) { updateText (s, e, ddeGr2 ); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                    <ClientSideEvents Click="function(s, e){ ddeGr2.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbGr2 ); }" />
                                <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbGr2); }"--%>
                            </dx:ASPxDropDownEdit>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="150" CssClass="riempimento">
                        <asp:Label ID="lblRegolaR2Count" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="50" TabIndex="1" MaxLength="3" Enabled="false" runat="server" ID="txtRegolaR2Percentage" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkRegolaR2" AutoPostBack="true" OnCheckedChanged="chkRegolaR2_CheckedChanged" />
                    </asp:TableCell>

                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <%--<asp:Label runat="server" ID="lblTitoloCountRegolaR3" AssociatedControlID="lblRegolaR3Count" Text="<b>GT alimentati a combustibile liquido o solido con potenza termica utile > a 100 kW</b><br/><br/>Solo generatori con:<br/> Combustibili: Olio combustibile, Gasolio, Kerosene<br/>Tipi di generatori: Gruppi termici singoli, Gruppi termici modulari" />--%>
                        <asp:Label runat="server" ID="lblTitoloCountRegolaR3" AssociatedControlID="lblRegolaR3Count" Text="<b>GT alimentati a combustibile liquido o solido</b><br/><br/>Solo generatori con:<br/>" />
                        potenza termica utile >
                        <asp:TextBox CssClass="txtClass_o" ID="txtPotenzaR3" runat="server" Width="50px" />kW
                         <cc1:FilteredTextBoxExtender ID="fbetxtPotenzaR3" runat="server" FilterType="Numbers, Custom" ValidChars="," TargetControlID="txtPotenzaR3" />
                        <br />
                        <div class="HelpFixAlignItem">
                            Combustibili:
                        <dx:ASPxDropDownEdit ClientInstanceName="ddeR3" ID="ddeCombustibileR3" Width="500px" runat="server" AnimationType="None">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbR3" SelectionMode="CheckColumn"
                                    runat="server" Height="200" EnableSelectAll="true">
                                    <Border BorderStyle="None" />
                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                    <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeR3 ); }"
                                        Init="function (s, e) { updateText (s, e, ddeR3 ); }" />
                                </dx:ASPxListBox>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="padding: 4px">
                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                <ClientSideEvents Click="function(s, e){ ddeR3.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DropDownWindowTemplate>
                            <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbR3 ); }" />
                            <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbR3); }"--%>
                        </dx:ASPxDropDownEdit>
                        </div>
                        <div class="HelpFixAlignItem">
                            Tipi di generatori:
                             <dx:ASPxDropDownEdit ClientInstanceName="ddeGr3" ID="ddeTipiGeneratoriR3" Width="400px" runat="server" AnimationType="None">
                                 <DropDownWindowStyle BackColor="#EDEDED" />
                                 <DropDownWindowTemplate>
                                     <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbGr3" SelectionMode="CheckColumn"
                                         runat="server" Height="200" EnableSelectAll="true">
                                         <Border BorderStyle="None" />
                                         <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                         <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeGr3 ); }"
                                             Init="function (s, e) { updateText (s, e, ddeGr3 ); }" />
                                     </dx:ASPxListBox>
                                     <table style="width: 100%">
                                         <tr>
                                             <td style="padding: 4px">
                                                 <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                     <ClientSideEvents Click="function(s, e){ ddeGr3.HideDropDown(); }" />
                                                 </dx:ASPxButton>
                                             </td>
                                         </tr>
                                     </table>
                                 </DropDownWindowTemplate>
                                 <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbGr3 ); }" />
                                 <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbGr3); }"--%>
                             </dx:ASPxDropDownEdit>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="150" CssClass="riempimento">
                        <asp:Label ID="lblRegolaR3Count" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="50" TabIndex="1" MaxLength="3" Enabled="false" runat="server" ID="txtRegolaR3Percentage" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkRegolaR3" AutoPostBack="true" OnCheckedChanged="chkRegolaR3_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <%--<asp:Label runat="server" ID="lblTitoloCountRegolaR4" AssociatedControlID="lblRegolaR4Count" Text="<b>GT alimentati combustibile liquido o solido con potenza termica utile 20 < P < 100 kW</b><br/><br/>Solo generatori con:<br/> Combustibili: Olio combustibile, Gasolio, Kerosene<br/>Tipi di generatori: Gruppi termici singoli, Gruppi termici modulari" />--%>
                        <asp:Label runat="server" ID="lblTitoloCountRegolaR4" AssociatedControlID="lblRegolaR4Count" Text="<b>GT alimentati combustibile liquido o solido</b><br/><br/>Solo generatori con:<br/>" />
                        con potenza termica utile
                        <asp:TextBox CssClass="txtClass_o" ID="txtPotenzaR4Da" runat="server" Width="50px" />
                        <cc1:FilteredTextBoxExtender ID="fbetxtPotenzaR4Da" runat="server" FilterType="Numbers, Custom" ValidChars="," TargetControlID="txtPotenzaR4Da" />
                        < P <                        
                        <asp:TextBox CssClass="txtClass_o" ID="txtPotenzaR4A" runat="server" Width="50px" />
                        <cc1:FilteredTextBoxExtender ID="fbetxtPotenzaR4A" runat="server" FilterType="Numbers, Custom" ValidChars="," TargetControlID="txtPotenzaR4A" />
                        kW
                        <br />
                        <div class="HelpFixAlignItem">
                            Combustibili:
                        <dx:ASPxDropDownEdit ClientInstanceName="ddeR4" ID="ddeCombustibileR4" Width="500px" runat="server" AnimationType="None">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbR4" SelectionMode="CheckColumn"
                                    runat="server" Height="200" EnableSelectAll="true">
                                    <Border BorderStyle="None" />
                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                    <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeR4 ); }"
                                        Init="function (s, e) { updateText (s, e, ddeR4 ); }" />
                                </dx:ASPxListBox>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="padding: 4px">
                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                <ClientSideEvents Click="function(s, e){ ddeR4.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DropDownWindowTemplate>
                            <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbR4 ); }"/>
                            <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbR4); }" --%>
                        </dx:ASPxDropDownEdit>
                        </div>
                        <div class="HelpFixAlignItem">
                            Tipi di generatori:
                            <dx:ASPxDropDownEdit ClientInstanceName="ddeGr4" ID="ddeTipiGeneratoriR4" Width="400px" runat="server" AnimationType="None">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbGr4" SelectionMode="CheckColumn"
                                        runat="server" Height="200" EnableSelectAll="true">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                        <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeGr4 ); }"
                                            Init="function (s, e) { updateText (s, e, ddeGr4 ); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                    <ClientSideEvents Click="function(s, e){ ddeGr4.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbGr4 ); }" />
                                 <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbGr4); }"--%>
                            </dx:ASPxDropDownEdit>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="150" CssClass="riempimento">
                        <asp:Label ID="lblRegolaR4Count" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="50" TabIndex="1" MaxLength="3" Enabled="false" runat="server" ID="txtRegolaR4Percentage" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkRegolaR4" AutoPostBack="true" OnCheckedChanged="chkRegolaR4_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="550" CssClass="riempimento">
                        <%--<asp:Label runat="server" ID="lblTitoloCountRegolaR5" AssociatedControlID="lblRegolaR5Count" Text="<b>NON viene registrato il RTCEE entro i sei (6) mesi successivi alla scadenza dell’obbligo</b><br/><br/>Solo generatori con:<br/>Potenza dei generatori >=10 kW<br/> Combustibili: Gas naturale, Gpl extra rete, Gpl, Gasolio, Kerosene, Olio Combustibile<br/>Tipi di generatori: Gruppi termici singoli, Gruppi termici modulari" />--%>
                        <asp:Label runat="server" ID="lblTitoloCountRegolaR5" AssociatedControlID="lblRegolaR5Count" Text="<b>NON viene registrato il RTCEE entro i sei (6) mesi successivi alla scadenza dell’obbligo</b><br/><br/>Solo generatori con:<br/>" />
                        Potenza dei generatori >=
                        <asp:TextBox CssClass="txtClass_o" ID="txtPotenzaR5" runat="server" Width="50px" />kW
                        <cc1:FilteredTextBoxExtender ID="fbetxtPotenzaR5" runat="server" FilterType="Numbers, Custom" ValidChars="," TargetControlID="txtPotenzaR5" />
                        <br />
                        <div class="HelpFixAlignItem">
                            Combustibili:
                         <dx:ASPxDropDownEdit ClientInstanceName="ddeR5" ID="ddeCombustibileR5" Width="500px" runat="server" AnimationType="None">
                             <DropDownWindowStyle BackColor="#EDEDED" />
                             <DropDownWindowTemplate>
                                 <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbR5" SelectionMode="CheckColumn"
                                     runat="server" Height="200" EnableSelectAll="true">
                                     <Border BorderStyle="None" />
                                     <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                     <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeR5 ); }"
                                         Init="function (s, e) { updateText (s, e, ddeR5 ); }" />
                                 </dx:ASPxListBox>
                                 <table style="width: 100%">
                                     <tr>
                                         <td style="padding: 4px">
                                             <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                 <ClientSideEvents Click="function(s, e){ ddeR5.HideDropDown(); }" />
                                             </dx:ASPxButton>
                                         </td>
                                     </tr>
                                 </table>
                             </DropDownWindowTemplate>
                             <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbR5 ); }" />
                             <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbR5); }"--%>
                         </dx:ASPxDropDownEdit>
                        </div>
                        <div class="HelpFixAlignItem">
                            Tipi di generatori:
                              <dx:ASPxDropDownEdit ClientInstanceName="ddeGr5" ID="ddeTipiGeneratoriR5" Width="400px" runat="server" AnimationType="None">
                                  <DropDownWindowStyle BackColor="#EDEDED" />
                                  <DropDownWindowTemplate>
                                      <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="lbGr5" SelectionMode="CheckColumn"
                                          runat="server" Height="200" EnableSelectAll="true">
                                          <Border BorderStyle="None" />
                                          <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                          <ClientSideEvents SelectedIndexChanged="function (s, e) { updateText (s, e, ddeGr5 ); }"
                                              Init="function (s, e) { updateText (s, e, ddeGr5 ); }" />
                                      </dx:ASPxListBox>
                                      <table style="width: 100%">
                                          <tr>
                                              <td style="padding: 4px">
                                                  <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                      <ClientSideEvents Click="function(s, e){ ddeGr5.HideDropDown(); }" />
                                                  </dx:ASPxButton>
                                              </td>
                                          </tr>
                                      </table>
                                  </DropDownWindowTemplate>
                                  <ClientSideEvents TextChanged="function (s, e) { SynchronizeListBoxValues (s, e, lbGr5 ); }" />
                                  <%--DropDown="function (s, e) { SynchronizeListBoxValues (s, e, lbGr5); }"--%>
                              </dx:ASPxDropDownEdit>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="150" CssClass="riempimento">
                        <asp:Label ID="lblRegolaR5Count" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="50" TabIndex="1" MaxLength="3" Enabled="false" runat="server" ID="txtRegolaR5Percentage" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkRegolaR5" AutoPostBack="true" OnCheckedChanged="chkRegolaR5_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowAggiornaRisultati">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Right" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAggiornaRisultati" Text="EFFETTUA VALUTAZIONE" OnClick="btnAggiornaRisultati_Click"
                            CssClass="buttonClass" Width="250px" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Right" CssClass="riempimento5">
                        <asp:CustomValidator ID="ServerErrorCustomValidator" runat="server" Text="Attenzione, ricontrollare i seguenti campi:<br/><br/>Potenza<br/>Combustibili<br/>Tipi di generatori" OnServerValidate="ServerErrorCustomValidator_ServerValidate" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table runat="server" ID="tblRisultatiRegoleControlli" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Left" CssClass="riempimento1">
                        RISULTATI REGOLE CONTROLLI
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento">
                        &nbsp;
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="200" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblResult0" Font-Bold="true" Text="Totale generatori selezionati" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="100" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCountGeneratoriDaInviare" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="200" CssClass="riempimento">
                        <asp:Label ID="lblResult1" runat="server" Font-Bold="true" Text="Totale importo ispezioni" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="100" HorizontalAlign="Center" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblImportoGeneratoriDaInviare" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Right" CssClass="riempimento5">
                        <asp:Button ID="btnInserGeneratoriInProgrammaIspezione" runat="server" TabIndex="1" CssClass="buttonClass" Width="250"
                            OnClick="btnInserGeneratoriInProgrammaIspezione_Click" Text="INSERISCI NEL PROGRAMMA ISPEZIONE" UseSubmitBehavior="false"
                            OnClientClick="if (confirm('Confermi di inserire i generatori selezionati nel programma ispezione?')) {disableBtn(this.id, 'Attendere, inserimento in corso...')} else {return false};" />
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <uc1:WebUSUpdateProgress runat="server" id="WebUSUpdateProgress" />
</asp:Content>