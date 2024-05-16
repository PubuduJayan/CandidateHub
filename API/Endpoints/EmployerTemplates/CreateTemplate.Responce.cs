
using Core.Entities;
using Core.Models;

namespace API.Endpoints.EmployerTemplates
{
    public class CreateTemplateResponce : BaseResponse
    {
        public TemplateResponseDto Template { get; set; }
    }
}