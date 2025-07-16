<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_QuestionarioSearch.aspx.cs" Inherits="COM_QuestionarioSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA LIBRETTI DI IMPIANTO SU CUI EFFETTUARE IL QUESTIONARIO
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_QuestionarioSearch_rblStatoQuestionario" AssociatedControlID="rblStatoQuestionario" Text="Stato Questionario" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoQuestionario" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA LIBRETTI DI IMPIANTO" OnClick="btnRicerca_Click"
                            CssClass="buttonClass" Width="300px" TabIndex="1" />
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
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="10" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDRapportoControlloTecnico"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDRapportoControlloTecnico" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoQuestionario" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteQuestionario" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Rapporti di Controllo Tecnico">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfoRapporti" Width="800" runat="server">
                                            <asp:TableRow Width="500">
                                                <asp:TableCell ColumnSpan="6">
                                                     <b>Questionario:&nbsp;</b><%#Eval("IDQuestionario") %><br />
                                                     <b>Stato Questionario:&nbsp;</b><%#Eval("StatoQuestionario") %>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Responsabile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="230">
                                                    <asp:Label ID="lblResponsabile" runat="server" /><br />
                                                    <asp:Label ID="lblResponsabileIndirizzo" runat="server" />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Impresa Manutentrice:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                      <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("SoggettoAzienda") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Data&nbsp;controllo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblDataControllo" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataControllo")) %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgEffettuaQuestionario" />
                                        <%--<a href="javascript:void(0);" onclick="OpenPopupWindows(this, '<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") %>')">
                                            Effettua Questionario
                                        </a>--%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NomeResponsabile" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CognomeResponsabile" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="RagioneSocialeResponsabile" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDQuestionario" Visible="false" ReadOnly="True" />

                                <asp:BoundColumn DataField="IndirizzoResponsabile" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CivicoResponsabile" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="ComuneResponsabile" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                        
                        <dx:ASPxPopupControl ID="receptioncleModelePopUp" runat="server" CloseAction="CloseButton" 
                            Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            ClientInstanceName="receptioncleModelePopUp" HeaderText="Questionario"
                            AllowDragging="True" PopupAnimationType="Fade" Width="950px" Height="700px" AllowResize="True">
                        </dx:ASPxPopupControl>
                        <script type="text/javascript">
                            function OpenPopupWindows(element, key)
                            {
                                var url = 'COM_Questionario.aspx?IDRapportoControlloTecnico=' + key;
                                receptioncleModelePopUp.SetContentUrl(url)
                                receptioncleModelePopUp.Show();
                            }
                        </script>

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