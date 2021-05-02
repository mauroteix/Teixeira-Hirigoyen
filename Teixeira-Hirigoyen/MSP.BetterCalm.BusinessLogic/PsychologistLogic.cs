using Msp.BetterCalm.HandleMessage;
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
            Psychologist psy = ToEntity(psychologist);
            _repository.Add(psychologist);
        }
        private Psychologist ToEntity(Psychologist psychologist)
        {
            Psychologist play = new Psychologist()
            {
                Name = psychologist.Name,
                AdressMeeting = psychologist.AdressMeeting,
                MeetingType = psychologist.MeetingType,
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
            if (psychologist.AdressEmpty()) throw new FieldEnteredNotCorrect("The adress meeting cannot be empty");
            if (psychologist.ExpertiseEmpty()) throw new FieldEnteredNotCorrect("The expertise cannot be empty");
            if (psychologist.Expertise.Count > 3) throw new FieldEnteredNotCorrect("Limit of 3 expertise,try again");
            ValidateMedicalConditionUnique(psychologist);
            ValidateMedicalConditionId(psychologist);
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
            unPsychologist.Meeting = psychologist.Meeting;
            unPsychologist.Expertise = psychologist.Expertise;

            _repository.Update(unPsychologist);
        }
        public User createMeeting(User user)
        {
            var medicalCondition = user.MedicalCondition;
            var date = DateTime.Now;
            date = changeDate(date);
            var list = ListOfPsychologist(user.MedicalCondition);
            var listFreePsy = ListFreePsychologist(list, date);
            Psychologist psy = new Psychologist();
            while(listFreePsy.Count == 0)
            {
                date = date.AddDays(1);
                date = changeDate(date);
                listFreePsy = ListFreePsychologist(list, date);
            }
            if(listFreePsy.Count > 1)
            {
                psy = SelectOlderPsychologist(listFreePsy);
            }
            else
            {
                psy = listFreePsy[0];
            }
            Meeting meeting = new Meeting();
            meeting.IdUser = user.Id;
            meeting.User = user;
            meeting.Psychologist = psy;
            meeting.IdPsychologist = psy.Id;
            user.Meeting.Add(meeting);

            return user;
        }
        public DateTime  changeDate(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }
        public List<Psychologist> ListOfPsychologist(MedicalCondition medicalCondition)
        {
            var list = medicalCondition.Expertise.ToList();
            List <Psychologist> listPsy = new List<Psychologist>();
            list.ForEach(c => listPsy.Add(c.Psychologist));
            return listPsy;
        }
        public List<Psychologist> ListFreePsychologist(List<Psychologist> listPsychologist,DateTime date)
        {
            var list = new List<Psychologist>();
            listPsychologist.ForEach(c =>
            {
                if (isFreeForMeeting(c,date)) list.Add(c);
            });
            return list;
        }
        public bool isFreeForMeeting(Psychologist psy,DateTime date) {
            var list = psy.Meeting.ToList();
            var listMeetingDate = new List<Meeting>();
            list.ForEach(c => 
            {
                if (c.Date.Equals(date)) listMeetingDate.Add(c);
            });
            return listMeetingDate.Count < 6;
        }
        public Psychologist SelectOlderPsychologist(List<Psychologist> list)
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
