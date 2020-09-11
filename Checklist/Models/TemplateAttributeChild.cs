using Checklist.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class TemplateAttributeChild : BaseModel
	{
		public int TemplateAttributeChildID { get; set; }
		public string TemplateAttributeChildName { get; set; }
		public string TemplateAttributeChildType { get; set; }
		public int TemplateAttributeChildOrder { get; set; }

		[Required]
		public TemplateAttribute TemplateAttribute { get; set; }
	}
}