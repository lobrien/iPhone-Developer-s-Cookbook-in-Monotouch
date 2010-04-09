
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Recipe2Dot7
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
			var ioView = new FlipView(UIScreen.MainScreen.ApplicationFrame);
			ioView.Image = UIImage.FromFile("io.png");
			ioView.UserInteractionEnabled = true;
			window.AddSubview (ioView);
			
			var chView = new FlipView(UIScreen.MainScreen.ApplicationFrame);
			chView.Image = UIImage.FromFile("chameleon.png");
			chView.UserInteractionEnabled = true;
			window.AddSubview(chView);
			
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}