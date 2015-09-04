using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JJK.Modules
{
	public class SampleViewModule : NancyModule
	{
		public SampleViewModule()
		{
			Get["/sample/{viewName}"] = _ =>
			{
				string viewName = _.viewName;
				Console.WriteLine(viewName);
				return View[viewName];
			};
		}
	}
}
