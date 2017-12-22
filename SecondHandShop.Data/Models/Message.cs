namespace SecondHandShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.MessagesContentMinLenth)]
        [MaxLength(DataConstants.MessagesContentMaxLenth)]
        public string Content { get; set; }

        public DateTime MessagedOn { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }
    }
}