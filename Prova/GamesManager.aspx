<%@ Page Title="Gestione videogiochi" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GamesManager.aspx.cs" Inherits="GamesManager" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <main id="Games">

        <pre>&nbsp;</pre>

        <asp:SqlDataSource ID="Edizioni" runat="server" ConnectionString="<%$ ConnectionStrings:Games %>" ProviderName="<%$ ConnectionStrings:Games.ProviderName %>" SelectCommand="SELECT e.IDEdizione, t.Titolo, e.SiglaConsole FROM [Edizioni] e INNER JOIN [Titoli] t ON e.IDTitolo = t.IDTitolo ORDER BY t.Titolo"></asp:SqlDataSource>
        <asp:SqlDataSource ID="DaCompletare" runat="server" ConnectionString="<%$ ConnectionStrings:Games %>" ProviderName="<%$ ConnectionStrings:Games.ProviderName %>" SelectCommand="SELECT e.IDEdizione, t.Titolo, e.SiglaConsole FROM [Edizioni] e INNER JOIN [Titoli] t ON e.IDTitolo = t.IDTitolo WHERE e.IDEdizione NOT IN (SELECT c.IDEdizione FROM Completamenti c) ORDER BY [t.Titolo]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="Completati" runat="server" ConnectionString="<%$ ConnectionStrings:Games %>" ProviderName="<%$ ConnectionStrings:Games.ProviderName %>" SelectCommand="SELECT e.IDEdizione, t.Titolo, e.SiglaConsole FROM [Edizioni] e INNER JOIN [Titoli] t ON e.IDTitolo = t.IDTitolo WHERE e.IDEdizione IN (SELECT c.IDEdizione FROM Completamenti c) ORDER BY [t.Titolo]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="AnniCompletati" runat="server" ConnectionString="<%$ ConnectionStrings:Games %>" ProviderName="<%$ ConnectionStrings:Games.ProviderName %>" SelectCommand="SELECT DISTINCT YEAR(c.Data) AS Anno FROM Completamenti c"></asp:SqlDataSource>

        <asp:SqlDataSource ID="Completamenti"
            runat="server"
            ConnectionString="<%$ ConnectionStrings:Games %>"
            ProviderName="<%$ ConnectionStrings:Games.ProviderName %>"
            SelectCommand="SELECT [Titolo], [Nome], [Data], [Ora], [GiornoDellaSettimana], [Finale], [Cento per cento] AS Cento_per_cento, [Note], [Codice] FROM [Visualizzazione completa]"></asp:SqlDataSource>

        <asp:SqlDataSource ID="Console"
            runat="server"
            ConnectionString="<%$ ConnectionStrings:Games %>"
            ProviderName="<%$ ConnectionStrings:Games.ProviderName %>"
            SelectCommand="SELECT [Sigla], [Nome] FROM [Console] ORDER BY [Nome]"></asp:SqlDataSource>

        <h1 class="text-center">
            <b>I tuoi videogiochi
            </b>
        </h1>

        <hr />

        <div class="container">
            <asp:Label
                runat="server"
                CssClass="row mx-2 primary-text"
                Font-Size="Medium"
                Font-Bold="true"
                Text="Data di completamento"></asp:Label>
            <div class="row justify-content-around">

                <div class="col-6 text-center">
                    <asp:Label
                        runat="server"
                        CssClass="form-label"
                        AssociatedControlID="dateFrom"
                        Font-Size="Small"
                        Font-Bold="true"
                        Text="Data di inizio"></asp:Label>

                </div>

                <div class="col-6 text-center">
                    <asp:Label
                        runat="server"
                        AssociatedControlID="dateTo"
                        CssClass="form-label"
                        Font-Size="Small"
                        Font-Bold="true"
                        Text="Data di fine"></asp:Label>

                </div>
            </div>

            <div class="row justify-content-around border-bottom py-1">
                <div class="col-6 justify-content-center">
                    <asp:TextBox ID="dateFrom"
                        runat="server"
                        TextMode="DateTimeLocal"
                        CssClass="custom-control mx-auto"></asp:TextBox>
                </div>
                <div class="col-6 text-center">
                    <asp:TextBox ID="dateTo"
                        runat="server" 
                        TextMode="DateTimeLocal"
                        CssClass="custom-control mx-auto"></asp:TextBox>
                </div>
            </div>


            <div class="row text-center">
                <div class="col-4">
                    <asp:Label
                        ID="ReleaseYearLabel"
                        runat="server"
                        Font-Size="Small"
                        Font-Bold="true">Anno di pubblicazione: </asp:Label>
                </div>
                <div class="col-6">
                    <asp:Label
                        ID="ConsoleSelectionLabel"
                        runat="server"
                        AssociatedControlID="ConsoleSelection"
                        Font-Size="Small"
                        Font-Bold="true"
                        Text="Console di gioco"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:Label
                        ID="completionCheckboxLabel"
                        runat="server"
                        AssociatedControlID="completionCheckBox"
                        Font-Size="Small"
                        Font-Bold="true"
                        Text="Completato al 100%"></asp:Label>
                </div>
            </div>

            <div class="row text-center">
                <div class="col-2">
                    <asp:Label
                        runat="server"
                        AssociatedControlID="ReleaseYearFrom"
                        Font-Size="Small"
                        Text="Da"></asp:Label>
                    <asp:DropDownList
                        ID="ReleaseYearFrom"
                        runat="server" />
                </div>
                <div class="col-2">
                    <asp:Label
                        runat="server"
                        Font-Size="Small"
                        AssociatedControlID="ReleaseYearTo"
                        Text="A"></asp:Label>
                    <asp:DropDownList
                        ID="ReleaseYearTo"
                        runat="server" />
                </div>
                <asp:DropDownList
                    ID="ConsoleSelection"
                    runat="server"
                    AppendDataBoundItems="true"
                    DataSourceID="Console"
                    DataValueField="Sigla"
                    DataTextField="Nome"
                    name="Console di gioco"
                    CssClass="col-6"
                    size="1"
                    TabIndex="1">
                    <asp:ListItem Value="%%" Text="--Tutte le console--"></asp:ListItem>
                </asp:DropDownList>
                <div class="col-2">

                    <asp:CheckBox
                        runat="server"
                        ID="completionCheckBox"></asp:CheckBox>

                </div>
            </div>

        <hr />


            <div class="row">
                <div class="col-6 mx-1 text-center">
                    <asp:Label
                        ID="TitleLabel"
                        runat="server"
                        AssociatedControlID="Titolo"
                        Font-Size="Small"
                        Font-Bold="true"
                        Text="Titolo"></asp:Label>
                </div>
                <div class="col-3 me-1 text-center">
                    <asp:Label ID="OrderCriteriaLabel"
                        runat="server"
                        AssociatedControlID="OrderCriteriaDDL"
                        Font-Size="Small"
                        Font-Bold="true"
                        Text="Ordinare per"></asp:Label>
                </div>
            </div>
            <div class="row">
                <asp:TextBox
                    ID="Titolo"
                    CssClass="col-6 mx-1"
                    placeholder="Titolo"
                    runat="server" />

                <asp:DropDownList
                    runat="server"
                    ID="OrderCriteriaDDL"
                    CssClass="col-3 me-1">
                    <asp:ListItem Selected="True" Value="Data" Text="Data di completamento"></asp:ListItem>
                    <asp:ListItem Selected="False" Value="Titolo" Text="Titolo"></asp:ListItem>
                    <asp:ListItem Selected="False" Value="Console" Text="Console"></asp:ListItem>
                </asp:DropDownList>

                <asp:Button
                    runat="server"
                    ID="QueryButton"
                    Text="Inizio Ricerca"
                    CssClass="btn btn-main col-2 mx-2"
                    OnClick="QueryButton_Click"
                    AutoPostBack="true" />
            </div>

            <div class="row">

                <asp:RadioButtonList
                    ID="titleSearchRadioButtons"
                    runat="server"
                    Font-Size="Medium"
                    CssClass="col-6 mx-1"
                    RepeatLayout="Flow"
                    RepeatDirection="Horizontal"
                    CellSpacing="10">
                    <asp:ListItem Selected="True" Text="Titolo simile" Value="Simile"></asp:ListItem>
                    <asp:ListItem Selected="False" Text="Iniziali identiche" Value="Iniziali"></asp:ListItem>
                    <asp:ListItem Selected="False" Text="Titolo Identico" Value="Identico"></asp:ListItem>
                </asp:RadioButtonList>
            </div>


        <hr />

            <h1>
                <center>
                    <b>Giochi da completare
                    </b>
                </center>
            </h1>


            <div class="row text-center">

                <div class="col-4">
                    <asp:DropDownList ID="notCompletedDropDownList" runat="server"
                        AutoPostBack="true"
                        AppendDataBoundItems="true"
                        DataSourceID="Edizioni"
                        DataValueField="IDEdizione"
                        DataTextField="Titolo"
                        OnSelectedIndexChanged="CambiaGiocoDaCompletare">
                        <asp:ListItem Text="-Seleziona un gioco-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:FileUpload
                        ID="imageUpload"
                        runat="server"
                        accept="image/*"
                        onchange="caricaImmagineDiCopertina('imageUpload','gameCover')"
                        ClientIDMode="Static" />
                    <asp:Button
                        ID="uploadCoverImageButton"
                        runat="server"
                        Text="Imposta copertina"
                        OnClick="ImpostaCopertina" />
                </div>

                <div class="col-4">

                    <div id="completedGame"
                        runat="server"
                        class="card game-card">

                        <asp:Image
                            ID="completedGameImage"
                            runat="server"
                            CssClass="card-header p-0"
                            ImageUrl="~/Immagini/Cover_Templates/ps3.png" />

                        <asp:Image
                            ID="gameCover"
                            ClientIDMode="Static"
                            runat="server"
                            Style="max-height: 40vh;"
                            CssClass="card-body p-0" />

                    </div>

                </div>

                <div class="col-4">

                    <div class="card h-100" style="border: 1px solid black;">

                        <div class="card-header bg-white">
                            <div class="row">
                                <asp:TextBox ID="completionDateTextbox" runat="server" type="date" Font-Bold="true" BorderStyle="None" BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-4">
                                    <asp:TextBox ID="completionHourTextbox" runat="server" type="number" min="0" max="23" class="form-control" placeholder="Ora"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="completionMinuteTextbox" runat="server" type="number" min="0" max="59" class="form-control" placeholder="Minuti"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="completionSecondTextbox" runat="server" type="number" min="0" max="59" class="form-control" placeholder="Secondi"></asp:TextBox>
                                </div>

                            </div>

                            <br />

                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button ID="CurrentDateButton" runat="server" Text="Data corrente" OnClick="ImpostaDataCorrente" />
                                </div>
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-12 w-100">
                                    <asp:TextBox ID="endingTextbox" runat="server" CssClass="form-control w-100" placeholder="Finale"></asp:TextBox>
                                </div>
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-12 w-100">
                                    <asp:TextBox ID="notesTextbox" runat="server" CssClass="form-control w-100" placeholder="Note"></asp:TextBox>
                                </div>
                            </div>

                            <br />

                            <div class="row text-center">
                                <div class="col-6">
                                    <asp:Button ID="InsertButton" runat="server" Text="Inserisci" OnClick="InserisciCompletamento" CssClass="w-100" />
                                </div>
                                <div class="col-6">
                                    <asp:Button ID="RemoveButton" runat="server" Text="Rimuovi" CssClass="w-100" />
                                </div>
                            </div>
                        </div>

                        <div class="card-footer">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="completeGameLabel" runat="server" Text="Completamento al 100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:CheckBox ID="completeGameCheckBox" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container">
                <asp:ListView ID="lvCompletamenti" runat="server" DataSourceID="Completamenti" GroupItemCount="3">
                    <AlternatingItemTemplate>
                        <td runat="server" style="background-color: #FFFFFF; color: #284775;">Titolo:
                            <asp:Label ID="TitoloLabel" runat="server" Text='<%# Eval("Titolo") %>' />
                            <br />
                            Nome:
                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                            <br />
                            Data:
                            <asp:Label ID="DataLabel" runat="server" Text='<%# Eval("Data") %>' />
                            <br />
                            Ora:
                            <asp:Label ID="OraLabel" runat="server" Text='<%# Eval("Ora") %>' />
                            <br />
                            GiornoDellaSettimana:
                            <asp:Label ID="GiornoDellaSettimanaLabel" runat="server" Text='<%# Eval("GiornoDellaSettimana") %>' />
                            <br />
                            Finale:
                            <asp:Label ID="FinaleLabel" runat="server" Text='<%# Eval("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Eval("Cento_per_cento") %>' Enabled="false" Text="Cento_per_cento" />
                            <br />
                            Note:
                            <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                            <br />
                            Codice:
                            <asp:Label ID="CodiceLabel" runat="server" Text='<%# Eval("Codice") %>' />
                            <br />
                        </td>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <td runat="server" style="background-color: #999999;">Titolo:
                            <asp:TextBox ID="TitoloTextBox" runat="server" Text='<%# Bind("Titolo") %>' />
                            <br />
                            Nome:
                            <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Bind("Nome") %>' />
                            <br />
                            Data:
                            <asp:TextBox ID="DataTextBox" runat="server" Text='<%# Bind("Data") %>' />
                            <br />
                            Ora:
                            <asp:TextBox ID="OraTextBox" runat="server" Text='<%# Bind("Ora") %>' />
                            <br />
                            GiornoDellaSettimana:
                            <asp:TextBox ID="GiornoDellaSettimanaTextBox" runat="server" Text='<%# Bind("GiornoDellaSettimana") %>' />
                            <br />
                            Finale:
                            <asp:TextBox ID="FinaleTextBox" runat="server" Text='<%# Bind("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Bind("Cento_per_cento") %>' Text="Cento_per_cento" />
                            <br />
                            Note:
                            <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
                            <br />
                            Codice:
                            <asp:TextBox ID="CodiceTextBox" runat="server" Text='<%# Bind("Codice") %>' />
                            <br />
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Aggiorna" />
                            <br />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Annulla" />
                            <br />
                        </td>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                            <tr>
                                <td>Non è stato restituito alcun dato.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <EmptyItemTemplate>
                        <td runat="server" />
                    </EmptyItemTemplate>
                    <GroupTemplate>
                        <tr id="itemPlaceholderContainer" runat="server">
                            <td id="itemPlaceholder" runat="server"></td>
                        </tr>
                    </GroupTemplate>
                    <InsertItemTemplate>
                        <td runat="server" style="">Titolo:
                            <asp:TextBox ID="TitoloTextBox" runat="server" Text='<%# Bind("Titolo") %>' />
                            <br />
                            Nome:
                            <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Bind("Nome") %>' />
                            <br />
                            Data:
                            <asp:TextBox ID="DataTextBox" runat="server" Text='<%# Bind("Data") %>' />
                            <br />
                            Ora:
                            <asp:TextBox ID="OraTextBox" runat="server" Text='<%# Bind("Ora") %>' />
                            <br />
                            GiornoDellaSettimana:
                            <asp:TextBox ID="GiornoDellaSettimanaTextBox" runat="server" Text='<%# Bind("GiornoDellaSettimana") %>' />
                            <br />
                            Finale:
                            <asp:TextBox ID="FinaleTextBox" runat="server" Text='<%# Bind("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Bind("Cento_per_cento") %>' Text="Cento_per_cento" />
                            <br />
                            Note:
                            <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
                            <br />
                            Codice:
                            <asp:TextBox ID="CodiceTextBox" runat="server" Text='<%# Bind("Codice") %>' />
                            <br />
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Inserisci" />
                            <br />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancella" />
                            <br />
                        </td>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <td runat="server" style="background-color: #E0FFFF; color: #333333;">Titolo:
                            <asp:Label ID="TitoloLabel" runat="server" Text='<%# Eval("Titolo") %>' />
                            <br />
                            Nome:
                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                            <br />
                            Data:
                            <asp:Label ID="DataLabel" runat="server" Text='<%# Eval("Data") %>' />
                            <br />
                            Ora:
                            <asp:Label ID="OraLabel" runat="server" Text='<%# Eval("Ora") %>' />
                            <br />
                            GiornoDellaSettimana:
                            <asp:Label ID="GiornoDellaSettimanaLabel" runat="server" Text='<%# Eval("GiornoDellaSettimana") %>' />
                            <br />
                            Finale:
                            <asp:Label ID="FinaleLabel" runat="server" Text='<%# Eval("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Eval("Cento_per_cento") %>' Enabled="false" Text="Cento_per_cento" />
                            <br />
                            Note:
                            <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                            <br />
                            Codice:
                            <asp:Label ID="CodiceLabel" runat="server" Text='<%# Eval("Codice") %>' />
                            <br />
                        </td>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr id="groupPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <td runat="server" style="background-color: #E2DED6; font-weight: bold; color: #333333;">Titolo:
                            <asp:Label ID="TitoloLabel" runat="server" Text='<%# Eval("Titolo") %>' />
                            <br />
                            Nome:
                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                            <br />
                            Data:
                            <asp:Label ID="DataLabel" runat="server" Text='<%# Eval("Data") %>' />
                            <br />
                            Ora:
                            <asp:Label ID="OraLabel" runat="server" Text='<%# Eval("Ora") %>' />
                            <br />
                            GiornoDellaSettimana:
                            <asp:Label ID="GiornoDellaSettimanaLabel" runat="server" Text='<%# Eval("GiornoDellaSettimana") %>' />
                            <br />
                            Finale:
                            <asp:Label ID="FinaleLabel" runat="server" Text='<%# Eval("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Eval("Cento_per_cento") %>' Enabled="false" Text="Cento_per_cento" />
                            <br />
                            Note:
                            <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                            <br />
                            Codice:
                            <asp:Label ID="CodiceLabel" runat="server" Text='<%# Eval("Codice") %>' />
                            <br />
                        </td>
                    </SelectedItemTemplate>
                </asp:ListView>
            </div>

        </div>
    </main>
</asp:Content>

