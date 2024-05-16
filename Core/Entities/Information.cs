using SharedKernal;

namespace Core.Entities
{
    public class Information : BaseEntity
    {      
        public bool PhoneIsInternal { get; set; } = false;
        public bool PhoneIsHide { get; set; } = false;
        public bool NationalityIsInternal { get; set; } = false;
        public bool NationalityIsHide { get; set; } = false;
        public bool ResidenceIsInternal { get; set; } = false;
        public bool ResidenceIsHide { get; set; } = false;
        public bool IDNumberIsInternal { get; set; } = false;
        public bool IDNumberIsHide { get; set; } = false;
        public bool DOBIsInternal { get; set; } = false;
        public bool DOBIsHide { get; set; } = false;
        public bool GenderIsInternal { get; set; } = false;
        public bool GenderIsHide { get; set; } = false;

        public List<Question> Questions = new();

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
         
        }
        public void UpdateQuestion(Question question)
        {
            Questions.Remove(question);
            Questions.Add(question);
        }

        public void RemoveAllQuestions()
        {
            Questions.Clear();
        }

    }
}
