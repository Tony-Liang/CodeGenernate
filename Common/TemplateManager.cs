using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCW.Framework.Common.Util;
using LCW.Framework.Common.SysFile;

namespace CodeGenernate.Common
{
    public class TemplateManager
    {
        private static TemplateFile root;
        public static TemplateFile GetInstance(string name)
        {
            if(root==null)
            {
                string path=AppSettingsHelper.GetString("codetemplate","codetemplate");

                string filepath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, path);
                DirectoryHelper.CreateDir(filepath);
                root = new TemplateFile(name, filepath, FileType.Directory);
            }
            return root;
        }
    }

    public enum FileType
    {
        Directory,
        File
    }

    public class TemplateFile
    {
        public TemplateFile(string name,string path,FileType type)
        {
            this.name=name;
            this.path=path;
            this.type = type;
        }

        private string path;
        public string Path
        {
            get
            {
                return path;
            }
        }

        private FileType type;
        public FileType Type
        {
            get
            {
                return type;
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        private IList<TemplateFile> files;
        public IList<TemplateFile> Files
        {
            get
            {
                if (files == null)
                {
                    List<TemplateFile> temp = new List<TemplateFile>();
                    try
                    {
                        System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(this.path);
                        System.IO.FileSystemInfo[] fs = info.GetFileSystemInfos();
                        if (fs != null && fs.Count() > 0)
                        {
                            foreach (System.IO.FileSystemInfo f in fs)
                            {
                                if (f is System.IO.DirectoryInfo)
                                    temp.Add(new TemplateFile(f.Name, f.FullName, FileType.Directory));
                                else if(f is System.IO.FileInfo)
                                    temp.Add(new TemplateFile(f.Name.Substring(0,f.Name.IndexOf(".")), f.FullName, FileType.File));
                            }
                            files = temp;
                        }
                    }
                    catch (System.IO.IOException ex)
                    {

                    }
                }
                return files;
            }
        }
    }

    public class DocumentEventArgs : EventArgs
    {
        public DocumentEventArgs(string tag,object sender)
        {
            this.tag = tag;
            this.sender = sender;
        }
        private string tag;
        public string Tag
        {
            get
            {
                return tag;
            }
            //set
            //{
            //    tag = value;
            //}
        }

        public object sender;
        public object Sender
        {
            get
            {
                return sender;
            }
            //set
            //{
            //    content = value;
            //}
        }
    }
}
