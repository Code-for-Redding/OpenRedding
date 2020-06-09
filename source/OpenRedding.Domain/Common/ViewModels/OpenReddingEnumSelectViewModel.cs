namespace OpenRedding.Domain.Common.ViewModels
{
    using System.Collections.Generic;

    public class OpenReddingEnumSelectViewModel<TEnum>
        where TEnum : struct
    {
        public OpenReddingEnumSelectViewModel() => SelectOptions = new Dictionary<TEnum, string>();

        public IDictionary<TEnum, string> SelectOptions { get; }

        public OpenReddingEnumSelectViewModel<TEnum> AddOption(TEnum enumValue, string displayValue)
        {
            SelectOptions.Add(enumValue, displayValue);

            return this;
        }
    }
}
