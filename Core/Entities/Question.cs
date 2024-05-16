using SharedKernal;

namespace Core.Entities
{
    public class Question : BaseEntity
    {    
        public string Type { get; init; }
        public string Title { get; init; } // Question
        public List<string>? Choices { get; set; } = new ();
        public bool? IsOtherOption {  get; set; }
        public int? MaxChoice {  get; set; }
        public string? Data { get; set; }
        public Question(string type, string title)
        {
            Type = type;
            Title = title;
        }

        public void AddOptions(List<string> options)
        {
            if (options.GroupBy(x => x).Any(group => group.Count() > 1))
                throw new Exception("Options needs to be unique");
            Choices = options;
        }

    }
}
