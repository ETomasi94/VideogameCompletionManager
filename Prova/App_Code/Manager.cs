using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public static class Manager
    {
        public static void SegnalaEccezione(Exception ex)
        {
            GestoreEccezioni.StampaEccezione(ex);
        }

    }
}