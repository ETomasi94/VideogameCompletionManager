﻿using OpenQA.Selenium.DevTools.V117.FedCm;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public static class GestoreDati
    {
        public static DataSet EseguiQuery(string dataProvider,string dataSource,string SQLCommand)
        {
            DataSet queryResult = new DataSet();

            try
            {
                using(ConnessioneAccess connessione = new ConnessioneAccess(dataProvider,dataSource))
                {
                    OleDbCommand cmd = new OleDbCommand(SQLCommand,connessione.connection);

                    connessione.ApriConnessione();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    adapter.Fill(queryResult);
                }
            }
            catch (Exception ex) 
            {
                GestoreEccezioni.StampaEccezione(ex);
            }

            return queryResult;
        }    
    }
}