using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public static class GestoreDati
    {

        public static DataSet GeneraDataSet(string dataProvider,string dataSource,string SQLCommand)
        {
            DataSet queryResult = new DataSet();

            try
            {
                using(ConnessioneAccess connessione = new ConnessioneAccess(dataProvider,dataSource))
                {
                    queryResult = ScriviSuDataSet(connessione, SQLCommand);
                }
            }
            catch (Exception ex) 
            {
                GestoreEccezioni.StampaEccezione(ex);
            }

             return queryResult;        
        }
        
        public static DataTable GeneraTabella(string dataProvider, string dataSource, string SQLCommand)
        {
            DataTable queryResult = new DataTable();

            try
            {
                using (ConnessioneAccess connessione = new ConnessioneAccess(dataProvider, dataSource))
                {
                    queryResult = ScriviSuTabella(connessione, SQLCommand);
                }
            }
            catch (Exception ex) 
            {
                GestoreEccezioni.StampaEccezione(ex);
            }

            return queryResult;
        }

        private static DataSet ScriviSuDataSet(ConnessioneAccess connessione,string command) 
        {
            DataSet ds = new DataSet();

            OleDbCommand cmd = new OleDbCommand(command, connessione.connection);

            connessione.ApriConnessione();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            adapter.Fill(ds);

            return ds;
        }

        private static DataTable ScriviSuTabella(ConnessioneAccess connessione,string command)
        {
            DataTable table = new DataTable();

            OleDbCommand cmd = new OleDbCommand(command, connessione.connection);

            connessione.ApriConnessione();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            adapter.Fill(table);

            return table;
        }
    }
}