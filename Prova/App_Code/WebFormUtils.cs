using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calendar = System.Web.UI.WebControls.Calendar;

public static class WebFormUtils
{
    public static IEnumerable<Control> RicercaRicorsiva(Control c, Func<Control, bool> verifica)
    {
        if (verifica(c)) { yield return c; }


        foreach (Control figlio in c.Controls)
        {
            if (verifica(figlio)) { yield return figlio; }
        }

        foreach (Control figlio in c.Controls)
        {
            foreach (Control corrispondente in RicercaRicorsiva(c, verifica))
            {
                yield return corrispondente;
            }
        }
    }
    public static void Visualizza(bool visibile, params Control[] controls)
    {
        if (NonNulli(controls))
        {
            foreach (Control control in controls)
            {
                control.Visible = visibile;
            }
        }
    }
    public static void Abilita(bool abilitato, params WebControl[] controls)
    {
        if (NonNulli(controls))
        {
            foreach (WebControl control in controls)
            {
                control.Enabled = abilitato;
            }
        }
    }
    public static void EseguiSuTutti<T>(Action<T> funzione, Control padre) where T : Control
    {
        if (NonNullo(padre))
        {
            foreach (Control c in padre.Controls)
            {
                if (c is T)
                {
                    funzione(c as T);
                }
            }
        }
        else
        {
            throw new ArgumentNullException("Il controllo padre da esaminare non esiste");
        }
    }
    public static void EseguiSuTutti<T>(Action<T> funzione, params Control[] controls) where T : Control
    {
        if (NonNulli(controls))
        {
            foreach (Control c in controls)
            {
                if (c is T)
                {
                    funzione(c as T);
                }
            }
        }
        else
        {
            throw new ArgumentNullException("L'insieme di controlli su cui eseguire la funzione o un suo controllo non esistono");
        }
    }
    public static void EseguiACondizione<T>(Action<T> funzione, bool condizione, Control padre) where T : Control
    {
        if (NonNullo(padre))
        {
            foreach (Control c in padre.Controls)
            {
                if (c is T && condizione)
                {
                    funzione(c as T);
                }
            }
        }
        else
        {
            throw new ArgumentNullException("Il controllo padre da esaminare non esiste");
        }
    }
    public static void EseguiSuTutti<T>(Action<T> funzione, bool condizione, params Control[] controls) where T : Control
    {
        if (NonNulli(controls))
        {
            foreach (Control c in controls)
            {
                if (c is T && condizione)
                {
                    funzione(c as T);
                }
            }
        }
        else
        {
            throw new ArgumentNullException("L'insieme di controlli su cui eseguire la funzione o un suo controllo non esistono");
        }
    }
    public static SelectedDatesCollection IntervalloDiDate(Calendar calendario, DateTime dataInizio, DateTime dataFine)
    {
        if (NonNulli(calendario))
        {
            DeselezionaDateCalendario(calendario);

            if (!NonNulli(dataInizio, dataFine))
            {
                return calendario.SelectedDates;
            }

            if (Nullo(dataInizio))
            {
                calendario.SelectedDates.Add(dataFine);
            }

            if (Nullo(dataFine))
            {
                calendario.SelectedDates.Add(dataInizio);
            }

            if (dataInizio < dataFine)
            {
                int differenzaDiDate = DifferenzaTraDateInGiorni(dataInizio, dataFine);

                for (int i = 0; i <= differenzaDiDate; i++)
                {
                    DateTime dataIntervallo = GiornoDopo(dataInizio, i);
                    calendario.SelectedDates.Add(dataIntervallo);
                }
            }

            return calendario.SelectedDates;
        }
        else
        {
            throw new ArgumentNullException("Calendario inesistente o senza selezione di data");
        }
    }

    public static string LettereACaso(int numeroLettere, params string[] parola)
    {
        Random random = new Random();

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < numeroLettere; i++)
        {
            int ind = random.Next(parola.Length);

            stringBuilder.Append(parola[ind]);
        }

