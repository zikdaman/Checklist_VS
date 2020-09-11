using Checklist.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class TemplateInstance : BaseModel
	{
		public int TemplateInstanceID { get; set; }

		public ICollection<TemplateAttributeValue> TemplateAttributeValues { get; set; }

		[Required]
		public Template Template { get; set; }
	}
}