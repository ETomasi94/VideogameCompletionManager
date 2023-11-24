using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace GameCompletionManager
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrdinaDropDownList(ConsoleSelection);
            }
        }

        public void QueryButton_Click(Object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ActualDateButton_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;


            SettaTextbox(YearFrom, dateTime.Year.ToString());
            SettaTextbox(MonthFrom, dateTime.Month.ToString());
            SettaTextbox(DayFrom, dateTime.Day.ToString());
            SettaTextbox(HourFrom, dateTime.Hour.ToString());
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

            ConfiguraTextbox(ReleaseYearFrom, ReleaseYearInterval.Checked, "Da");
        }


        public void YearInterval_OnCheckedChange(object sender, EventArgs e)
        {
            CambiaVisibilitaControls(YearInterval.Checked, YearTo);

            ConfiguraTextbox(YearFrom, YearInterval.Checked, "Da");
        }

        public void MonthInterval_OnCheckedChange(Object sender, EventArgs e)
        {
            CambiaVisibilitaControls(MonthInterval.Checked, MonthTo);

            ConfiguraTextbox(MonthFrom, MonthInterval.Checked, "Da");
        }

        public void DayInterval_OnCheckedChange(Object sender, EventArgs e)
        {
            CambiaVisibilitaControls(DayInterval.Checked, DayTo);

            ConfiguraTextbox(DayFrom, DayInterval.Checked, "Da");
        }

        public void HourInterval_OnCheckedChange(Object sender, EventArgs e)
        {
            CambiaVisibilitaControls(HourInterval.Checked, HourTo);

            ConfiguraTextbox(HourFrom, HourInterval.Checked, "Da");
        }

        public void InsertionButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SwitchMode(object sender, EventArgs e)
        {
            if (!QueryMode())
            {
                CambiaVisibilitaControls(false, TrovaControlsPerClasseCss("QueryExclusive"));
                CambiaVisibilitaControls(true, TrovaControlsPerClasseCss("InsertionExclusive"));
                RimuoviTutteDaConsoleDropdown();
            }
            else
            {
                CambiaVisibilitaControls(true, TrovaControlsPerClasseCss("QueryExclusive"));
                CambiaVisibilitaControls(false, TrovaControlsPerClasseCss("InsertionExclusive"));
                AggiungiTutteAConsoleDropDown();
            }
        }

        public void UploadButton_Click(Object sender,EventArgs e)
        {
                Debug.WriteLine(ConfigurationManager.ConnectionStrings["AccessConnection"].ConnectionString);
        }

        private WebControl[] TrovaControlsPerClasseCss(string classeCss)
        {
            return Page.Form.Controls.OfType<WebControl>().Where(c => c.CssClass.Equals(classeCss)).ToArray();
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

        private Boolean QueryMode()
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