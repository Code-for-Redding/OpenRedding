namespace OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv
{
	using System;

    public class DownloadSalaryCsvSuccessAction
    {
        public DownloadSalaryCsvSuccessAction(string link) =>
            CsvLink = link;

        public string CsvLink { get; }
    }
}
