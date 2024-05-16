
using Core.Entities;
using Core.Models;

namespace API.Endpoints.EmployerTemplates
{
    public class UpdateTemplateResponce : BaseResponse
    {
        public TemplateResponseDto Template { get; set; }
    }
}