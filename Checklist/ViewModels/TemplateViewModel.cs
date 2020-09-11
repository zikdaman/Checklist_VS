using Checklist.Models;
using System.Collections.Generic;

namespace Checklist.ViewModels
{
	public class TemplateViewModel
	{
		public string TemplateName { get; set; }
		public ICollection<TemplateAttribute> TemplateAttributes { get; set; }
	}
}