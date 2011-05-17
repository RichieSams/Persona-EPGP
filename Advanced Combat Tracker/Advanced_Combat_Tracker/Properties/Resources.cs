namespace Advanced_Combat_Tracker.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap back
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("back", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static Bitmap dn
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("dn", resourceCulture);
            }
        }

        internal static Bitmap play
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("play", resourceCulture);
            }
        }

        internal static Bitmap reorder
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("reorder", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Advanced_Combat_Tracker.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap up
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("up", resourceCulture);
            }
        }
    }
}

