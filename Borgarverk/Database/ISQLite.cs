using System;
using SQLite;

namespace Borgarverk
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}
