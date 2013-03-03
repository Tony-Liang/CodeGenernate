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

namespace CodeGenernate.Common
{
    public class VelocityWrapper
    {
        public static string CreateCode(string template)
        {
            StringWriter writer = new StringWriter();
            try
            {
                VelocityEngine engine = new VelocityEngine();
                engine.Init();
                NVelocity.VelocityContext context = new NVelocity.VelocityContext();
                context.Put("servicesite", DataBaseSchemaBuilder.GetInstance());                
                engine.Evaluate(context, writer, "", template);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return writer.ToString();
        }

        public static void CreateCode(string filepath,string outputpath)
        {
            StreamWriter writer=null;
            try
            {
                VelocityEngine engine = new VelocityEngine();
                ExtendedProperties extendedProperties = new ExtendedProperties();
                extendedProperties.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, filepath.Substring(0, filepath.LastIndexOf("\\")));

                engine.Init(extendedProperties);
                NVelocity.VelocityContext context = new NVelocity.VelocityContext();
                context.Put("servicesite", DataBaseSchemaBuilder.GetInstance());
                Template template = engine.GetTemplate(filepath.Substring(filepath.LastIndexOf("\\")+1));

                FileStream fos = new FileStream(outputpath + "\\1.vm", FileMode.Create);
                writer = new StreamWriter(fos);
                template.Merge(context, writer);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                //throw ex;
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
    }
}
