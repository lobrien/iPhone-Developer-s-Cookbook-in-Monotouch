
using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Recipe2dot3
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	public partial class AppDelegate : UIApplicationDelegate
	{
		UIView contentView;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			contentView = myController.View;
			contentView.BackgroundColor = UIColor.Black;
			window.AddSubview (myController.View);
			
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
		
		public override void WillTerminate (UIApplication application)
		{
			myController.UpdateDefaults();
		}
	}
}
