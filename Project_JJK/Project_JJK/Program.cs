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

            if (DBConnection() == true)
           {
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
           else
               Console.WriteLine("DB 접속 실패!");  			
        }

        public static bool DBConnection()
        {
    
            XmlDocument x = new XmlDocument();
            x.Load("Project_JJK\\Connection Info.xml\\");

            var connBuilder = new MySqlConnectionStringBuilder();
            connBuilder.Server = x.GetElementsByTagName("Server")[0].InnerText;
            connBuilder.UserID = x.GetElementsByTagName("UserID")[0].InnerText;
            connBuilder.Password = x.GetElementsByTagName("Password")[0].InnerText;
            connBuilder.Database = x.GetElementsByTagName("Database")[0].InnerText;
            connBuilder.CharacterSet = x.GetElementsByTagName("CharacterSet")[0].InnerText;
            string strconn = connBuilder.ToString();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(strconn))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO test1 VALUES (30, '예외처리')", conn);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }            
        }
    }
}
