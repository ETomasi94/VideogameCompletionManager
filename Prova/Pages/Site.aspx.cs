using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Diagnostics;

namespace GameCompletionManager
{
    public partial class MainPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrdinaDropDownList(ConsoleSelection);
                ResettaDropDownList(YearFrom, YearTo);
                RiempiDropDownListMesi(MonthFrom, MonthTo);
                RiempiDropDownListGiorni(DayFrom, DayTo);
                RiempiDropDownListOre(HourFrom, HourTo);
            }
        }

        protected void CompletionStatingStart_Click(object sender, EventArgs e)
        {
            SettaLabel(StatingLabel, "Decidi una combinazione di tasti");
        }

        public void QueryButton_Click(Object sender, EventArgs e)
        {
            string dbPath = "C:\\Users\\enryr\\Desktop\\Test.accdb";
            string dataProvider = "Microsoft.ACE.OLEDB.12.0";
            string SqlCommand = GenerateSqlCommand();

            DataSet source = Manager.RichiediQuery(dataProvider, dbPath, SqlCommand);

            GrigliaVideogiochi.DataSource = source;
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
            string dbPath = "C:\\Users\\enryr\\Desktop\\Test.accdb";
            string dataProvider = "Microsoft.ACE.OLEDB.12.0";
            string SqlCommand = "SELECT * FROM Videogiochi ORDER BY ID";

            DataSet source = Manager.RichiediQuery(dataProvider, dbPath, SqlCommand);

            int minYear = DateTime.Now.Year;
            int maxYear = DateTime.Now.Year;

            foreach (DataRow dr in source.Tables[0].Rows)
            {
                DateTime data = dr.Field<DateTime>("Data");

                minYear = Math.Min(minYear, data.Year);
                maxYear = Math.Max(maxYear, data.Year);
            }

            RiempiDropDownListAnni(minYear, maxYear, YearFrom, YearTo);
            RiempiDropDownListOrd(OrderCriteriaDDL, source);

            GrigliaVideogiochi.DataSource = source;
            GrigliaVideogiochi.DataBind();
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

            stringBuilder = GenerateWHERENotes(stringBuilder, CompletionNotesTextBox.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray());

            foreach (string note in CompletionNotesTextBox.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray())
            {
                Debug.WriteLine(note);
            }

            stringBuilder = GenerateWHEREConsole(stringBuilder);

            stringBuilder = GenerateORDERBYFromDDL(stringBuilder);

            Debug.WriteLine(stringBuilder.ToString());

            return stringBuilder.ToString();
        }

        private StringBuilder GenerateWHERENotes(StringBuilder stringBuilder, params string[] notes)
        {
            if (stringBuilder != null)
            {
                if (!RiceviTesto(CompletionNotesTextBox).Equals(String.Empty))
                {
                    foreach (string note in notes)
                    {
                        if (stringBuilder.ToString().Contains("WHERE"))
                        {
                            stringBuilder.Append(" and ").Append("Note LIKE " + "'%" + note + "%'");
                        }
                        else
                        {
                            stringBuilder.Append("WHERE ").Append("Note LIKE  " + "'%" + note + "%'");
                        }
                    }
                }

                if (Checked(completionCheckBox))
                {
                    if (stringBuilder.ToString().Contains("WHERE"))
                    {
                        return stringBuilder.Append(" and ").Append("Note LIKE " + "'%100%'");
                    }
                    else
                    {
                        return stringBuilder.Append("WHERE ").Append("Note LIKE " + "'%100%'");
                    }
                }
                else
                {
                    return stringBuilder;
                }
            }
            return null;
        }

        private StringBuilder GenerateWHERETitle(StringBuilder stringBuilder)
        {
            if (stringBuilder != null)
            {
                if (Title.Text.Equals(String.Empty))
                {
                    return stringBuilder;
                }
                else
                {
                    if (Checked(exactMatchCheckBox))
                    {
                        if (stringBuilder.ToString().Contains("WHERE"))
                        {
                            return stringBuilder.Append(" and ").Append("Titolo = " + "'" + Title.Text + "'");
                        }
                        else
                        {
                            return stringBuilder.Append("WHERE ").Append("Titolo = " + "'" + Title.Text + "'");
                        }
                    }
                    else if (Checked(FirstLettersCheckbox))
                    {
                        if (stringBuilder.ToString().Contains("WHERE"))
                        {
                            return stringBuilder.Append(" and ").Append("Titolo LIKE " + "'" + Title.Text + "%'");
                        }
                        else
                        {
                            return stringBuilder.Append("WHERE ").Append("Titolo LIKE " + "'" + Title.Text + "%'");
                        }
                    }
                    else
                    {
                        if (stringBuilder.ToString().Contains("WHERE"))
                        {
                            return stringBuilder.Append(" and ").Append("Titolo LIKE " + "'%" + Title.Text + "%'");
                        }
                        else
                        {
                            return stringBuilder.Append("WHERE ").Append("Titolo LIKE " + "'%" + Title.Text + "%'");
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

        private StringBuilder GenerateORDERBYFromDDL(StringBuilder stringBuilder)
        {
            stringBuilder.Append(" ORDER BY " + OrderCriteriaDDL.SelectedValue);

            return stringBuilder;
        }

        //DROPDOWN LISTS
        private void RiempiDropDownListOrd(DropDownList dropDownList, DataSet source)
        {
            if (dropDownList != null)
            {
                dropDownList.Visible = true;

                dropDownList.Items.Clear();

                foreach (DataColumn column in source.Tables[0].Columns)
                {
                    ListItem annoItem = new ListItem();
                    annoItem.Text = annoItem.Value = column.Caption;

                    dropDownList.Items.Add(annoItem);
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
                        ListItem meseItem = new ListItem();
                        meseItem.Value = i.ToString();
                        meseItem.Text = ((Mesi)i).ToString();

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

        private void RimuoviTutteDaConsoleDropdown()
        {
            foreach (ListItem item in ConsoleSelection.Items)
            {
                if (item.Value.Equals("ANY"))
                {
                    item.Enabled = false;
                    break;
                }
            }
        }

        private void AggiungiTutteAConsoleDropDown()
        {
            foreach (ListItem item in ConsoleSelection.Items)
            {
                if (item.Value.Equals("ANY"))
                {
                    item.Enabled = true;
                    break;
                }
            }

            ConsoleSelection.SelectedIndex = 0;
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
            ListItem anyItem = new ListItem();

            anyItem.Value = "ANY";
            anyItem.Text = "Tutti";

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

        private String RiceviTesto(TextBox tbox)
        {
            if (EsisteControl(tbox))
            {
                return tbox.Text;
            }
            else
            {
                return "";
            }
        }

        private void SettaTextbox(TextBox tbox, string outputText)
        {
            if (EsisteControl(tbox))
            {
                tbox.Text = outputText;
            }
        }

        private void ResettaTextbox(params TextBox[] tboxes)
        {
            if(EsistonoControls(tboxes))
            {
                foreach(TextBox tbox in tboxes)
                {
                    if (EsisteControl(tbox))
                    {
                        tbox.Text = "";
                    }
                }
            }
        }

        //MODES
        public void SwitchMode(object sender, EventArgs e)
        {
            if (IsInModifyMode())
            {
                SetModifyMode();
            }
            else
            {
                SetQueryMode();
            }
        }

        private void SetQueryMode()
        {
            CambiaVisibilitaControls(true, TrovaControlsPerClasseCss("QueryExclusive"));
            CambiaVisibilitaControls(false, TrovaControlsPerClasseCss("InsertionExclusive"));

            CambiaVisibilitaControls(Checked(YearInterval), YearTo);
            CambiaVisibilitaControls(Checked(MonthInterval), MonthTo);
            CambiaVisibilitaControls(Checked(DayInterval), DayTo);
            CambiaVisibilitaControls(Checked(HourInterval), HourTo);

            CambiaVisibilitaControls(false, FirstLettersLabel);
            CambiaVisibilitaControls(Unchecked(exactMatchCheckBox), FirstLettersCheckbox);

            CambiaVisibilitaControls(Unchecked(FirstLettersCheckbox), Title, TitleLabel, exactMatchCheckBox);
            CambiaVisibilitaControls(Checked(FirstLettersCheckbox), FirstLettersLabel);

            ConfiguraLabel(TitleLabel, Checked(exactMatchCheckBox), "Titolo esatto: ", "Titolo: ");

            AggiungiTutteAConsoleDropDown();
        }

        private void SetModifyMode()
        {
            CambiaVisibilitaControls(false, TrovaControlsPerClasseCss("QueryExclusive"));
            CambiaVisibilitaControls(true, TrovaControlsPerClasseCss("InsertionExclusive"));

            CambiaVisibilitaControls(true, Title, TitleLabel);

            ConfiguraLabel(TitleLabel, true, "Titolo: ");

            RimuoviTutteDaConsoleDropdown();
        }

        private bool IsInModifyMode()
        {
            return ModeCheckbox.Checked;
        }

        private bool IsInQueryMode()
        {
            return !ModeCheckbox.Checked;
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
                throw new NullControlException("L'insieme di controls risulta inesistente");
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
                throw new NullControlException("Il control risulta inesistente");
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
                throw new NullUIParameterException("Il parametro \"classeCss\" non è valido");
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
    }
}