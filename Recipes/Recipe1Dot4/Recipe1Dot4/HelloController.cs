
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Recipe1Dot4
{
	
	[MonoTouch.Foundation.Register("HelloController")]
	public partial class HelloController : UIViewController
	{
		
		public HelloController(IntPtr handle) : base(handle)
		{
			
		}
		
		public HelloController()
		{
			this.Title = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleName").ToString();
			//Place any further initialization here
		}

		
		private UIImageView contentView;
		
		public override void LoadView()
		{
			//Load an application image and set it as the primary view
			contentView = new UIImageView(UIScreen.MainScreen.ApplicationFrame);
			contentView.Image = UIImage.FromFile("fp_icon.png");
			this.View = contentView;
			
			//Provide support for auto-rotation and resizing
			contentView.AutosizesSubviews = true;
			contentView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}
