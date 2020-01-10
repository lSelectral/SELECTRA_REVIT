namespace CommandTab
{
    using System;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Gets the  image from ResourceImage based on user provided
    ///  file name with extension. Helper methods.
    /// </summary>
    public static class ResourceImage
    {
        #region Public methods

        /// <summary>
        /// Gets the icon image from resource assembly
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static BitmapImage GetIcon(string name)
        {
            // Create a new Bitmap Image instance
            BitmapImage image = new BitmapImage();

            // Construct and return
            image.BeginInit();

            // Get image source uri
            image.UriSource = new Uri(@"E:\SeLeCtRa\Icons\" + name);
            image.EndInit();

            // return constructed Bitmap Image
            return image;
        }

        #endregion
    }
}
