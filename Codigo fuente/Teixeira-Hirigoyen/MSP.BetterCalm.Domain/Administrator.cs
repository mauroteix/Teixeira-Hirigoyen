using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Token { get; set; }

        public Administrator()
        { }

        public Administrator(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Token = Guid.NewGuid();
        }

        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
        }
        public bool EmailEmpty()
        {
            return this.Email == null || this.Email.Length == 0;
        }
        public bool PasswordEmpty()
        {
            return this.Password == null || this.Password.Length == 0;
        }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Administrator admin = (Administrator)obj;
                return (this.Id == admin.Id);
            }
        }
    }
}
