
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.EmployerTemplates
{
    public class GetTemplateRequest
    {
        public const string Route = "/Template/{Title}";
        public required string Title { get; set; }
    }

}