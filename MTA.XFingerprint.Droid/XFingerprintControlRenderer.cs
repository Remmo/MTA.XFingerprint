//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xamarin.Forms;
//using MTA.XFingerprint;
//using MTA.XFingerprint.Droid;
//using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(XFingerprintControl), typeof(XFingerprintControlRenderer))]
//namespace MTA.XFingerprint.Droid
//{
//    class XFingerprintControlRenderer : FrameRenderer
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
//        {
//            base.OnElementChanged(e);
//            if (this.Element != null)
//            {

//                ((XFingerprintControl)this.Element).OnFingerprintAccepted();
//                //+= (s, ee) =>
//                //{

//                //};
//            }
//        }
//    }
//}