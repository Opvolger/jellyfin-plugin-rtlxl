using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Jellyfin.Plugin.RTLXL.ApiModel;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Channels;
using Newtonsoft.Json;

namespace Jellyfin.Plugin.RTLXL
{
    internal static class RTLXLContentProvider
    {
        public static async Task<List<ChannelItemInfo>> GetOverviewAsync()
        {
            var client = new HttpClient();
            string reqUrl = "http://www.rtl.nl/system/s4m/vfd/version=2/d=a2t/fmt=progressive/fun=az";
            var prodResp = await client.GetAsync(reqUrl).ConfigureAwait(false);

            var overviewString = await prodResp.Content.ReadAsStringAsync().ConfigureAwait(false);

            var content = JsonConvert.DeserializeObject<Overview>(overviewString);

            var all = new List<ChannelItemInfo>();

            for (var i = 0; i < content?.abstracts?.Count; i++)
            {
                var item = content.abstracts[i];
                var image = item?.coverurl?.Split(',').FirstOrDefault();
                all.Add(new ChannelItemInfo() { FolderType = ChannelFolderType.Container, Name = item?.name, Type = ChannelItemType.Folder, ImageUrl = $"{content?.meta?.thumb_base_url}{image}" });
            }

            return all;
        }
    }
}
