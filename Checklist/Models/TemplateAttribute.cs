using Checklist.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class TemplateAttribute : BaseModel
	{
		public int TemplateAttributeID { get; set; }
		public string TemplateAttributeName { get; set; }
		public string TemplateAttributeType { get; set; }
		public int TemplateAttributeOrder { get; set; }

		public ICollection<TemplateAttributeChild> TemplateAttributeChildren { get; set; }
		public ICollection<AttributeExtension> AttributeExtensions { get; set; }

		[Required]
		public int TemplateID { get; set; }
		public Template Template { get; set; }
	}
}