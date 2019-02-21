using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ACW = Autodesk.Connectivity.WebServices;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace CustomNumbering
{
    /// <summary>
    /// Interaction logic for SchemeParameters.xaml
    /// </summary>
    public partial class SchemeFields : Window
    {
        public SchemeFields()
        {
            InitializeComponent();
        }

        public SchemeFields(VDF.Vault.Currency.Connections.Connection connection, long schemeId)
        {
            InitializeComponent();

            ACW.NumSchm Scheme = connection.WebServiceManager.NumberingService.GetNumberingSchemeById(schemeId);

            this.Title += Scheme.Name;

            m_schemeParametersGrid.ItemsSource = Scheme.FieldArray;
        }
    }
}
