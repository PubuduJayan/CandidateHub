namespace Core.Models
{
    public class TemplateRequestDto
    {
        public required string  Title { get; set; }
        public required string Description { get; set; }
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
    }
}
