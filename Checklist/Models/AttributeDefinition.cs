using Checklist.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
	public class AttributeDefinition:BaseModel
	{
		public int AttributeDefinitionId { get; set; }
		[Display(Name="Type")]
		public string AttributeType { get; set; }
		[Display(Name = "Present As")]
		public string AttributeInput { get; set; }

		public ICollection<SubAttributeDefinition> SubAttributeDefinitions { get; set; }		
	}
}