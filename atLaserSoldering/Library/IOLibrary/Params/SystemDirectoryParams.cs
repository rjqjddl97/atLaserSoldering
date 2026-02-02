using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class SystemDirectoryParams
    {
        static string _strRootFolderPath = string.Empty;
        static string _strSystemFolderPath = string.Empty;
        static string _strLogFolderPath = string.Empty;
        static string _strResultFolderPath = string.Empty;
        static string _strRecipeFolderPath = string.Empty;
        static string _strImageFolderPath = string.Empty;

        static string _strSystemFileName = "SystemParameters.ini";
        static string _strStatisticsFileName = "Statistics.ini";
        static string _strFileSystemFileName = "FileSystem.ini";
        static string _strTackTimeFileName = "TackTimes.ini";
        static string _strDistanceFileName = "Distance.ini";
        static string _strAngleFileName = "Angle.ini";

        static public string RootFolderPath
        {
            get { return _strRootFolderPath; }
            set { _strRootFolderPath = value; }
        }

        static public string SystemFolderPath
        {
            get { return _strSystemFolderPath; }
            set { _strSystemFolderPath = value; }
        }

        static public string LogFolderPath
        {
            get { return _strLogFolderPath; }
            set { _strLogFolderPath = value; }
        }

        static public string ResultFolderPath
        {
            get { return _strResultFolderPath; }
            set { _strResultFolderPath = value; }
        }

        static public string RecipeFolderPath
        {
            get { return _strRecipeFolderPath; }
            set { _strRecipeFolderPath = value; }
        }
        static public string ImageFolderPath
        {
            get { return _strImageFolderPath; }
            set { _strImageFolderPath = value; }
        }
        static public string SystemFileName
        {
            get { return _strSystemFileName; }
        }

        static public string FileSystemFileName
        {
            get { return _strFileSystemFileName; }
        }

        static public string StatisticsFileName
        {
            get { return _strStatisticsFileName; }
        }
        static public string TackTimesFileName
        {
            get { return _strTackTimeFileName; }
        }
        static public string InspectDistanceFileName
        {
            get { return _strDistanceFileName; }
        }
        static public string InspectAngleFileName
        {
            get { return _strAngleFileName; }
        }
        static public void WriteFileSystem()
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData fileSystem = new IniData();

            fileSystem.Sections.AddSection("FileSystem");
            fileSystem.Sections["FileSystem"].AddKey("RootFolderPath", _strRootFolderPath);
            fileSystem.Sections["FileSystem"].AddKey("LogFolderPath", _strLogFolderPath);
            fileSystem.Sections["FileSystem"].AddKey("RecipeFolderPath", _strRecipeFolderPath);
            fileSystem.Sections["FileSystem"].AddKey("ResultFolderPath", _strResultFolderPath);
            fileSystem.Sections["FileSystem"].AddKey("SystemFolderPath", _strSystemFolderPath);
            fileSystem.Sections["FileSystem"].AddKey("ImageFolderPath", _strImageFolderPath);

            parser.WriteFile(string.Format(@"{0}\{1}", _strSystemFolderPath, _strFileSystemFileName), fileSystem);
        }

        static public void CreateSystemDirectory()
        {
            string[] strSystemFolders = new string[] { SystemDirectoryParams.RootFolderPath, SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.LogFolderPath, SystemDirectoryParams.ResultFolderPath, SystemDirectoryParams.RecipeFolderPath, SystemDirectoryParams.ImageFolderPath };

            for (int i = 0; i < strSystemFolders.Length; ++i)
            {
                if (!Directory.Exists(strSystemFolders[i]))
                {
                    Directory.CreateDirectory(strSystemFolders[i]);
                }
            }
        }
    }
}
