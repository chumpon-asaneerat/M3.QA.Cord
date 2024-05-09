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
        public const string COA4 = "M3.QA.Resources.Excels.COA4.xlsx";
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
    }
}
