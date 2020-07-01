namespace OpenRedding.Client.Components.Common
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Shared;

    public partial class OpenReddingDropdown<TEnum> : ComponentBase
        where TEnum : struct
    {
        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? LabelCss { get; set; }

        [Parameter]
        public string? DropdownId { get; set; }

        [Parameter]
        public TEnum ExistingSelection { get; set; }

        [Parameter]
        public OpenReddingEnumSelectViewModel<TEnum>? DropdownOptions { get; set; }

        [Parameter]
        public EventCallback<TEnum> ValueSelectedHandler { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ArgumentValidation.CheckNotNull(Label, nameof(Label));
            ArgumentValidation.CheckNotNull(LabelCss, nameof(LabelCss));
            ArgumentValidation.CheckNotNull(DropdownId, nameof(DropdownId));
            ArgumentValidation.CheckNotNull(ExistingSelection, nameof(ExistingSelection));
            ArgumentValidation.CheckNotNull(DropdownOptions, nameof(DropdownOptions));
            ArgumentValidation.CheckNotNull(ValueSelectedHandler, nameof(ValueSelectedHandler));

            await base.OnInitializedAsync();
        }

        private async Task OnValueSelected(ChangeEventArgs e)
        {
            if (ValueSelectedHandler.HasDelegate)
            {
                var selectedValue = Enum.Parse<TEnum>(e.Value.ToString());
                await ValueSelectedHandler.InvokeAsync(selectedValue);
            }
        }
    }
}
