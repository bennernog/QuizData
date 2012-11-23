
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuizData
{
	class ScoreHelper : Android.Database.Sqlite.SQLiteOpenHelper
	{
		private const string DbName = "QuizScore";
		private const int DbVersion = 1;
		
		public ScoreHelper(Context context) : base(context, DbName, null, DbVersion)
		{
			
		}
		
		
		public override void OnCreate(Android.Database.Sqlite.SQLiteDatabase db)
		{
			db.ExecSQL(@"CREATE TABLE IF NOT EXISTS QuizScore (QuizID INTEGER PRIMARY KEY AUTOINCREMENT," +
			           "ScoreDate varchar(30) NOT NULL, PlayerName varchar(10) NOT NULL, ScoreNumber NOT NULL");
		}
		
		public override void OnUpgrade(Android.Database.Sqlite.SQLiteDatabase db, int oldVersion, int newVersion)
		{
			db.ExecSQL("DROP TABLE IF EXISTS QuizScore");
			
			OnCreate(db);
		}
		
		
	}
}

