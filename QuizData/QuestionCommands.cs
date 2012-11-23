
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
	class QuestionCommands
	{
		private QuestionHelper qstHelp;
		public QuestionCommands(Context context)
		{
			qstHelp = new QuestionHelper(context);
			qstHelp.OnCreate(qstHelp.WritableDatabase);
		}
		
		public List<Question> GetAllQuestions()
		{
			Android.Database.ICursor quizCursor = qstHelp.ReadableDatabase.Query("Question", null, null, null, null, null, null, null);
			var questions = new List<Question>();
			while (quizCursor.MoveToNext())
			{
				Question qst = NewQuestion(quizCursor);
				questions.Add(qst);
			}
			return questions;
		}
		
		public long AddQuestion (string stringForQuestion, DateTime dateEntered)
		{
			string[] stringArrayForQuestion = stringForQuestion.Split('_');
			var values = new ContentValues();
			values.Put("ImageID", Convert.ToInt32(stringArrayForQuestion[0]));
			values.Put("QuestionString", stringArrayForQuestion[1]);
			values.Put("CorrectAnswer", stringArrayForQuestion[2]);
			values.Put("WrongAnswer1", stringArrayForQuestion[3]);
			values.Put("WrongAnswer2", stringArrayForQuestion[4]);
			values.Put("WrongAnswer3", stringArrayForQuestion[5]);
			values.Put("DateEntered", dateEntered.ToString());
			
			return qstHelp.WritableDatabase.Insert("Question", null, values);
		}

		private Question NewQuestion(Android.Database.ICursor cursor)
		{
			Question qst = new Question();
			qst.ImageID = cursor.GetInt(1);
			qst.QuestionString = cursor.GetString(2);
			qst.CorrectAnswer = cursor.GetString(3);
			qst.WrongAnswer1 = cursor.GetString(4);
			qst.WrongAnswer2 = cursor.GetString(5);
			qst.WrongAnswer3 = cursor.GetString(6);
			
			return (qst);
		}
	}
}

