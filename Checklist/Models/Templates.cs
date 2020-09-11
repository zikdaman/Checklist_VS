using Checklist.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class Template : BaseModel
	{		
		public int TemplateID { get; set; }
		[Display(Name="Template Name")]
		public string TemplateName { get; set; }
		public ICollection<TemplateAttribute> TemplateAttributes { get; set; }
		public ICollection<TemplateInstance> TemplateInstances { get; set; }
	}
}