using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MSP.BetterCalm.BusinessLogic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        IData<Administrator> repositoryAdministrator;
        public AdministratorLogic(IData<Administrator> repository)
        {
            repositoryAdministrator = repository;
        }

        public Administrator Get(int id)
        {
            ExistAdministrator(id);
            return repositoryAdministrator.Get(id);
        }

        public void Add(Administrator administrator)
        {
            ValidateAdministrator(administrator);
            ValidateEmailUnique(administrator.Email);
            administrator.Email = administrator.Email.ToLower();
            administrator.Token = Guid.NewGuid();
            repositoryAdministrator.Add(administrator);
        }

        public void Delete(Administrator administrator)
        {
            ExistAdministrator(administrator.Id);
            repositoryAdministrator.Delete(administrator);
        }

        public void Update(Administrator admin, int id)
        {
            ExistAdministrator(id);
            Administrator unAdministrator = repositoryAdministrator.Get(id);
            ValidateAdministrator(admin);
            if (!unAdministrator.Email.Equals(admin.Email)) ValidateEmailUnique(admin.Email);
            unAdministrator.Name = admin.Name;
            unAdministrator.Password = admin.Password;
            unAdministrator.Email = admin.Email.ToLower();
            repositoryAdministrator.Update(unAdministrator);
        }


        private void ValidateAdministrator(Administrator admin)
        {
            if (admin.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (admin.EmailEmpty()) throw new FieldEnteredNotCorrect("The email cannot be empty");
            if (admin.PasswordEmpty()) throw new FieldEnteredNotCorrect("The password cannot be empty");
            Regex regexEmail = new Regex(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$");
            if (!regexEmail.IsMatch(admin.Email)) throw new FieldEnteredNotCorrect("Incorrect email it must have this form: asdasd@hotmail.com");
            
        }
        //Ver el tema de la primary key en la database

        private void ValidateEmailUnique(string email)
        {
            bool existEmail = false;
            var listAdmin = repositoryAdministrator.GetAll().Select(u => u.Email).ToList();
            listAdmin.ForEach(c => {
                if (listAdmin.Contains(email.ToLower()))
                {
                    existEmail = true;
                }
            });
            if(existEmail) throw new FieldEnteredNotCorrect("The email already exist");
        }
   
        private void ExistAdministrator(int id)
        {
            Administrator unAdmin = repositoryAdministrator.Get(id);
            if (unAdmin == null) throw new EntityNotExists("The admin with id: " + id + " does not exist");
        }
    }
}
