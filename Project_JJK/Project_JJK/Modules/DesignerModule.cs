using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JJK.Modules
{
	public class DesignerModule : NancyModule
	{
		public DesignerModule()
		{
			Get["/designer/add"] = _ =>
			{
				return View["designer-add"];
			};

			Post["/designer/add"] = _ =>
			{
				string name = Request.Form["name"];
				string phone = Request.Form["phone"];

				Console.WriteLine("Add Designer: {0}, {1}", name, phone);

				return "result";
			};
		}
	}
}
