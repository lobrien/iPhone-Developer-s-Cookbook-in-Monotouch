
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Recipe2dot3
{

	[MonoTouch.Foundation.Register("DragViewController")]
	public class DragViewController : UIViewController
	{
		UIView contentView;
		const int MAXFLOWERS = 10;
		
		Random rand = new Random();
		
		String[] flowerNames = {"blueFlower.png", "pinkFlower.png", "orangeFlower.png"};
		
		public DragViewController (IntPtr hnd) : base(hnd)
		{
		}
		
		private PointF RandomPoint()
		{
			return new PointF(rand.Next(256), rand.Next(396));
		}
		
		public override void LoadView ()
		{
			//Create the main view with a black background
			var appRect = UIScreen.MainScreen.ApplicationFrame;
			contentView = new UIView(appRect);
			contentView.BackgroundColor = UIColor.Red;
			this.View = contentView;
			
			//Add the flowers to random points on the screen
			for(var i = 0; i < MAXFLOWERS; i++)
			{
				var dragRect = new RectangleF(0.0f, 0.0f, 64.0f, 64.0f);
				dragRect.Location = RandomPoint();
				var dragger = new DragView();
				dragger.SetNeedsDisplay();
				dragger.Frame = dragRect;
				dragger.UserInteractionEnabled = true;
				
				//Select random flower color
				var whichFlower = flowerNames[rand.Next(3)];
				dragger.Image = UIImage.FromFile(whichFlower);
				
				//Add the subview
				contentView.AddSubview(dragger);
			}
		}
	}
}
