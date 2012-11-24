
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
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "Score")]			
	public class EndActivity : Activity
	{
		TextView tvScore;
		int score;

		int[] topNames = new int[] {
			Resource.Id.name1, Resource.Id.name2, Resource.Id.name3, Resource.Id.name4, Resource.Id.name5,
			Resource.Id.name6, Resource.Id.name7, Resource.Id.name8, Resource.Id.name9, Resource.Id.name10
		};
		int[] topScores = new int[] {
			Resource.Id.score1, Resource.Id.score2, Resource.Id.score3, Resource.Id.score4, Resource.Id.score5,
			Resource.Id.score6, Resource.Id.score7, Resource.Id.score8, Resource.Id.score9, Resource.Id.score10
		};
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.score);
			
			score = Intent.GetIntExtra ("SCORE", 0);

			tvScore = FindViewById<TextView> (Resource.Id.scoreTxt);
			tvScore.Text = ScoreText ();
			DisplayHighscores ();

			
			Button btnAgain = FindViewById<Button> (Resource.Id.btnAgain);
			btnAgain.Click += delegate {
				Intent i = new Intent (this, typeof (QuizActivity));
				i.RemoveExtra ("SCORE");
				StartActivity (i);
				Finish ();
			};
			
			Button btnQuit = FindViewById<Button> (Resource.Id.btnQuit);
			btnQuit.Click += delegate {
				Finish();
			};
		}
		public string ScoreText ()
		{
			string text ="";
			if (score==500) {
				text = "Perfect!";
			} else if (score<500 && score>=400) {
				text = "Well done";
			} else if (score<400 && score>=300) {
				text = "Not bad";
			} else if (score<300 && score>=200) {
				text = "How about another go?";
			} else if (score<200 && score>=100) {
				text = "Not a science fan?";
			} else {
				text = "Are you ok?";
			}
			return String.Format("{0}\n{1}",score,text);;
		}
		void DisplayHighscores ()
		{
			TextView pName, pScore;

			List<Score> allScores = new List<Score> ();
			try {
				var dbS = new ScoreCommands (this);
				allScores = dbS.TopScores ();
			} catch (System.Exception sysExc) {
				Toast.MakeText (this, sysExc.Message, ToastLength.Long);
			}
			Score[] highScores = allScores.ToArray ();

			for (int i = 0; i<10; i++) {
				Score s = highScores[i];
				pName = FindViewById<TextView>(topNames[i]);
				pScore = FindViewById<TextView>(topScores[i]);

				pName.Text = s.PlayerName;
				pScore.Text = s.ScoreNumber.ToString ();
			}
		}
	}
}

