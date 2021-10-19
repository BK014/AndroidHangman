using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using HangManG.Adapter;
using HangManG.DataLayer;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangManG
{
    [Activity(Label = "HangmanActivity")] // main activity for play the game 
    public class HangmanActivity : Activity
    {
        WordPicker picker;  // word picker 
        HangmanConnection connection; // connection string 
        TextView word; // words 
        ImageView imageHang; // imges 
        GridView buttons; //  buttons 
        ButtonsAdapter adapter;
        Button btnPlay, btnReset, btnBack; // play, reset , go back button 
        int currentImage;
        string currentWord;
        string guess;
        string name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hangman);
            picker = new WordPicker(this);
            connection = new HangmanConnection(this);
            name = Intent.GetStringExtra("name");

            word = FindViewById<TextView>(Resource.Id.word);
            imageHang = FindViewById<ImageView>(Resource.Id.hang);
            buttons = FindViewById<GridView>(Resource.Id.buttons);
            btnPlay = FindViewById<Button>(Resource.Id.play);
            btnReset = FindViewById<Button>(Resource.Id.reset);
            btnBack = FindViewById<Button>(Resource.Id.back);

            btnPlay.Click += BtnPlay_Click;
            btnReset.Click += BtnReset_Click;
            btnBack.Click += BtnBack_Click;

            Play();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish(); // go back button 
        }

        public void Play() // play function 
        {
            adapter = new ButtonsAdapter(this);
            buttons.Adapter = adapter;

            currentWord = picker.GetGameWord();

            guess = ""; // guess word blank
            for (int index = 0; index < currentWord.Length; index++) // for loop 
            {
                guess += "_"; // imncrement words 
            }
            word.Text = ConvertToResult();
            btnPlay.Enabled = false;
            btnBack.Enabled = true;
        }

        private string ConvertToResult()
        {
            string result = "";
            for (int index = 0; index < guess.Length; index++)
            {
                result += guess[index] + " ";
            }
            return result; // return result
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            DisplayAlert("OOPS!!!", "You Reset The Game So You Lost The Game"); // when loose show meaasge for looser 
            connection.UpdateProfile(name, false); // not upadte profile 
            currentImage = 0; // no image 
            btnPlay.Enabled = false;// ply button false for play again 
            btnBack.Enabled = true; // button true for go back
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Play(); // button play  for start the game 
        }

        [Export("buttonClicked")] //button clicked 
        public void buttonPressed(View view)
        {
            //user has pressed a letter to guess
            string ltr = ((TextView)view).Text;
            char letter = ltr[0];

            view.Enabled = false;
            view.SetBackgroundColor(Color.White);

            bool right = false;
            string result = "";
            for (int index = 0; index < currentWord.Length; index++)
            {
                if (currentWord[index] == letter)
                {
                    right = true;
                    result += letter;
                }
                else
                {
                    result += guess[index]; // guess word 
                }
            }
            guess = result; // fresult 
            word.Text = ConvertToResult();
            Toast.MakeText(this, currentWord, ToastLength.Long).Show(); // toast show  
            if (right)
            {
                if (guess.Equals(currentWord)) // whwen guess and current word re equal 
                {
                    currentImage = 0;
                    DisplayAlert("GAME STATUS", "You Won The Game"); // message box show for winner 
                    connection.UpdateProfile(name, true); // updte profile 
                    word.Text = ""; // rest word 
                    btnPlay.Enabled = true; // button true for play agaia

                    btnReset.Enabled = false; // btn falase for reset
                    imageHang.SetImageResource(Resource.Drawable.win); // winner status show at last 
                }
            }
            else
            {
                currentImage++; // image increment 
                if (currentImage == 7) // if condition 
                {
                    currentImage = 0;
                    DisplayAlert("GAME STATUS!!!", "You Lost The Game"); // message box show when images  are equal to 7
                    connection.UpdateProfile(name, false); // not update profile 
                    word.Text = ""; // word blank means reset
                    btnPlay.Enabled = true; // button true for play agaion 
                    btnReset.Enabled = false; // button falase for reset 
                }
                else
                {
                    ChangeImage();
                }

            }
        }

        public void ChangeImage()
        {
            if (currentImage >= 0 && currentImage <= 6) // image incriment on each worng guess word
            {
                int image_id = Resource.Drawable.hang0; // images comiung form resourace  by Id 
                switch (currentImage)
                {
                    case 1:
                        image_id = Resource.Drawable.hang1;
                        break;
                    case 2:
                        image_id = Resource.Drawable.hang2;
                        break;
                    case 3:
                        image_id = Resource.Drawable.hang3;
                        break;
                    case 4:
                        image_id = Resource.Drawable.hang4;
                        break;
                    case 5:
                        image_id = Resource.Drawable.hang5;
                        break;
                    case 6:
                        image_id = Resource.Drawable.hang6;
                        break;
                    default:
                        image_id = Resource.Drawable.hang0;
                        break;
                }
                imageHang.SetImageResource(image_id);
            }

        }

        private void DisplayAlert(string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetNegativeButton("Close", (c, v) =>
            {
                alert.Dispose();
                ChangeImage(); // image change 
            });
            alert.Show(); // alert show 
        }
    }
}