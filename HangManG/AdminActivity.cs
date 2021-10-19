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

namespace HangManG
{
    [Activity(Label = "AdminActivity")] // Activity label 
    public class AdminActivity : Activity
    {
        Button btnSave, btnExit;    /// <summary>
                                    /// //buttons for save and exit the game 
                                    /// </summary>
        EditText etWord;
        HangmanConnection connection; // connevtion string
        ListView list; // list view 
        List<string> words; // words in the list 
        ArrayAdapter<string> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_admin);
            connection = new HangmanConnection(this);
            words = connection.GetAllWordsString();

            btnExit = FindViewById<Button>(Resource.Id.btnExit);  // exit button 
            btnSave = FindViewById<Button>(Resource.Id.btnSave); // save button
            etWord = FindViewById<EditText>(Resource.Id.etWord);
            list = FindViewById<ListView>(Resource.Id.list);

            btnExit.Click += BtnExit_Click;
            btnSave.Click += BtnSave_Click;

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, words);
            list.Adapter = adapter;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string wordstring = etWord.Text.Trim().ToUpper();
            string message = "";
            if (wordstring.Length == 0)
            {
                message = "Please Fill All Boxes"; // mesaage show 
            }
            else
            {
                Word word = new Word();  // else statement run
                word.Text = wordstring;
                if (connection.SaveWord(word))
                {
                    message = "Word is Saved"; // mesaage box show 
                    words.Add(wordstring);
                    adapter.NotifyDataSetChanged();
                }
                else
                {
                    message = "This Word is Already in List";  // message bos show 
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show(); // toast message show
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Finish(); // exit function on click btnexit
        }
    }
}