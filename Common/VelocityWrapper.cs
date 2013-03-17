using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity.App;
using System.IO;
using LCW.Framework.Common.Genernation.DataBases;
using NVelocity;
using Commons.Collections;
using NVelocity.Runtime;
using System.Text.RegularExpressions;
using System.Globalization;
using LCW.Framework.Common.CodeCompiler;

namespace CodeGenernate.Common
{
    public class VelocityWrapper
    {
        public static string CreateCode(string codeString,NVelocity.VelocityContext context)
        {
            StringWriter writer = new StringWriter();
            try
            {
                VelocityEngine engine= new VelocityEngine();
                engine.Init();
                if (context == null)
                    context = new VelocityContext();
                engine.Evaluate(context, writer, "", codeString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return writer.ToString();
        }

        public static string CreateCode(string codeString, IDictionary<string, object> data)
        {
            NVelocity.VelocityContext context=new VelocityContext();
            if (data != null)
            {
                foreach (var item in data.Keys)
                {
                    context.Put(item, data[item]);
                }
            }
            return CreateCode(codeString, context);
        }

        public static void CreateCode(string filepath, string outputpath, VelocityContext context)
        {
            StreamWriter writer=null;
            try
            {
                VelocityEngine engine = null;//new VelocityEngine();
                ExtendedProperties extendedProperties = new ExtendedProperties();
                //extendedProperties.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, filepath.Substring(0, filepath.LastIndexOf("\\")));

                engine.Init(extendedProperties);
                //Template template = engine.GetTemplate(filepath.Substring(filepath.LastIndexOf("\\")+1));
                //FileStream fos = new FileStream(outputpath + "\\1.vm", FileMode.Create);
                //writer = new StreamWriter(fos);
                //template.Merge(context, writer);
                //writer.Flush();
                //writer.Close();
                StringWriter output=new StringWriter();
                engine.Evaluate(context, output, "", filepath);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        protected internal static String GetFileName(String dir, String baseDir, String ext)
        {
            StringBuilder buf = new StringBuilder();
            if (dir != null)
                buf.Append(dir).Append('/');
            buf.Append(baseDir).Append('.').Append(ext);
            return buf.ToString();
        }

        public static string ToPropertyName(string name)
        {
            return ToPascalCase(name.Replace(" ", String.Empty));
        }

        public static string ApplyPascalCase(string name)
        {
            string notStartingAlpha = Regex.Replace(name, "^[^a-zA-Z]+", "");
            string workingString = ToLowerExceptCamelCase(notStartingAlpha);
            workingString = RemoveSeparatorAndCapNext(workingString);
            return workingString;
        }

        public static string ToPascalCase(string name)
        {
            // switch loopback
            //if (!l99_Info._chkApplyPascalCase) return name;

            return ApplyPascalCase(name);
        }

        public static string RemoveSeparatorAndCapNext(string input)
        {
            string dashUnderscore = "-_";
            string workingString = input;
            char[] chars = workingString.ToCharArray();
            int under = workingString.IndexOfAny(dashUnderscore.ToCharArray());
            while (under > -1)
            {
                chars[under + 1] = Char.ToUpper(chars[under + 1], CultureInfo.InvariantCulture);
                workingString = new String(chars);
                under = workingString.IndexOfAny(dashUnderscore.ToCharArray(), under + 1);
            }
            chars[0] = Char.ToUpper(chars[0], CultureInfo.InvariantCulture);
            workingString = new string(chars);
            return Regex.Replace(workingString, "[-_]", "");

        }

        public static string ToLowerExceptCamelCase(string input)
        {
            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int left = (i > 0 ? i - 1 : i);
                int right = (i < chars.Length - 1 ? i + 1 : i);
                if (i != left && i != right)
                {
                    if (Char.IsUpper(chars[i]) && Char.IsLetter(chars[left]) && Char.IsUpper(chars[left]))
                    {
                        chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                    }
                    else if (Char.IsUpper(chars[i]) && Char.IsLetter(chars[right]) && Char.IsUpper(chars[right]))
                    {
                        chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                    }
                    else if (Char.IsUpper(chars[i]) && !Char.IsLetter(chars[right]))
                    {
                        chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                    }
                }
            }
            if (chars.Length > 0)
                chars[chars.Length - 1] = Char.ToLower(chars[chars.Length - 1], CultureInfo.InvariantCulture);
            return new string(chars);
        }
    }
}
