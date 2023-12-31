﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Site.aspx.cs" Inherits="GameCompletionManager.MainPage" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Lista dei videogiochi</title>
    <link rel="stylesheet" href="/Style/MainSite.css" />
    <script src="script.js"></script>
</head>
<body>
    <form id="MainSiteForm" runat="server" enctype="multipart/form-data" method="post">

        <h1>Lista dei videogiochi completati</h1>

        <p>
            <img src="/Immagini/VideoGames.jpg" height="300" style="width: 1456px" />
        </p>

        <hr />

        <h3>Un po' di musica per caricarvi</h3>
        <iframe width="560" height="315" src="https://www.youtube.com/embed/pSvn0rp0kz0" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen=" true"></iframe>

        <hr />

        <h2>Questa è una lista dei videogiochi completati dal 2017 al 2023, memorizzati includento:</h2>

        <ul>
            <li>ID sequenziale del videogioco (in ordine di completamento)</li>
            <li>Data ed ora del completamento</li>
            <li>Console su cui è stato completato il videogioco</li>
            <li>Titolo del videogioco</li>
            <li>Note sul completamento del videogioco</li>
        </ul>

        <hr />

        <h4>Ricerca all'interno della tabella (da implementare)</h4>

        <div id="SearchAttributes">

            <label class="switch">
                <asp:CheckBox id="ModeCheckbox" runat="server" AutoPostBack="true" OnCheckedChanged="SwitchMode" />
                <span runat="server" class="slider round"></span>
            </label>

            <div id="CompletionDateAttributes">
            <p>
                <asp:Label ID="YearLabel" runat="server">Anno di completamento: </asp:Label>
                <asp:DropDownList ID="YearFrom" runat="server" />
                <asp:DropDownList ID="YearTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                <asp:CheckBox ID="YearInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="YearInterval_OnCheckedChange" />
            </p>

            <p>
                <asp:Label ID="MonthLabel" runat="server">Mese di completamento: </asp:Label>
                <asp:DropDownList ID="MonthFrom" runat="server" />
                <asp:DropDownList ID="MonthTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                <asp:CheckBox ID="MonthInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="MonthInterval_OnCheckedChange" />
            </p>

                <p>
                    <asp:Label ID="DayLabel" runat="server">Giorno di completamento: </asp:Label>
                    <asp:DropDownList ID="DayFrom" runat="server" />
                    <asp:DropDownList ID="DayTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                    <asp:CheckBox ID="DayInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="DayInterval_OnCheckedChange" />
                </p>

                <p>
                    <asp:Label ID="HourLabel" runat="server">Ora di completamento: </asp:Label>
                    <asp:DropDownList ID="HourFrom" runat="server" />
                    <asp:DropDownList ID="HourTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                    <asp:CheckBox ID="HourInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="HourInterval_OnCheckedChange" />
                </p>

                <p>
                    <asp:Button ID="ActualDateButton" runat="server" Text="Data ed ora attuali" CssClass="InsertionExclusive" OnClick="ActualDateButton_Click" AutoPostBack="false" />
                </p>
            </div>

            <div id="ReleaseYearAttributes">
                <p>
                    <asp:Label ID="ReleaseYearLabel" runat="server">Anno di pubblicazione: </asp:Label>
                    <asp:DropDownList ID="ReleaseYearFrom" runat="server" />
                    <asp:DropDownList ID="ReleaseYearTo" runat="server" CssClass="QueryExclusive" Visible="false" />
                    <asp:CheckBox ID="ReleaseYearInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="ReleaseYearInterval_OnCheckedChange" />
                </p>
            </div>

            <div id="GameTitleAttributes">
            <p>
                <asp:Label ID="TitleLabel" runat="server">Titolo: </asp:Label>
                <asp:TextBox ID="Title" placeholder="Titolo" runat="server" />
                <asp:Label ID="FirstLettersLabel" runat="server" CssClass="QueryExclusive"  Visible="false" >Iniziali: </asp:Label>
                <asp:CheckBox runat="server" ID="exactMatchCheckBox" Text="Titolo Esatto"  CssClass="QueryExclusive" AutoPostBack="true" OnCheckedChanged="ExactMatchCheckBox_OnCheckedChange" />
                <asp:CheckBox runat="server" ID="FirstLettersCheckbox" Text="Inizia con" CssClass="QueryExclusive" AutoPostBack="true" OnCheckedChanged="FirstLettersCheckbox_OnCheckedChange" />
            </p>
            </div>

            <div id="CompletionNotes">
                <p>
                    <asp:Label runat="server" ID="CompletionNotesLabel">Note: </asp:Label>
                    <asp:TextBox runat="server" ID="CompletionNotesTextBox" placeholder="Le tue note"></asp:TextBox>
                </p>
            </div>

            <p>
                <asp:CheckBox runat="server" ID="completionCheckBox" Text="Completamento al 100%"></asp:CheckBox>
            </p>

            <p>
                <asp:Label ID="ConsoleSelectionLabel" runat="server">Console di gioco: </asp:Label>

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
            </p>
        </div>

        <div id="OrderCriteriaDiv">
            <p>
                <asp:Label runat="server" ID="OrderCriteriaLabel" CssClass="QueryExclusive">Ordinare per</asp:Label>
                <asp:DropDownList runat="server" ID="OrderCriteriaDDL" CssClass="QueryExclusive"></asp:DropDownList>
            </p>
        </div>

        <p>
            <asp:Button ID="QueryButton" CssClass="QueryExclusive" Text="Inizio Ricerca" OnClick="QueryButton_Click" AutoPostBack="false" runat="server" />
            <asp:Button ID="InsertionButton" CssClass="InsertionExclusive" Text="Inserisci Videogioco" OnClick="InsertionButton_Click" Visible="false" AutoPostBack="false" runat="server" />
        </p>

        <hr />

        <div id="FileInsertionDiv" runat="server">
            <asp:Label runat="server" ID="FileInsertionLabel" Text="Inserisci un file"></asp:Label>
            <p>
                <input runat="server" id="FileInput" type="file" name="fileInput" /><span asp-validation-for="FileInput.FormFile"></span>
            </p>
            <p> <asp:Button runat="server" ID="UploadButton" name="Bottone di upload" text="Carica" OnClick="UploadButton_Click" /></p>
        </div>

        <hr />

        <div id="ResultTable" style="overflow:scroll">
            <asp:GridView ID="GrigliaVideogiochi" runat="server" AutoGenerateColumns="False"
                DataKeyNames="id" BackColor="White" BorderColor="White"
                BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
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
    </form>
</body>
</html>
