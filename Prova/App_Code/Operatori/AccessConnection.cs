using System.Data;
using System;
using System.Data.OleDb;
using System.Data.Common;

namespace GameCompletionManager
{

    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public  class ConnessioneAccess : DbConnection
    {
        readonly string PROVIDERTITLE = "Provider";
        readonly string DATASOURCETITLE = "Data Source";

        private string provider;
        private string dataSource;
        private string connectionString;

        public OleDbTransaction Transazione;
        private bool isInTransaction = false;

        public OleDbConnection connection;

        public ConnessioneAccess(string dataProvider,string dataSourceName)
        {
            if(NonNullParameters(dataProvider,dataSourceName))
            {
                SetProvider(dataProvider);
                SetDataSource(dataSourceName);

                connectionString = BuildConnectionString();

                connection = new OleDbConnection(connectionString);
            }
        }

        public bool ApriConnessione()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public  bool ChiudiConnessione()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                    Reset();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private  string BuildConnectionString()
        {
            return PROVIDERTITLE + "=" + GetProvider() + ";" + DATASOURCETITLE + "=" + GetDataSource();
        }

        public  void SetProvider(string dataProvider)
        {
            provider = dataProvider;
        }

        public  void SetDataSource(string dataSourceName)
        {
            dataSource = dataSourceName;
        }

        public  string GetProvider()
        {
            return provider;
        }

        public  string GetDataSource()
        {
            return dataSource;
        }

        private  void SetTransactionState(bool state)
        {
            isInTransaction = state;
        }

        private  bool IsInTransactionState()
        {
            return isInTransaction;
        }

        public override string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }
        }

        public override string Database
        {
            get
            {
                return connection.Database;
            }
        }

        public override string DataSource
        {
            get
            {
                return dataSource;
            }
        }

        public override string ServerVersion
        {
            get
            {
                return connection.ServerVersion;
            }
        }

        public override ConnectionState State
        {
            get
            {
                return connection.State;
            }
        }

        private  bool NonNullParameters(params string[] parameters)
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

        private  void Reset()
        {
            provider = null;
            dataSource = null;
            connectionString = null;

            isInTransaction = false;

            connection = null;
        }

        public override void Close()
        {
            connection.Close();
        }

        public override void ChangeDatabase(string databaseName)
        {
            connection.ChangeDatabase(databaseName);
        }

        public override void Open()
        {
            connection.Open();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            Transazione = connection.BeginTransaction();
            SetTransactionState(true);

            return Transazione;
        }

        protected override DbCommand CreateDbCommand()
        {
            throw new NotImplementedException();
        }
    }
}