using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Project_JJK.Utils
{
	public static class DB
	{
		static string _connectionString;

		static DB ()
		{
			XDocument xdoc = XDocument.Load(@"Project_JJK\DBConnectionInfo.xml");

			var connBuilder = new MySqlConnectionStringBuilder();
			connBuilder.Server = xdoc.Descendants("Server").ElementAt(0).Value;
			connBuilder.UserID = xdoc.Descendants("UserID").ElementAt(0).Value;
			connBuilder.Password = xdoc.Descendants("Password").ElementAt(0).Value;
			connBuilder.Database = xdoc.Descendants("Database").ElementAt(0).Value;
			connBuilder.CharacterSet = xdoc.Descendants("CharacterSet").ElementAt(0).Value;
			_connectionString = connBuilder.ToString();
		}

		public static bool IsConnected
		{
			get
			{
				MySqlConnection conn = new MySqlConnection(_connectionString);

				try
				{
					conn.Open();
					MySqlCommand cmd = new MySqlCommand("INSERT INTO test1 VALUES(30, '예외처리')", conn);
					cmd.ExecuteNonQuery();

					return true;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
				finally
				{
					conn.Close();
				}
			}
		}
	}
}
