using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrizione di riepilogo per Class1
/// </summary>
public static class Manager
{
    public static void SegnalaEccezione(Exception ex)
    {
        GestoreEccezioni.GestisciEccezione(ex);
    }

}