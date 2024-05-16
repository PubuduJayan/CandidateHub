using Ardalis.Specification;
using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.Templates
{
    public class GetTemplateByNameSpec : Specification<Template>
    {
        public GetTemplateByNameSpec(string name)
        {
            Query.Where(a => a.Title.Equals(name));
        }
    }
}
