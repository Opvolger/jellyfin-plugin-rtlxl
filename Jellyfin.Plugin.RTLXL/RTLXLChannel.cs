using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Entities;

namespace Jellyfin.Plugin.RTLXL
{
    /// <summary>
    /// RTLXL Challen implementation.
    /// </summary>
    public class RTLXLChannel : IChannel, IHasCacheKey
    {
        /// <inheritdoc/>
        public string Name => "RTL XL";

        /// <inheritdoc/>
        public string Description => "RTLXL";

        /// <inheritdoc/>
        public string DataVersion => "0.5";

        /// <inheritdoc/>
        public string HomePageUrl => "https://www.rtlxl.nl/";

        /// <inheritdoc/>
        public ChannelParentalRating ParentalRating => ChannelParentalRating.GeneralAudience;

        /// <inheritdoc/>
        public InternalChannelFeatures GetChannelFeatures()
        {
            return new InternalChannelFeatures
            {
                MediaTypes = new List<ChannelMediaType>() { ChannelMediaType.Video },
                SupportsContentDownloading = false,
                DefaultSortFields = new List<ChannelItemSortField>() { ChannelItemSortField.Name, ChannelItemSortField.PremiereDate }
            };
        }

        /// <inheritdoc/>
        public Task<DynamicImageResponse> GetChannelImage(ImageType type, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DynamicImageResponse
            {
                HasImage = false
            });
        }

        /// <inheritdoc/>
        public Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            var result = new ChannelItemResult();

            if (string.IsNullOrWhiteSpace(query.FolderId))
            {
                var lijst = RTLXLContentProvider.GetOverviewAsync().GetAwaiter().GetResult();
                result = new ChannelItemResult()
                {
                    Items = lijst,
                    TotalRecordCount = lijst.Count
                };

                return Task.FromResult(result);
            }

            return Task.FromResult(result);
        }

        private Task<ChannelItemResult> GetProgramList(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<ImageType> GetSupportedChannelImages()
        {
            return new List<ImageType>
            {
                 ImageType.Primary
            };
        }

        /// <inheritdoc/>
        public bool IsEnabledFor(string userId)
        {
            return true;
        }

        /// <inheritdoc/>
        public string GetCacheKey(string userId)
        {
            var now = DateTime.UtcNow;

            var values = new List<string>();

            values.Add(now.DayOfYear.ToString(CultureInfo.InvariantCulture));
            values.Add(now.Hour.ToString(CultureInfo.InvariantCulture));
            values.Add(now.Minute.ToString(CultureInfo.InvariantCulture));

            // double minute = now.Minute;
            // minute /= 5;

            // values.Add(Math.Floor(minute).ToString(CultureInfo.InvariantCulture));

            return string.Join('-', values.ToArray());
        }
    }
}
