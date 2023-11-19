using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void QueryButton_Click(Object sender, EventArgs e)
    {

    }

    public void FirstLettersCheckbox_OnCheckedChange(object sender, EventArgs e)
    {
        CambiaVisibilitaControls(!FirstLettersCheckbox.Checked,Title,TitleLabel,exactMatchCheckBox);
        CambiaVisibilitaControls(FirstLettersCheckbox.Checked, FirstLettersLabel, FirstLetters);
    }

    public void ExactMatchCheckBox_OnCheckedChange(object sender,EventArgs e)
    {
        CambiaVisibilitaControls(false, FirstLetters,FirstLettersLabel);
        CambiaVisibilitaControls(!exactMatchCheckBox.Checked, FirstLettersCheckbox);

        ConfiguraLabel(TitleLabel, exactMatchCheckBox.Checked, "Titolo esatto: ","Titolo: ");
    }

    public void ReleaseYearInterval_OnCheckedChange(object sender, EventArgs e)
    {
        CambiaVisibilitaControls(ReleaseYearInterval.Checked, ReleaseYearTo);

        ConfiguraTextbox(ReleaseYearFrom, ReleaseYearInterval.Checked, "Da");
    }


    public void YearInterval_OnCheckedChange(object sender, EventArgs e) 
    {
        CambiaVisibilitaControls(YearInterval.Checked,YearTo);

        ConfiguraTextbox(YearFrom, YearInterval.Checked,"Da");
    }

    public void MonthInterval_OnCheckedChange(Object sender, EventArgs e)
    {
        CambiaVisibilitaControls(MonthInterval.Checked, MonthTo);

        ConfiguraTextbox(MonthFrom, MonthInterval.Checked, "Da");
    }

    public void DayInterval_OnCheckedChange(Object sender,EventArgs e) 
    {
        CambiaVisibilitaControls(DayInterval.Checked, DayTo);

        ConfiguraTextbox(DayFrom, DayInterval.Checked, "Da");
    }

    public void InsertionButton_Click(object sender, EventArgs e)
    {

    }

    public void SwitchMode(object sender, EventArgs e)
    {
        if (ModeCheckbox.Checked)
        {
            CambiaVisibilitaControls(false, TrovaControlsPerClasseCss("QueryExclusive"));
            CambiaVisibilitaControls(true, TrovaControlsPerClasseCss("InsertionExclusive"));
        }
        else
        {
            CambiaVisibilitaControls(true, TrovaControlsPerClasseCss("QueryExclusive"));
            CambiaVisibilitaControls(false, TrovaControlsPerClasseCss("InsertionExclusive"));
        }
    }

    private void ConfiguraLabel(Label label, Boolean testoVisualizzato, string testoChecked,string testoNonChecked)
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

    private WebControl[] TrovaControlsPerClasseCss(string classeCss)
    {
        return Page.Form.Controls.OfType<WebControl>().Where(c => c.CssClass.Equals(classeCss)).ToArray();
    }

    private void ConfiguraLabel(Label label,Boolean testoVisualizzato,string testoDiOutput)
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

    private void ConfiguraTextbox(TextBox textBox,Boolean testoVisualizzato,string testoDiOutput)
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

    private void SettaTextbox(TextBox tbox,string outputText)
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

    private Boolean QueryMode()
    {
        return !ModeCheckbox.Checked;
    }

    private void CambiaVisibilitaControl(Boolean value,WebControl control)
    {
        if(control != null)
        {
            control.Visible = value;
        }
    }

    private void CambiaVisibilitaControls(Boolean value, params WebControl[] controls)
    {
        if(controls != null && controls.Length > 0)
        {
            foreach(WebControl control in controls) 
            {
                CambiaVisibilitaControl(value,control);
            }
        }
    }

}