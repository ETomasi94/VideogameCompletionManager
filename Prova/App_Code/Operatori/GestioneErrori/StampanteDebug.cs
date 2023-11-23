using System;
using System.Diagnostics;
using System.Drawing;

namespace GameCompletionManager
{
    static class StampanteDebug
    {
        readonly static string SPAZIO = " ";
        readonly static string TAB = "\t";
        readonly static string RITORNO = "\n";
        readonly static string DUEPUNTI = ":";
        readonly static string HYPHEN = "-";
        readonly static string UNDERSCORE = "_";
        readonly static string SLASH = "/";
        readonly static string BACKSLASH = "\\";

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

        private static string Valore(Type enumType, int val)
        {
            return Enum.GetName(enumType, val);
        }

        private static void Reset()
        {
            CERT = 0;
            LIV = 0;
            MOD = 0;
            TIP = "NON_DEFINITO";
        }

        private static string ComponiStringa(string delimiter, params string[] strings)
        {
            return string.Join(delimiter, strings);
        }

        private static void Stampa(string s)
        {
            Debug.WriteLine(s);
        }
    }
}