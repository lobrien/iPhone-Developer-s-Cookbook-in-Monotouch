
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace Recipe2Dot4ClippingSubviews
{

	[MonoTouch.Foundation.Register("DragViewController")]
	public class DragViewController : UIViewController
	{
		UIView contentView;
		const int MAXFLOWERS = 2;
		
		Random rand = new Random();
		
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
			
			for(var i = 0; i < MAXFLOWERS; i++)
			{
				var dragRect = new RectangleF(0.0f, 0.0f, 64.0f, 64.0f);
				dragRect.Location = RandomPoint();
				var dragger = new DragView();
				dragger.SetNeedsDisplay();
				dragger.Frame = dragRect;
				dragger.UserInteractionEnabled = true;
				
				dragger.WhichFlower = "Globe";
				dragger.Image = UIImage.FromFile("globe.png");
				
				//Add the subview
				contentView.AddSubview(dragger);
			}
		}
		
		public void UpdateDefaults()
		{
			var colors = new List<String>();
			var locations = new List<RectangleF>();
			
			foreach(DragView dragView in contentView.Subviews)
			{
				colors.Add(dragView.WhichFlower);
				locations.Add(dragView.Frame);
			}
			
			//Can't store strongly typed collection as NSObject, so serialize it to a string
			var color_serializer = new XmlSerializer(typeof(List<String>));
			var writer = new StringWriter();
			color_serializer.Serialize(writer, colors);
			var prefs = NSUserDefaults.StandardUserDefaults;
			string color_xml = writer.ToString ();
			prefs.SetString(color_xml, "colors");
			
			var loc_serializer = new XmlSerializer(typeof(List<RectangleF>));
			var w2 = new StringWriter();
			loc_serializer.Serialize(w2, locations);
			string location_xml = w2.ToString ();
			NSUserDefaults.StandardUserDefaults.SetString(location_xml, "locations");
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}
	}
}