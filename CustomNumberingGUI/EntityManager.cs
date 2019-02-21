using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACW = Autodesk.Connectivity.WebServices; 
using Autodesk.DataManagement.Client.Framework.Vault;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Entities;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;

using SQL = System.Data.SqlClient;

namespace CustomNumbering
{
    public static class EntityManager
    {
        public const long NoDefaultFileScheme = -1;

        public static string GetNumber(Connection conn, string entityClassId, long schemeId)
        {
            switch (entityClassId)
            {
                case EntityClassIds.ChangeOrders:
                    return GetChangeOrderNumber(conn, schemeId);
                case EntityClassIds.Items:
                    return GetItemNumber(conn, entityClassId);
                case EntityClassIds.Files:
                    return GetFileNumber(conn, schemeId);
                default:
                    break;
            }

            return "";
        }

        private static string GetFileNumber(Connection conn, long schemeId)
        {
            return conn.WebServiceManager.DocumentService.GenerateFileNumbers(new long[] { schemeId }, null).First();
        }

        private static string GetItemNumber(Connection conn, string entityClassId)
        {
            var Category = conn.WebServiceManager.CategoryService.GetCategoriesByEntityClassId(entityClassId, true).First();
            var Item = conn.WebServiceManager.ItemService.AddItemRevision(Category.Id);

            conn.WebServiceManager.ItemService.UpdateAndCommitItems(new ACW.Item[] { Item });
            var NewItem = conn.WebServiceManager.ItemService.GetItemsByIds(new long[] { Item.Id }).First();

            conn.WebServiceManager.ItemService.DeleteItems(new long[] { NewItem.MasterId });

            return NewItem.ItemNum;
        }

        private static string GetChangeOrderNumber(Connection conn, long schemeId)
        {
            return conn.WebServiceManager.ChangeOrderService.GetChangeOrderNumberBySchemeId(schemeId);
        }
    }
}
