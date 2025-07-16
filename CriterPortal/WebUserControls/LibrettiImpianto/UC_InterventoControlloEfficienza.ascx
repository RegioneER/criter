<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_InterventoControlloEfficienza.ascx.cs" Inherits="UC_InterventoControlloEfficienza" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCCheckbox.ascx" TagPrefix="uc1" TagName="UCCheckbox" %>

<asp:GridView ID="grdGrigliaInterventi" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="IDRapportoControlloTecnico"
    OnRowDataBound="grdGrigliaInterventi_OnRowDataBound" Width="100%">
    <HeaderStyle CssClass="riempimento1"></HeaderStyle>
    
    <Columns>
        <asp:BoundField DataField="IDRapportoControlloTecnico" HeaderText="ID" ReadOnly="True" SortExpression="IDRapportoControlloTecnico" Visible="False" />
        <asp:BoundField DataField="DataControllo" HeaderText="Data controllo" ReadOnly="True" SortExpression="DataControllo" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="RagioneSocialeImpresamanutentrice" HeaderText="Ragione sociale manutentore" ReadOnly="True" SortExpression="RagioneSocialeImpresamanutentrice" />
        <asp:BoundField DataField="NumeroIscrizioneAlboImprese" HeaderText="CCIAA" ReadOnly="True" SortExpression="NumeroIscrizioneAlboImprese" />
    </Columns>
</asp:GridView>
<asp:Label runat="server" ID="lblIDTargaturaImpianto" Visible="false" />