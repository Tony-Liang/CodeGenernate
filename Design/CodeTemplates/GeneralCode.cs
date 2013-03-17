using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenernate.Design.CodeTemplates
{
    public class GeneralCode:ICodeTemplate
    {
        public string Execute()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("using System;\r\n");
            str.AppendLine("public class $table");
            str.AppendLine("{");
            str.AppendLine("#foreach($column in $columns)");
            str.AppendLine("\tprivate $column.DataType.Name $column.Name;");
            str.AppendLine("\tpublic $column.DataType.Name $column.Name");
            str.AppendLine("\t{");
            str.AppendLine("\t\tget");
            str.AppendLine("\t\t{");
            str.AppendLine("\t\t\treturn $column.Name;");
            str.AppendLine("\t\t}");
            str.AppendLine("\t\tset");
            str.AppendLine("\t\t{");
            str.AppendLine("\t\t\t$column.Name=value;");
            str.AppendLine("\t\t}");
            str.AppendLine("\t}\r\n");
            str.AppendLine("#end");
            str.AppendLine("}");
            return str.ToString();
        }
    }
}
