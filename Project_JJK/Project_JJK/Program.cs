using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

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

            XDocument x = XDocument.Load("Project_JJK\\Connection Info.xml\\");

            var connBuilder = new MySqlConnectionStringBuilder();
            connBuilder.Server = x.Descendants("Server").ElementAt(0).Value;
            connBuilder.UserID = x.Descendants("UserID").ElementAt(0).Value;
            connBuilder.Password = x.Descendants("Password").ElementAt(0).Value;
            connBuilder.Database = x.Descendants("Database").ElementAt(0).Value;
            connBuilder.CharacterSet = x.Descendants("CharacterSet").ElementAt(0).Value;
            string strconn = connBuilder.ToString();
            MySqlConnection conn = new MySqlConnection(strconn);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO test1 VALUES (30, '예외처리')", conn);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return false;
            }            
        }
    }
}
