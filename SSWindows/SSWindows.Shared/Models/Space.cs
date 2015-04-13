using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Parse;
using SSWindows.Common;

namespace SSWindows.Models
{
    [ParseClassName("Space")]
    public class Space : ParseObject
    {
        private readonly IList<string> _filesCache;

        public Space()
        {
            _filesCache = new List<string>();
        }

        [ParseFieldName("title")]
        public string Title
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("address")]
        public string Address
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("city")]
        public string City
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("state")]
        public string State
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("country")]
        public string Country
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("postalCode")]
        public string PostalCode
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("originalPoster")]
        public ParseUser OriginalPoster
        {
            get { return GetProperty<ParseUser>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("latitude")]
        public float Latitude
        {
            get { return GetProperty<float>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("longitude")]
        public float Longitude
        {
            get { return GetProperty<float>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("images")]
        public IList<ParseFile> Images
        {
            get { return GetProperty<IList<ParseFile>>(); }
        }

        [ParseFieldName("point")]
        public int Point
        {
            get { return GetProperty<int>(); }
        }

        [ParseFieldName("flag")]
        public int Flag
        {
            get { return GetProperty<int>(); }
        }

        public async Task VoteUpAsync()
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

        public async Task VoteDownAsync()
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

        public async Task FlagSpaceAsync()
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

        public async Task UnFlagSpaceAsync()
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

        public async Task AddImageAsync(StorageFile image)
        {
            var path = await FileHelper.Instance.CopyAndAssignFilePathAsync(image);
            var fileStream = await FileHelper.Instance.GetFileStreamFromFileAsync(path);
            var file = new ParseFile(image.DisplayName, fileStream.AsStream());
            _filesCache.Add(path);
            Images.Add(file);
        }

        private async Task UploadImagesAsync()
        {
            foreach (var parseFile in Images)
            {
                await parseFile.SaveAsync();
            }

            foreach (var file in _filesCache)
            {
                await FileHelper.Instance.DeleteCopyAsync(file);
            }
        }

        public async Task SavePlaceAsync()
        {
            await UploadImagesAsync();
            await SaveAsync();
        }
    }
}