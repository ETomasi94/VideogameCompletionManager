<%@ Page Title="Gestione videogiochi" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GamesManager.aspx.cs" Inherits="GamesManager" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <main id="Games">

        <pre>&nbsp;</pre>

        <asp:SqlDataSource ID="Edizioni" runat="server" ConnectionString="<%$ ConnectionStrings:Games %>" ProviderName="<%$ ConnectionStrings:Games.ProviderName %>" SelectCommand="SELECT e.IDEdizione, t.Titolo, e.SiglaConsole FROM [Edizioni] e INNER JOIN [Titoli] t ON e.IDTitolo = t.IDTitolo"></asp:SqlDataSource>
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
                    <b>Giochi completati
                    </b>
                </center>
            </h1>

            <div class="row text-center">

                <div class="col-3">

                    <div class="row">
                        <asp:Label ID="YearLabel" runat="server">Anno di completamento </asp:Label>
                    </div>

                    <div class="row">
                        <div class="col-6">
                            <asp:DropDownList ID="YearFrom" runat="server" DataSourceID="AnniCompletati" DataTextField="Anno" DataValueField="Anno">
                                <asp:ListItem Selected="True" Value="ANY">Tutti</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-6">
                            <asp:DropDownList ID="YearTo" runat="server" CssClass="QueryExclusive" Visible="false" DataSourceID="AnniCompletati" DataTextField="Anno" DataValueField="Anno">
                                <asp:ListItem Selected="True" Value="ANY">Tutti</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <asp:CheckBox ID="YearInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="YearInterval_OnCheckedChange" />
                    </div>
                </div>

                <div class="col-3">
                    <div class="row">
                        <asp:Label ID="MonthLabel" runat="server">Mese di completamento </asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:DropDownList ID="MonthFrom" runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:DropDownList ID="MonthTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:CheckBox ID="MonthInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="MonthInterval_OnCheckedChange" />
                    </div>
                </div>

                <div class="col-3">
                    <div class="row">
                        <asp:Label ID="DayLabel" runat="server">Giorno di completamento </asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:DropDownList ID="DayFrom" runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:DropDownList ID="DayTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:CheckBox ID="DayInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="DayInterval_OnCheckedChange" />
                    </div>
                </div>

                <div class="col-3">
                    <div class="row">
                        <asp:Label ID="HourLabel" runat="server">Ora di completamento </asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:DropDownList ID="HourFrom" runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:DropDownList ID="HourTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:CheckBox ID="HourInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="HourInterval_OnCheckedChange" />
                    </div>
                </div>
            </div>

            <br />

            <div class="row text-center">
                <div class="col-3">
                    <div class="row">
                        <asp:Label ID="ReleaseYearLabel" runat="server">Anno di pubblicazione: </asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:DropDownList ID="ReleaseYearFrom" runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:DropDownList ID="ReleaseYearTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:CheckBox ID="ReleaseYearInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="ReleaseYearInterval_OnCheckedChange" />
                    </div>
                </div>
                <div class="col-3">
                    <div class="row">
                        <asp:Label ID="TitleLabel" runat="server">Titolo: </asp:Label>
                    </div>
                    <div class="row">
                        <asp:TextBox ID="Titolo" placeholder="Titolo" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:CheckBox runat="server" ID="exactMatchCheckBox" Text="Titolo Esatto" CssClass="QueryExclusive" AutoPostBack="true" OnCheckedChanged="ExactMatchCheckBox_OnCheckedChange" />

                        </div>
                        <div class="col-6">
                            <asp:CheckBox runat="server" ID="FirstLettersCheckbox" Text="Inizia con" CssClass="QueryExclusive" AutoPostBack="true" OnCheckedChanged="FirstLettersCheckbox_OnCheckedChange" />
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="row">
                        <asp:Label ID="ConsoleSelectionLabel" runat="server">Console di gioco: </asp:Label>
                    </div>
                    <div class="row">
                        <asp:DropDownList ID="ConsoleSelection" name="Console di gioco" size="1" TabIndex="1" AutoPostBack="True" runat="server">
                            <asp:ListItem Selected="True" Value="ANY">Tutte</asp:ListItem>
                            <asp:ListItem Value="PC">PC</asp:ListItem>
                            <asp:ListItem Value="PS1">Sony Playstation</asp:ListItem>
                            <asp:ListItem Value="PS2">Sony Playstation 2</asp:ListItem>
                            <asp:ListItem Value="PS3">Sony Playstation 3</asp:ListItem>
                            <asp:ListItem Value="PS4">Sony Playstation 4</asp:ListItem>
                            <asp:ListItem Value="PS5">Sony Playstation 5</asp:ListItem>
                            <asp:ListItem Value="PSP">Sony Playstation Portable</asp:ListItem>
                            <asp:ListItem Value="PSVITA">Sony Playstation Vita</asp:ListItem>
                            <asp:ListItem Value="NES">Nintendo Entertainment System</asp:ListItem>
                            <asp:ListItem Value="SNES">Super Nintendo Entertainment System</asp:ListItem>
                            <asp:ListItem Value="N64">Nintendo 64</asp:ListItem>
                            <asp:ListItem Value="GC">Nintendo GameCube</asp:ListItem>
                            <asp:ListItem Value="WII">Nintendo WII</asp:ListItem>
                            <asp:ListItem Value="WIIU">Nintendo WII U</asp:ListItem>
                            <asp:ListItem Value="NSWITCH">Nintendo Switch</asp:ListItem>
                            <asp:ListItem Value="GB">Nintendo Game Boy</asp:ListItem>
                            <asp:ListItem Value="GBC">Nintendo Game Boy Color</asp:ListItem>
                            <asp:ListItem Value="GBA">Nintendo Game Boy Advance</asp:ListItem>
                            <asp:ListItem Value="NDS">Nintendo DS</asp:ListItem>
                            <asp:ListItem Value="N3DS">Nintendo 3DS</asp:ListItem>
                            <asp:ListItem Value="SMS">Sega Master System</asp:ListItem>
                            <asp:ListItem Value="SMD">Sega Mega Drive</asp:ListItem>
                            <asp:ListItem Value="SST">Sega Saturn</asp:ListItem>
                            <asp:ListItem Value="SDC">Sega Dreamcast</asp:ListItem>
                            <asp:ListItem Value="SGG">Sega Game Gear</asp:ListItem>
                            <asp:ListItem Value="SMJ">Sega Mega Jet</asp:ListItem>
                            <asp:ListItem Value="SNMD">Sega Nomad</asp:ListItem>
                            <asp:ListItem Value="NGEO">Neo Geo</asp:ListItem>
                            <asp:ListItem Value="ARCADE">Arcade</asp:ListItem>
                            <asp:ListItem Value="XBOX">Microsoft Xbox</asp:ListItem>
                            <asp:ListItem Value="XBOX360">Microsoft Xbox 360</asp:ListItem>
                            <asp:ListItem Value="XBOXONE">Microsoft Xbox One</asp:ListItem>
                            <asp:ListItem Value="XBOXSX">Microsoft Xbox Series X / S</asp:ListItem>
                            <asp:ListItem Value="CELLPHONE">Mobile phone</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="row">
                        <center>
                            Completamento al 100%
                        </center>
                        <asp:CheckBox runat="server" ID="completionCheckBox"></asp:CheckBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="container">

            <div class="row">
                <div class="col-6">
                    <asp:Label runat="server" ID="OrderCriteriaLabel" CssClass="QueryExclusive">Ordinare per</asp:Label>
                    <asp:DropDownList runat="server" ID="OrderCriteriaDDL" CssClass="QueryExclusive"></asp:DropDownList>
                </div>
                <div class="col-6">
                    <asp:DropDownList ID="completedDropDownList" runat="server"
                        DataSourceID="Completati"
                        DataValueField="IDEdizione"
                        DataTextField="Titolo">
                    </asp:DropDownList>
                </div>
            </div>

            <p>
                <asp:Button ID="QueryButton" CssClass="QueryExclusive" Text="Inizio Ricerca" OnClick="QueryButton_Click" AutoPostBack="false" runat="server" />
            </p>

            <hr />

            <div id="FileInsertionDiv" runat="server">
                <asp:Label runat="server" ID="FileInsertionLabel" Text="Inserisci un file"></asp:Label>
                <p>
                    <input runat="server" id="FileInput" type="file" name="fileInput" /><span asp-validation-for="FileInput.FormFile"></span>
                </p>
                <p>
                    <asp:Button runat="server" ID="UploadButton" name="Bottone di upload" Text="Carica" OnClick="UploadButton_Click" />
                </p>
            </div>

            <hr />

            <div id="ResultTable" style="overflow: scroll">

                <asp:GridView ID="GrigliaVideogiochi"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="id"
                    BackColor="White"
                    BorderColor="White"
                    BorderStyle="Ridge"
                    BorderWidth="2px"
                    CellPadding="3"
                    CellSpacing="1"
                    GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <EditItemTemplate>
                                <asp:TextBox ID="IDField" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="IDContent" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data">
                            <EditItemTemplate>
                                <asp:TextBox ID="DataField" runat="server" Text='<%# Bind("Data") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="DataContent" runat="server" Text='<%# Bind("Data") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Console">
                            <EditItemTemplate>
                                <asp:TextBox ID="ConsoleField" runat="server" Text='<%# Bind("Console") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="ConsoleContent" runat="server" Text='<%# Bind("Console") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titolo">
                            <EditItemTemplate>
                                <asp:TextBox ID="TitoloField" runat="server" Text='<%# Bind("Titolo") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="TitoloContent" runat="server" Text='<%# Bind("Titolo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Note">
                            <EditItemTemplate>
                                <asp:TextBox ID="NoteField" runat="server" Text='<%# Bind("Note") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="NoteContent" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>

            </div>

        </div>

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

