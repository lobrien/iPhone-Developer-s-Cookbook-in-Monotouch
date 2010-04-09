
using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Recipe2Dot5
{

	[MonoTouch.Foundation.Register("ToggleView")]
	public class ToggleView : UIView
	{
		bool isVisible;
		UIImageView imgView;
		
		public ToggleView ()
		{
			
		}
		
		public ToggleView(RectangleF frame):base(frame)
		{
			isVisible = true;
			imgView = new UIImageView(UIScreen.MainScreen.ApplicationFrame);
			imgView.Frame = frame;
			imgView.Image = UIImage.FromFile("alphablend.png");
			imgView.UserInteractionEnabled = false;
			AddSubview(imgView);
		}
		
		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			//Only respond to mousedown events
			var touch = touches.AnyObject as UITouch;
			if(touch.Phase != UITouchPhase.Began)
			{
				return;
			}
			
			isVisible = !isVisible;
			
			var ctxt = UIGraphics.GetCurrentContext();
			UIView.BeginAnimations("");
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDuration(1.0);
			Console.WriteLine("imgView alpha is " + imgView.Alpha);
			imgView.Alpha = isVisible ? 1.0f : 0.0f;
			Console.WriteLine("imgView alpha is " + imgView.Alpha);
			
			UIView.CommitAnimations();
		}
		
		protected override void Dispose (bool disposing)
		{
			imgView.Dispose();
			base.Dispose (disposing);
		}


	
		
	}
}
