using System;
namespace MyPass.Core.Models
{
    public class PasswordItem
    {
        public PasswordItem()
        {
            Id = Guid.NewGuid();
        }

        public PasswordItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            Password = PasswordGenerator.Generate();
        }

        public PasswordItem(string title, string password)
        {
            Id = Guid.NewGuid();
            Title = title;
            Password = password;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
    }
}
