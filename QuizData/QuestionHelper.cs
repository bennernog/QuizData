
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
	class QuestionHelper : Android.Database.Sqlite.SQLiteOpenHelper
	{
		private const string DbName = "QuizDb";
		private const int DbVersion = 1;
		
		public QuestionHelper (Context context) : base (context, DbName, null, DbVersion)
		{
			
		}
		
		
		public override void OnCreate (Android.Database.Sqlite.SQLiteDatabase db)
		{
			db.ExecSQL (@"CREATE TABLE IF NOT EXISTS Question (QuestionID INTEGER PRIMARY KEY AUTOINCREMENT," +
			           "IMAGEID NOT NULL, QUESTIONSTRING VARCHAR(150) NOT NULL,CORRECTANSWER VARCHAR(150) NOT NULL,"+
			           "WRONGANSWER1 VARCHAR(100) NOT NULL,WRONGANSWER2 VARCHAR(100) NOT NULL,WRONGANSWER3 VARCHAR(100) NOT NULL,"+
			           "DATEENTERED varchar(30) NOT NULL)");
		}
		
		public override void OnUpgrade (Android.Database.Sqlite.SQLiteDatabase db, int oldVersion, int newVersion)
		{
			db.ExecSQL ("DROP TABLE IF EXISTS Question");
			
			OnCreate (db);
		}
		
		
	}
}

