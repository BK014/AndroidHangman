using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangManG.Adapter
{
    public class ButtonsAdapter : BaseAdapter<string>
    {
        private string[] letters;
        private Context context;
        public ButtonsAdapter(Context c)
        {
            string letter_string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            letters = new string[letter_string.Length];
            for (int index = 0; index < letters.Length; index++)
            {
                letters[index] = letter_string[index] + "";
            }
            this.context = c;
        }

        public override int Count
        {
            get { return letters.Length; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return letters[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Button btn;

            if (convertView == null)
            {
                btn = (Button)LayoutInflater.From(context).Inflate(Resource.Layout.layout_button, null, false);
            }
            else
            {
                btn = (Button)convertView;
            }
            btn.Text = letters[position];
            return btn;
        }

    }
}