using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Checklist.Models;

namespace Checklist.DataAccess
{
	public class BaseModel
	{
		[Display(Name = "Date Created")]
		public DateTime? CreatedDate { get; set; }
		[Display(Name = "Created By")]
		public string CreatedBy { get; set; }
		[Display(Name = "Date Modified")]
		public DateTime? ModifiedDate { get; set; }
		[Display(Name = "Modified By")]
		public string ModifiedBy { get; set; }
	}

	public class ChecklistDBContext : DbContext
	{
		public DbSet<Template> Templates { get; set; }
		public DbSet<TemplateAttribute> TemplateAttributes { get; set; }
		public DbSet<TemplateAttributeChild> TemplateAttributeChildren { get; set; }
		public DbSet<TemplateInstance> TemplateInstances { get; set; }
		public DbSet<TemplateAttributeValue> TemplateAttributeValues { get; set; }
		public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
		public DbSet<AttributeExtension> AttributeExtensions { get; set; }
		public DbSet<SubAttributeDefinition> SubAttributeDefinitions { get; set; }

		public override int SaveChanges()
		{
			AddTimestamps();
			return base.SaveChanges();
		}

		private void AddTimestamps()
		{
			var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

			var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
				? HttpContext.Current.User.Identity.Name
				: "Anonymous";

			foreach (var entity in entities)
			{
				if (entity.State == EntityState.Added)
				{
					((BaseModel)entity.Entity).CreatedDate = DateTime.Now;
					((BaseModel)entity.Entity).CreatedBy = currentUsername;
				}

				((BaseModel)entity.Entity).ModifiedDate = DateTime.Now;
				((BaseModel)entity.Entity).ModifiedBy = currentUsername;
			}
		}

		
	}
}