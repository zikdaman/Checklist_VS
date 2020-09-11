using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class AttributeExtension
	{
		[Key]
		public int AttributeExtentionID { get; set; }
		public string AttributeExtentionType { get; set; }
		public string AttributeExtentionData { get; set; }

		[Required]
		public int TemplateAttributeID { get; set; }
		public TemplateAttribute TemplateAttribute { get; set; }
	}
}