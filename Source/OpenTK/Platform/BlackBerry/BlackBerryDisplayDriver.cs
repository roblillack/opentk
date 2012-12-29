using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using BlackBerry.Screen;

namespace OpenTK.Platform.BlackBerry
{
    sealed class BlackBerryDisplayDriver : DisplayDeviceBase
    {
        public BlackBerryDisplayDriver()
        {
            
			//var resolution = new DisplayResolution (0, 0, 1280, 720, 32, 60.0f);
			var resolution = new DisplayResolution (0, 0, 1024, 600, 32, 60.0f);
			var resolutions = new List<DisplayResolution> ();
			resolutions.Add (resolution);
			var device = new DisplayDevice (resolution, true, resolutions, resolution.Bounds, null);

			Primary = device;
			AvailableDevices.Add (device);
        }

        static internal IntPtr HandleTo(DisplayDevice displayDevice)
        {
            return (IntPtr)displayDevice.Id;
        }

        public sealed override bool TryChangeResolution(DisplayDevice device, DisplayResolution resolution)
        {
            return false;
        }

        public sealed override bool TryRestoreResolution(DisplayDevice device)
        {
            return false;
        }
    }
}
