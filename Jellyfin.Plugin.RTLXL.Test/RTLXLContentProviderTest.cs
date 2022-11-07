using MediaBrowser.Controller.Channels;

namespace Jellyfin.Plugin.RTLXL.Test
{
    [TestClass]
    public class RTLXLContentProviderTest
    {
        [TestMethod]
        public void Overview_NOT_NULL()
        {
            var Overview = RTLXLContentProvider.GetOverviewAsync();
            var test = Overview.ConfigureAwait(false).GetAwaiter().GetResult();
            Assert.IsNotNull(test);
        }
    }
}
