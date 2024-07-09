#region Using

using System;

#endregion

namespace M3.QA
{
    public static class AppConsts
    {
        // common properties
        public static string Version = "1";
        public static string Minor = "0";
        public static string CompanyName = "VS Enterprise Group Co., Ltd.";

        public static class Application
        {
            public static class Cord
            {
                public static class QA
                {
                    public static string ApplicationName = @"M3 QA Cord Application";
                    // common
                    public static string Version = AppConsts.Version;
                    public static string Minor = AppConsts.Minor;
                    public static string Build = "465";
                    public static DateTime LastUpdate = new DateTime(2024, 7, 9, 12, 30, 00);
                }

                public static class ExcelTest
                {
                    public static string ApplicationName = @"M3 QA Cord Excel Test Application";
                    // common
                    public static string Version = AppConsts.Version;
                    public static string Minor = AppConsts.Minor;
                    public static string Build = "11";
                    public static DateTime LastUpdate = new DateTime(2023, 12, 08, 15, 00, 00);
                }
            }
        }
    }
}
