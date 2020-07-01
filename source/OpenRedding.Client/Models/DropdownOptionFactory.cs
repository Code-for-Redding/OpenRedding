namespace OpenRedding.Client.Models
{
    using OpenRedding.Domain.Common.ViewModels;

    public abstract class DropdownOptionFactory<TEnum>
        where TEnum : struct
    {
        public abstract OpenReddingEnumSelectViewModel<TEnum> GetDropdownOptions();
    }
}
