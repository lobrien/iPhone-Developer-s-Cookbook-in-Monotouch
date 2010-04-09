
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace Recipe2Dot6
{

	[Register("ToggleView")]
	public class ToggleView : UIView
	{
		readonly RectangleF BIGRECT = new RectangleF(0.0f, 0.0f, 320.0f, 435.0f);
		readonly RectangleF SMALLRECT = new RectangleF(130.0f, 187.0f, 60.0f, 60.0f);

		UIImageView imgView1, imgView2;
		bool isOne;
		
		public ToggleView (RectangleF frame):base(frame)
		{
			//Load both views, make them noninteractive
			imgView1 = new UIImageView(BIGRECT);
			imgView2 = new UIImageView(SMALLRECT);
			imgView1.Image = UIImage.FromFile("io.png");
			imgView2.Image = UIImage.FromFile("chameleon.png");
			imgView1.UserInteractionEnabled = false;
			imgView2.UserInteractionEnabled = false;
			
			//image 1 is in front of image 2 to begin
			AddSubview(imgView2);
			AddSubview(imgView1);
			isOne = true;
		}
		
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			//Determine which view occupies which role
			var big = isOne ? imgView1 : imgView2;
			var little = isOne ? imgView2 : imgView1;
			isOne = !isOne;
			
			//Pack all the changes into the animation block
			UIView.BeginAnimations("");
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDuration(1.0);
			
			big.Frame = SMALLRECT;
			big.Alpha = 0.5f;
			little.Frame = BIGRECT;
			little.Alpha = 1.0f;
			
			UIView.CommitAnimations();
			
			//Hide the shrunken "big image"
			big.Alpha = 0.0f;
			big.Superview.BringSubviewToFront(big);
		}
		
		protected override void Dispose (bool disposing)
		{
			imgView1.Dispose();
			imgView2.Dispose();
			base.Dispose (disposing);
		}


	}
}
