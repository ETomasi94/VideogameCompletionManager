﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using OpenQA.Selenium.DevTools.V117.Network;
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
                ResettaDropDownList(YearFrom,YearTo);
                RiempiDropDownListMesi(MonthFrom, MonthTo);
                RiempiDropDownListGiorni(DayFrom, DayTo);
                RiempiDropDownListOre(HourFrom, HourTo);
            }
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

        private string GenerateSqlCommand()
        {
            StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Videogiochi ORDER BY ID");

            stringBuilder = GenerateWHEREFromDDL(stringBuilder,YearFrom, YearTo, SQLFunzioniTime.YEAR);
            stringBuilder = GenerateWHEREFromDDL(stringBuilder,MonthFrom, MonthTo,SQLFunzioniTime.MONTH);
            stringBuilder = GenerateWHEREFromDDL(stringBuilder,DayFrom,DayTo,SQLFunzioniTime.DAY);

            Debug.WriteLine(stringBuilder.ToString());

            return stringBuilder.ToString();
        }

        private StringBuilder GenerateWHEREFromDDL(StringBuilder stringBuilder,DropDownList firstDDL,DropDownList secondDDL,SQLFunzioniTime timeFunction)
        {
            int timeValue1, timeValue2;
            string timeCriteria = ConvalidaEnum.GeneraFunzioneSQL(timeFunction, "Data");

            string result;

            if (IsANYSelected(firstDDL))
            {
                if (!IsANYSelected(secondDDL))
                {
                    timeValue1 = Int32.Parse(secondDDL.SelectedItem.Text);

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
                    timeValue1 = Int32.Parse(firstDDL.SelectedItem.Text);
                    timeValue2 = Int32.Parse(secondDDL.SelectedItem.Text);

                    result = (timeCriteria + " >= " + Math.Min(timeValue1,timeValue2) + " and " + timeCriteria + " <=" + Math.Max(timeValue1,timeValue2));
                }
                else
                {
                    timeValue1 = Int32.Parse(firstDDL.SelectedItem.Text);

                    result = (timeCriteria + " >= " + timeValue1);
                }
            }

            if(result.Equals(String.Empty))
            {
                return stringBuilder;
            }
            else
            {
                if(stringBuilder.ToString().Contains("WHERE"))
                {
                    return stringBuilder.Append(" and ").Append(result);
                }
                else
                {
                    return stringBuilder.Append("WHERE ").Append(result);
                }
            }
        }

        private bool IsANYSelected(params DropDownList[] dropDownLists)
        {
            bool isForAll = true;

            foreach(DropDownList dropDownList in dropDownLists )
            {
                if (isForAll)
                {
                    isForAll = dropDownList.SelectedItem.Value.Equals("ANY");
                }
            }

            return isForAll;
        }

        public void ActualDateButton_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
        }

        public void FirstLettersCheckbox_OnCheckedChange(object sender, EventArgs e)
        {
            CambiaVisibilitaControls(!FirstLettersCheckbox.Checked, Title, TitleLabel, exactMatchCheckBox);
            CambiaVisibilitaControls(FirstLettersCheckbox.Checked, FirstLettersLabel, FirstLetters);
        }

        public void ExactMatchCheckBox_OnCheckedChange(object sender, EventArgs e)
        {
            CambiaVisibilitaControls(false, FirstLetters, FirstLettersLabel);
            CambiaVisibilitaControls(!exactMatchCheckBox.Checked, FirstLettersCheckbox);

            ConfiguraLabel(TitleLabel, exactMatchCheckBox.Checked, "Titolo esatto: ", "Titolo: ");
        }

        public void ReleaseYearInterval_OnCheckedChange(object sender, EventArgs e)
        {
            CambiaVisibilitaControls(ReleaseYearInterval.Checked, ReleaseYearTo);
        }

        public void YearInterval_OnCheckedChange(object sender, EventArgs e)
        {
            CambiaVisibilitaControls(YearInterval.Checked, YearTo);
        }

        public void MonthInterval_OnCheckedChange(Object sender, EventArgs e)
        {
            CambiaVisibilitaControls(MonthInterval.Checked, MonthTo);
        }

        public void DayInterval_OnCheckedChange(Object sender, EventArgs e)
        {
            CambiaVisibilitaControls(DayInterval.Checked, DayTo);
        }

        public void HourInterval_OnCheckedChange(Object sender, EventArgs e)
        {
            CambiaVisibilitaControls(HourInterval.Checked, HourTo);
        }

        public void InsertionButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SwitchMode(object sender, EventArgs e)
        {
            if (ModifyMode())
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

            CambiaVisibilitaControls(YearInterval.Checked, YearTo);
            CambiaVisibilitaControls(MonthInterval.Checked, MonthTo);
            CambiaVisibilitaControls(DayInterval.Checked, DayTo);
            CambiaVisibilitaControls(HourInterval.Checked, HourTo);

            CambiaVisibilitaControls(false, FirstLetters, FirstLettersLabel);
            CambiaVisibilitaControls(!exactMatchCheckBox.Checked, FirstLettersCheckbox);

            CambiaVisibilitaControls(!FirstLettersCheckbox.Checked, Title, TitleLabel, exactMatchCheckBox);
            CambiaVisibilitaControls(FirstLettersCheckbox.Checked, FirstLettersLabel, FirstLetters);

            ConfiguraLabel(TitleLabel, exactMatchCheckBox.Checked, "Titolo esatto: ", "Titolo: ");

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

        public void UploadButton_Click(Object sender,EventArgs e)
        {
            string dbPath = "C:\\Users\\enryr\\Desktop\\Test.accdb";
            string dataProvider = "Microsoft.ACE.OLEDB.12.0";
            string SqlCommand = "SELECT * FROM Videogiochi ORDER BY ID";

            DataSet source = Manager.RichiediQuery(dataProvider, dbPath,SqlCommand);

            int minYear = DateTime.Now.Year;
            int maxYear = DateTime.Now.Year;

            foreach (DataRow dr in source.Tables[0].Rows)
            {
                DateTime data = dr.Field<DateTime>("Data");

                minYear = Math.Min(minYear, data.Year);
                maxYear = Math.Max(maxYear, data.Year);
            }

            RiempiDropDownListAnni(minYear, maxYear,YearFrom,YearTo);
            RiempiDropDownListOrd(OrderCriteriaDDL, source);

            GrigliaVideogiochi.DataSource = source;
            GrigliaVideogiochi.DataBind();
        }

        private void RiconosciTipoQuery(string filePath)
        {
            string fileType; 
        }

        private WebControl[] TrovaControlsPerClasseCss(string classeCss)
        {
            return Page.Form.Controls.OfType<WebControl>().Where(c => c.CssClass.Equals(classeCss)).ToArray();
        }

        private void RiempiDropDownListOrd(DropDownList dropDownList,DataSet source)
        {
            if (dropDownList != null)
            {
                dropDownList.Visible = true;

                dropDownList.Items.Clear();

                foreach(DataColumn column in source.Tables[0].Columns)
                {
                    ListItem annoItem = new ListItem();
                    annoItem.Text = annoItem.Value = column.Caption;

                    dropDownList.Items.Add(annoItem);
                }
            }
        }

        private void RiempiDropDownListAnni(int annoMin,int annoMax, params DropDownList[] dropDownLists)
        {
            foreach (DropDownList dropDownList in dropDownLists)
            {
                if (dropDownList != null)
                {
                    dropDownList.Items.Clear();

                    dropDownList.Items.Add(CreateAnyElement());

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

                    dropDownList.Items.Add(CreateAnyElement());

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

                    dropDownList.Items.Add(CreateAnyElement());

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

                    dropDownList.Items.Add(CreateAnyElement());

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
                    dropDownList.Items.Add(CreateAnyElement());
                }
            }
        }

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

        private void SettaLabel(Label label, string outputText)
        {
            label.Text = outputText;
        }

        private void ResettaLabel(Label label)
        {
            label.Text = "";
        }

        private Boolean RiceviSpunta(CheckBox checkBox)
        {
            return checkBox.Checked;
        }

        private String RiceviTesto(TextBox tbox)
        {
            return tbox.Text;
        }

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

        private void SettaTextbox(TextBox tbox, string outputText)
        {
            tbox.Text = outputText;
        }

        private void ResettaTextbox(TextBox tbox)
        {
            tbox.Text = "";
        }

        private Boolean EsisteControl(WebControl control)
        {
            return (control != null);
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

        private ListItem CreateAnyElement()
        {
            ListItem anyItem = new ListItem();

            anyItem.Value = "ANY";
            anyItem.Text = "Tutti";

            return anyItem;
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

        private bool ModifyMode()
        {
            return ModeCheckbox.Checked;
        }

        private bool QueryMode()
        {
            return !ModeCheckbox.Checked;
        }

        private void CambiaVisibilitaControl(Boolean value, WebControl control)
        {
            if (control != null)
            {
                control.Visible = value;
            }
        }

        private void CambiaVisibilitaControls(Boolean value, params WebControl[] controls)
        {
            if (controls != null && controls.Length > 0)
            {
                foreach (WebControl control in controls)
                {
                    CambiaVisibilitaControl(value, control);
                }
            }
        }
    }
}