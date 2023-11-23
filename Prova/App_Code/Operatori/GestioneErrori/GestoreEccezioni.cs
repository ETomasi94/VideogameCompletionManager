using System;
using System.Diagnostics;


namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public static class GestoreEccezioni
    {
        public static void StampaEccezione(Exception ex)
        {
            Debug.WriteLine("ECCEZIONE: " + ex.GetType().ToString());
            Debug.WriteLine("MESSAGGIO: " + ex.Message);
            Debug.WriteLine("STACK TRACE: " + ex.StackTrace);
        }

        private static void CertificaEccezione(Exception e)
        {
            if (e != null)
            {
                if(e is PredictedException)
                {
                    StampanteDebug.SetCert((int)Certificati.PREVISTA);
                }
                else
                {
                    StampanteDebug.SetCert((int)Certificati.NON_PREVISTA);
                }
            }
        }
    }
}