        return stringBuilder.ToString();
    }
    public static string Iniziali(int numeroLettere, string parola)
    {
        return parola.Substring(0, numeroLettere);
    }
    public static string Finali(int numeroLettere, string parola)
    {
        return parola.Substring(parola.Length - numeroLettere - 1, numeroLettere);
    }
    public static DateTime GiornoDopo(DateTime giorno, int numeroDiGiorni)
    {
        return giorno.Add(TimeSpan.FromDays(numeroDiGiorni));
    }
    public static DateTime GiornoPrima(DateTime giorno, int numeroDiGiorni)
    {
        return giorno.Add(TimeSpan.FromDays(-numeroDiGiorni));
    }
    public static int DifferenzaTraDateInGiorni(DateTime dataInizio, DateTime dataFine)
    {
        return (dataFine - dataInizio).Days;
    }
    public static SelectedDatesCollection IntervalloDiDate(Calendar calendario, DateTime dataScelta)
    {
        if (NonNulli(calendario, dataScelta))
        {
            DeselezionaDateCalendario(calendario);

            calendario.SelectedDates.Add(dataScelta);

            return calendario.SelectedDates;
        }
        else
        {
            throw new ArgumentNullException("Calendario inesistente o senza selezione di data");
        }
    }
    public static void DeselezionaDateCalendario(Calendar calendario)
    {
        if (NonNullo(calendario))
        {
            calendario.SelectedDates.Clear();
        }
    }

    public static ListViewDataItem EstraiPrimoElemento (ListView listView)
    {
        if (NonNullo(listView))
        {
            ListViewDataItem primoElemento = PrimoElemento(listView);

            listView.Items.RemoveAt(0);

            return primoElemento;
        }
        else
        {
            throw new ArgumentException("Listview non valida");
        }
    }

    public static bool EsistonoAttributi(DataTable tabella, params string[] attributi)
    {
        if (NonNulli(tabella, attributi))
        {
            if (tabella.Columns.Count > 0)
            {
                bool risultato = true;

                foreach (string attributo in attributi)
                {
                    risultato = risultato && tabella.Columns.Contains(attributo);
                }

                return risultato;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static int RicercaIndicePerValore(DataTable tabella, string attributo, object valore)
    {
        if(NonNulli(tabella, attributo, valore))
        {
            if(!Vuoto(tabella) && EsistonoAttributi(tabella, attributo))
            {
                foreach(DataRow riga in tabella.Rows)
                {
                    if (riga[attributo].Equals(valore))
                    {
                        return tabella.Rows.IndexOf(riga);
                    }
                }
            }

            return -1;
        }
        else
        {
            throw new ArgumentException("Tabella, attributo o valore non validi");
        }
    }

    public static ListViewDataItem RicercaElementoPerCondizione(ListView listView, Func<ListViewDataItem, bool> condizione)
    {
        if (NonNulli(listView, condizione))
        {
            foreach (ListViewDataItem elemento in listView.Items)
            {
                if (condizione(elemento))
                {
                    return elemento;
                }         
            }

            return null;
           
        }
        else
        {
            throw new ArgumentException("Listview o chiave non valide");
        }
    }

    public static int RicercaIndicePerCondizione(ListView listView, Func<ListViewDataItem,bool> condizione)
    {
        if (NonNulli(listView, condizione))
        {
            foreach (ListViewDataItem elemento in listView.Items)
            {
                if (condizione(elemento))
                {
                    return elemento.DataItemIndex;
                }
            }

            return -1;
        }
        else
        {
            throw new ArgumentException("Listview o chiave non valide");
        }
    }

    public static ListViewDataItem RicercaElementoPerChiave(ListView listView, string chiave)
    {
        if (NonNulli(listView, chiave))
        {
            foreach (ListViewDataItem elemento in listView.Items)
            {
                DataKey chiaveDati = ChiaveDatiElemento(listView, elemento);

                if (chiaveDati.Value.ToString().Equals(chiave))
                {
                    return elemento;
                }
            }

            return null;
        }
        else
        {
            throw new ArgumentException("Listview o chiave non valide");
        }
    }

    public static int RicercaIndicePerChiave(ListView listView, string chiave)
    {
        if(NonNulli(listView,chiave))
        {
            foreach(ListViewDataItem elemento in listView.Items)
            {
                DataKey chiaveDati = ChiaveDatiElemento(listView, elemento);

                if (chiaveDati.Value.ToString().Equals(chiave))
                {
                    return elemento.DataItemIndex;
                }
            }

            return -1;
        }
        else
        {
            throw new ArgumentException("Listview o chiave non valide");
        }
    }

    public static DataKey ChiaveDatiElemento(ListView listView, ListViewDataItem elemento)
    {
        return listView.DataKeys[elemento.DataItemIndex];
    }

    public static ListViewDataItem EstraiUltimoElemento  (ListView listView)
    {
        if (NonNullo(listView))
        {
            ListViewDataItem ultimoElemento = UltimoElemento(listView);

            listView.Items.RemoveAt(listView.Items.Count - 1);

            return ultimoElemento;
        }
        else
        {
            throw new ArgumentException("Listview non valida");
        }
    }

    public static ListViewDataItem PrimoElemento (ListView listView)
    {
        if (NonNullo(listView))
        {
            return listView.Items[0];
        }
        else
        {
            throw new ArgumentException("Listview non valida");
        }
    }

    public static ListViewDataItem UltimoElemento(ListView listView)
    {
        if (NonNullo(listView))
        {
            return listView.Items[listView.Items.Count - 1];
        }
        else
        {
            throw new ArgumentException("Listview non valida");
        }
    }

    public static ListViewDataItem ElementoSelezionato(ListView listView)
    {
        if (NonNullo(listView) && ListViewSelezionata(listView))
        {
            return listView.Items[listView.SelectedIndex];
        }
        else
        {
            throw new ArgumentException("Listview non valida o elemento non selezionato");
        }
    }

    public static void CancellaGridView(GridView gridView)
    {
        if (gridView != null)
        {
            DeselezionaGridView(gridView);

            gridView.DataSource = null;
        }
    }
    public static void CancellaListView(ListView listView)
    {
        if (listView != null)
        {
            DeselezionaListView(listView);

            listView.DataSource = null;
        }
    }
    public static void VisualizzaDati(MarshalByValueComponent dataComponent, DataBoundControl view)
    {
        if (dataComponent != null)
        {
            view.DataSource = dataComponent;
            view.DataBind();
        }
    }
    public static void DeselezionaGridView(GridView gridView)
    {
        if (NonNullo(gridView))
        {
            gridView.SelectedIndex = -1;
        }
    }
    public static void DeselezionaListView(ListView listView)
    {
        if (NonNullo(listView))
        {
            if (listView.SelectedIndex > -1)
            {
                listView.SelectedIndex = -1;
            }
        }
    }
    public static bool GridViewSelezionata(GridView gridView)
    {
        return NonNullo(gridView) && (gridView.SelectedIndex > -1);
    }
    public static bool ListViewSelezionata(ListView listView)
    {
        return NonNullo(listView) && (listView.SelectedIndex > -1);
    }
    public static string DeselezionaPrimoSelezionato(CheckBoxList checkBoxList)
    {
        string value = "-1";

        if (NonNullo(checkBoxList))
        {
            foreach (ListItem item in checkBoxList.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;

                    value = item.Value;

                    return value;
                }
            }
        }

        return value;
    }
    public static void Spunta(bool spunta, params CheckBoxList[] checkBoxLists)
    {
        if (NonNulli(checkBoxLists))
        {
            foreach (CheckBoxList checkBoxList in checkBoxLists)
            {
                foreach (ListItem item in checkBoxList.Items)
                {
                    item.Selected = spunta;
                }
            }
        }
    }
    public static void ConfrontaIDCheckBoxLista(bool spunta, List<int> lista, CheckBoxList checkBoxList)
    {
        if (NonNulli(lista, checkBoxList) && lista.Count > 0)
        {
            foreach (int valore in lista)
            {
                checkBoxList.Items.FindByValue(valore.ToString()).Selected = spunta;
            }
        }
    }
    public static void SpuntaACondizione(bool spunta,bool condizione,params CheckBox[] checkBoxes)
    {
        if (NonNulli(checkBoxes))
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (NonNullo(checkBox) && condizione)
                {
                    checkBox.Checked = spunta;
                }
            }
        }
    }
    public static void SpuntaPerID(bool spunta, string testo, params CheckBox[] checkBoxes)
    {
        if (NonNulli(testo, checkBoxes))
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (NonNullo(checkBox) && (checkBox.ID == testo))
                {
                    checkBox.Checked = spunta;
                }
            }
        }
    }
    public static void SpuntaPerTesto(bool spunta, string testo, params CheckBox[] checkBoxes)
    {
        if (NonNulli(testo, checkBoxes))
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (NonNullo(checkBox) && (checkBox.Text == testo))
                {
                    checkBox.Checked = spunta;
                }
            }
        }
    }
    public static void Spunta(bool spunta, params CheckBox[] checkBoxes)
    {
        if (NonNulli(checkBoxes))
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                checkBox.Checked = spunta;
            }
        }
    }
    public static List<int> Valori(CheckBoxList checkBoxList)
    {
        List<int> result = new List<int>();

        if (checkBoxList != null && !Vuoto(checkBoxList))
        {
            foreach (ListItem item in checkBoxList.Items)
            {
                result.Add(Convert.ToInt32(item.Value));
            }
        }

        return result;
    }
    public static List<int> ValoriSelezionati(CheckBoxList checkBoxList)
    {
        List<int> result = new List<int>();

        if (checkBoxList != null)
        {
            foreach (ListItem item in checkBoxList.Items)
            {
                if (item.Selected == true)
                {
                    result.Add(Convert.ToInt32(item.Value));
                }
            }
        }

        return result;
    }
    public static void EliminaSpaziHTML(params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                EliminaSpazioHTML(textBox);
            }
        }
    }
    public static void EliminaSpazioHTML(TextBox textBox)
    {
        if (NonNullo(textBox) && (textBox.Text.Equals("&nbsp;")))
        {
            Resetta(textBox);
        }
    }

    public static bool TestoVuoto(Button button)
    {
        bool result = true;

        if (NonNullo(button))
        {
            result = button.Text.Equals(String.Empty);
        }

        return result;
    }


    public static void Segna(Func<TextBox,bool> condizione, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach(TextBox textBox in textBoxes)
            {
                if(condizione(textBox))
                {
                    Segna(Color.LightGreen, Color.Black, textBox);
                }
                else
                {
                    Segna(Color.White, Color.Black, textBox);
                }
            }
        }
    }

    public static bool SegnaSeVuoti(params TextBox[] textBoxes)
    {
        bool result = false;

        if(NonNulli(textBoxes))
        {
            foreach(TextBox textBox in textBoxes)
            {
                if(Vuoto(textBox))
                {
                    Segna(Color.LightGreen, Color.Black,textBox);
                    result = true;
                }
                else
                {
                    Segna(Color.White, Color.Black, textBox);
                }
            }
        }

        return result;
    }

    public static void Segna(Color coloreSfondo, Color coloreTesto, Color coloreBordo, params TextBox[] textBoxes)
    {
        if(NonNulli(textBoxes))
        {
            foreach(TextBox textBox in textBoxes)
            {
                textBox.BackColor = coloreSfondo;

                textBox.BorderStyle = BorderStyle.Solid;
                textBox.BorderWidth = 1;
                textBox.BorderColor = coloreBordo;

                textBox.ForeColor = coloreTesto;
            }
        }
    }

    public static void Segna(Color coloreSfondo, Color coloreTesto, params TextBox[] textBoxes)
    {
        if(NonNulli(textBoxes))
        {
            foreach(TextBox textBox in textBoxes)
            {
                Segna(coloreTesto, textBox);

                textBox.BackColor = coloreSfondo;
            }
        }
    }

    public static void Segna(Color coloreTesto, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.ForeColor = coloreTesto;
            }
        }
    }

    

    public static bool Vuoti(params TextBox[] textBoxes)
    {
        bool result = false;

        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                result = textBox.Text.Equals(String.Empty) || textBox.Text.Length <= 0;

                if(result.Equals(true))
                {
                    return result;
                }
            }
        }

        return result;
    }
    public static bool Vuoto(TextBox textBox)
    {
        bool result = true;

        if (NonNullo(textBox))
        {
            result = textBox.Text.Equals(String.Empty) || textBox.Text.Length <= 0;
        }

        return result;
    }

    public static void Setta(bool condizione, params CheckBox[] checkBoxes)
    {
        if (NonNulli(checkBoxes))
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                checkBox.Checked = condizione;
            }
        }
    }

    public static void Setta(string testoInInput, Color coloreTesto, params Button[] buttons)
    {
        if (NonNulli(buttons))
        {
            foreach (Button button in buttons)
            {
                button.Text = testoInInput;

                button.ForeColor = coloreTesto;
            }
        }
    }
    public static void Setta(string testoInInput, params Button[] buttons)
    {
        if (NonNulliCheck(buttons))
        {
            foreach (Button button in buttons)
            {
                button.Text = testoInInput;
            }
        }
    }
    public static void Setta(string testoInInput, Color coloreTesto, Color coloreSfondo, bool grassetto, bool sottolinea, bool corsivo,params Label[] labels)
    {
        if (NonNulli(labels))
        {
            foreach(Label label in labels)
            {
                Setta(testoInInput, coloreTesto, coloreSfondo, label);

                label.Font.Bold = grassetto;
                label.Font.Underline = sottolinea;
                label.Font.Italic = corsivo;
            }
        }
    }
    public static void Setta(string testoInInput, Color coloreTesto, Color coloreSfondo, params Label[] labels)
    {
        if (NonNulli(labels))
        {
            foreach (Label label in labels)
            {
                Setta(testoInInput, coloreTesto, label);

                label.BackColor = coloreSfondo;
            }
        }
    }
    public static void Setta(string testoInInput, Color coloreTesto, params Label[] labels)
    {
        if (NonNulli(coloreTesto, labels))
        {
            foreach (Label l in labels)
            {
                if (NonNullo(l))
                {
                    l.ForeColor = coloreTesto;

                    Setta(testoInInput, l);
                }
            }
        }
    }
    public static void Setta(string testoInInput, params Label[] labels)
    {
        if (NonNulli(labels))
        {
            foreach (Label label in labels)
            {
                label.Text = testoInInput;
            }
        }
    }
    public static void Setta(string testoInInput, Color coloreTesto, Color coloreSfondo, bool grassetto, bool sottolinea, bool corsivo, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                Setta(testoInInput, coloreTesto, coloreSfondo, textBox);

                textBox.Font.Bold = grassetto;
                textBox.Font.Underline = sottolinea;
                textBox.Font.Italic = corsivo;
            }
        }
    }
    public static void Setta(string testoInInput, Color coloreTesto, Color coloreSfondo, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                Setta(testoInInput, coloreTesto, textBox);

                textBox.BackColor = coloreSfondo;
            }
        }
    }
    public static void Setta(string testoInInput, Color coloreTesto, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = testoInInput;

                textBox.ForeColor = coloreTesto;
            }
        }
    }
    public static void Setta(string testoInInput, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = testoInInput;
            }
        }
    }
    public static void Resetta(params Label[] labels)
    {
        if (NonNullo(labels))
        {
            foreach (Label l in labels)
            {
                if (NonNullo(l))
                {
                    Resetta(l);
                }
            }
        }
    }
    public static void Resetta(Label label)
    {
        if (NonNullo(label))
        {
            label.Text = String.Empty;
        }
    }
    public static void Resetta(TextBox textBox, Color coloreTesto, Color coloreSfondo, bool grassetto, bool sottolinea, bool corsivo)
    {
        if (NonNullo(textBox))
        {
            textBox.Text = String.Empty;

            textBox.BackColor = coloreSfondo;
            textBox.ForeColor = coloreTesto;

            textBox.Font.Bold = grassetto;
            textBox.Font.Underline = sottolinea;
            textBox.Font.Italic = corsivo;
        }
    }
    public static void Resetta(Color coloreTesto, Color coloreSfondo, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = String.Empty;

                textBox.BackColor = coloreSfondo;
                textBox.ForeColor = coloreTesto;
            }
        }
    }
    public static void Resetta(Color coloreTesto, params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = String.Empty;

                textBox.ForeColor = coloreTesto;
            }
        }
    }
    public static void Resetta(params TextBox[] textBoxes)
    {
        if (NonNulli(textBoxes))
        {
            foreach (TextBox t in textBoxes)
            {
                t.Text = String.Empty;
            }
        }
    }
    public static bool Selezionati(params CheckBoxList[] checkBoxLists)
    {
        bool selected = false;

        if (NonNulli(checkBoxLists))
        {
            foreach (CheckBoxList checkBoxList in checkBoxLists)
            {
                selected = Selezionati(checkBoxList);
            }
        }

        return selected;
    }
    private static bool Selezionati(CheckBoxList checkBoxList)
    {
        bool selected = false;

        if (NonNullo(checkBoxList))
        {
            foreach (ListItem item in checkBoxList.Items)
            {
                if (item.Selected == true)
                {
                    return true;
                }
            }
        }

        return selected;
    }

    public static bool Vuota(ListView listView)
    {
        if(NonNullo(listView))
        {
            return listView.Items.Count <= 0;
        }
        else
        {
            throw new ArgumentException("Listview non valida");
        }
    }

    public static bool Vuoto(DataTable table)
    {
        if (NonNullo(table))
        {
            return table.Rows.Count <= 0;
        }

        return true;
    }
    public static bool Valido(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }
    public static bool Vuoto(CheckBoxList checkBoxList)
    {
        return checkBoxList.Items.Count <= 0;
    }
    public static bool NonNulliCheck(params object[] objects)
    {
        if (NonNullo(objects))
        {
            foreach (object obj in objects)
            {
                if (Nullo(obj))
                {
                    throw new ArgumentNullException("Un oggetto dell'insieme non esiste");
                }
            }
        }
        else
        {
            throw new ArgumentNullException("L'insieme di oggetti passato come parametro non esiste");
        }

        return true;
    }
    public static bool NonNulli(params object[] objects)
    {
        bool result = false;

        if (NonNullo(objects))
        {
            result = true;

            foreach (object obj in objects)
            {
                if (NonNullo(obj))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
        }

        return result;
    }
    public static bool NonNullo(object obj)
    {
        return obj != null;
    }
    public static bool Nullo(object obj)
    {
        return obj == null;
    }
}