
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace Recipe2dot3
{

	[MonoTouch.Foundation.Register("DragView")]
	public class DragView : UIView
	{
		PointF startLocation;
		UIImage image;
		
		public DragView ()
		{
			this.BackgroundColor = UIColor.Clear;
		}
		
		public UIImage Image {
			get { return image; }
			set { image = value; }
		}
		
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
		
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			System.Console.Write("T");
			var touch = (UITouch) touches.AnyObject;
			if(touch != null)
			{
				var pt = touch.LocationInView(this);
				var frame = this.Frame;
				
				frame.Location = new PointF(frame.Location.X + pt.X - startLocation.X, frame.Location.Y + pt.Y - startLocation.Y);
				
				this.Frame = frame;
			}
		}
		
		
		//Note: This is actually Recipe 2-5: Clipped Views
		const float SIDELENGTH = 110.0f;
		public override void Draw (RectangleF rect)
		{
			//System.Console.WriteLine("Draw()");
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
		
		//Listing 2-6
		public override bool PointInside (PointF point, UIEvent uievent)
		{
			System.Console.WriteLine("PointInside({0}, {1})", point.X, point.Y);
			//System.Console.WriteLine("My start Location is {0}, {1}", this.startLocation.X, this.startLocation.Y);
			//System.Console.WriteLine("My bounds are {0},{1} - {2}, {3}", Bounds.Left, Bounds.Top, Bounds.Right, Bounds.Bottom);
			//System.Console.WriteLine("My frame is {0}, {1} - {2}, {3}", Frame.Left, Frame.Top, Frame.Right, Frame.Bottom);
			var HALFSIDE = SIDELENGTH / 2.0f;
			
			//Normalize with centered origin
			var ctrPt = HALFSIDE / 2.0f;
			var x = (point.X - ctrPt) / HALFSIDE;
			var y = (point.Y - ctrPt) / HALFSIDE;
			
			System.Console.WriteLine("Normalized pt is {0}, {1}", x, y);
			
			//x^2 + Y^2 = hypotenuse length squared
			var xSquared = x * x;
			var ySquared = y * y;
			var hypSquared = xSquared + ySquared;
			System.Console.WriteLine(hypSquared.ToString());
			//If the length is < 1, the point is within the clipped circle
			
			if(hypSquared < 1.0)
				return true;
			else
				return false;
			
		}

	}
}