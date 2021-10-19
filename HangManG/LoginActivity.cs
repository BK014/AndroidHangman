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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        Button btnLogin, btnRegister;
        EditText etName, etPassword;
        HangmanConnection connection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            connection = new HangmanConnection(this);
            etName = FindViewById<EditText>(Resource.Id.etName);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;


        }

        private void BtnRegister_Click(object sender, EventArgs e) // register new user 
        {
            string name = etName.Text.Trim(); // name 
            string password = etPassword.Text;// password 
            string message = ""; // mesaage 
            if (name.Length == 0 || password.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                Profile profile = new Profile();
                profile.ProfileName = name;// name 
                profile.Password = password;// password 
                if (connection.SaveProfile(profile)) // save profile 
                {
                    message = "Profile is saved";
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("name", name);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    message = "Profile is not Saved Due To " + connection.GetError(); // error message 
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show(); // Toast show at the end
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string name = etName.Text.Trim(); // login name 
            string password = etPassword.Text; // password 
            string message = "";
            if (name.Length == 0 || password.Length == 0) // if match then login 
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                if (name.Equals("admin") && password.Equals("admin"))
                {
                    message = "Welcome To Admin!!!";  // meaage bos for welcome dmin 
                    Intent intent = new Intent(this, typeof(AdminActivity));
                    StartActivity(intent);
                    Finish(); // finish function 
                }
                else if (connection.CheckProfile(name, password))
                {
                    message = "Welcome To Hangman"; // message bos for welcome
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("name", name);
                    StartActivity(intent);
                    Finish(); // finish function 
                }
                else
                {
                    message = "Invalid Name and Password Given"; // message bos for worng credentails 
                }

            }
            Toast.MakeText(this, message, ToastLength.Long).Show(); // toast show at the end 
        }
    }
}