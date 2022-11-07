using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.RTLXL.ApiModel
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    internal class Material
    {
        public string? uuid { get; set; }

        public string? episode_key { get; set; }

        public string? classname { get; set; }

        public string? image { get; set; }

        public string? title { get; set; }

        public string? station { get; set; }

        public string? synopsis { get; set; }
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
