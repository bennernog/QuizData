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
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "AstroQuiz2", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.welcome);

			MakeQuestionDB ();

			Button button = FindViewById<Button> (Resource.Id.btnStarQuiz);
			button.Click += delegate {
				StartActivity(typeof(QuizActivity));
				Finish();
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
	}
}


