using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jellyfin.Plugin.RTLXL.ApiModel;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.MediaInfo;
using Newtonsoft.Json;

namespace Jellyfin.Plugin.RTLXL
{
    internal static class RTLXLContentProvider
    {
        public static List<ChannelItemInfo> GetFolder(string folderid)
        {
            var content = GetResponseAsync<Folder>($"http://www.rtl.nl/system/s4m/vfd/version=1/fun=abstract/d=pc/fmt=smooth/ak={folderid}/output=json/pg=1/").GetAwaiter().GetResult();
            var all = new List<ChannelItemInfo>();

            for (var i = 0; i < content?.material?.Count; i++)
            {
                var item = content.material[i];
                if (item != null)
                {
                    var image = item?.image?.Split(',').FirstOrDefault();
                    var studios = new List<string>();
                    if (item != null && item?.station != null)
                    {
                        studios = new List<string>() { item.station };
                    }

                    all.Add(new ChannelItemInfo()
                    {
                        FolderType = ChannelFolderType.Container,
                        Name = content?.collection?.name,
                        Type = ChannelItemType.Media,
                        ImageUrl = $"{content?.meta?.thumb_base_url}{image}",
                        Id = item?.uuid,
                        IndexNumber = i,
                        Studios = studios,
                        ContentType = ChannelMediaContentType.TvExtra,
                        MediaSources = new List<MediaSourceInfo>
                        {
                            new MediaSourceInfo()
                            {
                                Path = $"https://api.rtl.nl/watch/play/api/play/xl/{folderid}?device=web&format=hls",
                                Protocol = MediaProtocol.Http
                            }
                        }
                    });
                }
            }

            return all;
        }

        private static async Task<T?> GetResponseAsync<T>(string reqUrl)
            where T : class
        {
            var client = new HttpClient();
            var prodResp = await client.GetAsync(reqUrl).ConfigureAwait(false);
            var overviewString = await prodResp.Content.ReadAsStringAsync().ConfigureAwait(false);

            var content = JsonConvert.DeserializeObject<T>(overviewString);
            return content;
        }

        public static List<ChannelItemInfo> GetAllFolders()
        {
            var content = GetResponseAsync<Overview>("http://www.rtl.nl/system/s4m/vfd/version=2/d=a2t/fmt=progressive/fun=az").GetAwaiter().GetResult();

            var all = new List<ChannelItemInfo>();

            for (var i = 0; i < content?.abstracts?.Count; i++)
            {
                var item = content.abstracts[i];
                if (item != null)
                {
                    var image = item?.coverurl?.Split(',').FirstOrDefault();
                    var studios = new List<string>();
                    if (item != null && item?.station != null)
                    {
                        studios = new List<string>() { item.station };
                    }

                    all.Add(new ChannelItemInfo()
                    {
                        FolderType = ChannelFolderType.Container,
                        Name = item?.name,
                        Type = ChannelItemType.Folder,
                        ImageUrl = $"{content?.meta?.thumb_base_url}{image}",
                        Id = item?.key,
                        IndexNumber = i,
                        Studios = studios,
                    });
                }
            }

            return all;
        }
    }
}
