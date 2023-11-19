using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

/// <summary>
/// Descrizione di riepilogo per Class1
/// </summary>
public static class GestoreEccezioni
{
    public static void GestisciEccezione(Exception ex)
    {
        Debug.WriteLine("ECCEZIONE: " + ex.GetType().ToString());
        Debug.WriteLine("MESSAGGIO: " + ex.Message);
        Debug.WriteLine("STACK TRACE: " + ex.StackTrace);
    }
}