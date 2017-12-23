namespace SecondHandShop.Web.Models.ManageViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EnableAuthenticatorViewModel
    {
        [Required]
        [StringLength(DataConstants.CodeMaxLenth, ErrorMessage = DataConstants.ErrorMessageTwoFactorCode, MinimumLength = DataConstants.MinLenthOfStuffs)]
        [DataType(DataType.Text)]
        [Display(Name = DataConstants.DisplayCode)]
        public string Code { get; set; }

        [ReadOnly(true)]
        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }
}