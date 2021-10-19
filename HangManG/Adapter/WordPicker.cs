using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HangManG.Common;
using HangManG.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangManG.Adapter
{
    public class WordPicker
    {
        List<string> wordList;
        HangmanConnection connection;
        Random random;
        public WordPicker(Context context)
        {
            try
            {
                connection = new HangmanConnection(context);
                random = new Random();
                wordList = new List<string>();
                List<Word> words = connection.GetAllWords();
                foreach (Word word in words)
                {
                    wordList.Add(word.Text);
                }
            }
            catch (Exception ex) { }

        }

        public string GetGameWord()
        {
            int index = random.Next(wordList.Count());
            string t = wordList[index];
            if (t.Count() <= 10)
                return wordList[index];
            else
                return GetGameWord();
        }
    }
}