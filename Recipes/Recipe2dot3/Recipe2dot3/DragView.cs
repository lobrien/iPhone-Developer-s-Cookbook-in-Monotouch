
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace Recipe2dot3
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
	}
}
