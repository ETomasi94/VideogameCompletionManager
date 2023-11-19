<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Site.aspx.cs" Inherits="MainPage" MaintainScrollPositionOnPostback="true" %>

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
                <select size="1" id="ConsoleSelection" name="Console di gioco" tabindex="1">
                    <option value="ANY">Tutte</option>
                    <option value="PC">PC</option>
                    <option value="PS1">Sony Playstation</option>
                    <option value="PS2">Sony Playstation 2</option>
                    <option value="PS3">Sony Playstation 3</option>
                    <option value="PS4">Sony Playstation 4</option>
                    <option value="PS5">Sony Playstation 5</option>
                    <option value="PSP">Sony Playstation Portable</option>
                    <option value="PSVITA">Sony Playstation Vita</option>
                    <option value="NES">Nintendo Entertainment System</option>
                    <option value="SNES">Super Nintendo Entertainment System</option>
                    <option value="N64">Nintendo 64</option>
                    <option value="GC">Nintendo GameCube</option>
                    <option value="WII">Nintendo WII</option>
                    <option value="WIIU">Nintendo WII U</option>
                    <option value="NSWITCH">Nintendo Switch</option>
                    <option value="GB">Nintendo Game Boy</option>
                    <option value="GBC">Nintendo Game Boy Color</option>
                    <option value="GBA">Nintendo Game Boy Advance</option>
                    <option value="NDS">Nintendo DS</option>
                    <option value="N3DS">Nintendo 3DS</option>
                    <option value="SMS">Sega Master System</option>
                    <option value="SMD">Sega Mega Drive</option>
                    <option value="SST">Sega Saturn</option>
                    <option value="SDC">Sega Dreamcast</option>
                    <option value="SGG">Sega Game Gear</option>
                    <option value="SMJ">Sega Mega Jet</option>
                    <option value="SNMD">Sega Nomad</option>
                    <option value="NGEO">Neo Geo</option>
                    <option value="ARCADE">Arcade</option>
                    <option value="XBOX">Microsoft Xbox</option>
                    <option value="XBOX360">Microsoft Xbox 360</option>
                    <option value="XBOXONE">Microsoft Xbox One</option>
                    <option value="XBOXSX">Microsoft Xbox Series X / S</option>
                    <option value="CELLPHONE">Mobile phone</option>
                </select>
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
