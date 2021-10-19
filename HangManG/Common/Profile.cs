using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangManG.Common
{
    public class Profile
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public string ProfileName { get; set; }

        public string Password { get; set; }

        public int Won { get; set; }

        public int Lost { get; set; }
    }
}