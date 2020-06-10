namespace OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv
{
    public class DownloadSalaryCsvSuccessAction
    {
        public DownloadSalaryCsvSuccessAction(string link) =>
            CsvLink = link;

        public string CsvLink { get; }
    }
}
