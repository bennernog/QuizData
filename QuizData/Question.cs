
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
	public class Question
	{
		public Question () { }
		public int ImageID { get; set; }
		public string QuestionString { get; set; }
		public string CorrectAnswer { get; set; }
		public string WrongAnswer1 { get; set; }
		public string WrongAnswer2 { get; set; }
		public string WrongAnswer3 { get; set; }
		public string DateEntered { get; set; }
	}
}

