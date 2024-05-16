using Core.Entities;

namespace Core.Models
{
    public class TemplateResponseDto
    {
        public Guid TemapleId { get;  set; } 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid InformationId { get; set; }
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
        public List<Question> Questions { get; set; } = new();
    }
}
