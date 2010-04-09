
using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Recipe2Dot7
{


	public class FlipView : UIImageView
	{

		public FlipView ()
		{
		}
		
		public FlipView(RectangleF frame):base(frame)
		{
		}
		
		public FlipView(IntPtr hnd) : base(hnd)
		{
		}
		
		public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			UIView.BeginAnimations("");
			UIView.SetAnimationTransition(UIViewAnimationTransition.FlipFromLeft, this.Superview, true);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDuration(1.0);
			
			//Animations
			Superview.ExchangeSubview(0, 1);
			
			//Commit Animation Block
			UIView.CommitAnimations();
		}

	}
}
