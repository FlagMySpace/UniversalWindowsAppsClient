using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Parse;
using SSWindows.Common;

namespace SSWindows.Models
{
    public class Place : ParseObject
    {
        public string Title
        {
            get { return Get<string>("title"); }
            set { this["title"] = value; }
        }

        public ParseUser OriginalPoster
        {
            get { return Get<ParseUser>("originalPoster"); }
            set { this["originalPoster"] = value; }
        }

        public string Description
        {
            get { return Get<string>("description"); } 
            set { this["description"] = value; }
        }

        public float Latitude
        {
            get { return Get<float>("latitude"); }
            set { this["latitude"] = value; }
        }

        public float Longitude
        {
            get { return Get<float>("longitude"); }
            set { this["longitude"] = value; }
        }

        public IList<ParseFile> Images {
            get { return Get<IList<ParseFile>>("images"); }
        }

        public int Point
        {
            get { return Get<int>("point"); }
        }

        public int Flag
        {
            get
            {
                return Get<int>("flag");
            }
        }

        public async Task VoteUp()
        {
            if (ParseUser.CurrentUser != null)
            {
                Increment("point");

                // Add user to voteup
                var userRelationVoteUp = GetRelation<ParseUser>("userVoteUp");
                var userVoteUp = userRelationVoteUp.Query.WhereEqualTo("userVoteUp", ParseUser.CurrentUser);
                if (await userVoteUp.FirstAsync() == null)
                {
                    userRelationVoteUp.Add(ParseUser.CurrentUser);
                }
                else
                {
                    throw new ModelException(409, "You are already voting up this space");
                }

                // Remove the votedown if any
                var userRelationVoteDown = GetRelation<ParseUser>("userVoteDown");
                var userVoteDown = userRelationVoteDown.Query.WhereEqualTo("userVoteDown", ParseUser.CurrentUser);
                if (await userVoteDown.FirstAsync() != null)
                {
                    userRelationVoteDown.Remove(ParseUser.CurrentUser);
                }

                await SaveAsync();
            }
            else
            {
                throw new ModelException(401, "You are not logged in");
            }
        }

        public async Task VoteDown()
        {
            if (ParseUser.CurrentUser != null)
            {
                Increment("point", -1);

                // Add user to votedown
                var userRelationVoteDown = GetRelation<ParseUser>("userVoteDown");
                var userVoteDown = userRelationVoteDown.Query.WhereEqualTo("userVoteDown", ParseUser.CurrentUser);
                if (await userVoteDown.FirstAsync() == null)
                {
                    userRelationVoteDown.Add(ParseUser.CurrentUser);
                }
                else
                {
                    throw new ModelException(409, "You are already voting down this space");
                }


                // Remove the voteup if any
                var userRelationVoteUp = GetRelation<ParseUser>("userVoteUp");
                var userVoteUp = userRelationVoteUp.Query.WhereEqualTo("userVoteUp", ParseUser.CurrentUser);
                if (await userVoteUp.FirstAsync() != null)
                {
                    userRelationVoteUp.Remove(ParseUser.CurrentUser);
                }

                await SaveAsync();
            }
            else
            {
                throw new ModelException(401, "You are not logged in");
            }
        }

        public async Task FlagSpace()
        {
            Increment("flag");

            // Add user to flag
            var userRelationFlag = GetRelation<ParseUser>("userFlag");
            var userFlag = userRelationFlag.Query.WhereEqualTo("userFlag", ParseUser.CurrentUser);
            if (await userFlag.FirstAsync() == null)
            {
                userRelationFlag.Add(ParseUser.CurrentUser);
            }
            else
            {
                throw new ModelException(409, "You are already flag this space");
            }

            await SaveAsync();
        }

        public async Task UnFlagSpace()
        {
            Increment("flag", -1);

            // Remove user from flag
            var userRelationFlag = GetRelation<ParseUser>("userFlag");
            var userFlag = await userRelationFlag.Query.WhereEqualTo("userFlag", ParseUser.CurrentUser).FirstAsync();
            if (userFlag != null)
            {
                userRelationFlag.Remove(ParseUser.CurrentUser);
            }

            await SaveAsync();
        }
    }
}
