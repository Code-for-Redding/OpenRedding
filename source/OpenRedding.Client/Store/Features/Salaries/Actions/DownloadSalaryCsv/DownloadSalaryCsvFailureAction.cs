namespace OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv
{
    using OpenRedding.Client.Store.Features.Shared.Actions;

    public class DownloadSalaryCsvFailureAction : FailureAction
    {
        public DownloadSalaryCsvFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
