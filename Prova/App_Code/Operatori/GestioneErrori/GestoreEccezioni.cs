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
            AnalizzaTipo(ex);
            CertificaEccezione(ex);
            ConvalidaLivello(ex);

            StampanteDebug.StampaEccezione(ex);
        }

        private static void AnalizzaTipo(Exception e)
        {
            StampanteDebug.SetTip(e.GetType().ToString());
        }

        private static void CertificaEccezione(Exception e)
        {
            if (Exists(e))
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

        private static void ConvalidaLivello(Exception e)
        {
            if(Exists(e)) 
            {
                if(e is DatabaseException || e is UnpredictedDatabaseException)
                {
                    StampanteDebug.SetLiv((int)Livelli.ACCESS);
                }
                else if(e is UserInterfaceException ||  e is UnpredictedUIException)
                {
                    StampanteDebug.SetLiv((int)Livelli.INTERFACCIA_UTENTE);
                }
                else if(e is SystemException || e is UnpredictedSystemException)
                {
                    StampanteDebug.SetLiv((int)Livelli.SISTEMA_CENTRALE);
                }
                else
                {
                    StampanteDebug.SetLiv((int)Livelli.NON_IDENTIFICATO);
                }
            }
        }

        private static bool Exists(Exception e)
        {
            return e != null;
        }
    }
}