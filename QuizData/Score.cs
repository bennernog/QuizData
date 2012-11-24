
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
	class Score
	{
		public Score () { }
		public int ScoreID { get; set; }
		public int ScoreNumber { get; set; }
		public string ScoreDate { get; set; }
		public string PlayerName { get; set; }
	}
}

