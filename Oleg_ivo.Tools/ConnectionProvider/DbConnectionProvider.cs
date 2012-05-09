using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.ConnectionProvider
{
    ///<summary>
    /// Провайдер подключения к базе данных
    ///</summary>
    public class DbConnectionProvider
    {
        #region Singleton

        private static DbConnectionProvider _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static DbConnectionProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbConnectionProvider();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DbConnectionProvider" />.
        /// </summary>
        protected DbConnectionProvider()
        {
            _connectionStrings = new Dictionary<string, string>();
        }

        #endregion

        private readonly Dictionary<string, string> _connectionStrings;

        ///<summary>
        /// Строки подключения
        ///</summary>
        public string this[string connectionName]
        {
            get
            {
                return _connectionStrings[connectionName];
            } 
            set
            {
                if (value != null)
                {
                    if (_connectionStrings.ContainsKey(connectionName))
                    {
                        _connectionStrings[connectionName] = value;
                    }
                    else
                    {
                        _connectionStrings.Add(connectionName, value);
                    }
                }
                else if (_connectionStrings.ContainsKey(connectionName))
                {
                    _connectionStrings.Remove(connectionName);
                }
            } 
        }

        ///<summary>
        /// Строка подключения по умолчанию
        ///</summary>
        public string DefaultConnectionString
        {
            get { return this[""]; }
            set { this[""] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public IDbConnection GetConnection()
        {
            DbConnection connection = null;
            string connectionString = DefaultConnectionString;

            if (connectionString!=null)
            {
                if (connectionString.Contains("OLEDB"))
                {
                    connection = OleDbFactory.Instance.CreateConnection();
                }
                else
                {
                    connection = System.Data.SqlClient.SqlClientFactory.Instance.CreateConnection();
                }

                connection.ConnectionString = connectionString;
            }
            
            return connection;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="command"></param>
        public void OpenConnection(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            command.Connection = connection;
            if(connection.State!=ConnectionState.Open)
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Невозможно открыть соединение с БД. \n{0}", ex);
                    throw;
                }
        }

        ///<summary>
        /// 
        ///</summary>
        ///<param name="adapter"></param>
        public void OpenConnection(IDbDataAdapter adapter)
        {
            OpenConnection(adapter.SelectCommand);
            if (adapter.InsertCommand != null) adapter.InsertCommand.Connection = adapter.SelectCommand.Connection;
            if (adapter.UpdateCommand != null) adapter.UpdateCommand.Connection = adapter.SelectCommand.Connection;
            if (adapter.DeleteCommand != null) adapter.DeleteCommand.Connection = adapter.SelectCommand.Connection;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="command"></param>
        public void CloseConnection(IDbCommand command)
        {
            if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
        }

        ///<summary>
        /// 
        ///</summary>
        ///<param name="adapter"></param>
        public void CloseConnection(IDbDataAdapter adapter)
        {
            CloseConnection(adapter.SelectCommand);
        }

        /// <summary>
        /// Установить строку подключения из конфигурационного файла запускаемого приложения
        /// </summary>
        public void SetupConnectionStringFromConfigurationFile()
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                if (configuration.ConnectionStrings.ConnectionStrings["Default"] == null)
                {
                    configuration.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Default", "ConnectionStringValue"));
                }

                ConnectionStringSettings connectionString = configuration.ConnectionStrings.ConnectionStrings["Default"];
                DefaultConnectionString = connectionString.ConnectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}