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

			var connBuilder = new MySqlConnectionStringBuilder
			{
				Server = xdoc.Descendants("Server").ElementAt(0).Value,
				UserID = xdoc.Descendants("UserID").ElementAt(0).Value,
				Password = xdoc.Descendants("Password").ElementAt(0).Value,
				Database = xdoc.Descendants("Database").ElementAt(0).Value,
				CharacterSet = xdoc.Descendants("CharacterSet").ElementAt(0).Value,
			};
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
					MySqlCommand cmd = new MySqlCommand("INSERT INTO LogServerStart (startTime, machineName, userName) VALUES(now(), '" + Environment.MachineName + "', '" + Environment.UserName + "')", conn);
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
