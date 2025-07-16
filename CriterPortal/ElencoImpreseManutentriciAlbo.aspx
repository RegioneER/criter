<%@ Page Title="" Language="C#" MasterPageFile="~/BootstrapItalia.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ElencoImpreseManutentriciAlbo.aspx.cs" Inherits="ElencoImpreseManutentriciAlbo" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var keyValue;
        function OnMoreInfoClick(element, key)
        {
            callbackPanel.SetContentHtml("");
            popup.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }

        function validateForm() {
            var txtNomeAzienda = document.getElementById('<%= txtNomeAzienda.ClientID %>').value;
            if (txtNomeAzienda.trim() === "") {
                alert("Impresa di Manutenzione/Installazione: campo obbligatorio");
                return false; // impedisce l'invio del modulo
            }
            return true; // consente l'invio del modulo
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table ID="Elenco" runat="server" CssClass="TableClassNoBorder">
                <asp:TableRow>
                    <asp:TableCell CssClass="riempimento1" ColumnSpan="4">
                        <strong>
                            <span id="spnTitoloTestata"><big>&nbsp;&raquo;&nbsp;Elenco Imprese Manutentrici&nbsp;</big>
                            <asp:Label runat="server" ID="lblDataAggiornamento" /></span></strong>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblInfoDataGrid" runat="server">
                    Le imprese di installazione/manutenzione selezionati sono visualizzati in ordine alfabetico 
                    e partendo da una lettera selezionata ad ogni accesso in modo random dal sistema.
                    E' possibile ordinare alfabeticamente ogni colonna dell’elenco cliccando sull’intestazione della colonna stessa.
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="tblTipoRicerca" runat="server">
                    <asp:TableCell CssClass="riempimento">
                        <%--Selezionare il tipo di ricerca che si desidera effettuare--%>
                        <dx:ASPxRadioButtonList ID="rblTipoRicerca" AutoPostBack="true" Theme="iOS" CssClass=""  AccessibilityCompliant="true" OnSelectedIndexChanged="rblTipoRicerca_SelectedIndexChanged" runat="server" Width="100%">
                            <CaptionSettings Position="Top" />
                            <Items>
                                <dx:ListEditItem Text="Ricerca Imprese di Manutenzione/Installazione per Province" Selected="true" Value="0" />
                                <dx:ListEditItem Text="Ricerca Imprese di Manutenzione/Installazione per Nominativo" Value="1" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowRicercaAzienda1" Visible="false" >
                    <asp:TableCell>
                       <asp:Table ID="tblRicerca" runat="server">
                            <asp:TableRow CssClass="riempimento">
                                <asp:TableCell CssClass="riempimento1" Width="370px">
                                    <asp:Label runat="server" ID="lblInfo" Text="Impresa di Manutenzione/Installazione" Width="100%" />
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento" Width="880px">
                                    <asp:TextBox ID="txtNomeAzienda" runat="server" ValidationGroup="vgAnagrafica" Width="300px" MaxLength="200" />
                                    <asp:RequiredFieldValidator ID="rfvtxtNomeAzienda" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" EnableClientScript="true"
                                        ErrorMessage="Impresa di Manutenzione/Installazione: campo obbligatorio"
                                        ControlToValidate="txtNomeAzienda"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowRicercaAzienda2" Visible="false">
                    <asp:TableCell ColumnSpan="4" ID="cellCustomAlign">
                        <asp:Button ID="btnRicerca" runat="server" OnClick="btnRicerca_Click" OnClientClick="return validateForm();" ValidationGroup="vgAnagrafica" Text="Ricerca Impresa Installazione/Manutenzione" Visible="false" CssClass="buttonClass" Width="370px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="TableClassNoBorder" runat="server" ID="rowRicercaAzienda3" Visible="false">
                    <asp:TableCell HorizontalAlign="Center">
                        <br />
                        <asp:ValidationSummary ID="vsAnagrafica" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="false"
                            ShowSummary="true" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                    </asp:TableCell>
                </asp:TableRow>
                <%--<asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblInfoFilterProvince" runat="server">
                  Clicca su una provincia per visualizzare Elenco di Manutentori per provincia
                  di Operatività Tecnica     
                            <br />
                        </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>--%>
            </asp:Table>
            <asp:Table ID="tblFilterProvince" runat="server" CssClass="TableClassNoBorder">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageMap ID="mappaEmilia" ImageUrl="~/images/ImageMapER/ER.png" runat="server" OnClick="mappaEmilia_Click" HotSpotMode="PostBack" Visible="true">
                            <asp:PolygonHotSpot PostBackValue="8" AlternateText="Bologna" Coordinates="339,81,341,80,345,81,345,84,343,89,343,93,343,96,345,98,350,97,352,93,354,90,356,89,357,86,358,85,361,84,367,83,368,83,373,85,376,86,381,88,390,93,399,94,403,97,404,97,409,101,410,102,413,107,414,110,417,113,423,115,426,114,427,115,430,116,433,122,434,125,434,128,434,132,437,136,436,139,434,143,433,144,431,146,431,149,431,154,433,157,436,163,437,167,439,171,436,178,435,181,434,182,432,186,431,187,427,191,422,194,420,195,417,196,411,197,408,199,407,201,404,204,401,208,401,209,401,213,398,215,397,216,395,221,395,222,393,225,393,227,391,230,386,231,382,229,379,225,377,220,370,215,363,215,361,217,358,219,353,222,351,225,346,227,342,230,339,230,340,234,341,236,343,237,344,239,343,242,340,244,337,245,333,243,329,243,322,243,320,244,316,245,308,245,306,246,306,246,304,246,302,243,300,240,298,240,295,243,293,246,285,251,282,249,279,244,274,243,273,241,270,238,269,236,269,232,271,231,272,227,271,223,274,219,276,217,278,214,280,214,281,217,284,219,289,218,289,216,294,213,296,210,297,206,298,202,296,199,295,198,305,195,305,194,308,186,307,182,307,179,307,176,309,172,310,169,309,165,303,163,303,160,306,159,312,154,313,154,313,152,313,150,312,146,315,144,317,144,320,140,324,136,330,132,329,126,328,123,326,119,317,114,317,110,322,108,324,103,325,100,325,94,325,87,328,81,331,80,338,79,343,79" />
                            <asp:PolygonHotSpot PostBackValue="23" AlternateText="Forlì-Cesena" Coordinates="533, 210, 538, 213, 541, 219, 544, 224, 546, 228, 544, 234, 540, 238, 540, 243, 537, 246, 537, 250, 534, 255, 534, 259, 530, 262, 528, 266, 526, 268, 521, 272, 519, 274, 512, 278, 511, 279, 503, 280, 500, 280, 494, 280, 490, 284, 493, 290, 492, 294, 489, 303, 488, 307, 488, 312, 485, 315, 481, 318, 474, 318, 461, 314, 454, 309, 447, 305, 436, 303, 434, 301, 421, 294, 419, 288, 415, 282, 413, 274, 411, 269, 408, 264, 405, 255, 413, 250, 418, 243, 422, 237, 422, 235, 423, 229, 423, 221, 425, 217, 427, 217, 435, 218, 438, 218, 449, 220, 458, 215, 455, 211, 455, 209, 459, 206, 463, 203, 466, 199, 469, 192, 471, 189, 476, 191, 483, 197, 488, 201, 492, 205, 499, 206, 499, 203, 502, 206, 508, 211, 514, 209, 518, 212, 523, 214, 534, 215, 527, 214, 531, 211, 532, 211" />
                            <asp:PolygonHotSpot PostBackValue="53" AlternateText="Rimini" Coordinates="545, 224, 543, 232, 541, 239, 541, 245, 538, 251, 532, 264, 532, 267, 530, 272, 528, 273, 527, 276, 531, 279, 535, 279, 540, 276, 548, 273, 549, 268, 552, 270, 552, 274, 549, 276, 546, 282, 548, 286, 552, 287, 553, 288, 556, 292, 556, 294, 557, 298, 564, 296, 564, 294, 569, 303, 579, 305, 584, 303, 587, 297, 591, 293, 593, 291, 594, 287, 595, 284, 594, 279, 596, 275, 597, 271, 595, 267, 588, 267, 580, 261, 578, 258, 575, 255, 572, 252, 566, 244, 563, 240, 556, 239, 554, 237, 548, 229, 547, 225, 546, 225" />
                            <asp:PolygonHotSpot PostBackValue="37" AlternateText="Modena" Coordinates="267, 241, 267, 239, 267, 234, 268, 226, 269, 222, 270, 220, 273, 215, 274, 214, 276, 214, 283, 212, 284, 213, 285, 213, 289, 212, 292, 209, 293, 208, 295, 201, 297, 200, 299, 199, 302, 195, 306, 192, 306, 187, 306, 183, 306, 177, 305, 172, 305, 166, 303, 162, 303, 157, 307, 153, 310, 143, 316, 141, 317, 138, 324, 136, 326, 135, 328, 130, 328, 122, 321, 117, 317, 113, 315, 110, 316, 109, 319, 103, 320, 101, 323, 95, 323, 88, 323, 83, 329, 80, 330, 77, 334, 77, 337, 76, 341, 77, 343, 77, 347, 74, 352, 73, 355, 71, 356, 70, 358, 70, 362, 66, 356, 62, 351, 58, 345, 52, 342, 50, 344, 46, 344, 43, 343, 42, 340, 42, 336, 42, 333, 44, 330, 44, 324, 42, 315, 38, 309, 39, 306, 39, 298, 43, 296, 43, 291, 46, 287, 49, 285, 50, 282, 54, 283, 59, 283, 60, 280, 63, 275, 66, 274, 72, 273, 77, 276, 83, 275, 86, 273, 90, 272, 93, 273, 97, 273, 100, 273, 106, 271, 110, 269, 114, 264, 122, 264, 123, 264, 128, 265, 133, 264, 139, 260, 144, 257, 148, 253, 151, 250, 153, 247, 157, 246, 159, 244, 162, 243, 164, 241, 171, 239, 176, 239, 178, 237, 181, 233, 181, 227, 182, 224, 184, 223, 187, 222, 190, 221, 194, 221, 198, 221, 200, 217, 207, 215, 210, 212, 216, 211, 222, 213, 224, 214, 226, 217, 230, 218, 235, 220, 237, 225, 240, 228, 241, 232, 241, 237, 242, 239, 237, 239, 235, 247, 230, 249, 230, 256, 233, 257, 233, 258, 235, 260, 238, 262, 240, 264, 241" />
                            <asp:PolygonHotSpot PostBackValue="51" AlternateText="Reggio Emilia" Coordinates="220, 50, 221, 51, 231, 52, 235, 49, 237, 48, 240, 43, 244, 39, 246, 34, 252, 32, 254, 32, 260, 35, 262, 39, 263, 41, 265, 44, 272, 44, 273, 45, 276, 48, 279, 50, 280, 50, 281, 53, 281, 57, 279, 60, 278, 61, 277, 63, 273, 67, 273, 69, 272, 72, 272, 75, 272, 78, 273, 83, 273, 84, 272, 88, 272, 94, 272, 99, 272, 100, 273, 104, 273, 106, 269, 110, 269, 115, 268, 118, 267, 123, 264, 125, 264, 127, 265, 131, 265, 135, 265, 137, 264, 142, 261, 146, 260, 148, 258, 151, 255, 152, 251, 155, 248, 158, 246, 160, 244, 164, 242, 166, 241, 169, 239, 171, 239, 173, 239, 174, 240, 175, 238, 179, 236, 180, 233, 180, 227, 181, 224, 182, 223, 184, 222, 190, 220, 194, 220, 201, 219, 203, 218, 204, 217, 206, 214, 209, 210, 212, 206, 213, 204, 213, 202, 211, 198, 209, 196, 204, 194, 202, 189, 201, 185, 202, 181, 202, 175, 204, 173, 200, 171, 199, 167, 193, 163, 190, 160, 188, 158, 189, 157, 186, 157, 183, 159, 180, 165, 176, 170, 175, 171, 174, 173, 169, 178, 166, 180, 161, 185, 158, 189, 157, 190, 153, 193, 150, 195, 144, 197, 143, 200, 140, 202, 134, 203, 134, 203, 129, 203, 124, 206, 124, 207, 118, 207, 110, 208, 109, 211, 104, 212, 102, 212, 98, 209, 93, 208, 87, 207, 82, 207, 80, 209, 76, 211, 72, 213, 68, 213, 64, 215, 62, 215, 59, 214, 56, 218, 55, 219, 54, 220, 53" />
                            <asp:PolygonHotSpot PostBackValue="46" AlternateText="Parma" Coordinates="151, 30, 156, 29, 158, 28, 160, 27, 166, 26, 169, 26, 172, 27, 181, 30, 183, 33, 185, 35, 190, 37, 191, 38, 193, 38, 196, 39, 201, 39, 202, 38, 205, 39, 207, 42, 209, 46, 210, 48, 213, 51, 215, 54, 215, 55, 213, 59, 212, 62, 211, 66, 210, 67, 208, 69, 208, 74, 206, 77, 206, 81, 206, 83, 207, 90, 208, 91, 209, 96, 209, 97, 209, 103, 209, 105, 207, 108, 207, 112, 206, 118, 206, 121, 204, 126, 203, 128, 202, 132, 202, 134, 200, 138, 195, 142, 192, 148, 189, 152, 188, 153, 188, 157, 185, 159, 182, 162, 180, 164, 179, 166, 175, 168, 172, 170, 170, 172, 167, 175, 161, 178, 159, 180, 158, 181, 157, 184, 154, 186, 152, 186, 148, 183, 144, 181, 140, 179, 136, 176, 136, 173, 138, 168, 137, 165, 135, 163, 130, 159, 120, 155, 112, 157, 108, 160, 108, 162, 102, 168, 100, 172, 97, 174, 92, 178, 85, 179, 84, 180, 82, 180, 77, 173, 76, 171, 73, 169, 69, 168, 62, 168, 56, 168, 53, 169, 42, 170, 41, 168, 41, 161, 47, 156, 47, 154, 49, 152, 53, 140, 56, 137, 60, 133, 63, 128, 65, 127, 70, 124, 75, 121, 76, 118, 77, 115, 78, 115, 80, 110, 81, 110, 89, 110, 98, 111, 99, 110, 98, 107, 100, 106, 101, 105, 102, 103, 107, 101, 108, 98, 110, 96, 111, 93, 115, 93, 116, 93, 117, 92, 119, 91, 120, 85, 123, 84, 123, 80, 124, 79, 129, 76, 133, 74, 137, 71, 140, 67, 141, 63, 141, 56, 141, 51, 142, 47, 143, 42, 145, 41, 146, 38, 148, 32, 152, 31, 151, 27" />
                            <asp:PolygonHotSpot PostBackValue="50" AlternateText="Ravenna" Coordinates="515, 119, 517, 126, 517, 130, 516, 135, 514, 138, 515, 146, 517, 153, 518, 159, 519, 165, 523, 171, 523, 175, 524, 182, 526, 187, 529, 195, 530, 200, 530, 202, 531, 206, 529, 208, 524, 210, 520, 210, 513, 209, 511, 209, 506, 210, 501, 207, 496, 204, 491, 201, 486, 197, 479, 192, 478, 190, 475, 190, 470, 191, 468, 192, 467, 194, 466, 195, 463, 201, 460, 204, 458, 206, 452, 210, 451, 213, 449, 217, 443, 220, 441, 219, 434, 216, 428, 218, 422, 218, 423, 220, 423, 224, 423, 227, 417, 232, 414, 236, 411, 237, 401, 238, 398, 236, 397, 234, 397, 226, 393, 224, 388, 226, 395, 220, 397, 213, 401, 211, 403, 209, 408, 202, 410, 198, 415, 196, 417, 196, 423, 195, 424, 194, 429, 190, 432, 185, 438, 180, 438, 171, 438, 166, 434, 162, 434, 154, 432, 150, 432, 147, 435, 140, 436, 133, 440, 134, 448, 130, 453, 129, 463, 124, 467, 126, 472, 130, 478, 134, 482, 133, 486, 136, 494, 138, 497, 137, 501, 134, 503, 131, 504, 129, 514, 127, 511, 127, 512, 123, 515, 120" />
                            <asp:PolygonHotSpot PostBackValue="40" AlternateText="Piacenza" Coordinates="152, 27, 151, 30, 147, 32, 146, 33, 145, 35, 143, 37, 142, 40, 141, 41, 141, 43, 140, 45, 140, 50, 140, 53, 140, 57, 140, 60, 139, 63, 138, 66, 137, 70, 135, 72, 131, 74, 126, 76, 124, 79, 122, 80, 118, 83, 117, 84, 116, 87, 115, 88, 114, 90, 112, 92, 110, 92, 106, 92, 105, 94, 103, 97, 102, 99, 98, 103, 97, 105, 96, 109, 94, 110, 91, 108, 87, 108, 85, 108, 83, 108, 78, 110, 77, 111, 76, 116, 76, 118, 70, 122, 66, 124, 65, 125, 63, 128, 61, 130, 58, 132, 56, 133, 54, 138, 52, 139, 49, 137, 50, 137, 44, 138, 42, 135, 40, 131, 39, 133, 37, 134, 32, 134, 24, 130, 23, 130, 18, 129, 16, 128, 11, 128, 7, 126, 5, 126, 3, 122, 3, 120, 2, 117, 2, 112, 4, 111, 5, 108, 9, 108, 9, 103, 13, 103, 14, 105, 20, 106, 21, 106, 23, 102, 21, 99, 21, 97, 19, 88, 19, 85, 22, 81, 27, 75, 27, 70, 24, 63, 20, 61, 19, 57, 24, 46, 25, 37, 30, 32, 33, 20, 34, 18, 38, 15, 45, 12, 52, 10, 58, 13, 66, 14, 63, 8, 64, 6, 66, 6, 76, 6, 78, 4, 79, 7, 83, 13, 85, 16, 92, 17, 94, 13, 98, 10, 103, 14, 104, 15, 108, 19, 110, 14, 113, 13, 120, 14, 120, 12, 117, 4, 121, 3, 128, 4, 129, 8, 140, 3, 145, 11, 147, 14, 150, 18, 150, 22, 151, 23, 151, 24, 152, 25" />
                            <asp:PolygonHotSpot PostBackValue="20" AlternateText="Ferrara" Coordinates="347, 43, 347, 45, 346, 48, 345, 49, 349, 52, 355, 56, 361, 62, 362, 64, 363, 69, 360, 74, 357, 75, 354, 76, 348, 80, 348, 81, 346, 84, 346, 89, 346, 92, 345, 95, 345, 96, 350, 93, 353, 89, 355, 87, 362, 82, 368, 79, 382, 86, 382, 87, 387, 90, 392, 92, 401, 94, 408, 97, 411, 102, 413, 108, 417, 113, 423, 113, 428, 111, 432, 113, 432, 116, 432, 119, 433, 124, 431, 128, 430, 129, 432, 130, 436, 130, 437, 132, 445, 133, 450, 129, 456, 128, 457, 127, 464, 122, 467, 126, 471, 129, 479, 131, 481, 133, 487, 134, 490, 137, 500, 137, 500, 134, 507, 132, 511, 126, 512, 123, 514, 120, 514, 116, 515, 112, 515, 105, 514, 100, 513, 94, 513, 85, 516, 81, 516, 73, 518, 70, 521, 72, 524, 76, 529, 81, 530, 83, 536, 84, 537, 79, 537, 76, 536, 74, 531, 69, 526, 63, 524, 61, 519, 58, 520, 55, 521, 45, 519, 44, 515, 43, 509, 43, 506, 44, 502, 44, 493, 44, 490, 40, 487, 37, 480, 36, 469, 33, 463, 33, 450, 33, 442, 33, 436, 33, 431, 35, 429, 37, 419, 42, 411, 47, 409, 50, 402, 48, 399, 46, 394, 44, 391, 43, 386, 43, 384, 43, 374, 42, 371, 40, 365, 38, 362, 37, 355, 35, 353, 35" />
                        </asp:ImageMap>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dx:ASPxListBox ID="lbProvince" runat="server" SelectionMode="Single" Theme="iOS" Width="250" Height="390" CssClass="TableClassNoBorder"
                            ValueField="IDProvincia" ValueType="System.String" TextField="Provincia" AutoPostBack="true"
                            OnSelectedIndexChanged="lbProvince_SelectedIndexChanged" >
                            <CaptionSettings Position="Top" />
                        </dx:ASPxListBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Table ID="tblGrid" runat="server" CssClass="TableClass" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <dx:ASPxGridView ID="DataGrid" ClientInstanceName="DataGrid" Theme="iOS" SettingsText-EmptyDataRow="NESSUNA IMPRESA DI INSTALLAZIONE/MANUTENZIONE TROVATA!" 
                            EnableCallbackAnimation="true" runat="server" KeyFieldName="IDSoggetto" Width="100%" OnHtmlRowCreated="DataGrid_HtmlRowCreated">
                            <SettingsPager PageSize="30" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                            <Styles AlternatingRow-BackColor="#fafafb" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="IDSoggetto" VisibleIndex="0" Visible="false" />
                                <dx:GridViewDataColumn FieldName="Impresa" VisibleIndex="1" />
                                <%--<dx:GridViewDataTextColumn Caption="Ruolo" VisibleIndex="2">
                                    <DataItemTemplate>
                                        <asp:Label runat="server" ID="lblRuoli" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>

                                <dx:GridViewDataColumn FieldName="Provincia" Caption="Provincia di Operatività" VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="StatoAccreditamento" Caption="Stato di Accreditamento" VisibleIndex="3" />
                                <dx:GridViewDataColumn VisibleIndex="5" Width="0px">
                                    <DataItemTemplate>
                                        <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                                            <center>
                                                <img alt="Dettaglio" runat="server" src="images/info.png" />
                                            </center>
                                        </a>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="TableClass">
                        <dx:ASPxPopupControl ID="popup" ClientInstanceName="popup" Theme="iOS" runat="server" AllowDragging="true"
                            PopupHorizontalAlign="OutsideLeft" HeaderText="Dettaglio Informazioni Impresa Manutenzione/Installazione">
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server" Visible="true">
                                    <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" Theme="iOS" runat="server"
                                        Width="600px" Height="370px" OnCallback="callbackPanel_Callback" EnableCallbackAnimation="true" SettingsCollapsing-ExpandEffect="Slide" RenderMode="Table" Visible="true">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <asp:Table runat="server" ID="Detail" CssClass="TableClass" HorizontalAlign="Center" Width="100%" Height="84%" Visible="true">
                                                    <asp:TableRow>
                                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="lblSoggetto" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Telefono" Text="Telefono:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="lblTelefono" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Email" Text="Email:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridAlternativeItem">
                                                            <asp:Label runat="server" ID="lblEmail" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="EmailPec" Text="Email Pec:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="lblEmailPec" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Fax" Text="Fax:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridAlternativeItem">
                                                            <asp:Label runat="server" ID="lblFax" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Citta" Text="Città:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="lblCitta" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Indirizzo" Text="Indirizzo:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridAlternativeItem">
                                                            <asp:Label runat="server" ID="lblIndirizzo" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Cap" Text="Cap:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="lblCap" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="Provincia" Text="Provincia:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridAlternativeItem">
                                                            <asp:Label runat="server" ID="lblProvincia" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="SitoWeb" Text="Sito Web:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="lblSitoWeb" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="PartitaIVA" Text="Partita IVA:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridAlternativeItem">
                                                            <asp:Label runat="server" ID="lblPartitaIVA" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow Width="30%">
                                                        <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                                                            <asp:Label runat="server" ID="AmministratoreDelegato" Text="Amministratore delegato:" />
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:Label runat="server" ID="lblAmministratoreDelegato" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                            <ClientSideEvents Shown="popup_Shown" />
                        </dx:ASPxPopupControl>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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