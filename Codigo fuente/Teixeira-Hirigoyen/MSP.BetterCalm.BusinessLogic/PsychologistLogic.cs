﻿using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class PsychologistLogic : IPsychologistLogic
    {
        IData<Psychologist> _repository;
        IData<MedicalCondition> _repositoryMedicalCondition;
        public PsychologistLogic(IData<Psychologist> repository, IData<MedicalCondition> repositoryMedicalCondition)
        {
            _repository = repository;
            _repositoryMedicalCondition = repositoryMedicalCondition;

        }
        public void Add(Psychologist psychologist)
        {
            ValidatePsychologist(psychologist);
            if (psychologist.Meeting.Count > 0) throw new FieldEnteredNotCorrect("The meeting must be empty");
            Psychologist psy = ToEntity(psychologist);
            _repository.Add(psychologist);
        }
        private Psychologist ToEntity(Psychologist psychologist)
        {
            if ((int)psychologist.MeetingType == 1) psychologist.AdressMeeting = "";
            Psychologist play = new Psychologist()
            {
                Name = psychologist.Name,
                MeetingType = psychologist.MeetingType,
                AdressMeeting = psychologist.AdressMeeting,
                MeetingPrice = psychologist.MeetingPrice
            };
            List<Expertise> listExpertise = psychologist.Expertise.Select(py => new Expertise()
            {
                MedicalCondition = _repositoryMedicalCondition.Get(py.IdMedicalCondition),
                IdMedicalCondition = py.IdMedicalCondition,
                Psychologist = psychologist,
                IdPsychologist = psychologist.Id
            }).ToList();
            psychologist.Expertise = listExpertise;

            return psychologist;
        }
     
        private void ValidatePsychologist(Psychologist psychologist)
        {
            if (psychologist.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");            
            if (psychologist.ExpertiseEmpty()) throw new FieldEnteredNotCorrect("The expertise cannot be empty");
            if (psychologist.Expertise.Count > 3) throw new FieldEnteredNotCorrect("Limit of 3 expertise,try again");
            if(!ValidateMeetingType(psychologist)) throw new FieldEnteredNotCorrect("Only 2 types of meetingType");
            if (!ValidateMeetingPrice(psychologist)) throw new FieldEnteredNotCorrect("Only 4 types of meetingPrice");
            if (psychologist.AdressMeetingEmpty() && (int)psychologist.MeetingType == 2) throw new FieldEnteredNotCorrect("Need to have an adress when is face to face");
            ValidateMedicalConditionUnique(psychologist);
            ValidateMedicalConditionId(psychologist);
        }
        private bool ValidateMeetingType(Psychologist psychologist)
        {
            int valor = (int)psychologist.MeetingType;
            if (valor > 2 && valor < 1) return false;
            return true;
        }
        private bool ValidateMeetingPrice(Psychologist psychologist)
        {
            int valor = (int)psychologist.MeetingPrice;
            if (valor > 4 && valor < 1) return false;
            return true;
        }
        private void ValidateMedicalConditionUnique(Psychologist psychologist) 
        {
            var list = psychologist.Expertise.ToList();
            var repetidos = false;
            var iterador = 1;
            list.ForEach(c => {
                if (list.Skip(iterador).Contains(c))
                {
                    repetidos = true;
                }

                iterador++;
            });
            if (repetidos) throw new FieldEnteredNotCorrect("There are two or more equal medical condition");
        }
        private void ValidateMedicalConditionId(Psychologist psychologist)
        {
            var medicalConditionList = _repositoryMedicalCondition.GetAll().Select(c => c.Id).ToList();
            var list = psychologist.Expertise.ToList();
            var exist = true;
            list.ForEach(c => {
                if (!medicalConditionList.Contains(c.IdMedicalCondition))
                {
                    exist = false;
                }
            });
            if (!exist) throw new FieldEnteredNotCorrect("One ore more medical condition do not exist");

        }
        private void ExistPsychologist(int id)
        {
            Psychologist unPsychologist = _repository.Get(id);
            if (unPsychologist == null) throw new EntityNotExists("The psychologist with id: " + id + " does not exist");
        }

        public void Delete(Psychologist psychologist)
        {
            ExistPsychologist(psychologist.Id);
            _repository.Delete(psychologist);
        }

        public Psychologist Get(int id)
        {
            ExistPsychologist(id);
            return _repository.Get(id);
        }

        public List<Psychologist> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(Psychologist psychologist, int id)
        {
            ExistPsychologist(id);
            Psychologist unPsychologist = _repository.Get(id);
            ValidatePsychologist(psychologist);
            unPsychologist.Name = psychologist.Name;
            unPsychologist.MeetingType = psychologist.MeetingType;
            unPsychologist.AdressMeeting = CreateAdress(psychologist);
            unPsychologist.Expertise = psychologist.Expertise;
            unPsychologist.MeetingPrice = psychologist.MeetingPrice;

            _repository.Update(unPsychologist);
        }
        public User CreateMeeting(User user)
        {
            user.Meeting = new List<Meeting>();
            var medicalCondition = _repositoryMedicalCondition.Get(user.MedicalCondition.Id);
            if (medicalCondition == null) throw new EntityNotExists("This medical condition not exist");
            user.MedicalCondition = medicalCondition;
            var date = DateTime.Now;
            date = ChangeDate(date);
            var list = ListOfPsychologist(medicalCondition);
            if (list.Count == 0 ) throw new EntityNotExists("There are no psychologist for this medical condition");
            Meeting meeting = new Meeting();
            Psychologist unPsychologist = FreePsychologist(list, date,meeting);

            meeting.IdUser = user.Id;
            meeting.User = user;
            meeting.Psychologist = unPsychologist;
            meeting.IdPsychologist = unPsychologist.Id;
            
            meeting.AdressMeeting = CreateAdress(unPsychologist);
            user.Meeting.Add(meeting);
            return user;
        }

        private Psychologist FreePsychologist(List<Psychologist> list,DateTime date,Meeting meeting)
        {
            var listFreePsy = ListFreePsychologist(list, date);
            Psychologist psychologist = new Psychologist();
            while (listFreePsy.Count == 0)
            {
                date = date.AddDays(1);
                date = ChangeDate(date);
                listFreePsy = ListFreePsychologist(list, date);
            }
            if (listFreePsy.Count > 1)
            {
                psychologist = SelectOlderPsychologist(listFreePsy);
            }
            else
            {
                psychologist = listFreePsy[0];
            }
            psychologist = _repository.Get(psychologist.Id);
            meeting.Date = date;
            return psychologist;
        }
        public string CreateAdress(Psychologist psychologist)
        {
            string adress = "";
            Guid guid = Guid.NewGuid();
            if((int)psychologist.MeetingType == 1)
            {
                adress = "https://bettercalm.com.uy/meeting/";
                var valor = guid;
                adress = adress + valor;
            }
            else
            {
                adress = psychologist.AdressMeeting;
            }
          
            return adress;
        }
        private DateTime  ChangeDate(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }
        private List<Psychologist> ListOfPsychologist(MedicalCondition medicalCondition)
        {
            var list = medicalCondition.Expertise.ToList();
            List <Psychologist> listPsychologist = new List<Psychologist>();
            if (medicalCondition.Name.Equals("Otros")) return _repository.GetAll().ToList();
            
            list.ForEach(c => listPsychologist.Add(_repository.Get(c.Psychologist.Id)));
            return listPsychologist;
        }
        private List<Psychologist> ListFreePsychologist(List<Psychologist> listPsychologist,DateTime date)
        {
            var list = new List<Psychologist>();
            listPsychologist.ForEach(c =>
            {
                if (c.Meeting == null) c.Meeting = new List<Meeting>();
                if (IsFreeForMeeting(c,date)) list.Add(c);

            });
            return list;
        }
        private bool IsFreeForMeeting(Psychologist psychologist,DateTime date) {
            var list = psychologist.Meeting.ToList();
            var listMeetingDate = new List<Meeting>();
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            list.ForEach(c => 
            {
                if (c.Date.Year == year && c.Date.Month == month && c.Date.Day == day) listMeetingDate.Add(c);
            });

            return listMeetingDate.Count < 6;
        }
        private Psychologist SelectOlderPsychologist(List<Psychologist> list)
        {
            int position = 0;
            int minId = list[0].Id;
            for(int i  = 0; i < list.Count; i++)
            {
                if(list[i].Id < minId)
                {
                    position = i;
                    minId = list[i].Id;
                }
            }
            return list[position];
        }
    }
}