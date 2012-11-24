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
using Android.Database.Sqlite;

namespace QuizData
{
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "AstroQuiz2", MainLauncher = true)]
	public class Activity1 : Activity
	{
		ISharedPreferences prefs;
		ISharedPreferencesEditor editor;
		bool firstTime;
		private const string FIRST = "FIRST";
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.welcome);

			prefs = GetPreferences (FileCreationMode.Append);
			firstTime = prefs.GetBoolean (FIRST, true);
			if (firstTime) {
				MakeQuestionDB ();
				MakeScoreDB ();
				editor = prefs.Edit ();
				editor.PutBoolean (FIRST, false);
				editor.Commit ();

			}

			Button button = FindViewById<Button> (Resource.Id.btnStarQuiz);
			button.Click += delegate {
				StartActivity (typeof(QuizActivity));
				Finish ();
			};
			
		}

		void MakeQuestionDB ()
		{
			var dbq = new QuestionCommands (this);
			string[] qsts = Resources.GetStringArray (Resource.Array.quiz);
			foreach (string s in qsts) {
				dbq.AddQuestion (s, DateTime.Now);
			}
		}

		void MakeScoreDB ()
		{
			var dbs = new ScoreCommands (this);
			string[] scr = Resources.GetStringArray (Resource.Array.scores);

			foreach (string s in scr) {
				string[] st = s.Split ('_');

				dbs.AddScore (Convert.ToInt32 (st[0]), DateTime.Now, st[1]);
			}
		}
	}
}


