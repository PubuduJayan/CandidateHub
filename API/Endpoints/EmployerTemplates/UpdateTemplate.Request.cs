
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.EmployerTemplates
{
    public class UpdateTemplateRequest
    {
        public const string Route = "/Template";
        public required Guid Id { get; set; }
        public TemplateRequestDto? Temaplate { get; set; }
        public List<QuestionCreateModel> QuestionUpdate { get; set; } = new();
    }

  
}