﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Site.aspx.cs" Inherits="MainPage" MaintainScrollPositionOnPostback="true" %>

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
    <form id="form1" runat="server">

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
                <asp:TextBox ID="YearFrom" runat="server" />
                <asp:TextBox ID="YearTo" Text="A" runat="server" Visible="false" />
                <asp:CheckBox ID="YearInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="YearInterval_OnCheckedChange" />
            </p>

            <p>
                <asp:Label ID="MonthLabel" runat="server">Mese di completamento: </asp:Label>
                <asp:TextBox ID="MonthFrom" runat="server" />
                <asp:TextBox ID="MonthTo" Text="A" runat="server" Visible="false" />
                <asp:CheckBox ID="MonthInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="MonthInterval_OnCheckedChange" />
            </p>

                <p>
                    <asp:Label ID="DayLabel" runat="server">Giorno di completamento: </asp:Label>
                    <asp:TextBox ID="DayFrom" runat="server" />
                    <asp:TextBox ID="DayTo" Text="A" runat="server" Visible="false" />
                    <asp:CheckBox ID="DayInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="DayInterval_OnCheckedChange" />
                </p>

                <p>
                    <asp:Label ID="HourLabel" runat="server">Ora di completamento: </asp:Label>
                    <asp:TextBox ID="HourFrom" runat="server" />
                    <asp:TextBox ID="HourTo" Text="A" runat="server" Visible="false" />
                    <asp:CheckBox ID="HourInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="HourInterval_OnCheckedChange" />
                </p>

                <p>
                    <asp:Button ID="ActualDateButton" runat="server" Text="Data ed ora attuali" OnClick="ActualDateButton_Click" AutoPostBack="false" />
                    <asp:Label ID="TimeFormatLabel" runat="server">Formato di data ed ora: </asp:Label>
                    <asp:DropDownList ID="TimeFormatSelection" name="Formato data di completamento" size="1" TabIndex="1" AutoPostBack="True" runat="server"></asp:DropDownList>
                    <asp:Label runat="server" ID="TimeFormatOptionsLabel">Includere i secondi</asp:Label>
                    <asp:CheckBox runat="server" OnCheckedChanged="CheckSecond_OnCheckedChange" ID="CheckSecond" GroupName="TimeFormatButtons"  />
                </p>
            </div>

            <div id="ReleaseYearAttributes">
                <p>
                    <asp:Label ID="ReleaseYearLabel" runat="server">Anno di pubblicazione: </asp:Label>
                    <asp:TextBox ID="ReleaseYearFrom" runat="server" />
                    <asp:TextBox ID="ReleaseYearTo" Text="A" runat="server" Visible="false" />
                    <asp:CheckBox ID="ReleaseYearInterval" Text="Intervallo" runat="server" CssClass="QueryExclusive" Checked="false" AutoPostBack="true" OnCheckedChanged="ReleaseYearInterval_OnCheckedChange" />
                </p>
            </div>

            <div id="GameTitleAttributes">
            <p>
                <asp:Label ID="TitleLabel" runat="server">Titolo: </asp:Label>
                <asp:TextBox ID="Title" placeholder="Titolo" runat="server" />
                <asp:Label ID="FirstLettersLabel" runat="server" Visible="false" >Iniziali: </asp:Label>
                <asp:TextBox ID="FirstLetters" placeholder="Inizia con" runat="server" Visible="false"> </asp:TextBox>
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
                <asp:CheckBox runat="server" ID="completionCheckBox" Text="Completamento al 100%:"></asp:CheckBox>
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

        <asp:Label runat="server" ID="DateOrderLabel" CssClass="QueryExclusive"><h5>Ordinare per data di completamento?</h5></asp:Label>

        <div id="DateOrderRadio">
            <p>
                <asp:RadioButton runat="server" CssClass="QueryExclusive" ID="DateOrderYes" Text="SI" GroupName="QueryDateOrder" />
                <asp:RadioButton runat="server" CssClass="QueryExclusive" ID="DateOrderNo" Text="NO" GroupName="QueryDateOrder" />
            </p>
        </div>

        <p>
            <asp:Button ID="QueryButton" CssClass="QueryExclusive" Text="Inizio Ricerca" OnClick="QueryButton_Click" AutoPostBack="false" runat="server" />
        </p>
        <p>
            <asp:Button ID="InsertionButton" CssClass="InsertionExclusive" Text="Inserisci Videogioco" OnClick="InsertionButton_Click" Visible="false" AutoPostBack="false" runat="server" />
        </p>

        <hr />

        <div id="FileInsertion">
            <p>
                <asp:Label runat="server" ID="FileInsertionLabel" Text="Inserisci un file"></asp:Label>
                <asp:TextBox runat="server" ID="FileInsertionPath" placeholder="Nome del file"></asp:TextBox>
                <asp:Button runat="server" ID="UploadButton" Text="Carica" />
            </p>
        </div>

        <hr />

        <div id="ResultTable">
            <h2>Tabella dei videogiochi</h2>
            <table border="1" bgcolor="#ffffff" cellspacing="0">
                <caption><b>Videogiochi</b></caption>
            </table>
        </div>
    </form>
</body>
</html>