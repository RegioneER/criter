<%@ Page Title="" Async="true" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CriterApi.aspx.cs" Inherits="CriterApi_CriterApi" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    
    <dx:ASPxHyperLink ID="lnkApiDocumentation" Font-Size="Medium" runat="server" Text="Documentazione Api" Target="_blank" Font-Bold="true" />
    <br /><br />
    
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" ClientInstanceName="roundPanel"
        EnableAnimation="true" ShowCollapseButton="true" ShowHeader="true" AllowCollapsingByHeaderClick="true"
        HeaderText="Metodi GET" runat="server" Width="870">
        <PanelCollection>
            <dx:PanelContent>
                <asp:Table Width="870" ID="tblGet" CssClass="TableClass" runat="server">
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento1">
                                Chiamata Api
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento1" Width="200">
                                Descrizione Api
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento1">
                                Risultato Json
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGetCodiciTargatura" runat="server" Text="Get Codici Targatura" Width="150" CssClass="buttonClass" OnClick="Btn_GetCodiciTargaturaImpianto_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato load:<br />
                                            <asp:Label runat="server" ID="lblCodiciTargaturaResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtCodiciTargaturaApiKey" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtCodiciTargaturaCodiceSoggetto" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtCodiciTargaturaApiKeySoggetto" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>GetCodiciTargaturaByCodiceSoggetto</h5>
                            <h6>Ritorna un lista di codici targatura in formato json</h6>
                            <h5>Parametro in formato json:<br /><font color="green"><b><i>{'CriterAPIKey':'your Key', 'CodiceSoggetto':'cod. soggetto', 'CriterAPIKeySoggetto':'key soggetto'}</i></b></font></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlCodiciTargatura" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkGetCodiciTargatura" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Result_CodiciTargatura" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_GetLibrettoImpianto_ByCodice" runat="server" Text="Get Libretto" Width="150" CssClass="buttonClass" OnClick="Btn_GetLibrettoImpianto_ByCodice_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Api key:<br />
                                            <asp:TextBox ID="txtApiKeyLibretto" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtGetCodiceSoggettoLibretto" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtGetApiKeySoggettoLibretto" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            Codice targatura:<br />
                                            <asp:TextBox ID="Txt_DefaultParam_GetLibrettoImpianto_ByCodice" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="D9F7C49E-B8AA-4210-9AC3-A22A011CB49D" /><br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>GetLibrettoImpiantoByCodice</h5>
                            <h6>Ritorna un libretto di impianto in formato json</h6>
                            <h5>Parametro in formato json:<br /><font color="green"><b><i>{'CriterAPIKey':'your Key', 'CodiceTargatura':'cod. targatura', 'CodiceSoggetto':'cod. soggetto', 'CriterAPIKeySoggetto':'key soggetto'}</i></b></font></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlLibretto" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkGetLibretto" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Result_GetLibrettoImpianto_ByCodice" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_GetRapportoTecnico_GT_ByID" runat="server" Text="Get Rapporto GT" Width="150" CssClass="buttonClass" OnClick="Btn_GetRapportoTecnico_GT_ByID_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Api key:<br />
                                            <asp:TextBox ID="txtApiKeyGT" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtGetCodiceSoggettoGT" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtGetApiKeySoggettoGT" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            IDRapporto:<br />
                                            <asp:TextBox ID="Txt_DefaultParam_GetRapportoTecnico_GT_ByID" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="FB4F4D45-A934-4C6F-8EAC-E4E47A8DB187" /><br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <h5>GetRapportoTecnico_GT_ByID</h5>
                            <h6>Ritorna un rapporto tecnico in formato json</h6>
                            <h5>Parametro in formato json:<br /><font color="green"><b><i>{'CriterAPIKey':'your Key', 'ID':'id rapporto', 'CodiceSoggetto':'cod. soggetto', 'CriterAPIKeySoggetto':'key soggetto'}</i></b></font></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlGT" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkGetGT" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Result_GetRapportoTecnico_GT_ByID" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_GetRapportoTecnico_GF_ByID" runat="server" Text="Get Rapporto GF" Width="150" CssClass="buttonClass" OnClick="Btn_GetRapportoTecnico_GF_ByID_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Api key:<br />
                                            <asp:TextBox ID="txtApiKeyGF" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtGetCodiceSoggettoGF" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtGetApiKeySoggettoGF" Enabled="false" Text="5frad52e5aa63368128ffdd0626d08b69414c32cc67a6b5192074af228b8b4dc" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            IDRapporto:<br />
                                            <asp:TextBox ID="Txt_DefaultParam_GetRapportoTecnico_GF_ByID" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="1B6437F6-9231-490F-9FD1-2BA98F544714" /><br />
                                             <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <h5>GetRapportoTecnico_GF_ByID</h5>
                            <h6>Ritorna un rapporto tecnico in formato json</h6>
                            <h5>Parametro in formato json:<br /><font color="green"><b><i>{'CriterAPIKey':'your Key', 'ID':'id rapporto', 'CodiceSoggetto':'cod. soggetto', 'CriterAPIKeySoggetto':'key soggetto'}</i></b></font></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlGF" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkGetGF" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Result_GetRapportoTecnico_GF_ByID" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_GetRapportoTecnico_SC_ByID" runat="server" Text="Get Rapporto SC" Width="150" CssClass="buttonClass" OnClick="Btn_GetRapportoTecnico_SC_ByID_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Api key:<br />
                                            <asp:TextBox ID="txtApiKeySC" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                           <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtGetCodiceSoggettoSC" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtGetApiKeySoggettoSC" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                             <br />
                                            IDRapporto:<br />
                                            <asp:TextBox ID="Txt_DefaultParam_GetRapportoTecnico_SC_ByID" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="1B8016A6-9CB1-4D17-87C7-EB8D2BFFFA5E" /><br />
                                             <br />
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <h5>GetRapportoTecnico_SC_ByID</h5>
                            <h6>Ritorna un rapporto tecnico in formato json</h6>
                            <h5>Parametro in formato json:<br /><font color="green"><b><i>{'CriterAPIKey':'your Key', 'ID':'id rapporto', 'CodiceSoggetto':'cod. soggetto', 'CriterAPIKeySoggetto':'key soggetto'}</i></b></font></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlSC" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkGetSC" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Result_GetRapportoTecnico_SC_ByID" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_GetRapportoTecnico_CG_ByID" runat="server" Text="Get Rapporto CG" Width="150" CssClass="buttonClass" OnClick="Btn_GetRapportoTecnico_CG_ByID_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Api key:<br />
                                            <asp:TextBox ID="txtApiKeyCG" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtGetCodiceSoggettoCG" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtGetApiKeySoggettoCG" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            IDRapporto:<br />
                                            <asp:TextBox ID="Txt_DefaultParam_GetRapportoTecnico_CG_ByID" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="5EFF51AE-E9C9-401E-8581-3089D802CF90" /><br />
                                             <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <h5>GetRapportoTecnico_CG_ByID</h5>
                            <h6>Ritorna un rapporto tecnico in formato json</h6>
                            <h5>Parametro in formato json:<br /><font color="green"><b><i>{'CriterAPIKey':'your Key', 'ID':'id rapporto', 'CodiceSoggetto':'cod. soggetto', 'CriterAPIKeySoggetto':'key soggetto'}</i></b></font></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlCG" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkGetCG" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Result_GetRapportoTecnico_CG_ByID" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>

    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" ClientInstanceName="roundPanel"
        EnableAnimation="true" ShowCollapseButton="true" ShowHeader="true" AllowCollapsingByHeaderClick="true"
        HeaderText="Metodi VALIDATE" runat="server" Width="870">
        <PanelCollection>
            <dx:PanelContent>
                <asp:Table Width="870" ID="tblValidate" CssClass="TableClass" runat="server">
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento1">
                                Test Chiamata Api
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento1" Width="200">
                                Descrizione Api
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento1">
                                Upload Json
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_ValidateLibrettoImpianto_ByCodice" runat="server" Text="Validate Libretto" Width="150" CssClass="buttonClass"
                                                OnClick="Btn_ValidateLibrettoImpianto_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato validazione:<br />
                                            <asp:Label runat="server" ID="lblLibrettoValidateResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtValidateApiKeyLibretto" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>ValidationLibrettoImpianto</h5>
                            <h6>Ritorna l'esito di validazione del json</h6>
                            <h5>Parametro in formato json: <b><i>{'CriterAPIKey':'your Key', '<asp:HyperLink runat="server" ID="lnkLibretto" Target="_blank" Text="libretto" />'</i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlValidateLibretto" /></i></b></h5>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Validate_LibrettoImpianto" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_ValidateRapportoGT" runat="server" Text="Validate Rapporto GT" Width="150" CssClass="buttonClass" OnClick="Btn_ValidateRapportoGT_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato validazione:<br />
                                            <asp:Label runat="server" ID="lblRapportoGTValidateResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtValidateApiKeyRapportoGT" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>ValidationRapportoControlloTecnico_GT</h5>
                            <h6>Ritorna l'esito di validazione del json</h6>
                            <h5>Parametro in formato json: <b><i>{'CriterAPIKey':'your Key', '<asp:HyperLink runat="server" ID="lnkGT" Target="_blank" Text="Rapporto GT" />'}</i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlValidateRapportoGT" /></i></b></h5>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Validate_RapportoGT" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_ValidateRapportoGF" runat="server" Text="Validate Rapporto GF" Width="150" CssClass="buttonClass" OnClick="Btn_ValidateRapportoGF_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato validazione:<br />
                                            <asp:Label runat="server" ID="lblRapportoGFValidateResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtValidateApiKeyRapportoGF" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>ValidationRapportoControlloTecnico_GF</h5>
                            <h6>Ritorna l'esito di validazione del json</h6>
                            <h5>Parametro in formato json: <b><i>{'CriterAPIKey':'your Key', '<asp:HyperLink runat="server" ID="lnkGF" Target="_blank" Text="Rapporto GF" />'}</i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlValidateRapportoGF" /></i></b></h5>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Validate_RapportoGF" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_ValidateRapportoSC" runat="server" Text="Validate Rapporto SC" Width="150" CssClass="buttonClass" OnClick="Btn_ValidateRapportoSC_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato validazione:<br />
                                            <asp:Label runat="server" ID="lblRapportoSCValidateResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtValidateApiKeyRapportoSC" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>ValidationRapportoControlloTecnico_SC</h5>
                            <h6>Ritorna l'esito di validazione del json</h6>
                            <h5>Parametro in formato json: <b><i>{'CriterAPIKey':'your Key', '<asp:HyperLink runat="server" ID="lnkSC" Target="_blank" Text="Rapporto SC" />'}</i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlValidateRapportoSC" /></i></b></h5>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Validate_RapportoSC" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_ValidateRapportoCG" runat="server" Text="Validate Rapporto CG" Width="150" CssClass="buttonClass" OnClick="Btn_ValidateRapportoCG_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato validazione:<br />
                                            <asp:Label runat="server" ID="lblRapportoCGValidateResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtValidateApiKeyRapportoCG" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>ValidationRapportoControlloTecnico_CG</h5>
                            <h6>Ritorna l'esito di validazione del json</h6>
                            <h5>Parametro in formato json: <b><i>{'CriterAPIKey':'your Key', '<asp:HyperLink runat="server" ID="lnkCG" Target="_blank" Text="Rapporto CG" />'}</i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlValidateRapportoCG" /></i></b></h5>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Validate_RapportoCG" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>

    <dx:ASPxRoundPanel ID="ASPxRoundPanel3" ClientInstanceName="roundPanel"
        EnableAnimation="true" ShowCollapseButton="true" ShowHeader="true" AllowCollapsingByHeaderClick="true"
        HeaderText="Metodi UPLOAD" runat="server" Width="870">
        <PanelCollection>
            <dx:PanelContent>
                <asp:Table Width="870" ID="tblUpload" CssClass="TableClass" runat="server">
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento1">
                                Test Chiamata Api
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento1" Width="200">
                                Descrizione Api
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento1">
                                Upload Json
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_UploadLibrettoImpianto" runat="server" Text="Load Libretto" Width="150" CssClass="buttonClass" OnClick="Btn_UploadLibrettoImpianto_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato load:<br />
                                            <asp:Label runat="server" ID="lblLibrettoUploadResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtUploadApiKeyLibretto" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtUploadCodiceSoggettoLibretto" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtUploadApiKeySoggettoLibretto" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>LoadLibrettoImpianto</h5> 
                            <h6>Importa un libretto di impianto in formato json</h6>
                            <h5>Parametro: <b><i><asp:HyperLink runat="server" ID="lnkPomJ_Lib" Target="_blank" Text="POMJ_LibrettoImpianto" /></i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlUploadLibretto" /></i></b>
                            </h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkEsempioLibretto" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Upload_LibrettoImpianto" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_UploadRapportoGT" runat="server" Text="Load Rapporto GT" Width="150" CssClass="buttonClass" OnClick="Btn_UploadRapportoGT_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato load:<br />
                                            <asp:Label runat="server" ID="lblRapportoGTUploadResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtUploadApiKeyRapportoGT" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtUploadCodiceSoggettoRapportoGT" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtUploadApiKeySoggettoRapportoGT" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>LoadRapportoControlloTecnico_GT</h5>
                            <h6>Importa un rapporto di controllo GT in formato json</h6>
                            <h5>Parametro: <b><i><asp:HyperLink runat="server" ID="lnkPomJ_RappGT" Target="_blank" Text="POMJ_RapportoControlloTecnico_GT" /></i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlUploadRapportoGT" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkEsempioRappGT" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Upload_RapportoGT" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_UploadRapportoGF" runat="server" Text="Load Rapporto GF" Width="150" CssClass="buttonClass" OnClick="Btn_UploadRapportoGF_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato load:<br />
                                            <asp:Label runat="server" ID="lblRapportoGFUploadResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtUploadApiKeyRapportoGF" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtUploadCodiceSoggettoRapportoGF" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtUploadApiKeySoggettoRapportoGF" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>LoadRapportoControlloTecnico_GF</h5>
                            <h6>Importa un rapporto di controllo GF in formato json</h6>
                            <h5>Parametro: <b><i><asp:HyperLink runat="server" ID="lnkPomJ_RappGF" Target="_blank" Text="POMJ_RapportoControlloTecnico_GF" /></i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlUploadRapportoGF" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkEsempioRappGF" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Upload_RapportoGF" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_UploadRapportoSC" runat="server" Text="Load Rapporto SC" Width="150" CssClass="buttonClass" OnClick="Btn_UploadRapportoSC_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato load:<br />
                                            <asp:Label runat="server" ID="lblRapportoSCUploadResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtUploadApiKeyRapportoSC" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                            <br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtUploadCodiceSoggettoRapportoSC" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtUploadApiKeySoggettoRapportoSC" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>LoadRapportoControlloTecnico_SC</h5>
                            <h6>Importa un rapporto di controllo SC in formato json</h6>
                            <h5>Parametro: <b><i><asp:HyperLink runat="server" ID="lnkPomJ_RappSC" Target="_blank" Text="POMJ_RapportoControlloTecnico_SC" /></i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlUploadRapportoSC" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkEsempioRappSC" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Upload_RapportoSC" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="riempimento">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Btn_UploadRapportoCG" runat="server" Text="Load Rapporto CG" Width="150" CssClass="buttonClass" OnClick="Btn_UploadRapportoCG_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Risultato load:<br />
                                            <asp:Label runat="server" ID="lblRapportoCGUploadResult" ForeColor="Green" Font-Bold="true" />
                                            <br />
                                            Api key:<br />
                                            <asp:TextBox ID="txtUploadApiKeyRapportoCG" runat="server"
                                                CssClass="txtClass_o" Width="170px" Text="" /><br />
                                            Codice soggetto:<br />
                                            <asp:TextBox ID="txtUploadCodiceSoggettoRapportoCG" MaxLength="11" runat="server"
                                                CssClass="txtClass_o" Width="170px" /><br />
                                            Api key soggetto:<br />
                                            <asp:TextBox ID="txtUploadApiKeySoggettoRapportoCG" runat="server"
                                                CssClass="txtClass_o" Width="170px" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="riempimento" Width="200">
                            <h5>LoadRapportoControlloTecnico_CG</h5>
                            <h6>Importa un rapporto di controllo CG in formato json</h6>
                            <h5>Parametro: <b><i><asp:HyperLink runat="server" ID="lnkPomJ_RappCG" Target="_blank" Text="POMJ_RapportoControlloTecnico_CG" /></i></b></h5>
                            <h5>Metodo: <b><i>Post</i></b></h5>
                            <h5>Url: <b><i>
                                <asp:Label runat="server" ID="lblUrlUploadRapportoCG" /></i></b></h5>
                            <br />
                            <asp:HyperLink runat="server" Target="_blank" ID="lnkEsempioRappCG" Text="Esempio chiamata Postman"></asp:HyperLink>
                        </asp:TableCell>

                        <asp:TableCell CssClass="riempimento">
                            <asp:TextBox ID="Txt_Upload_RapportoCG" runat="server" CssClass="txtClass_o" TextMode="MultiLine" Width="200px" Height="150px" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
</asp:Content>