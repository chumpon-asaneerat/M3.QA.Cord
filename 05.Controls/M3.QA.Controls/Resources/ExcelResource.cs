using NLib.Utils;
using NLib;
using System;
using System.IO;
using System.Reflection;

namespace M3.QA
{
    internal class ExcelResource
    {
        public const string COA1 = "M3.QA.Resources.Excels.COA1.xlsx";
        public const string COA1a = "M3.QA.Resources.Excels.COA1a.xlsx";
        public const string COA2 = "M3.QA.Resources.Excels.COA2.xlsx";
        public const string COA3 = "M3.QA.Resources.Excels.COA3.xlsx";
        public const string COA4 = "M3.QA.Resources.Excels.COA4.xlsx";
        public const string COA4a = "M3.QA.Resources.Excels.COA4a.xlsx";
        public const string COA5 = "M3.QA.Resources.Excels.COA5.xlsx";
    }

    public sealed class ExcelExportUtils
    {
        #region Create Resource

        private static bool CreateResource(string resourceName,
            string FullFileName, bool AutoOverwrite)
        {
            if (File.Exists(FullFileName))
            {
                if (!AutoOverwrite)
                    throw (new Exception("File " + FullFileName + " is Exist"));
                File.Delete(FullFileName);
            }

            using (WindowFormsResourceAccess resAccess = new WindowFormsResourceAccess())
            {
                ResourceStreamOptions option = new ResourceStreamOptions()
                {
                    ResourceName = resourceName,
                    CallerType = typeof(ExcelResource),
                    TargetPath = Path.GetDirectoryName(FullFileName),
                    TargetFileName = Path.GetFileName(FullFileName)
                };

                resAccess.CreateFile(option);
            }

            return File.Exists(FullFileName);
        }

        private static void MoveFile(string sourceFileName, string targetFileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // move file
            if (File.Exists(sourceFileName))
            {
                try
                {
                    File.Move(sourceFileName, targetFileName);
                }
                catch (Exception ex)
                {
                    ex.Err(med);
                }
            }
        }

        private static void DeleteFile(string fileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // delete file
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    ex.Err(med);
                }
            }
        }

        private static bool CreateFileFromResource(string embededResourceName,
            string outputFileNameOnly, string FullFileName, bool AutoOverwrite)
        {
            if (string.IsNullOrWhiteSpace(embededResourceName) ||
                string.IsNullOrWhiteSpace(outputFileNameOnly) ||
                string.IsNullOrWhiteSpace(FullFileName))
                return false;

            if (CreateResource(embededResourceName, FullFileName, AutoOverwrite))
            {
                // Rename
                string resourceFileOutput =
                    Path.GetDirectoryName(FullFileName) + outputFileNameOnly;
                // Move resource output file  to target file
                MoveFile(resourceFileOutput, FullFileName);
            }

            return true;
        }

        #endregion

        #region COA1

        /// <summary>
        /// Create COA1 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA1File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA1,
                @"\COA1.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region COA1a

        /// <summary>
        /// Create COA1a File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA1aFile(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA1a,
                @"\COA1.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region COA2

        /// <summary>
        /// Create COA2 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA2File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA2,
                @"\COA2.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region COA3

        /// <summary>
        /// Create COA3 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA3File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA3,
                @"\COA3.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region COA4

        /// <summary>
        /// Create COA4 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA4File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA4,
                @"\COA4.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region COA4a

        /// <summary>
        /// Create COA4a File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA4aFile(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA4a,
                @"\COA4a.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region COA5

        /// <summary>
        /// Create COA5 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateCOA5File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.COA5,
                @"\COA5.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion
    }
}
