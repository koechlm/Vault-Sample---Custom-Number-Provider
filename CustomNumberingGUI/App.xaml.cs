using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using VDF = Autodesk.DataManagement.Client.Framework;

namespace CustomNumbering
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            VDF.Vault.Forms.Settings.LoginSettings settings = new VDF.Vault.Forms.Settings.LoginSettings();
            VDF.Vault.Currency.Connections.Connection Connection = VDF.Vault.Forms.Library.Login(settings);

            if (Connection == null)
            {
                Application.Current.Shutdown();
                return;
            }

            new MainWindow(Connection).Show();
        }
    }
}
