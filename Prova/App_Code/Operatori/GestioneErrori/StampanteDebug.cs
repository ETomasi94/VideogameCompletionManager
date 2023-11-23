using System;
using System.Diagnostics;

namespace GameCompletionManager
{
    static class StampanteDebug
    {
        readonly static string DELIMITER = "-----------------";

        private static int CERT = 0;
        private static int LIV = 0;
        private static int MOD = 0;
        private static string TIP = "NON_DEFINITO";

        private static DateTime ReportDate;

        public static void StampaEccezione(Exception e)
        {
            string reportHeader = GeneraHeader(e);
            string reportLog = GeneraLog(e);

            string completeReport = ComponiStringa("\n"+DELIMITER+"\n", reportHeader, reportLog);

            Stampa(completeReport);

            Reset();
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

        private static string GeneraHeader(Exception e)
        {
            ReportDate = DateTime.Now;

            return ComponiStringa("\n",
                DELIMITER,
                "DATA ED ORA: " + ReportDate.ToString("G"),
                "PREVISIONE ECCEZIONE: " + Valore(typeof(Certificati), CERT),
                "LIVELLO: " + Valore(typeof(Livelli), LIV),
                "MODULO: " + Valore(typeof(Moduli), MOD),
                "TIPO: " + TIP,
                "MESSAGGIO: " + e.Message);
        }

        private static string GeneraLog(Exception e)
        {
            return e.StackTrace;
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