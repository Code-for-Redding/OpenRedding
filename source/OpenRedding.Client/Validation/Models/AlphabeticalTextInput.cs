namespace OpenRedding.Client.Validation.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AlphabeticalTextInput
    {
        [RegularExpression("^[a-zA-Z \\-']*$", ErrorMessage = "Please enter a valid alphabetical value")]
        public string? InputValue { get; set; }
    }
}
