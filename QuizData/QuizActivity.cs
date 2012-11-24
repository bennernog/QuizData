using System;

using Android.App;
using Android.Util;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace QuizData
{
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "Astro")]			
	public class QuizActivity : Activity
	{
		Button button1, button2, button3, button4;
		TextView tvQuestion;
		LinearLayout mainLL;
		
		Question[] quiz;
		string answer;

		int[] imageId = new int[]{
			Resource.Drawable.vraag00, Resource.Drawable.vraag01, Resource.Drawable.vraag02, Resource.Drawable.vraag03,
			Resource.Drawable.vraag04, Resource.Drawable.vraag05, Resource.Drawable.vraag06, Resource.Drawable.vraag07,
			Resource.Drawable.vraag08, Resource.Drawable.vraag09, Resource.Drawable.vraag10, Resource.Drawable.vraag11,
			Resource.Drawable.vraag12, Resource.Drawable.vraag13, Resource.Drawable.vraag14, Resource.Drawable.vraag15,
			Resource.Drawable.vraag16, Resource.Drawable.vraag17, Resource.Drawable.vraag18, Resource.Drawable.vraag19
		};
		int count = 0, score = 0, points = 0;
		
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			if (bundle!=null) score = bundle.GetInt ("SCORE",0);
			SetContentView (Resource.Layout.Main);

			mainLL = FindViewById<LinearLayout> (Resource.Id.LLmain);
			tvQuestion = FindViewById<TextView> (Resource.Id.textView1);
			button1 = FindViewById<Button> (Resource.Id.btn1);
			button2 = FindViewById<Button> (Resource.Id.btn2);
			button3 = FindViewById<Button> (Resource.Id.btn3);
			button4 = FindViewById<Button> (Resource.Id.btn4);
			
			button1.Click += button_Click;
			button2.Click += button_Click;
			button3.Click += button_Click;
			button4.Click += button_Click;
			
			quiz = NewQuiz ();
			NewQuestion (quiz);
		}
		
		private void button_Click (object sender, EventArgs e)
		{
			Button btn = (Button) sender;
			if (btn.Text.Equals (answer)) {
				count++;
				Toast.MakeText (this, answerText(), ToastLength.Short).Show();
				addToScore ();
				
				if (count<10)
				{
					NewQuestion (quiz);
				}
				else
				{
					Dialog d = new Dialog (this);
					d = scoreDialog ();
					d.Show ();
				}
				
				
			} else 
			{
				
				Toast.MakeText (this, "Try Again", ToastLength.Short).Show();
				points++;
				
			}
			
		}  
		
		public Question[] NewQuiz ()
		{
			List<Question> allquestions = new List<Question> ();
			try
			{
				var qC = new QuestionCommands (this);
				allquestions = qC.GetAllQuestions ();
			}
			catch (System.Exception sysExc)
			{
				Toast.MakeText (this, sysExc.Message, ToastLength.Long);
			}
			allquestions = Shuffle (allquestions);

			return allquestions.ToArray ();
		}

		public void NewQuestion(Question[] questionList)
		{
			Question q = questionList[count];
			answer = q.CorrectAnswer;

			SetImage (q.ImageID);
			SetNewQuestion (q.QuestionString);
			SetAnswers (answer, q.WrongAnswer1, q.WrongAnswer2, q.WrongAnswer3); 
		}
		
		public void SetImage (int index)
		{
			mainLL.SetBackgroundResource (imageId[index]);
		}
		public void SetNewQuestion (string questionString)
		{
			string q = String.Format ("{0}. {1}", (count+1).ToString (),questionString);
			tvQuestion.Text = q;
		}
		public void SetAnswers (string cAnswer, string wAnswer1, string wAnswer2, string wAnswer3)
		{
			List<string> answers = new List<string>();
			answers.Add (cAnswer);
			answers.Add (wAnswer1);
			answers.Add (wAnswer2);
			answers.Add (wAnswer3);
			answers=Shuffle (answers);
			string [] mixedAnswers = answers.ToArray();
			
			button1.Text = mixedAnswers[0];
			button2.Text = mixedAnswers[1];
			button3.Text = mixedAnswers[2];
			button4.Text = mixedAnswers[3];
			
		}

		private void addToScore ()
		{
			switch (points) {
			case 0:
				score += 50;
				break;
			case 1:
				score += 40;
				break;
			case 2:
				score += 30;
				break;
			case 3:
				score += 20;
				break;
			default:
				score += 0;
				break;
			}
			points = 0;
		}

		private string answerText ()
		{
			string text;
			switch (points) {
			case 0:
				text = "Perfect!";
				break;
			case 1:
				text = "Well done";
				break;
			case 2:
				text = "Good guess";;
				break;
			case 3:
				text = "About time!!";
				break;
			default:
				text = "Seriously?";
				break;
			}
			return text;
		}

		public static List<T> Shuffle<T> ( List<T> list)
		{
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
			int n = list.Count;
			while (n > 1)
			{
				byte[] box = new byte[1];
				do provider.GetBytes(box);
				while (!(box[0] < n * (Byte.MaxValue / n)));
				int k = (box[0] % n);
				n--;
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
		}
		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutInt ("SCORE", score);
			base.OnSaveInstanceState (outState);
		}
		Dialog scoreDialog ()
		{
			Dialog dialog = new Dialog (this);
			dialog.SetContentView (Resource.Layout.namePrompt);
			dialog.SetTitle ("Enter Your Name"); 
			dialog.SetCancelable (true);

			TextView tvScore = (TextView) dialog.FindViewById (Resource.Id.score);
			tvScore.Text = String.Format ("Your Score: {0}",score);
			EditText input = (EditText) dialog.FindViewById (Resource.Id.etName);
			Button ok = (Button) dialog.FindViewById (Resource.Id.ok);

			ok.Click += delegate {
				if (input.Text.Length > 0) {

					var dbS = new ScoreCommands (this);
					dbS.AddScore (score, DateTime.Now, input.Text);
					Intent i = new Intent (this, typeof (EndActivity));
					i.PutExtra ("SCORE", score);
					StartActivity (i);
					Finish ();
					dialog.Cancel ();

				} else {
					Toast.MakeText (this, "Enter Name", ToastLength.Short).Show ();
				}
				

			}; 

			return dialog;
		}
	}
}

