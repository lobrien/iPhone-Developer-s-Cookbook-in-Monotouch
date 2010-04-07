
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Recipe2Dot1NestedSubviews
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var myViewController = new MyViewController();
			window.AddSubview (myViewController.View);
			
			window.MakeKeyAndVisible ();
			
			return true;
		}
		

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
		
		
	}
}
