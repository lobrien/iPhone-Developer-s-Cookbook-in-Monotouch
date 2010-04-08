
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Recipe2Dot4ClippingSubviews
{

	[MonoTouch.Foundation.Register("DragView")]
	public class DragView : UIImageView
	{
		PointF startLocation;
		
		public DragView ()
		{
			BackgroundColor = UIColor.Clear;
		}
		
		public String WhichFlower {
			get;
			set;
		}

		IntPtr bitmapData = IntPtr.Zero;
		SizeF imageSize;
		
		//Note the touch point and bring the touched view to the front
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			var touch = (UITouch) touches.AnyObject;
			if(touch != null)
			{
				var pt = touch.LocationInView(this);
				startLocation = pt;
				
				this.Superview.BringSubviewToFront(this);
			}
		}
		
		//As the user drags, move the flower with the brush
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			var touch = (UITouch) touches.AnyObject;
			if(touch != null)
			{
				var pt = touch.LocationInView(this);
				var frame = this.Frame;
				
				frame.Location = new PointF(frame.Location.X + pt.X - startLocation.X, frame.Location.Y + pt.Y - startLocation.Y);
				
				this.Frame = frame;
			}
		}
		
		
		const float SIDELENGTH = 64.0f;
		public override void Draw (RectangleF rect)
		{
			var bounds = new RectangleF(0.0f, 0.0f, SIDELENGTH, SIDELENGTH);
			var context = UIGraphics.GetCurrentContext();
			var path = new CGPath();
			
			//sp! Elipse [sic]
			path.AddElipseInRect(this.Bounds);
			
			context.AddPath(path);
			context.Clip();
			
			context.DrawPath(CGPathDrawingMode.Fill);
			
			this.Image.Draw(bounds);
		}
		
		public override bool PointInside (PointF point, UIEvent uievent)
		{
			if(bitmapData == IntPtr.Zero)
			{
				bitmapData = RequestImagePixelData(Image);
			}
			
			//Check for out of bounds
			if(point.Y < 0 || point.X < 0 || point.Y > Image.Size.Height || point.X > Image.Size.Width)
			{
				return false;
			}
			var startByte = (int) ((point.Y * this.Image.Size.Width + point.X) * 4);
			
			byte alpha = GetByte(startByte, this.bitmapData);
			Console.WriteLine("Alpha value of {0}, {1} is {2}", point.X, point.Y, alpha);
			
			if(alpha > 127)
				return true;
			else
				return false;
		}
		
		unsafe byte GetByte(int offset, IntPtr buffer)
		{
			byte* bufferAsBytes = (byte*) buffer;
			return bufferAsBytes[offset];
		}
		
		//Listing 2-7
		protected CGBitmapContext CreateARGBBitmapContext(CGImage inImage)
		{
			var pixelsWide = inImage.Width;
			var pixelsHigh = inImage.Height;
			var bitmapBytesPerRow = pixelsWide * 4;
			var bitmapByteCount = bitmapBytesPerRow * pixelsHigh;
			//Note implicit colorSpace.Dispose() 
			using(var colorSpace = CGColorSpace.CreateDeviceRGB())
			{
				//Allocate the bitmap and create context
				var bitmapData = Marshal.AllocHGlobal(bitmapByteCount);
				//I think this is unnecessary, as I believe Marshal.AllocHGlobal will throw OutOfMemoryException
				if(bitmapData == IntPtr.Zero)
				{
					throw new Exception("Memory not allocated.");
				}
				
				var context = new CGBitmapContext(bitmapData, pixelsWide, pixelsHigh, 8,
				                                  bitmapBytesPerRow, colorSpace, CGImageAlphaInfo.PremultipliedFirst);
				if(context == null)
				{
					throw new Exception("Context not created");
				}
				return context;
			}
		}
		
		//Store pixel data as an ARGB Bitmap
		protected IntPtr RequestImagePixelData(UIImage inImage)
		{
			imageSize = inImage.Size;
			CGBitmapContext ctxt = CreateARGBBitmapContext(inImage.CGImage);
			var rect = new RectangleF(0.0f, 0.0f, imageSize.Width, imageSize.Height);
			ctxt.DrawImage(rect, inImage.CGImage);
			var data = ctxt.Data;
			return data;
			
		}
		
		override protected void Dispose (bool disposing)
		{
			if(bitmapData != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(bitmapData);
				bitmapData = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}

	}
}
