using System.Data.OleDb;

namespace GameCompletionManager
{

    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public class ConnessioneAccess
    {
        readonly static string PROVIDERTITLE = "Provider";
        readonly static string DATASOURCETITLE = "Data Source";

        private static string provider;
        private static string dataSource;
        private static string connectionString;

        public static OleDbTransaction Transazione;
        private static bool isInTransaction = false;

        public static OleDbConnection connection;

        public ConnessioneAccess(string dataProvider,string dataSourceName)
        {
            if(NonNullParameters(dataProvider,dataSourceName))
            {
                SetProvider(dataProvider);
                SetDataSource(dataSourceName);

                BuildConnectionString();
            }
        }

        public string BuildConnectionString()
        {
            return PROVIDERTITLE + "=" + GetProvider() + ";" + DATASOURCETITLE + "=" + GetDataSource();
        }

        public string GetConnectionString()
        {
            return connectionString;
        }

        public void SetProvider(string dataProvider)
        {
            provider = dataProvider;
        }

        public void SetDataSource(string dataSourceName)
        {
            dataSource = dataSourceName;
        }

        public string GetProvider()
        {
            return provider;
        }

        public string GetDataSource()
        {
            return dataSource;
        }

        private void SetTransactionState(bool state)
        {
            isInTransaction = state;
        }

        private bool IsInTransactionState()
        {
            return isInTransaction;
        }

        private bool NonNullParameters(params string[] parameters)
        { 
            foreach(string parameter in parameters)
            {
                if(parameter == null)
                {
                    return false;
                }
            }

            return true;
        }

        private void Reset()
        {
            provider = null;
            dataSource = null;
            connectionString = null;

            isInTransaction = false;

            connection = null;
        }
    }
}