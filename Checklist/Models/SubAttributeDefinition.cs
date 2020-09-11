using Checklist.DataAccess;
using System.ComponentModel.DataAnnotations;
namespace Checklist.Models
{
	public class SubAttributeDefinition:BaseModel
	{
		[Key]
		public int SubAttributeID { get; set; }
		public string SubAttributeType { get; set; }

		[Required]
		public int AttributeDefinitionId { get; set; }
		public AttributeDefinition AttributeDefinition { get; set; }
	}
}