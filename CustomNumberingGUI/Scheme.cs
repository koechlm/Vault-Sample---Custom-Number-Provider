using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACW = Autodesk.Connectivity.WebServices;

namespace CustomNumbering
{
    internal class Scheme
    {
        public Scheme() { }

        public Scheme(ACW.NumSchm scheme)
        {
            SchemeId = scheme.SchmID;
            Name = scheme.Name;
            FirstFieldType = scheme.FieldArray.First().FieldTyp.ToString();
            GeneratedNumber = "";
            ErrorMessage = "";
        }

        public long SchemeId { get; set; }
        public string Name { get; set; }
        public string FirstFieldType { get; set; }
        public string GeneratedNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}
