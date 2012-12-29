using System;
using System.Diagnostics;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using BlackBerry;
using BlackBerry.Screen;

namespace OpenTK.Platform.BlackBerry
{
	public class BlackBerryNative : INativeWindow
	{
		Context ctx;
		Window win;

		public string Title { get; set; }
		public Rectangle Bounds { get; set; }

		public BlackBerryNative (int x, int y, int width, int height, string title,
		                         GraphicsMode graphicsMode, GameWindowFlags flags,
		                         DisplayDevice displayDevice) {
			Title = title;
			Bounds = new Rectangle (x, y, width, height);

			win = new Window (Context.GetInstance (ContextType.Application));
			win.SetIntProperty (Property.SCREEN_PROPERTY_COLOR, 0xff0000aa);
			win.Usage = Usage.SCREEN_USAGE_OPENGL_ES1;
			win.Transparency = Transparency.None;
			win.PixelFormat = PixelFormat.SCREEN_FORMAT_RGBA8888;
			win.SetIntProperty (Property.SCREEN_PROPERTY_SWAP_INTERVAL, 1);
			win.IsVisible = true;
			win.AddBuffer ();
			win.Buffers [0].Fill (0xffffffaa);
			win.Render (win.Buffers [0]);
			Debug.Print ("BB Native Window created: {0}, Size: {1}", Title, Size);
			//win.Identifier = title;
			//win.IsVisible = true;
			//win.Render (win.Buffers [0]);
		}

		public Point Location {
			get {
				return Bounds.Location;
			}
			set {
				Debug.Print ("Setting location not allowed.");
			}
		}
		
		public Size Size {
			get {
				return Bounds.Size;
			}
			set {
				Debug.Print ("Setting size not allowed.");
			}
		}

		public Rectangle ClientRectangle {
			get {
				return Bounds;
			}
			set {
				Bounds = value;
			}
		}
		
		public Size ClientSize {
			get	{
				return Size;
			}
			set {
				Size = value;
			}
		}

		public int Width {
			get {
				return Bounds.Width;
			}
			set {}
		}

		public int Height {
			get { 
				return Bounds.Height;
			}
			set {}
		}
		
		public int X {
			get { return Bounds.X; }
			set { Location = new Point(value, Y); }
		}

		public int Y {
			get { return Bounds.Y; }
			set { Location = new Point(X, value); }
		}
		
		public Icon Icon {
			get {
				return null;
			}
			set {}
		}

		public bool Focused	{
			get { return true; }
		}

		public bool Visible {
			get {
				//return win.IsVisible;
				return true;
			}
			set {
				//win.IsVisible = value;
			}
		}

		public bool Exists { get { return true; } }

		public bool CursorVisible {
			get { return true; }
			set {}
		}

		public void Close() {
			//win.IsVisible = false;
		}
		
		public WindowState WindowState {
			get {
				return WindowState.Fullscreen;
			}
			set {}
		}
		
		public WindowBorder WindowBorder {
			get {
				return WindowBorder.Hidden;
			}
			set {}
		}

		public Point PointToClient(Point point) {
			return point;
		}
		
		public Point PointToScreen(Point point) {
			return point;
		}
		
		public event EventHandler<EventArgs> Move = delegate { };
		public event EventHandler<EventArgs> Resize = delegate { };
		public event EventHandler<System.ComponentModel.CancelEventArgs> Closing = delegate { };
		public event EventHandler<EventArgs> Closed = delegate { };
		public event EventHandler<EventArgs> Disposed = delegate { };
		public event EventHandler<EventArgs> IconChanged = delegate { };
		public event EventHandler<EventArgs> TitleChanged = delegate { };
		public event EventHandler<EventArgs> VisibleChanged = delegate { };
		public event EventHandler<EventArgs> FocusedChanged = delegate { };
		public event EventHandler<EventArgs> WindowBorderChanged = delegate { };
		public event EventHandler<EventArgs> WindowStateChanged = delegate { };
		public event EventHandler<OpenTK.Input.KeyboardKeyEventArgs> KeyDown = delegate { };
		public event EventHandler<KeyPressEventArgs> KeyPress = delegate { };
		public event EventHandler<OpenTK.Input.KeyboardKeyEventArgs> KeyUp = delegate { }; 
		public event EventHandler<EventArgs> MouseEnter = delegate { };
		public event EventHandler<EventArgs> MouseLeave = delegate { };

		public void ProcessEvents()
		{
			//PlatformServices.Run ();
			Console.WriteLine ("PROCESS EVENTS.");
			PlatformServices.NextEvent (1);
		}

		public IInputDriver InputDriver {
			get {
				throw new NotImplementedException ();
			}
		}

		public IWindowInfo WindowInfo {
			get {
				return new BlackBerryWindowInfo (win, true);
			}
		}

		public void Dispose () {
			win.Dispose ();
			/*ctx.Dispose ();*/
		}
	}
}

