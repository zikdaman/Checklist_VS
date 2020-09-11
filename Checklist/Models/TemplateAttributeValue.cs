using Checklist.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class TemplateAttributeValue : BaseModel
	{
		public int TemplateAttributeValueID { get; set; }
		public string TemplateAttibuteValue { get; set; }

		[Required]
		public TemplateInstance TemplateInstance { get; set; }

		public TemplateAttribute TemplateAttribute { get; set; }

		public TemplateAttributeChild TemplateAttributeChild { get; set; }
	}
}