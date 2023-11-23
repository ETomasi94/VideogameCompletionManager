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
    private static Boolean debug = true; 

    public static void GestisciEccezione(Exception ex)
    {
        Debug.WriteLine("ECCEZIONE: " + ex.GetType().ToString());
        Debug.WriteLine("MESSAGGIO: " + ex.Message);
        Debug.WriteLine("STACK TRACE: " + ex.StackTrace);
    }

    public static void CertificaEccezione(Exception e)
    {
        if(e != null && debug)
        {
           
        }
    }
}