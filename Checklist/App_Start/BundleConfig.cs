﻿using System.Web.Optimization;

namespace Checklist
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			bundles.Add(new ScriptBundle("~/bundles/Mobilejs").Include(
					"~/Scripts/jquery.mobile-1.*",
					"~/Scripts/jquery-{version}.js"));

			bundles.Add(new StyleBundle("~/Content/Mobilecss").Include(
					"~/Content/jquery.mobile.structure-1.4.5.min.css",
					"~/Content/jquery.mobile-1.4.5.css"));

			bundles.Add(new ScriptBundle("~/bundles/customscripts").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/Sortable.min.js",
						"~/Scripts/customscripts.js"));
		}
	}
}
