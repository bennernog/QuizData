
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
	class ScoreCommands
	{
		private ScoreHelper scrHelp;
		public ScoreCommands(Context context)
		{
			scrHelp = new ScoreHelper(context);
			scrHelp.OnCreate(scrHelp.WritableDatabase);
		}
		
		public List<Score> GetAllScores()
		{
			Android.Database.ICursor quizCursor = scrHelp.ReadableDatabase.Query("QuizScore", null, null, null, null, null, null, null);
			var scores = new List<Score>();
			while (quizCursor.MoveToNext())
			{
				Score scr = MapScores(quizCursor);
				scores.Add(scr);
			}
			return scores;
		}
		
		public long AddScore(int scoreNumber, DateTime scoreDate, string name)
		{
			var values = new ContentValues();
			values.Put("ScoreNumber", scoreNumber);
			values.Put("ScoreDate", scoreDate.ToString());
			values.Put("PlayerName", name);
			
			return scrHelp.WritableDatabase.Insert("QuizScore", null, values);
		}
		
		
		public void DeleteScore(int scoreID)
		{
			string[] vals = new string[1];
			vals[0] = scoreID.ToString();
			
			scrHelp.WritableDatabase.Delete("QuizScore", "ScoreId=?", vals);
		}
		private Score MapScores(Android.Database.ICursor cursor)
		{
			Score scr = new Score();
			scr.ScoreID = cursor.GetInt(0);
			scr.ScoreDate = cursor.GetString(1);
			scr.ScoreNumber = cursor.GetInt(2);
			scr.PlayerName = cursor.GetString(3);
			
			return (scr);
		}
	}
}

