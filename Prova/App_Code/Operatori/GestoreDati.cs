using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;


namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public static class GestoreDati
    {

        #region Variabili di classe
        readonly static int MAX_TENTATIVI = 3;

        private static string connectionString = null;
        private static OleDbConnection connessione = null;

        private static bool inTransazione = false;
        private static OleDbTransaction transazione = null;

        private static OleDbCommand comando = null;
        #endregion

        #region Metodi di gestione del database
        public static DataSet ADataSet(string connectionString, OleDbCommand inputCommand)
        {
            Installa(connectionString, inputCommand);

            DataSet result;

            result = LeggiInDataSetAdapter();

            Reset();

            return result;
        }

        public static DataTable ATabella(string connectionString, OleDbCommand inputCommand)
        {
            Installa(connectionString, inputCommand);

            DataTable result = LeggiInTabella();

            Reset();

            return result;
        }

        public static int AFlag(string connectionString, OleDbCommand inputCommand)
        {
            Installa(connectionString, inputCommand);

            int result = LeggiInFlag();

            Reset();

            return result;
        }

        public static int Interagisci(string connectionString, OleDbCommand inputCommand)
        {
            Installa(connectionString, inputCommand);

            int result = InteragisciConDatabase();

            Reset();

            return result;
        }

        public static int InteragisciERestituisciIntero(string connectionString,
                                                        OleDbParameter returnParameter,
                                                        OleDbCommand inputCommand)
        {
            Installa(connectionString, inputCommand);

            int result = InteragisciConDatabaseRestIntero(returnParameter);

            Reset();

            return result;
        }
        #endregion

        #region Metodi di gestione delle transazioni
        private static void InizioTransazione()
        {
            try
            {
                transazione = connessione.BeginTransaction();

                inTransazione = true;
            }
            catch (OleDbException)
            {
                RollbackTransaction();
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseException("Le transazioni parallele non sono supportate: " + ex.Message, ex);
            }
            catch (Exception e)
            {
                throw new DatabaseException("Errore non previsto durante l'avvio della transazione: " + e.Message, e);
            }
        }

        private static void CommittaTransazione()
        {
            try
            {
                transazione.Commit();

                inTransazione = false;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Non è stato possibile eseguire il commit della transazione: " + ex.Message, ex);
            }
            catch (Exception e)
            {
                throw new DatabaseException("Errore non previsto durante il commit della transazione: " + e.Message, e);
            }
        }

        private static void RollbackTransaction()
        {
            if (inTransazione)
            {
                try
                {
                    transazione.Rollback();

                    inTransazione = false;
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Non è stato possibile eseguire il rollback della transazione", ex);
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Errore non previsto durante il rollback della transazione", e);
                }
            }
        }
        #endregion

        #region Metodi di installazione del gestore
        private static void Installa(string connectionString, OleDbCommand inputCommand)
        {
            SettaStringaDiConnessione(connectionString);
            CreaConnessione();
            SettaComandoSQL(inputCommand);
            CollegaComandoEConnessione();
        }

        private static void SettaComandoSQL(OleDbCommand comandoSQL)
        {
            if (comandoSQL != null)
            {
                comando = comandoSQL;
            }
            else
            {
                throw new DatabaseException("Il comando SQL in input non è valido");
            }
        }

        private static void CollegaComandoETransazione()
        {
            comando.Transaction = transazione;

        }

        private static void CollegaComandoEConnessione()
        {
            comando.Connection = connessione;
        }

        private static void SettaStringaDiConnessione(string s)
        {
            if (s != null)
            {
                connectionString = s;
            }
            else
            {
                throw new InvalidInputException("La stringa con cui inizializzare la connessione non è valida");
            }
        }

        private static void CreaConnessione()
        {
            connessione = new OleDbConnection(connectionString);
        }
        #endregion

        #region Metodi di lettura dei dati
        private static DataSet LeggiInDataSetAdapter()
        {
            DataSet result = new DataSet();

            using (connessione)
            {
                try
                {
                    Connetti();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(comando.CommandText, connessione);

                    adapter.Fill(result);
                }
                catch (OleDbException e)
                {
                    throw e;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    comando.Parameters.Clear();

                    comando.Dispose();

                    Disconnetti();
                }
            }

            return result;
        }

        private static int LeggiInFlag()
        {
            OleDbDataReader rdr = null;

            int result = -1;

            using (connessione)
            {
                try
                {
                    Connetti();

                    rdr = comando.ExecuteReader();

                    while (rdr.Read())
                    {
                        result = rdr.GetInt32(0);
                    }
                }
                catch (OleDbException e)
                {
                    throw e;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    rdr.Close();

                    comando.Parameters.Clear();

                    comando.Dispose();

                    Disconnetti();
                }
            }

            return result;
        }

        private static DataTable LeggiInTabella()
        {
            OleDbDataReader rdr = null;
            DataTable result = new DataTable();

            using (connessione)
            {
                try
                {
                    Connetti();

                    rdr = comando.ExecuteReader();

                    result.Load(rdr);
                }
                catch (OleDbException e)
                {
                    throw e;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    rdr.Close();

                    comando.Parameters.Clear();

                    comando.Dispose();

                    Disconnetti();
                }
            }

            return result;
        }

        private static int InteragisciConDatabase()
        {
            int res = 0;

            using (connessione)
            {
                try
                {
                    Connetti();

                    InizioTransazione();

                    CollegaComandoETransazione();

                    res = comando.ExecuteNonQuery();

                    CommittaTransazione();
                }
                catch (OleDbException ex)
                {
                    RollbackTransaction();

                    throw ex;
                }
                catch (Exception e)
                {
                    RollbackTransaction();

                    throw new DatabaseException("Modifica al database non riuscita: " + e.Message, e);
                }
                finally
                {
                    comando.Parameters.Clear();

                    comando.Dispose();

                    transazione.Dispose();

                    Disconnetti();
                }
            }

            return res;
        }

        private static int InteragisciConDatabaseRestIntero(OleDbParameter returnParameter)
        {
            int res = 0;

            using (connessione)
            {
                try
                {
                    Connetti();

                    InizioTransazione();

                    CollegaComandoETransazione();

                    comando.Parameters.Add(returnParameter);

                    comando.ExecuteNonQuery();

                    res = (int)returnParameter.Value;

                    CommittaTransazione();
                }
                catch (OleDbException ex)
                {
                    RollbackTransaction();

                    throw ex;
                }
                catch (Exception e)
                {
                    RollbackTransaction();

                    throw new DatabaseException("Modifica al database non riuscita: " + e.Message, e);
                }
                finally
                {
                    comando.Parameters.Clear();

                    comando.Dispose();

                    transazione.Dispose();

                    Disconnetti();
                }
            }

            return res;
        }

        #endregion

        #region Metodi di connessione
        private static void Connetti()
        {
            int TENTATIVI_EFFETTUATI = 1;

            Debug.WriteLine("----\nINIZIO CONNESSIONE:"+ DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"));

            bool connessioneRiuscita = false;

            while (TENTATIVI_EFFETTUATI <= MAX_TENTATIVI && !connessioneRiuscita)
            {
                try
                {
                    if (!ConnessioneAperta())
                    {
                        connessione.Open();
                    }
                    else
                    {
                        break;
                    }
                }
                catch (SqlException sqlex)
                {
                    if (TENTATIVI_EFFETTUATI >= MAX_TENTATIVI)
                    {
                        throw sqlex;
                    }
                    else
                    {
                        TENTATIVI_EFFETTUATI++;
                    }
                }
                catch (InvalidOperationException iex)
                {
                    throw new DatabaseException("Eccezione non prevista nella connessione al database:" + iex.Message, iex);
                }
                catch (ConfigurationErrorsException cex)
                {
                    throw cex;
                }
            }

            Debug.WriteLine("Tentativi effettuati: "+TENTATIVI_EFFETTUATI);
        }

        private static void Disconnetti()
        {
            try
            {
                if (!ConnessioneChiusa())
                {
                    connessione.Close();
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                throw new DatabaseException("La disconnessione dal database non è riuscita:" + ex.Message, ex);
            }
            finally
            {
                Debug.WriteLine("FINE CONNESSIONE: "+DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"));
            }
        }
        private static bool ConnessioneChiusa()
        {
            return connessione.State == ConnectionState.Closed;
        }

        private static bool ConnessioneAperta()
        {
            return connessione.State == ConnectionState.Open;
        }
        #endregion

        #region Metodi generici
        private static void Reset()
        {
            connectionString = null;
            connessione = null;

            inTransazione = false;
            transazione = null;

            comando = null;
        }
        #endregion
    }
}