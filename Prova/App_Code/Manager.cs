﻿using System;
using System.Data;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public static class Manager
    {
        public static DataSet RichiediQuery(string dataProvider,string dataSource)
        {
            throw new NotImplementedException();
        }

        public static void SegnalaEccezione(Exception ex)
        {
            GestoreEccezioni.StampaEccezione(ex);
        }

    }
}