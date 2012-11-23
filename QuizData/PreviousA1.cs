using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using System.Collections.Generic;

namespace QuizData
{
//	[Activity (Label = "PreviousA1")]			
//	public class PreviousA1 : Activity
//	{
//		TextView tv;
//		readonly string DatabaseName = "UserData.db3";//TODO why read only?
//		string documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
//		List<int> randoms;
//		
//		protected override void OnCreate (Bundle bundle)
//		{
//			base.OnCreate (bundle);
//			SetContentView (Resource.Layout.Main);
//			
//			randoms =  new List<int> ();
//			tv = FindViewById<TextView>(Resource.Id.Results);
//			FindViewById<Button> (Resource.Id.myButton).Click += CreateDataBaseButton_Click;
//			Button PullDataButton = FindViewById<Button>(Resource.Id.myGetButton);
//			PullDataButton.Click += PullDataButton_Click;
//		}
//		
//		string[] getQuestion (int index)
//		{
//			string[] stringArrayForQuestion = Resources.GetStringArray(Resource.Array.quiz);
//			string stringForQuestion = stringArrayForQuestion[index];
//			return stringForQuestion.Split('_');
//			
//		}
//		
//		string dbPath ()
//		{
//			return Path.Combine(documents, DatabaseName); 
//		}
//		
//		int RandNumber ()
//		{
//			int rnd = new int ();
//			Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
//			do
//			{
//				rnd = rndNum.Next(0, 19);
//			} while (randoms.Contains(rnd));
//			randoms.Add(rnd);
//			
//			return rnd;
//		}
//		
//		void CreateDataBaseButton_Click (object sender, EventArgs e)
//		{
//			bool exists = File.Exists (dbPath ());  
//			if (exists) {
//				File.Delete(dbPath ());
//				SqliteConnection.CreateFile(dbPath ());
//			}
//			if (!exists)  
//			{
//				SqliteConnection.CreateFile(dbPath ());
//			}
//			var conn = new SqliteConnection("Data Source=" + dbPath ());
//			
//			var commands = new[] { 
//				"CREATE TABLE IF NOT EXISTS QUESTION(QUESTIONID BIGINT PRIMARY KEY, " +
//				"IMAGEID INT, QUESTIONSTRING VARCHAR(150),CORRECTANSWER VARCHAR(150),"+
//				"WRONGANSWER1 VARCHAR(100),WRONGANSWER2 VARCHAR(100),WRONGANSWER3 VARCHAR(100),"+
//				"DATEENTERED DATETIME)",
//				"CREATE TRIGGER IF NOT EXISTS QUESTION_INSERT INSERT ON QUESTION " +
//				"BEGIN UPDATE QUESTION SET DATEENTERED=DATE('now') " +
//				"WHERE QUESTIONID=NEW.QUESTIONID; END;",
//				"CREATE INDEX IF NOT EXISTS IDX_QUESTIONNAME ON QUESTION (IMAGEID)",
//				"CREATE INDEX IF NOT EXISTS IDX_DATEENTERED ON QUESTION (DATEENTERED);"};
//			
//			try
//			{
//				foreach (var cmd in commands)
//					using (var sqlitecmd = conn.CreateCommand())
//				{
//					sqlitecmd.CommandText = cmd;
//					sqlitecmd.CommandType = CommandType.Text;
//					conn.Open();
//					sqlitecmd.ExecuteNonQuery();
//					conn.Close();
//				}
//				
//				SqliteCommand sqlc = new SqliteCommand();
//				sqlc.Connection = conn;
//				conn.Open();
//				
//				string strSql = "INSERT INTO QUESTION" + 
//					"(IMAGEID, QUESTIONSTRING, CORRECTANSWER, " + 
//						"WRONGANSWER1, WRONGANSWER2,WRONGANSWER3) " +  
//						"VALUES (@IMAGEID, @QUESTIONSTRING, @CORRECTANSWER, " + 
//						"@WRONGANSWER1, @WRONGANSWER2, @WRONGANSWER3)";
//				
//				sqlc.CommandText = strSql;
//				sqlc.CommandType = CommandType.Text;
//				for (int i =0; i<(Resources.GetStringArray(Resource.Array.quiz)).Length; i++){
//					
//					string[] newQuestionArray = getQuestion(i);
//					sqlc.Parameters.Add(new SqliteParameter("@IMAGEID", Convert.ToInt32(newQuestionArray[0])));
//					sqlc.Parameters.Add(new SqliteParameter("@QUESTIONSTRING", newQuestionArray[1]));
//					sqlc.Parameters.Add(new SqliteParameter("@CORRECTANSWER", newQuestionArray[2]));
//					sqlc.Parameters.Add(new SqliteParameter("@WRONGANSWER1", newQuestionArray[3]));
//					sqlc.Parameters.Add(new SqliteParameter("@WRONGANSWER2", newQuestionArray[4]));
//					sqlc.Parameters.Add(new SqliteParameter("@WRONGANSWER3", newQuestionArray[5]));
//					sqlc.ExecuteNonQuery();
//				}
//				
//				if (conn.State != ConnectionState.Closed)
//				{
//					conn.Close();
//				}
//				conn.Dispose();
//				
//				tv.Text = "Commands completed.";
//			}
//			catch (System.Exception sysExc)
//			{
//				tv.Text = "Exception: " + sysExc.Message;
//			}
//		}
//		
//		void PullDataButton_Click(object sender, EventArgs e)
//		{
//			//TODO general question
//			/* I thought about looking into storing the images in the database as well
//			 * Why is it better to write everything to a (local)database if all the info has to be contained in the app anyways?
//			 * Isn't that double work?
//			 */
//			
//			/*
//			 * -> there are databases that handle binary data (images) as blob storage. In the abstraction layer we use, we add bindary data such
//			 * as images to the object and our datamanager handles the storage.
//			 * What you could do is, store the filename as a string in the database, and load the image from there.
//			 * 
//			 * The reason we store this data locally, is 1 persistence between multiple runs of the application. Secondly, SQL allows you the 
//			 * request, filter, ... data in a structured way.
//			 * 
//			 * Does that answer your question?
//			 */ 
//			var conn = new SqliteConnection("Data Source=" + dbPath ());
//			var strSql = "select QuestionString, CorrectAnswer, WrongAnswer1, WrongAnswer2, WrongAnswer3 from Question where IMAGEID=@IMAGEID";
//			var cmd = new SqliteCommand(strSql, conn);
//			cmd.CommandType = CommandType.Text;
//			cmd.Parameters.Add(new SqliteParameter("@IMAGEID", RandNumber ()));
//			
//			try
//			{
//				conn.Open();
//				
//				var sdr = cmd.ExecuteReader ();
//				
//				while (sdr.Read())
//				{
//					// TODO RunOnUiThread
//					/* Found out how - not sure why though?
//					 */ 
//					string questionString, correctAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3;
//					questionString = Convert.ToString(sdr["Questionstring"]);
//					correctAnswer = Convert.ToString(sdr["Correctanswer"]);
//					wrongAnswer1 = Convert.ToString(sdr["WrongAnswer1"]);
//					wrongAnswer2 = Convert.ToString(sdr["WrongAnswer2"]);
//					wrongAnswer3 = Convert.ToString(sdr["WrongAnswer3"]);
//					RunOnUiThread(() => tv.Text = questionString+" - "+correctAnswer);
//				}
//			}
//			catch (System.Exception sysExc)
//			{
//				tv.Text = sysExc.Message;
//			}
//			finally
//			{
//				if (conn.State != ConnectionState.Closed)
//				{
//					conn.Close();
//				}
//				conn.Dispose();
//			}
//		}
//	}
}


