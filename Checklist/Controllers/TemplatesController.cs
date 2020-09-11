using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Checklist.DataAccess;
using Checklist.Models;

namespace Checklist.Controllers
{
	public class TemplatesController : Controller
    {
        private ChecklistDBContext db = new ChecklistDBContext();

        // GET: Templates
        public ActionResult Index()
        {
            return View(db.Templates.ToList());
        }

		// GET: Templates/_TemplateTableRefresh
		public ActionResult TemplateTableRefresh()
		{
			return PartialView(db.Templates.ToList());
		}

		// GET: Templates/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Template template = db.Templates.Find(id);

			if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // GET: Templates/Create
        public ActionResult Create()
        {
			
			return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Template template)
        {
            if (ModelState.IsValid)
            {				
                db.Templates.Add(template);
                db.SaveChanges();
				return RedirectToAction("Index");
				//return Json(new { success = true });
            }

            return Json(new { succcess = false });
        }

		// GET: Templates/Edit/5
		public ActionResult Edit(int? id)
        {

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//Template template = db.Templates.Find(id);
			db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
			Template template = db.Templates.Include(t => t.TemplateAttributes.Select(a=>a.AttributeExtensions))
				   .Where(t => t.TemplateID == id).FirstOrDefault<Template>();

			if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Template template)
        {
            if (ModelState.IsValid)
            {
				if (template.TemplateAttributes != null)
				{
					foreach (var attribute in template.TemplateAttributes)
					{
						if (attribute.TemplateAttributeID > 0)
						{
							if (attribute.AttributeExtensions != null)
							{
								foreach (var extension in attribute.AttributeExtensions)
								{
									if (extension.AttributeExtentionID > 0)
									{
										db.Entry(extension).State = EntityState.Modified;
									}
									else
									{
										extension.TemplateAttributeID = attribute.TemplateAttributeID;
										db.Entry(extension).State = EntityState.Added;
									}
								}
							}
							db.Entry(attribute).State = EntityState.Modified;
							db.Entry(attribute).Property(p => p.CreatedBy).IsModified = false;
							db.Entry(attribute).Property(p => p.CreatedDate).IsModified = false;
						}
						else
						{
							attribute.TemplateID = template.TemplateID;
							db.Entry(attribute).State = EntityState.Added;
						}
					}
				}
				db.Entry(template).State = EntityState.Modified;
				db.Entry(template).Property(p => p.CreatedBy).IsModified = false;
				db.Entry(template).Property(p => p.CreatedDate).IsModified = false;
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(template);
        }

        // GET: Templates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = db.Templates.Find(id);
            db.Templates.Remove(template);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		// GET: Templates/GetTemplateAttributes/5
		public ActionResult GetTemplateAttributes(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var templateAttributes = from a in db.TemplateAttributes
									 where a.Template.TemplateID==id
									select a;

			if (templateAttributes == null)
			{
				return HttpNotFound();
			}
			return View(templateAttributes);
		}

		// GET: Templates/BlankTemplateAttribute/108
		public PartialViewResult BlankTemplateAttribute(string attributeType="")
		{
			TemplateAttribute templateAttribute = new TemplateAttribute();

			if (attributeType != "")
			{
				templateAttribute.TemplateAttributeType = attributeType;
			}

			string pView;
			switch (attributeType) {
				case "Text":
					pView = "_TemplateAttributes";
					break;
				case "Select":
					pView = "_TemplateAttributesList";
					break;
				case "Autofill":
					pView = "_TemplateAttributesAutofill";
					break;
				default:
					pView = "_TemplateAttributes";
					break;
			}
			return PartialView(pView,templateAttribute);
		}

		// GET: Templates/CreateAttributeDefinition
		public ActionResult CreateAttributeDefinition()
		{
			return View();
		}

		// POST: Templates/CreateAttributeDefinition
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateAttributeDefinition(AttributeDefinition attributeDefinition)
		{
			if (ModelState.IsValid)
			{
				db.AttributeDefinitions.Add(attributeDefinition);
				db.SaveChanges();
				return RedirectToAction("GetAttributeDefinitions");
			}

			return View();
		}

		// GET: Templates/GetAttributeDefinitions
		public ActionResult GetAttributeDefinitions()
		{
			return View(db.AttributeDefinitions.ToList());
		}

		// GET: Templates/AttributeTypeDropdown/
		public ActionResult AttributeTypeDropdown()
		{
			List<AttributeDefinition> attributeDefinition = db.AttributeDefinitions.ToList();
			ViewBag.Data = attributeDefinition;
			return View("_AttributeTypeDropdown",attributeDefinition);
		}

		public PartialViewResult BlankListOption(string prefix)
		{
			ViewBag.Prefix = prefix;
			AttributeExtension attributeExtension = new AttributeExtension();
			return PartialView("_ListOption", attributeExtension);
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
