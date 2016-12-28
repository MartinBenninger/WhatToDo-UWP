namespace WhatToDo.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Utility class that can be used to find and load embedded resources into memory.
    /// </summary>
    public static class ResourceLoader
    {
        /// <summary>
        /// Attempts to find and return the given resource.
        /// </summary>
        /// <param name="resourceFileName">Resource file name.</param>
        /// <returns>The embedded resource stream.</returns>
        public static Stream GetEmbeddedResourceStream(string resourceFileName)
        {
            var resourcePrefix = "WhatToDo.";

            ////#if __IOS__
            ////            resourcePrefix += "iOS.";
            ////#endif

            ////#if __ANDROID__
            ////            resourcePrefix += "Droid.";
            ////#endif

            ////#if WINDOWS
            ////            resourcePrefix += "Windows.";
            ////#endif

            ////#if WINDOWS_PHONE
            ////            resourcePrefix += "WinPhone.";
            ////#endif

            ////#if UWP
            ////            resourcePrefix += "UWP.";
            ////#endif

            return typeof(ResourceLoader).GetTypeInfo().Assembly.GetManifestResourceStream(resourcePrefix + resourceFileName);
        }
    }
}