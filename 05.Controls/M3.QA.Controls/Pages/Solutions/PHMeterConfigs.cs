#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;
using System.Linq.Expressions;

using NLib.IO;
using System.IO;

#endregion

namespace NLib.Serial
{
    public class PHMeterConfig
    {
        public PHMeterConfig()
        {
            Device1 = new SerialPortConfig();
            Device1.DeviceName = "PHMETER - 1";
            Device1.PortName = "COM1";
            Device1.BaudRate = 9600;
            Device1.Parity = Parity.None;
            Device1.StopBits = StopBits.One;
            Device1.DataBits = 0;
            Device1.Handshake = Handshake.None;
            /*
            Device2 = new SerialPortConfig();
            Device2.DeviceName = "PHMETER - 2";
            Device2.PortName = "COM2";
            Device2.BaudRate = 9600;
            Device2.Parity = Parity.None;
            Device2.StopBits = StopBits.One;
            Device2.DataBits = 0;
            Device2.Handshake = Handshake.None;
            */
        }

        #region Private Methods

        /// <summary>
        /// Gets application config path name.
        /// </summary>
        private static string AppConfigPath
        {
            get { return Path.Combine(Folders.Assemblies.CurrentExecutingAssembly, "Configs"); }
        }

        private static bool ConfigExists(string filename)
        {
            string configPath = ConfigFolder;
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            string fullFileName = Path.Combine(configPath, filename);
            return File.Exists(fullFileName);
        }

        private static string ConfigFolder
        {
            get { return Path.Combine(AppConfigPath, "Devices"); }
        }

        private static string ConfigFileName
        {
            get { return "PHMeterConfig.config.json"; }
        }

        #endregion

        #region Public Properties

        public SerialPortConfig Device1 { get; set; }
        //public SerialPortConfig Device2 { get; set; }

        #endregion

        #region Static Methods

        public static PHMeterConfig GetConfig()
        {
            PHMeterConfig cfg;

            var folder = ConfigFolder;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Path.Combine(ConfigFolder, ConfigFileName);
            if (!ConfigExists(fileName))
            {
                // create new one and save.
                cfg = new PHMeterConfig();
                NJson.SaveToFile(cfg, fileName, false);
            }

            cfg = NJson.LoadFromFile<PHMeterConfig>(fileName);

            if (null == cfg)
            {
                // create new one and save.
                cfg = new PHMeterConfig();
                //NJson.SaveToFile(cfg, fileName, false);
            }
            return cfg;
        }

        #endregion
    }
}
