using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenTK.Graphics;
using OpenTK.Platform.BlackBerry;
using BlackBerry.Screen;

namespace OpenTK.Platform.Egl
{
	class EglBlackBerryPlatformFactory : IPlatformFactory
	{
		public IGraphicsContext CreateGLContext(GraphicsMode mode, IWindowInfo window, IGraphicsContext shareContext, bool directRendering, int major, int minor, GraphicsContextFlags flags)
		{
			try {
				Debug.Print ("CREATE GL CONTEXT: {0}.{1}", major, minor);
				Debug.Indent ();
				if (window == null) {
					throw new ArgumentNullException ();
				}
				Debug.Print ("WindowInfo: {0}", window.GetType ().FullName);
				BlackBerryWindowInfo bbwin = window as BlackBerryWindowInfo;
				Debug.Print ("Creating GL context for window: {0}", bbwin.Window.Handle.ToString ());
				bbwin.Window.Usage = major > 1 ? Usage.SCREEN_USAGE_OPENGL_ES2 : Usage.SCREEN_USAGE_OPENGL_ES1;
				IntPtr egl_display = GetDisplay(IntPtr.Zero);
				Debug.Print ("EGL display: {0}", egl_display.ToString ());
				EglWindowInfo egl_win = new EglWindowInfo(bbwin.Window.Handle, egl_display);
				Debug.Print ("EGL window before: {0}, Surface: {1}, Handle: {2}",
				             egl_win.Display, egl_win.Surface, egl_win.Handle);
				var ctx = new EglContext(mode, egl_win, shareContext, major, minor, flags);
				Debug.Print ("Display: {0}, Surface: {1}, Context: {2}",
				             Egl.GetCurrentDisplay (), egl_win.Surface, Egl.GetCurrentContext ());
				//Context.Instance.Flush ();
				return ctx;
			} finally {
				Debug.Unindent ();
			}
		}
		
		public IGraphicsContext CreateGLContext(ContextHandle handle, IWindowInfo window, IGraphicsContext shareContext, bool directRendering, int major, int minor, GraphicsContextFlags flags)
		{
			BlackBerryWindowInfo bbwin = window as BlackBerryWindowInfo;
			bbwin.Window.Usage = major > 1 ? Usage.SCREEN_USAGE_OPENGL_ES2 : Usage.SCREEN_USAGE_OPENGL_ES1;
			IntPtr egl_display = GetDisplay(IntPtr.Zero);
			EglWindowInfo egl_win = new EglWindowInfo(bbwin.Window.Handle, egl_display);
			return new EglContext(handle, egl_win, shareContext, major, minor, flags);
		}
		
		public GraphicsContext.GetCurrentContextDelegate CreateGetCurrentGraphicsContext()
		{
			return (GraphicsContext.GetCurrentContextDelegate)delegate
			{
				return new ContextHandle(Egl.GetCurrentContext());
			};
		}

		IntPtr GetDisplay(IntPtr dc)
		{
			IntPtr display = Egl.GetDisplay(dc);
			if (display == IntPtr.Zero)
				display = Egl.GetDisplay(IntPtr.Zero);
			
			return display;
		}

		public virtual INativeWindow CreateNativeWindow (int x, int y, int width, int height, string title,
		                                                 GraphicsMode graphicsMode, GameWindowFlags flags,
		                                                 DisplayDevice displayDevice) {
			return new BlackBerryNative (x, y, width, height, title, graphicsMode, flags, displayDevice);
		}

		public virtual IDisplayDeviceDriver CreateDisplayDeviceDriver () {
			return new BlackBerry.BlackBerryDisplayDriver ();
		}

		public virtual IGraphicsMode CreateGraphicsMode () {
			Debug.Print ("CREATE GRAPHICS MODE");
			return new EglGraphicsMode ();
		}

		public virtual OpenTK.Input.IKeyboardDriver2 CreateKeyboardDriver () {
			throw new NotImplementedException ();
		}

		public virtual OpenTK.Input.IMouseDriver2 CreateMouseDriver () {
			throw new NotImplementedException ();
		}
	}
}
