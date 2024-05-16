
using Core.Entities;
using Core.Models;

namespace API.Endpoints.EmployerTemplates
{
    public class GetTemplateResponce : BaseResponse
    {
        public TemplateResponseDto Template { get; set; }
    }
}