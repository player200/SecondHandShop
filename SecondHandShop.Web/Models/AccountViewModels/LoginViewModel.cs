namespace SecondHandShop.Web.Models.AccountViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required]
        [Display(Name = DataConstants.DisplayUserName)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = DataConstants.DisplayRememberMe)]
        public bool RememberMe { get; set; }
    }
}