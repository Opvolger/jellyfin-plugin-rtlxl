using MediaBrowser.Controller.Channels;

namespace Jellyfin.Plugin.RTLXL.Test
{
    [TestClass]
    public class RTLXLContentProviderTest
    {
        [TestMethod]
        public void Overview_NOT_NULL()
        {
            var overview = RTLXLContentProvider.GetAllFolders();
            Assert.IsNotNull(overview);
        }
    }
}
