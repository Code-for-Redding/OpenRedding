namespace OpenRedding.Shared
{
    public class OpenReddingConstants
    {
        public const string Author = "Joey Mckenzie";
        public const string Version = "v1";
        public const int RequestElapsedMillisecondTimeWarningLimit = 500;
        public const int MaxPageSizeResult = 25;
        public static readonly string ApiPath = $"/{Version}/api";

        public static readonly string[] Urls =
        {
            "https://transcal.s3.amazonaws.com/public/export/redding-2018.csv",
            "https://transcal.s3.amazonaws.com/public/export/redding-2017.csv",
            "https://transcal.s3.amazonaws.com/public/export/redding-2016.csv",
            "https://transcal.s3.amazonaws.com/public/export/redding-2015.csv",
            "https://transcal.s3.amazonaws.com/public/export/redding-2014.csv",
            "https://transcal.s3.amazonaws.com/public/export/redding-2013.csv",
            "https://transcal.s3.amazonaws.com/public/export/redding-2012.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2018.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2017.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2016.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2015.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2014.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2013.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2012.csv",
            "https://transcal.s3.amazonaws.com/public/export/shasta-county-2011.csv"
        };

        public static readonly string[] ColumnHeaders =
        {
            "Employee Name",
            "Job Title",
            "Base Pay",
            "Overtime Pay",
            "Other Pay",
            "Benefits",
            "Pension Debt",
            "Total Pay",
            "Total Pay & Benefits",
            "Year",
            "Notes",
            "Agency",
            "Status"
        };
    }
}
