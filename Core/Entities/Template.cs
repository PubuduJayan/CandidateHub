using SharedKernal;

namespace Core.Entities
{
    public class Template : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InformationId { get; set; }

        public Template(string title, string description, Guid informationId)
        {
            Title = title;
            Description = description;
            InformationId = informationId;
        }
    }
}
