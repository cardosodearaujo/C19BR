using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Everaldo.Cardoso.C19BR.Framework.ToolBox
{
    public static class FileManager
    {
        public static string FileLogName { get; set; }

        public static bool CreateFolder(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool CreateFile(string file)
        {
            try
            {
                if (!File.Exists(file))
                {
                    var fileStream = new FileStream(file, FileMode.Create);
                    fileStream.Dispose();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool MoveFile(string source, string destiny)
        {
            try
            {
                if (File.Exists(source))
                {
                    File.Move(source, destiny);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string ReadFile(string file)
        {
            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            }
            return string.Empty;
        }

        public static void RegisterLog(this Exception log)
        {
            SaveFile(log.Message);
        }

        public static void RegisterLog(this string log)
        {
            SaveFile(log);
        }

        public static void SaveFile(string data)
        {
            if (string.IsNullOrEmpty(FileLogName)) return;
            FileStream fileStream;
            if (!File.Exists(FileLogName))
            {
                fileStream = new FileStream(FileLogName, FileMode.Create);
                fileStream.Dispose();
            }
            fileStream = null;
            try
            {
                fileStream = new FileStream(FileLogName, FileMode.Append);
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default, 512, false))
                {
                    streamWriter.WriteLine("|----------- Registrado em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " ----------|");
                    streamWriter.WriteLine(data);
                    streamWriter.WriteLine("|--------------------------------------------------------|");
                    streamWriter.Close();
                }
            }
            finally
            {
                if (fileStream != null) fileStream.Dispose();
            }
        }

        public static void SaveFile(string file, string data)
        {
            if (string.IsNullOrEmpty(file)) return;
            FileStream fileStream;
            if (!File.Exists(file))
            {
                fileStream = new FileStream(file, FileMode.Create);
                fileStream.Dispose();
            }
            fileStream = null;
            try
            {
                fileStream = new FileStream(file, FileMode.Append);
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default, 512, false))
                {
                    streamWriter.WriteLine(data);
                    streamWriter.Close();
                }
            }
            finally
            {
                if (fileStream != null) fileStream.Dispose();
            }
        }

        public static bool DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
        }

        public static bool ExistsFile(string file)
        {
            return File.Exists(file);
        }

        public static IOrderedEnumerable<FileSystemInfo> GetFilesOfPath(string path, string formatFile = "")
        {
            return new DirectoryInfo(path)
                        .GetFileSystemInfos("*" + formatFile, SearchOption.AllDirectories)
                        .OrderBy(F => F.CreationTime);
        }
    }
}
