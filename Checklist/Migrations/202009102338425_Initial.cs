namespace Checklist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttributeDefinitions",
                c => new
                    {
                        AttributeDefinitionId = c.Int(nullable: false, identity: true),
                        AttributeType = c.String(),
                        AttributeInput = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.AttributeDefinitionId);
            
            CreateTable(
                "dbo.SubAttributeDefinitions",
                c => new
                    {
                        SubAttributeID = c.Int(nullable: false, identity: true),
                        SubAttributeType = c.String(),
                        AttributeDefinitionId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.SubAttributeID)
                .ForeignKey("dbo.AttributeDefinitions", t => t.AttributeDefinitionId, cascadeDelete: true)
                .Index(t => t.AttributeDefinitionId);
            
            CreateTable(
                "dbo.AttributeExtensions",
                c => new
                    {
                        AttributeExtentionID = c.Int(nullable: false, identity: true),
                        AttributeExtentionType = c.String(),
                        AttributeExtentionData = c.String(),
                        TemplateAttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttributeExtentionID)
                .ForeignKey("dbo.TemplateAttributes", t => t.TemplateAttributeID, cascadeDelete: true)
                .Index(t => t.TemplateAttributeID);
            
            CreateTable(
                "dbo.TemplateAttributes",
                c => new
                    {
                        TemplateAttributeID = c.Int(nullable: false, identity: true),
                        TemplateAttributeName = c.String(),
                        TemplateAttributeType = c.String(),
                        TemplateAttributeOrder = c.Int(nullable: false),
                        TemplateID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.TemplateAttributeID)
                .ForeignKey("dbo.Templates", t => t.TemplateID, cascadeDelete: true)
                .Index(t => t.TemplateID);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        TemplateID = c.Int(nullable: false, identity: true),
                        TemplateName = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.TemplateID);
            
            CreateTable(
                "dbo.TemplateInstances",
                c => new
                    {
                        TemplateInstanceID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        Template_TemplateID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TemplateInstanceID)
                .ForeignKey("dbo.Templates", t => t.Template_TemplateID, cascadeDelete: true)
                .Index(t => t.Template_TemplateID);
            
            CreateTable(
                "dbo.TemplateAttributeValues",
                c => new
                    {
                        TemplateAttributeValueID = c.Int(nullable: false, identity: true),
                        TemplateAttibuteValue = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        TemplateAttribute_TemplateAttributeID = c.Int(),
                        TemplateAttributeChild_TemplateAttributeChildID = c.Int(),
                        TemplateInstance_TemplateInstanceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TemplateAttributeValueID)
                .ForeignKey("dbo.TemplateAttributes", t => t.TemplateAttribute_TemplateAttributeID)
                .ForeignKey("dbo.TemplateAttributeChilds", t => t.TemplateAttributeChild_TemplateAttributeChildID)
                .ForeignKey("dbo.TemplateInstances", t => t.TemplateInstance_TemplateInstanceID, cascadeDelete: true)
                .Index(t => t.TemplateAttribute_TemplateAttributeID)
                .Index(t => t.TemplateAttributeChild_TemplateAttributeChildID)
                .Index(t => t.TemplateInstance_TemplateInstanceID);
            
            CreateTable(
                "dbo.TemplateAttributeChilds",
                c => new
                    {
                        TemplateAttributeChildID = c.Int(nullable: false, identity: true),
                        TemplateAttributeChildName = c.String(),
                        TemplateAttributeChildType = c.String(),
                        TemplateAttributeChildOrder = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        TemplateAttribute_TemplateAttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TemplateAttributeChildID)
                .ForeignKey("dbo.TemplateAttributes", t => t.TemplateAttribute_TemplateAttributeID, cascadeDelete: true)
                .Index(t => t.TemplateAttribute_TemplateAttributeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemplateAttributeValues", "TemplateInstance_TemplateInstanceID", "dbo.TemplateInstances");
            DropForeignKey("dbo.TemplateAttributeValues", "TemplateAttributeChild_TemplateAttributeChildID", "dbo.TemplateAttributeChilds");
            DropForeignKey("dbo.TemplateAttributeChilds", "TemplateAttribute_TemplateAttributeID", "dbo.TemplateAttributes");
            DropForeignKey("dbo.TemplateAttributeValues", "TemplateAttribute_TemplateAttributeID", "dbo.TemplateAttributes");
            DropForeignKey("dbo.TemplateInstances", "Template_TemplateID", "dbo.Templates");
            DropForeignKey("dbo.TemplateAttributes", "TemplateID", "dbo.Templates");
            DropForeignKey("dbo.AttributeExtensions", "TemplateAttributeID", "dbo.TemplateAttributes");
            DropForeignKey("dbo.SubAttributeDefinitions", "AttributeDefinitionId", "dbo.AttributeDefinitions");
            DropIndex("dbo.TemplateAttributeChilds", new[] { "TemplateAttribute_TemplateAttributeID" });
            DropIndex("dbo.TemplateAttributeValues", new[] { "TemplateInstance_TemplateInstanceID" });
            DropIndex("dbo.TemplateAttributeValues", new[] { "TemplateAttributeChild_TemplateAttributeChildID" });
            DropIndex("dbo.TemplateAttributeValues", new[] { "TemplateAttribute_TemplateAttributeID" });
            DropIndex("dbo.TemplateInstances", new[] { "Template_TemplateID" });
            DropIndex("dbo.TemplateAttributes", new[] { "TemplateID" });
            DropIndex("dbo.AttributeExtensions", new[] { "TemplateAttributeID" });
            DropIndex("dbo.SubAttributeDefinitions", new[] { "AttributeDefinitionId" });
            DropTable("dbo.TemplateAttributeChilds");
            DropTable("dbo.TemplateAttributeValues");
            DropTable("dbo.TemplateInstances");
            DropTable("dbo.Templates");
            DropTable("dbo.TemplateAttributes");
            DropTable("dbo.AttributeExtensions");
            DropTable("dbo.SubAttributeDefinitions");
            DropTable("dbo.AttributeDefinitions");
        }
    }
}
