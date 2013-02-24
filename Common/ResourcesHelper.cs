using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Linq.Expressions;
namespace CodeGenernate.Common
{
    public class ResourcesHelper
    {
        public static Image FetchDB_icon(string type)
        {
            Image image = null;
            switch (type.ToLower())
            {
                case "schema":
                    image = global::CodeGenernate.Properties.Resources.db_Schema;
                    break;
                case "table":
                    image = global::CodeGenernate.Properties.Resources.db_Table;
                    break;
                case "view":
                    image = global::CodeGenernate.Properties.Resources.db_View;
                    break;
                case "command":
                    image = global::CodeGenernate.Properties.Resources.db_execute;
                    break;
                case "column":
                    image = global::CodeGenernate.Properties.Resources.db_Column;
                    break;
                case "trigger":
                    image = global::CodeGenernate.Properties.Resources.db_Trigger;
                    break;
                case "stop":
                    image = global::CodeGenernate.Properties.Resources.db_Stop;
                    break;
                case "connection":
                    image = global::CodeGenernate.Properties.Resources.server_icon;
                    break;
                case "package":
                    image = global::CodeGenernate.Properties.Resources.package;
                    break;
                default:
                    break;
            }
            return image;
        }

        public static Image FetchDB_icon(Expression<Func<DBIcon,string>> expression)
        {
            return FetchDB_icon(((MemberExpression)expression.Body).Member.Name);
        }
    }

    public class DBIcon
    {
        public string Schema { get; set; }
        public string Table { get; set; }
        public string View { get; set; }
        public string Command { get; set; }
        public string Column { get; set; }
        public string Trigger { get; set; }
        public string Connection { get; set; }
        public string Stop { get; set; }
        public string Package { get; set; }
    }
}
