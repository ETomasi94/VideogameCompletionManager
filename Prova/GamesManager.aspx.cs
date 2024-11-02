using GameCompletionManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Runtime.Serialization;
using System.Globalization;



public partial class GamesManager : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    
    /*public void QueryButton_Click(Object sender, EventArgs e)
    {
        OleDbCommand comandoSelezione = DbConnectionUtils.CreateCommand(CommandType.Text, GenerateSqlCommand());

        DataTable tuttiIGiochi = GestoreDati.ATabella(GetConnectionString(), comandoSelezione);

        GrigliaVideogiochi.DataSource = tuttiIGiochi;
        GrigliaVideogiochi.DataBind();
    }

    public void ActualDateButton_Click(object sender, EventArgs e)
    {
        DateTime dateTime = DateTime.Now;
    }

    public void FirstLettersCheckbox_OnCheckedChange(object sender, EventArgs e)
    {
        ConfiguraLabel(TitleLabel, Checked(FirstLettersCheckbox), "Iniziali del titolo: ", "Titolo: ");
    }

    public void ExactMatchCheckBox_OnCheckedChange(object sender, EventArgs e)
    {
        CambiaVisibilitaControls(!exactMatchCheckBox.Checked, FirstLettersCheckbox);

        ConfiguraLabel(TitleLabel, exactMatchCheckBox.Checked, "Titolo esatto: ", "Titolo: ");
    }

    public void ReleaseYearInterval_OnCheckedChange(object sender, EventArgs e)
    {
        CambiaVisibilitaControls(ReleaseYearInterval.Checked, ReleaseYearTo);
        SelectANYElement(!ReleaseYearInterval.Checked, ReleaseYearTo);
    }

    public void YearInterval_OnCheckedChange(object sender, EventArgs e)
    {
        CambiaVisibilitaControls(YearInterval.Checked, YearTo);
        SelectANYElement(!YearInterval.Checked, YearTo);
    }

    public void MonthInterval_OnCheckedChange(Object sender, EventArgs e)
    {
        CambiaVisibilitaControls(MonthInterval.Checked, MonthTo);
        SelectANYElement(!MonthInterval.Checked, MonthTo);
    }

    public void DayInterval_OnCheckedChange(Object sender, EventArgs e)
    {
        CambiaVisibilitaControls(DayInterval.Checked, DayTo);
        SelectANYElement(!DayInterval.Checked, DayTo);
    }

    public void HourInterval_OnCheckedChange(Object sender, EventArgs e)
    {
        CambiaVisibilitaControls(HourInterval.Checked, HourTo);
        SelectANYElement(!HourInterval.Checked, HourTo);
    }

    public void InsertionButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void UploadButton_Click(Object sender, EventArgs e)
    {
        string sqlCommand = "SELECT * FROM Videogiochi ORDER BY ID";

        OleDbCommand comandoSelezione = DbConnectionUtils.CreateCommand(CommandType.Text, sqlCommand);

        int minYear = DateTime.Now.Year;
        int maxYear = DateTime.Now.Year;

        DataTable tabellaVideogiochi = GestoreDati.ATabella(GetConnectionString(), comandoSelezione);

        foreach (DataRow dr in tabellaVideogiochi.Rows)
        {
            DateTime data = dr.Field<DateTime>("Data");

            minYear = Math.Min(minYear, data.Year);
            maxYear = Math.Max(maxYear, data.Year);
        }

        RiempiDropDownListAnni(minYear, maxYear, YearFrom, YearTo);
    }

    //QUERY
    private string GenerateSqlCommand()
    {
        char[] separators = new char[] { ',', '.', '\\', '/', ' ' };
        StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Videogiochi ");

        stringBuilder = GenerateWHEREDate(Checked(YearInterval), stringBuilder, YearFrom, YearTo, SQLFunzioniTime.YEAR);
        stringBuilder = GenerateWHEREDate(Checked(MonthInterval), stringBuilder, MonthFrom, MonthTo, SQLFunzioniTime.MONTH);
        stringBuilder = GenerateWHEREDate(Checked(DayInterval), stringBuilder, DayFrom, DayTo, SQLFunzioniTime.DAY);

        stringBuilder = GenerateWHERETitle(stringBuilder);



        stringBuilder = GenerateWHEREConsole(stringBuilder);

        stringBuilder = GenerateORDERBYFromDDL(stringBuilder);

        Debug.WriteLine(stringBuilder.ToString());

        return stringBuilder.ToString();
    }

    private StringBuilder GenerateWHERETitle(StringBuilder stringBuilder)
    {
        if (stringBuilder != null)
        {
            if (Titolo.Text.Equals(String.Empty))
            {
                return stringBuilder;
            }
            else
            {
                if (Checked(exactMatchCheckBox))
                {
                    if (stringBuilder.ToString().Contains("WHERE"))
                    {
                        return stringBuilder.Append(" AND ").Append("Titolo = " + "'" + Titolo.Text + "'");
                    }
                    else
                    {
                        return stringBuilder.Append("WHERE ").Append("Titolo = " + "'" + Titolo.Text + "'");
                    }
                }
                else if (Checked(FirstLettersCheckbox))
                {
                    if (stringBuilder.ToString().Contains("WHERE"))
                    {
                        return stringBuilder.Append(" AND ").Append("Titolo LIKE " + "'" + Titolo.Text + "%'");
                    }
                    else
                    {
                        return stringBuilder.Append("WHERE ").Append("Titolo LIKE " + "'" + Titolo.Text + "%'");
                    }
                }
                else
                {
                    if (stringBuilder.ToString().Contains("WHERE"))
                    {
                        return stringBuilder.Append(" and ").Append("Titolo LIKE " + "'%" + Titolo.Text + "%'");
                    }
                    else
                    {
                        return stringBuilder.Append("WHERE ").Append("Titolo LIKE " + "'%" + Titolo.Text + "%'");
                    }
                }
            }
        }
        return null;
    }

    private StringBuilder GenerateWHEREConsole(StringBuilder stringBuilder)
    {
        if (stringBuilder != null)
        {
            if (!IsANYSelected(ConsoleSelection))
            {
                if (stringBuilder.ToString().Contains("WHERE"))
                {
                    return stringBuilder.Append(" and ").Append("Console = " + "'" + ConsoleSelection.SelectedValue + "'");
                }
                else
                {
                    return stringBuilder.Append("WHERE ").Append("Console = " + "'" + ConsoleSelection.SelectedValue + "'");
                }
            }
            else
            {
                return stringBuilder;
            }
        }

        return null;
    }

    private StringBuilder GenerateORDERBYFromDDL(StringBuilder stringBuilder)
    {
        stringBuilder.Append(" ORDER BY " + OrderCriteriaDDL.SelectedValue);

        return stringBuilder;
    }
    */

    private StringBuilder GenerateWHEREDate(bool IsInterval, StringBuilder stringBuilder, DropDownList firstDDL, DropDownList secondDDL, SQLFunzioniTime timeFunction)
    {
        int timeValue1, timeValue2;
        string timeCriteria = ConvalidaEnum.GeneraFunzioneSQL(timeFunction, "Data");

        string result;

        if (IsInterval)
        {
            if (IsANYSelected(firstDDL))
            {
                if (!IsANYSelected(secondDDL))
                {
                    timeValue1 = Int32.Parse(secondDDL.SelectedItem.Value);

                    result = (timeCriteria + " <= " + timeValue1);
                }
                else
                {
                    result = String.Empty;
                }
            }
            else
            {
                if (!IsANYSelected(secondDDL))
                {
                    timeValue1 = Int32.Parse(firstDDL.SelectedItem.Value);
                    timeValue2 = Int32.Parse(secondDDL.SelectedItem.Value);

                    result = (timeCriteria + " >= " + Math.Min(timeValue1, timeValue2) + " and " + timeCriteria + " <= " + Math.Max(timeValue1, timeValue2));
                }
                else
                {
                    timeValue1 = Int32.Parse(firstDDL.SelectedItem.Value);

                    result = (timeCriteria + " >= " + timeValue1);
                }
            }
        }
        else
        {
            if (IsANYSelected(firstDDL))
            {
                result = String.Empty;
            }
            else
            {
                timeValue1 = Int32.Parse(firstDDL.SelectedItem.Value);
                Debug.WriteLine(timeValue1);

                result = (timeCriteria + " = " + timeValue1);
            }
        }

        if (result.Equals(String.Empty))
        {
            return stringBuilder;
        }
        else
        {
            if (stringBuilder.ToString().Contains("WHERE"))
            {
                return stringBuilder.Append(" and ").Append(result);
            }
            else
            {
                return stringBuilder.Append("WHERE ").Append(result);
            }
        }
    }


    private void RiempiDropDownListAnni(int annoMin, int annoMax, params DropDownList[] dropDownLists)
    {
        foreach (DropDownList dropDownList in dropDownLists)
        {
            if (dropDownList != null)
            {
                dropDownList.Items.Clear();

                dropDownList.Items.Add(CreateANYElement());

                for (int i = annoMin; i <= annoMax; i++)
                {
                    ListItem annoItem = new ListItem();
                    annoItem.Text = annoItem.Value = i.ToString();

                    dropDownList.Items.Add(annoItem);
                }
            }
        }
    }

    private void RiempiDropDownListMesi(params DropDownList[] dropDownLists)
    {
        foreach (DropDownList dropDownList in dropDownLists)
        {
            if (dropDownList != null)
            {
                dropDownList.Items.Clear();

                dropDownList.Items.Add(CreateANYElement());

                for (int i = 1; i <= 12; i++)
                {
                    ListItem meseItem = new ListItem
                    {
                        Value = i.ToString(),
                        Text = ((Mesi)i).ToString()
                    };

                    dropDownList.Items.Add(meseItem);
                }
            }
        }
    }

    private void RiempiDropDownListGiorni(params DropDownList[] dropDownLists)
    {
        foreach (DropDownList dropDownList in dropDownLists)
        {
            if (dropDownList != null)
            {
                dropDownList.Items.Clear();

                dropDownList.Items.Add(CreateANYElement());

                for (int i = 1; i <= 31; i++)
                {
                    ListItem giornoItem = new ListItem();
                    giornoItem.Value = giornoItem.Text = i.ToString();


                    dropDownList.Items.Add(giornoItem);
                }
            }
        }
    }

    private void RiempiDropDownListOre(params DropDownList[] dropDownLists)
    {
        foreach (DropDownList dropDownList in dropDownLists)
        {
            if (dropDownList != null)
            {
                dropDownList.Items.Clear();

                dropDownList.Items.Add(CreateANYElement());

                for (int i = 0; i <= 23; i++)
                {
                    ListItem oraItem = new ListItem();
                    oraItem.Value = oraItem.Text = i.ToString();


                    dropDownList.Items.Add(oraItem);
                }
            }
        }
    }

    private void ResettaDropDownList(params DropDownList[] dropDownLists)
    {
        foreach (DropDownList dropDownList in dropDownLists)
        {
            if (EsisteControl(dropDownList))
            {
                dropDownList.Items.Clear();
                dropDownList.Items.Add(CreateANYElement());
            }
        }
    }

    private void SelectANYElement(bool cond, params DropDownList[] dropDownLists)
    {
        if (cond)
        {
            foreach (DropDownList dropDownList in dropDownLists)
            {
                if (dropDownList != null && HasANYElement(dropDownList))
                {
                    dropDownList.SelectedValue = "ANY";
                }
            }
        }
    }

    private bool IsANYSelected(params DropDownList[] dropDownLists)
    {
        bool isForAll = true;

        foreach (DropDownList dropDownList in dropDownLists)
        {
            if (isForAll)
            {
                isForAll = dropDownList.SelectedItem.Value.Equals("ANY");
            }
        }

        return isForAll;
    }

    private ListItem CreateANYElement()
    {
        ListItem anyItem = new ListItem
        {
            Value = "ANY",
            Text = "Tutti"
        };

        return anyItem;
    }

    private bool HasANYElement(DropDownList dropDownList)
    {
        return dropDownList.Items.FindByValue("ANY") != null;
    }

    private void OrdinaDropDownList(DropDownList dropDownList)
    {
        List<ListItem> itemList = new List<ListItem>();
        ListItem anyConsole = null;

        foreach (ListItem item in dropDownList.Items)
        {
            if (!item.Value.Equals("ANY"))
            {
                itemList.Add(item);
            }
            else
            {
                anyConsole = item;
            }
        }

        itemList.Sort((x, y) => String.Compare(x.Text, y.Text));

        if (anyConsole != null)
        {
            itemList.Insert(0, anyConsole);
        }

        dropDownList.Items.Clear();
        dropDownList.Items.AddRange(itemList.ToArray());
    }

    //CHECKBOXES
    private bool Checked(CheckBox checkBox)
    {
        return checkBox.Checked;
    }

    private bool Unchecked(CheckBox checkBox)
    {
        return !checkBox.Checked;
    }

    //LABELS
    private void ConfiguraLabel(Label label, Boolean testoVisualizzato, string testoDiOutput)
    {
        if (EsisteControl(label))
        {
            if (testoVisualizzato)
            {
                SettaLabel(label, testoDiOutput);
            }
            else
            {
                ResettaLabel(label);
            }
        }
    }

    private void ConfiguraLabel(Label label, Boolean testoVisualizzato, string testoChecked, string testoNonChecked)
    {
        if (EsisteControl(label))
        {
            if (testoVisualizzato)
            {
                SettaLabel(label, testoChecked);
            }
            else
            {
                SettaLabel(label, testoNonChecked);
            }
        }
    }

    private void SettaLabel(Label label, string outputText)
    {
        if (EsisteControl(label))
        {
            label.Text = outputText;
        }
    }

    private void ResettaLabel(params Label[] labels)
    {
        if (EsistonoControls(labels))
        {
            foreach (Label label in labels)
            {
                label.Text = "";
            }
        }
    }

    //TEXTBOXES
    private void ConfiguraTextbox(TextBox textBox, Boolean testoVisualizzato, string testoDiOutput)
    {
        if (EsisteControl(textBox))
        {
            if (testoVisualizzato)
            {
                SettaTextbox(textBox, testoDiOutput);
            }
            else
            {
                ResettaTextbox(textBox);
            }
        }
    }

    private void SettaTextbox(TextBox tbox, string outputText)
    {
        if (EsisteControl(tbox))
        {
            tbox.Text = outputText;
        }
        else
        {
            throw new ArgumentNullException("tbox","Textbox da settare inesistente");
        }
    }

    private void ResettaTextbox(params TextBox[] tboxes)
    {
        if (EsistonoControls(tboxes))
        {
            foreach (TextBox tbox in tboxes)
            {
                if (EsisteControl(tbox))
                {
                    tbox.Text = "";
                }
                else
                {
                    throw new ArgumentException("Textbox da resettare inesistente");
                }
            }
        }
        else
        {
            throw new ArgumentNullException("tboxes","Insieme di textboxes da resettare inesistente");
        }
    }

    //GENERIC CONTROLS
    private void CambiaVisibilitaControls(bool value, params WebControl[] controls)
    {
        if (EsistonoControls(controls))
        {
            foreach (WebControl control in controls)
            {
                if (EsisteControl(control))
                {
                    try
                    {
                        CambiaVisibilitaControl(value, control);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
        else
        {
            throw new ArgumentNullException("controls","L'insieme di controls risulta inesistente");
        }
    }

    private void CambiaVisibilitaControl(bool value, WebControl control)
    {
        if (EsisteControl(control))
        {
            control.Visible = value;
        }
        else
        {
            throw new ArgumentNullException("control","Il control risulta inesistente");
        }
    }

    private WebControl[] TrovaControlsPerClasseCss(string classeCss)
    {
        if (Esiste(classeCss))
        {
            return Page.Form.Controls.OfType<WebControl>().Where(c => c.CssClass.Equals(classeCss)).ToArray();
        }
        else
        {
            throw new ArgumentException("Il parametro \"classeCss\" non è valido", "classeCss");
        }
    }

    private bool EsistonoControls(params WebControl[] controls)
    {
        return controls != null && controls.Length > 0;
    }

    private bool EsisteControl(WebControl control)
    {
        return (control != null);
    }

    private bool Esiste(string s)
    {
        return (s != null);
    }

    private string GeneraCodiceCompletamento(int idEdizione,DateTime dataCompletamento)
    {
        StringBuilder sbuilder = new StringBuilder();

        Random randomizer = new Random();

        string comandoSelezioneAttributi = "SELECT t.Titolo, ed.SiglaConsole " +
                                           "FROM Edizioni ed " +
                                           "INNER JOIN Titoli t ON ed.IDTitolo = t.IDTitolo " +
                                           "WHERE ed.IDEdizione = "+idEdizione;

        OleDbCommand comandoSelezione = DbConnectionUtils.CreateCommand(CommandType.Text, comandoSelezioneAttributi);



        DataTable informazioniGioco = GestoreDati.ATabella(GetConnectionString(), comandoSelezione);

        string titoloGioco = informazioniGioco.Rows[0]["Titolo"].ToString();
        string consoleGioco = informazioniGioco.Rows[0]["SiglaConsole"].ToString();
        string annoCompletamento = dataCompletamento.Year.ToString().Substring(1);

        string fineCodice = consoleGioco.Substring(1,2) + annoCompletamento;

        for (int i = 0; i < 10; i++)
        {
            sbuilder.Append(titoloGioco[randomizer.Next(titoloGioco.Length)]);
        }

        sbuilder.Append(fineCodice);

        return sbuilder.ToString().ToUpper();
    }

    private void ImpostaCopertina(int i)
    {
            byte[] immagine = File.ReadAllBytes("C:\\Users\\enryr\\Desktop\\Cover templates\\" + i + ".jpg");

            string command = "UPDATE Edizioni SET [Immagine] = @Immagine WHERE [IDEdizione] = @IDGioco";

            OleDbCommand comandoModifica = DbConnectionUtils.CreateCommand(CommandType.Text, command);
            comandoModifica.Parameters.AddWithValue("@Immagine", immagine);
            comandoModifica.Parameters.AddWithValue("@IDGioco", i);

            GestoreDati.Interagisci(GetConnectionString(), comandoModifica);
    }

    protected void ImpostaCopertina(object sender, EventArgs e)
    {

        int idGioco = Convert.ToInt32(notCompletedDropDownList.SelectedValue);

        if (idGioco > 0 && imageUpload.HasFile)
        {
            string fileName = Path.GetFileName(imageUpload.FileName);
            string fileExtension = Path.GetExtension(fileName);

            byte[] byteImmagine;

            using (var binaryReader = new BinaryReader(imageUpload.PostedFile.InputStream))
            {
                byteImmagine = binaryReader.ReadBytes(imageUpload.PostedFile.ContentLength);
            }

            string command = "UPDATE Edizioni SET [Immagine] = @Immagine WHERE [IDEdizione] = @IDGioco";

            OleDbCommand comandoModifica = DbConnectionUtils.CreateCommand(CommandType.Text, command);
            comandoModifica.Parameters.AddWithValue("@Immagine", byteImmagine);
            comandoModifica.Parameters.AddWithValue("@IDGioco", idGioco);

            GestoreDati.Interagisci(GetConnectionString(), comandoModifica);
        }
    }

    protected void CambiaGiocoDaCompletare(object sender, EventArgs e)
    {
        int idGiocoDaCompletare = Convert.ToInt32(notCompletedDropDownList.SelectedValue);

        string command = "SELECT e.Immagine FROM Edizioni e WHERE e.IDEdizione = " + idGiocoDaCompletare;

        OleDbCommand comandoSelezione = DbConnectionUtils.CreateCommand(CommandType.Text, command);

        System.Data.DataTable immagineDatabase = GestoreDati.ATabella(GetConnectionString(), comandoSelezione);

        if (immagineDatabase.Rows.Count > 0 && !String.IsNullOrEmpty(immagineDatabase.Rows[0]["Immagine"].ToString()))
        {
            byte[] immagineCopertina = (byte[])immagineDatabase.Rows[0]["Immagine"];

            gameCover.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(immagineCopertina, 0, immagineCopertina.Length);
        }
    }

    protected void ImpostaDataCorrente(object sender, EventArgs e)
    {
        DateTime currentDateTime = DateTime.Now;

        completionDateTextbox.Text = currentDateTime.Date.ToString("yyyy-MM-dd");
        completionHourTextbox.Text = currentDateTime.Hour.ToString();
        completionMinuteTextbox.Text = currentDateTime.Minute.ToString();
        completionSecondTextbox.Text = currentDateTime.Second.ToString();
    }

    protected void InserisciCompletamento(object sender, EventArgs e)
    {
        int editionID = Convert.ToInt32(notCompletedDropDownList.SelectedValue);

        DateTime completionDate = DateTime.Parse(completionDateTextbox.Text + " " + completionHourTextbox.Text + ":" + completionMinuteTextbox.Text + ":" + completionSecondTextbox.Text,CultureInfo.CurrentCulture,DateTimeStyles.None);

        string codiceGioco = GeneraCodiceCompletamento(editionID,completionDate);

        if (InformazioniCompletamentoCompilate() && (codiceGioco.Length >= 15))
        {
            string dateString = completionDate.ToString("dd/MM/yyyy");
            string hourString = completionDate.ToString("HH:mm:ss");
            string dayOfWeek = completionDate.DayOfWeek.ToString();

            string ending = endingTextbox.Text;
            string notes = notesTextbox.Text;

            bool totalComplete = completeGameCheckBox.Checked;


            string command = "INSERT INTO Completamenti ([Codice],[IDEdizione],[Data],[Ora],[GiornoDellaSettimana],[Finale],[Cento per cento],[Note]) " +
                                "VALUES (@Codice,@Id,@DataCompl,@OraCompl,@GiornoSetCompl,@FinaleCompl,@Cento,@NoteCompl)";

            OleDbCommand oleDbCommand = DbConnectionUtils.CreateCommand(CommandType.Text, command);
            oleDbCommand.Parameters.AddWithValue("@Codice", codiceGioco);
            oleDbCommand.Parameters.AddWithValue("@Id", editionID);
            oleDbCommand.Parameters.AddWithValue("@DataCompl", dateString);
            oleDbCommand.Parameters.AddWithValue("@OraCompl", hourString);
            oleDbCommand.Parameters.AddWithValue("@GiornoSetCompl", dayOfWeek);
            oleDbCommand.Parameters.AddWithValue("@FinaleCompl", ending);
            oleDbCommand.Parameters.AddWithValue("@Cento", totalComplete);
            oleDbCommand.Parameters.AddWithValue("@NoteCompl", notes);

            GestoreDati.Interagisci(GetConnectionString(),oleDbCommand);
        }
    }

    private bool InformazioniCompletamentoCompilate()
    {
        return completionDateTextbox.Text.Length > 0 &&
               completionHourTextbox.Text.Length > 0 &&
               completionMinuteTextbox.Text.Length > 0 &&
               completionSecondTextbox.Text.Length > 0;
    }

    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["Games"].ToString();
    }

    protected void allCovers_Click(object sender, EventArgs e)
    {
        for(int i=42; i<105; i++)
        {
            ImpostaCopertina(i);
        }
    }
}
