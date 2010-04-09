
using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace Dot_DetectingMultitouch
{

	[MonoTouch.Foundation.Register("MultiTouchView")]
	public class MultiTouchView : UIView
	{
		public MultiTouchView ()
		{
			MultipleTouchEnabled = true;
		}
		
		public PointF Loc1 {
			get;
			set;
		}
		public PointF Loc2 {
			get;
			set;
		}

		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			var allTouches = touches.ToArray<UITouch>();
			var count = allTouches.Length;
			if(count > 0)
			{
				Loc1 = allTouches[0].LocationInView(this);
			}
			if(count > 1)
			{
				Loc2 = allTouches[1].LocationInView(this);
			}
			SetNeedsDisplay();
		}
		
		//React to moved touches the same as to "began"
		public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			TouchesBegan(touches, evt);
		}
		
		public override void Draw (System.Drawing.RectangleF rect)
		{
			//Get the current context
			CGContext ctxt = UIGraphics.GetCurrentContext();
			ctxt.ClearRect(rect);
			
			//Set up the stroke and fill characteristics
			ctxt.SetLineWidth(3.0f);
			float[] gray = { 0.5f, 0.5f, 0.5f, 1.0f};
			ctxt.SetStrokeColor(gray);
			float[] red = { 0.75f, 0.25f, 0.25f, 1.0f};
			ctxt.SetFillColor(red);
			
			//Draw a line between the two location points
			ctxt.MoveTo(Loc1.X, Loc1.Y);
			ctxt.AddLineToPoint(Loc2.X, Loc2.Y);
			ctxt.StrokePath();
			
			var p1Box = new RectangleF(Loc1.X, Loc1.Y, 0.0f, 0.0f);
			var p2Box = new RectangleF(Loc2.X, Loc2.Y, 0.0f, 0.0f);
			var offset = -8.0f;
			
			foreach(var r in new RectangleF[] {p1Box, p2Box})
			{
				using(var path = new CGPath())
				{
					var cpath = new CGPath();
					r.Inflate (new SizeF (offset, offset));
					cpath.AddElipseInRect(r);
					ctxt.AddPath(cpath);
					ctxt.FillPath();
				}
			}
		}

	}
}
