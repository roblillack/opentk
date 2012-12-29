using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BlackBerry.Screen;

namespace OpenTK.Platform.BlackBerry
{
	sealed class BlackBerryWindowInfo : IWindowInfo
	{
		bool disposed;
		bool ownHandle;

		public BlackBerryWindowInfo (Window win, bool own)
		{
			Window = win;
			ownHandle = own;
		}
		
		public Window Window { get; set; }

		public void Dispose ()
		{
			Dispose(true);
		}
		
		void Dispose (bool disposing)
		{
			if (disposed) {
				return;
			}
			
			if (disposing)
			{

			}
			
			if (ownHandle && Window != null)
			{
				Window.Dispose ();
				Window = null;
			}
			
			disposed = true;
		}
		
		~BlackBerryWindowInfo()
		{
			Dispose (false);
		}
	}
}
