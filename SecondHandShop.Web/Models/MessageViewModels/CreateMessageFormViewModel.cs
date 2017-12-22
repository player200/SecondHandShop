namespace SecondHandShop.Web.Models.MessageViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class CreateMessageFormViewModel
    {
        [Required]
        [MinLength(DataConstants.MessagesContentMinLenth)]
        [MaxLength(DataConstants.MessagesContentMaxLenth)]
        public string Content { get; set; }

        public string ReceiverUserName { get; set; }
    }
}