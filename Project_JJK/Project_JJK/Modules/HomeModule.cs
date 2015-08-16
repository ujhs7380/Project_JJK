using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using System.Dynamic;

namespace Project_JJK.Modules
{
	public class HomeModule : NancyModule
	{
		/// <summary>
		/// http://localhost 로 들어온 명령을 처리한다.
		/// Get["/"]은 기본 도메인 주소일 때 호출되는 함수.
		/// View["home"] 이 의미하는 것은 Views 폴더에서 home.cshtml 을 찾아서 보여주겠다는 뜻.
		/// </summary>
		public HomeModule()
		{
			//Get["/"] = _ => View["home"]; // 이렇게 써도 되지만 View 하기 전에 다른 로직들이 있다면 아래처럼 해야겠지.

			Get["/"] = _ =>
			{
				return View["home"];
			};
			Get["Test"] = _ =>
			{
				return View["Test"];
			};

			Post["/designer"] = _ =>
			{
				string designerName = Request.Query["designerName"];

				//Designer.AddDesigner();
			};
		}
	}
}
