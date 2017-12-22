namespace SecondHandShop.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(DataConstants.UsersNameMinLenth)]
        [MaxLength(DataConstants.UsersNameMaxLenth)]
        public string Name { get; set; }

        public List<Advertisement> Advertisements { get; set; } = new List<Advertisement>();

        public List<Message> MessagesSended { get; set; } = new List<Message>();

        public List<Message> MessagesReceived { get; set; } = new List<Message>();
    }
}