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

        <center>
            <h1>
                <b>I tuoi videogiochi
                </b>
            </h1>
        </center>

        <hr />

        <center>
            <h2>Informazioni memorizzate</h2>
        </center>

        <ul>
            <center>
                <li>Titolo del videogioco</li>
                <li>Data ed ora del completamento</li>
                <li>Data di pubblicazione</li>
                <li>Console su cui è stato completato il videogioco</li>
                <li>F
                    inale ottenuto al completamento</li>
                <li>Nota sul completamento totale</li>
                <li>Note sul completamento del videogioco</li>
            </center>
        </ul>

        <hr />

        <div class="container">

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
                        <td runat="server" style="background-color: #FFFFFF;color: #284775;">Titolo:
                            <asp:Label ID="TitoloLabel" runat="server" Text='<%# Eval("Titolo") %>' />
                            <br />Nome:
                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                            <br />Data:
                            <asp:Label ID="DataLabel" runat="server" Text='<%# Eval("Data") %>' />
                            <br />Ora:
                            <asp:Label ID="OraLabel" runat="server" Text='<%# Eval("Ora") %>' />
                            <br />GiornoDellaSettimana:
                            <asp:Label ID="GiornoDellaSettimanaLabel" runat="server" Text='<%# Eval("GiornoDellaSettimana") %>' />
                            <br />Finale:
                            <asp:Label ID="FinaleLabel" runat="server" Text='<%# Eval("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Eval("Cento_per_cento") %>' Enabled="false" Text="Cento_per_cento" />
                            <br />Note:
                            <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                            <br />Codice:
                            <asp:Label ID="CodiceLabel" runat="server" Text='<%# Eval("Codice") %>' />
                            <br />
                        </td>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <td runat="server" style="background-color: #999999;">Titolo:
                            <asp:TextBox ID="TitoloTextBox" runat="server" Text='<%# Bind("Titolo") %>' />
                            <br />Nome:
                            <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Bind("Nome") %>' />
                            <br />Data:
                            <asp:TextBox ID="DataTextBox" runat="server" Text='<%# Bind("Data") %>' />
                            <br />Ora:
                            <asp:TextBox ID="OraTextBox" runat="server" Text='<%# Bind("Ora") %>' />
                            <br />GiornoDellaSettimana:
                            <asp:TextBox ID="GiornoDellaSettimanaTextBox" runat="server" Text='<%# Bind("GiornoDellaSettimana") %>' />
                            <br />Finale:
                            <asp:TextBox ID="FinaleTextBox" runat="server" Text='<%# Bind("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Bind("Cento_per_cento") %>' Text="Cento_per_cento" />
                            <br />Note:
                            <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
                            <br />Codice:
                            <asp:TextBox ID="CodiceTextBox" runat="server" Text='<%# Bind("Codice") %>' />
                            <br />
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Aggiorna" />
                            <br />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Annulla" />
                            <br /></td>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
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
                            <br />Nome:
                            <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Bind("Nome") %>' />
                            <br />Data:
                            <asp:TextBox ID="DataTextBox" runat="server" Text='<%# Bind("Data") %>' />
                            <br />Ora:
                            <asp:TextBox ID="OraTextBox" runat="server" Text='<%# Bind("Ora") %>' />
                            <br />GiornoDellaSettimana:
                            <asp:TextBox ID="GiornoDellaSettimanaTextBox" runat="server" Text='<%# Bind("GiornoDellaSettimana") %>' />
                            <br />Finale:
                            <asp:TextBox ID="FinaleTextBox" runat="server" Text='<%# Bind("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Bind("Cento_per_cento") %>' Text="Cento_per_cento" />
                            <br />Note:
                            <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
                            <br />Codice:
                            <asp:TextBox ID="CodiceTextBox" runat="server" Text='<%# Bind("Codice") %>' />
                            <br />
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Inserisci" />
                            <br />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancella" />
                            <br /></td>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <td runat="server" style="background-color: #E0FFFF;color: #333333;">Titolo:
                            <asp:Label ID="TitoloLabel" runat="server" Text='<%# Eval("Titolo") %>' />
                            <br />Nome:
                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                            <br />Data:
                            <asp:Label ID="DataLabel" runat="server" Text='<%# Eval("Data") %>' />
                            <br />Ora:
                            <asp:Label ID="OraLabel" runat="server" Text='<%# Eval("Ora") %>' />
                            <br />GiornoDellaSettimana:
                            <asp:Label ID="GiornoDellaSettimanaLabel" runat="server" Text='<%# Eval("GiornoDellaSettimana") %>' />
                            <br />Finale:
                            <asp:Label ID="FinaleLabel" runat="server" Text='<%# Eval("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Eval("Cento_per_cento") %>' Enabled="false" Text="Cento_per_cento" />
                            <br />Note:
                            <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                            <br />Codice:
                            <asp:Label ID="CodiceLabel" runat="server" Text='<%# Eval("Codice") %>' />
                            <br />
                        </td>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr id="groupPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF"></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <td runat="server" style="background-color: #E2DED6;font-weight: bold;color: #333333;">Titolo:
                            <asp:Label ID="TitoloLabel" runat="server" Text='<%# Eval("Titolo") %>' />
                            <br />Nome:
                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                            <br />Data:
                            <asp:Label ID="DataLabel" runat="server" Text='<%# Eval("Data") %>' />
                            <br />Ora:
                            <asp:Label ID="OraLabel" runat="server" Text='<%# Eval("Ora") %>' />
                            <br />GiornoDellaSettimana:
                            <asp:Label ID="GiornoDellaSettimanaLabel" runat="server" Text='<%# Eval("GiornoDellaSettimana") %>' />
                            <br />Finale:
                            <asp:Label ID="FinaleLabel" runat="server" Text='<%# Eval("Finale") %>' />
                            <br />
                            <asp:CheckBox ID="Cento_per_centoCheckBox" runat="server" Checked='<%# Eval("Cento_per_cento") %>' Enabled="false" Text="Cento_per_cento" />
                            <br />Note:
                            <asp:Label ID="NoteLabel" runat="server" Text='<%# Eval("Note") %>' />
                            <br />Codice:
                            <asp:Label ID="CodiceLabel" runat="server" Text='<%# Eval("Codice") %>' />
                            <br />
                        </td>
                    </SelectedItemTemplate>
                </asp:ListView>
            </div>

        </div>

    </main>
</asp:Content>

