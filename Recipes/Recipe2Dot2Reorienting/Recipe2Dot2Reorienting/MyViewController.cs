using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Recipe2Dot2Reorienting
{

	[MonoTouch.Foundation.Register("MyViewController")]
	public partial class MyViewController : UIViewController
	{
		
		public MyViewController(IntPtr handle) : base(handle)
		{
			
		}

		public MyViewController ()
		{
		}
		
		UIView contentView;
		
		//Recipe 2-1 Adding Nested Subviews
		public override void LoadView()
		{
			//Create the main view
			RectangleF appRect = UIScreen.MainScreen.ApplicationFrame;
			
			contentView = new UIView(appRect);
			contentView.BackgroundColor = UIColor.Green;
			
			//Provide support for autorotation and resizing
			contentView.AutosizesSubviews = true;
			contentView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			this.View = contentView;
						
			// reset the origin point for subviews. The new origin is 0, 0
			appRect.Location = new PointF(0.0f, 0.0f);
			
			//Add the subviews, each stepped by 32 pixels on each side
			var subview = new UIView(RectangleF.Inflate(appRect, -32.0f, -32.0f));
			subview.BackgroundColor = UIColor.Clear;
			contentView.AddSubview(subview);
			
			subview = new UIView(RectangleF.Inflate(appRect, -64.0f, -64.0f));
			subview.BackgroundColor = UIColor.DarkGray;
			contentView.AddSubview(subview);
			
			subview = new UIView(RectangleF.Inflate(appRect, -96.0f, -96.0f));
			subview.BackgroundColor = UIColor.Black;
			contentView.AddSubview(subview);
			
		}
		
		//Recipe 2-2 Adding Reorientation Support to the Preceding Subview Example
		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			var appRect = new RectangleF();
			appRect.Offset(new PointF(0.0f, 0.0f));
			//Adjust the frame based on the actual orientation
			if(toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || 
			   toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
			{
				appRect.Size = new SizeF(480.0f, 300.0f);
			}
			else
			{
				appRect.Size = new SizeF(320.0f, 460.0f);	
			}
			
			//Resize each subview accordingly
			var offset = -32.0f;
			foreach(var subview in contentView.Subviews)
			{
				var frame = RectangleF.Inflate(appRect, offset, offset);
				subview.Frame = frame;
				offset -= 32.0f;
			}
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}


		
	}
}
