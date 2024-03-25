#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.IO;

#endregion

namespace M3.QA
{
    #region Connection Config

    /// <summary>
    /// The QA Cord SqlServer database config.
    /// </summary>
    public class QACordDbConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public QACordDbConfig() : base()
        {
            ServerName = "localhost";
            DatabaseName = "QACord";
            Authentication = 0;
            UserName = "sa";
            Password = "winnt123";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets ServerName or IP address.
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// Gets or sets DatabaseName.
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// Gets or sets Authentication mode.
        /// </summary>
        public int Authentication { get; set; }
        /// <summary>
        /// Gets or sets User Name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }

        #endregion
    }

    #endregion

    #region M3 QA DbServer

    /// <summary>
    /// M3 QA Db Server (SqlServer).
    /// </summary>
    public class DbServer : NSingelton<DbServer>
    {
        #region Internal Variables

        private NDbConnection _connection;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DbServer() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DbServer()
        {
            Shutdown();
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets local Configs folder path name.
        /// </summary>
        private static string ConfigFolder
        {
            get
            {
                string localFilder = Folders.Combine(
                    Folders.Assemblies.CurrentExecutingAssembly, "Configs");

                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }

        #endregion

        #region Private Methods

        private bool ConfigExists(string filename)
        {
            string configPath = Path.Combine(NJson.AppPath, "Configs");
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            string fullFileName = Path.Combine(configPath, filename);
            return File.Exists(fullFileName);
        }

        private QACordDbConfig GetConfig()
        {
            QACordDbConfig cfg;

            string fileName = Path.Combine(ConfigFolder, "QACordDbServer.json");
            if (!ConfigExists(fileName))
            {
                // create new one and save.
                cfg = new QACordDbConfig();
                NJson.SaveToFile(cfg, fileName, false);
            }

            cfg = NJson.LoadFromFile<QACordDbConfig>(fileName);

            if (null == cfg)
            {
                // create new one and save.
                cfg = new QACordDbConfig();
                //NJson.SaveToFile(cfg, fileName, false);
            }
            return cfg;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start.
        /// </summary>
        public void Start()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (null == Db)
            {
                lock (typeof(DbServer))
                {
                    try
                    {
                        var config = new SqlServerConfig();
                        var jCfg = GetConfig();
                        // M3 Code
                        config.DataSource.ServerName = jCfg.ServerName;
                        config.DataSource.DatabaseName = jCfg.DatabaseName;

                        config.Security.Authentication = (jCfg.Authentication == 0) ?
                            AuthenticationMode.Server : AuthenticationMode.Windows;
                        config.Security.PersistSecurity = PersistSecurityMode.Default;

                        config.Security.UserName = jCfg.UserName;
                        config.Security.Password = jCfg.Password;

                        _connection = new NDbConnection();
                        _connection.Config = config;

                        _connection.Connect();
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                        _connection = null;

                        OnConectError.Call(this, EventArgs.Empty);
                    }
                    if (null != _connection && null != _connection.DbConnection)
                    {
                        OnConnected.Call(this, EventArgs.Empty);
                    }
                    else
                    {
                        Shutdown();

                        OnConectError.Call(this, EventArgs.Empty);
                    }
                }
            }
        }
        /// <summary>
        /// Shutdown.
        /// </summary>
        public void Shutdown()
        {
            if (null != _connection)
            {
                _connection.Disconnect();
            }
            _connection = null;

            OnDisconnected.Call(this, EventArgs.Empty);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets IDbConnection.
        /// </summary>
        public IDbConnection Db
        {
            get { return (null != _connection) ? _connection.DbConnection : null; }
        }
        /// <summary>
        /// Gets is database connected.
        /// </summary>
        public bool Connected
        {
            get { return null != _connection && null != _connection.DbConnection && _connection.IsConnected; }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// OnConnected event.
        /// </summary>
        public event EventHandler OnConnected;
        /// <summary>
        /// OnDisconnected event.
        /// </summary>
        public event EventHandler OnDisconnected;
        /// <summary>
        /// OnConectError event.
        /// </summary>
        public event EventHandler OnConectError;

        #endregion
    }

    #endregion
}
