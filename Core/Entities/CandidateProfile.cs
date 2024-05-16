using SharedKernal;

namespace Core.Entities
{
    public class CandidateProfile : BaseEntity
    {   
        public Guid TemapleId { get; private set; }
        public string TemapleName { get; private set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? Residence { get; set; }
        public string? IDNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public List<Question> Questions { get; set; } = new();

        public CandidateProfile(Guid temapleId,string temapleName, string firstName, string lastName, string email)
        {
            TemapleId = temapleId;
            TemapleName = temapleName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
       
        }
        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }


    }
}
