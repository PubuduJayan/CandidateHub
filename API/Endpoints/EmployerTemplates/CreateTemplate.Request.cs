
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.EmployerTemplates
{
    public class CreateTemplateRequest
    {
        public const string Route = "/Template";      
        public required TemplateRequestDto Temaplate { get; set; }
        public List<QuestionCreateModel> QuestionCreate { get; set; } = new();
    }

    public class QuestionCreateModel
    {
        public required string Type { get; set; }
        public required string Question { get; set; }
        public List<string>? Choices { get; set; } = new();
        public bool? IsOtherOption { get; set; }
        public int? MaxChoice { get; set; }
    }
}