using System.Data.Common;
using System.Data;
using System;
using System.Data.OleDb;

public static class DbConnectionUtils
    {
        public static OleDbCommand CreateStoredProcedureCommand(string storedProcedure, params OleDbParameter[] parameters)
        {
            OleDbCommand cmd = CreateCommand(CommandType.StoredProcedure, storedProcedure, parameters);

            return cmd;
        }

        public static  OleDbCommand CreateCommand()
        {
            return new OleDbCommand();
        }

        public static OleDbCommand CreateCommand(CommandType commandType)
        {
            OleDbCommand command = CreateCommand();
            command.CommandType = commandType;

            return command;
        }

        public static OleDbCommand CreateCommand(CommandType commandType, string commandText)
        {
            OleDbCommand command = CreateCommand(commandType);
            command.CommandText = commandText;

            return command;
        }

        public static OleDbCommand CreateCommand(CommandType commandType, params DbParameter[] dbParameters)
        {
            OleDbCommand command = CreateCommand(commandType);
            command.Parameters.AddRange(dbParameters);

            return command;
        }

        public static OleDbCommand CreateCommand(CommandType commandType, string commandText, params DbParameter[] dbParameters)
        {
            OleDbCommand command = CreateCommand(commandType, commandText);
            command.Parameters.AddRange(dbParameters);

            return command;
        }

        public static OleDbParameter CreateIntegerReturnParameter(string name, DbType type)
        {
            OleDbParameter result = new OleDbParameter(name, type);
            result.Direction = ParameterDirection.ReturnValue;

            return result;
        }

        public static bool HasValue(OleDbParameter parameter)
        {
            if (NotNullCheck(parameter))
            {
                return NotNullCheck(parameter.Value) && NotEmpty(parameter.Value.ToString());
            }
            else
            {
                return false;
            }
        }

        public static bool NotEmpty(string s)
        {
            if (NotNullCheck(s))
            {
                return !(s.Equals(String.Empty));
            }
            else
            {
                return false;
            }
        }

        public static bool NotNullCheck(params Object[] functionParameters)
        {
            bool validParameters = true;

            foreach (Object functionParameter in functionParameters)
            {
                if (functionParameter == null)
                {
                    throw new ArgumentNullException("Il parametro " + functionParameter.ToString() + " non esiste");
                }
            }

            return validParameters;
        }
    }