using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HangManG.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HangManG.DataLayer
{
    public class HangmanConnection
    {
        private SQLiteConnection connection;

        private string error;

        public string GetError()
        {
            return error;
        }
        public HangmanConnection(Context context)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            connection = new SQLiteConnection(Path.Combine(path, "data.db"));
            if (!CheckTable())
            {
                connection.CreateTable<Profile>();
                connection.CreateTable<Word>();
                try
                {
                    StreamReader br = new StreamReader(context.Assets.Open("game_words.txt"));
                    string line;
                    while ((line = br.ReadLine()) != null)
                    {
                        if (line.Length >= 4 && line.Length <= 20)
                        {
                            Word word = new Word();
                            word.Text = line.Trim().ToUpper();
                            SaveWord(word);
                        }
                    }
                }
                catch (IOException ex)
                {

                }
            }

        }

        public List<string> GetAllWordsString()
        {
            List<string> wordStrings = new List<string>();
            List<Word> words = GetAllWords();
            foreach (Word word in words)
            {
                wordStrings.Add(word.Text);
            }
            return wordStrings;
        }

        public bool SaveWord(Word word)
        {
            try
            {
                connection.Insert(word);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool CheckProfile(string name, string password)
        {
            List<Profile> profiles = connection.Query<Profile>("Select * from Profile");
            foreach (Profile profile in profiles)
            {
                if (profile.ProfileName.Equals(name) && profile.Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateProfile(string name, bool winning)
        {
            try
            {
                var profiles = connection.Table<Profile>();
                var profile = (from pro in profiles
                               where pro.ProfileName == name
                               select pro).Single();
                if (winning)
                {
                    profile.Won += 1;
                }
                else
                {
                    profile.Lost += 1;
                }
                connection.Update(profile);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool SaveProfile(Profile profile)
        {
            try
            {
                connection.Insert(profile);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public List<Word> GetAllWords()
        {
            List<Word> words = connection.Query<Word>("Select * from Word");
            return words;
        }

        public List<Profile> GetLoserProfile()
        {
            List<Profile> profiles = connection.Query<Profile>("Select * from Profile Order by TotalLost  Desc");
            return profiles;
        }

        public List<Profile> GetWinnerProfile()
        {
            List<Profile> profiles = connection.Query<Profile>("Select * from Profile Order by TotalWon Desc");
            return profiles;
        }

        private bool CheckTable()
        {
            try
            {
                connection.Get<Word>(1);
                connection.Get<Profile>(1);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}