
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;

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
		
		
		//Note: This is actually Recipe 2-5: Clipped Views
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
		
		//Listing 2-6
		public override bool PointInside (PointF point, UIEvent uievent)
		{
			var HALFSIDE = SIDELENGTH / 2.0f;
			
			//Normalize with centered origin
			var x = (point.X - HALFSIDE) / HALFSIDE;
			var y = (point.Y - HALFSIDE) / HALFSIDE;
			
			//x^2 + Y^2 = hypotenuse length squared
			var xSquared = x * x;
			var ySquared = y * y;
			var hypSquared = xSquared + ySquared;
			//If the length is < 1, the point is within the clipped circle of this view
			
			
			if(hypSquared < 1.0)
				return true;
			else
				return false;
		}

	}
}
