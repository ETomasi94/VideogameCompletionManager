using System;
using System.Diagnostics;
using System.Drawing;

static class StampanteDebug
{
    private static string SPAZIO = " ";
    private static string TAB = "\t";
    private static string RITORNO = "\n";
    private static string DUEPUNTI = ":";
    private static string HYPHEN = "-";
    private static string UNDERSCORE = "_";
    private static string SLASH = "/";
    private static string BACKSLASH = "\\";

    private static int CERT = 0;
    private static int LIV = 0;
    private static int MOD = 0;
    private static string TIP = "NON_DEFINITO";

    private static string LogPath = "/Logs";

    public static void StampaEccezione(Exception e)
    {

    }


    public static void SetCert(int cert)
    {
        CERT = cert;
    }

    public static void SetLiv(int liv)
    { 
        LIV = liv; 
    }

    public static void SetMod(int mod)
    {
        MOD = mod;
    }

    public static void SetTip(string tip)
    {
        TIP = tip;
    }

    private static string Valore(Type enumType,int val)
    {
       return Enum.GetName(enumType,val);
    }

    private static void Reset()
    {
        CERT = 0;
        LIV = 0;
        MOD = 0;
        TIP = "NON_DEFINITO";
    }

    private static string ComponiStringa(string delimiter,params string[] strings)
    {
        return string.Join(delimiter, strings);  
    }

    private static void Stampa(string s)
    {
        Debug.WriteLine(s); 
    }
}