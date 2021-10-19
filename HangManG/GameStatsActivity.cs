using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using HangManG.Adapter;
using HangManG.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangManG
{
    [Activity(Label = "GameStatsActivity")]   // start game activity label 
    public class GameStatsActivity : Activity
    {
        ListView list;  // list view 
        Button btnBack; // back button 
        string type;
        HangmanConnection connection; // connection string
        GameStatsAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState) // on Crete function 
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_game_stats);

            connection = new HangmanConnection(this);
            type = Intent.GetStringExtra("type");

            list = FindViewById<ListView>(Resource.Id.list); // listview 
            btnBack = FindViewById<Button>(Resource.Id.back); // Button for back

            btnBack.Click += BtnBack_Click;

            if (type.Equals("winner"))
            {
                adapter = new GameStatsAdapter(this, connection.GetWinnerProfile());
                list.Adapter = adapter;
            }
            else
            {
                adapter = new GameStatsAdapter(this, connection.GetLoserProfile());
                list.Adapter = adapter;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish(); // back page  function on click btnVack
        }
    }
}