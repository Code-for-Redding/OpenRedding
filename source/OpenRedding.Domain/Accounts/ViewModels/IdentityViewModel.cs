namespace OpenRedding.Domain.Accounts.ViewModels
{
	using System.Collections.Generic;
	using OpenRedding.Domain.Common.Dto;

	public class IdentityViewModel
    {
        public IdentityViewModel()
        {
            Errors = new List<OpenReddingErrorDto>();
        }

        public IList<OpenReddingErrorDto> Errors { get; }

        public bool HasErrors => Errors.Count > 0;
    }
}
