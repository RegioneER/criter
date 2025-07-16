<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_GoogleAutosuggest.ascx.cs" Inherits="WebUserControls_WUC_GoogleAutosuggest" %>

<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=<%= System.Configuration.ConfigurationManager.AppSettings["GoogleApiKey"].ToString() %>&libraries=places"></script>
<script type="text/javascript">
    function OnInit(s, e)
    {
        var options =
        {
            types: ['address'],
            componentRestrictions:
            {
                country: 'it'
            }
        };

        var places = new google.maps.places.Autocomplete(s.GetInputElement(), options);
        google.maps.event.addListener(places, 'place_changed',
        function ()
        {
            var place = places.getPlace();
            for (var i = 0; i < place.address_components.length; i++)
            {
                for (var j = 0; j < place.address_components[i].types.length; j++)
                {
                    if (place.address_components[i].types[j] == "postal_code")
                    {        
                        lblPostalCode.SetText(place.address_components[i].long_name)
                    }
                    if (place.address_components[i].types[j] == "route")
                    {
                        lblNormalizeAddress.SetText(place.address_components[i].short_name)
                    }
                    if (place.address_components[i].types[j] == "locality")
                    {
                        lblLocality.SetText(place.address_components[i].long_name)
                    }
                }
            }
        });
    }
</script>

<asp:Table runat="server" ID="tblAddress" Width="600px">
    <asp:TableRow ID="rowFeedBack" runat="server">
        <asp:TableCell ColumnSpan="2">
            <asp:Label runat="server" ID="lblIDLibrettoImpianto" Visible="false" />
            <asp:Label runat="server" ID="lblFeedBackAddress" Font-Bold="true" /><br />
            <asp:Label runat="server" ID="lblFeedBackCivico" Font-Bold="true" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="rowAddress" runat="server">
        <asp:TableCell ColumnSpan="2">
            <asp:Label runat="server" ID="lblAddress" /><br /><br />
            <asp:Button ID="btnNormalizzaIndirizzo" runat="server" Text="NORMALIZZA INDIRIZZO POSTALE" 
                   OnClick="btnNormalizzaIndirizzo_Click" Visible="false" CssClass="buttonSmallClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="rowCivico" runat="server">
        <asp:TableCell ColumnSpan="2">
            <asp:Button ID="btnNormalizzaCivico" runat="server" Text="NORMALIZZA CIVICO POSTALE" 
                   OnClick="btnNormalizzaCivico_Click" Visible="false" CssClass="buttonSmallClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowCap" runat="server">
        <asp:TableCell ColumnSpan="2">
            <asp:Button ID="btnNormalizzaCap" runat="server" Text="SOSTITUISCI CAP POSTALE" 
                   OnClick="btnNormalizzaCap_Click" CssClass="buttonSmallClass" Width="250px" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow ID="rowNormalizzeAddress" Visible="false" runat="server">
        <asp:TableCell>
             <dx:ASPxTextBox ID="tb" runat="server" Width="350px" ClientInstanceName="tb">
                <ClientSideEvents Init="OnInit" />
            </dx:ASPxTextBox>
            <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="lblNormalizeAddress" runat="server" ClientInstanceName="lblNormalizeAddress" Width="184" ReadOnly="true" Border-BorderStyle="None" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="lblPostalCode" runat="server" ClientInstanceName="lblPostalCode" Width="50" ReadOnly="true" Border-BorderStyle="None" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="lblLocality" runat="server" ClientInstanceName="lblLocality" Width="100" ReadOnly="true" Border-BorderStyle="None" />
                    </td>
                </tr>
            </table>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Button ID="btnSaveAddress" runat="server" Text="SALVA" OnClientClick="javascript:return confirm('Confermi di inserire l\'indirizzo normalizzato?');"
                   OnClick="btnSaveAddress_Click" CssClass="buttonSmallClass" Width="150px" />
            <asp:Button ID="btnAnnullaAddress" runat="server" Text="ANNULLA" 
                   OnClick="btnAnnullaAddress_Click" CssClass="buttonSmallClass" Width="150px" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="rowNormalizzeCivico" Visible="false" runat="server">
        <asp:TableCell>
             <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="txtCivico" runat="server" MaxLength="5" Width="100" Border-BorderStyle="None" />
                    </td>
                </tr>
            </table>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Button ID="btnSaveCivico" runat="server" Text="SALVA CIVICO" OnClientClick="javascript:return confirm('Confermi di inserire il civico normalizzato?');"
                   OnClick="btnSaveCivico_Click" CssClass="buttonSmallClass" Width="150px" />
            <asp:Button ID="btnAnnullaCivico" runat="server" Text="ANNULLA" 
                   OnClick="btnAnnullaCivico_Click" CssClass="buttonSmallClass" Width="150px" />
        </asp:TableCell>
    </asp:TableRow>


    <asp:TableRow ID="rowNormalizzeCap" Visible="false" runat="server">
        <asp:TableCell>
             <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="txtCap" runat="server" MaxLength="5" Width="100" Border-BorderStyle="None" />
                    </td>
                </tr>
            </table>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Button ID="btnSaveCap" runat="server" Text="SALVA CAP" OnClientClick="javascript:return confirm('Confermi di sostituire il cap?');"
                   OnClick="btnSaveCap_Click" CssClass="buttonSmallClass" Width="150px" />
            <asp:Button ID="btnAnnullaCap" runat="server" Text="ANNULLA" 
                   OnClick="btnAnnullaCap_Click" CssClass="buttonSmallClass" Width="150px" />
        </asp:TableCell>
    </asp:TableRow>

</asp:Table>