using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Xml;

namespace Project_JJK
{
    class Program
    {
        static void Main(string[] args)
        {
            Insert();

  			// Nancy는 WebServer!!
			// 음 그러니까 웹로 접속 할 수 있는 뭔가를 만들겠다는 뜻!
			// https://ko.wikipedia.org/wiki/웹_서버
			using (var host = new NancyHost(new Uri("http://localhost:80")))
			{
				host.Start(); // 시작한다.
				Console.WriteLine("Start server.");
				Console.ReadLine(); // 이거 안해주면 host가 그냥 끝나버린다.
			}
        }

        public static void Insert()
        {
            XmlDocument x = new XmlDocument();
            x.Load("Project_JJK\\Connection Info.xml\\");
            XmlNodeList Servers = x.GetElementsByTagName("Server");
            XmlNodeList UserIDs = x.GetElementsByTagName("UserID");
            XmlNodeList Passwords = x.GetElementsByTagName("Password");
            XmlNodeList Databases = x.GetElementsByTagName("Database");
            XmlNodeList CharacterSets = x.GetElementsByTagName("CharacterSet");

            var connBuilder = new MySqlConnectionStringBuilder();
            connBuilder.Server = Servers[0].InnerText;
            connBuilder.UserID = UserIDs[0].InnerText;
            connBuilder.Password = Passwords[0].InnerText;
            connBuilder.Database = Databases[0].InnerText;
            connBuilder.CharacterSet = CharacterSets[0].InnerText;
            string strconn = connBuilder.ToString();

            
            using (MySqlConnection conn = new MySqlConnection(strconn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO test1 VALUES (25, '보안체크')", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
