using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HangManG.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangManG.Adapter
{
    public class GameStatsAdapter : BaseAdapter<Profile>
    {
        private Activity context;
        private List<Profile> profiles;

        public GameStatsAdapter(Activity context, List<Profile> profiles)
        {
            this.profiles = profiles;
            this.context = context;
        }

        public override int Count
        {
            get { return profiles.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Profile this[int position]
        {
            get { return profiles[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.game_stats_row, null, false);
            }

            TextView textName = row.FindViewById<TextView>(Resource.Id.textName);
            TextView textWon = row.FindViewById<TextView>(Resource.Id.textWon);
            TextView textLost = row.FindViewById<TextView>(Resource.Id.textLost);

            textName.Text = profiles[position].ProfileName;
            textWon.Text = " Won : " + profiles[position].Won + " ";
            textLost.Text = " Lost: " + profiles[position].Lost;

            return row;
        }
    }
}