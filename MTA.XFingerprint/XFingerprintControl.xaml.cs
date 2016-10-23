using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MTA.XFingerprint
{
    public partial class XFingerprintControl : Frame
    {
        public XFingerprintControl()
        {
            InitializeComponent();
            BackgroundColor = Color.Transparent;
            OutlineColor = Color.Transparent;
            Padding = 0;
            this.btFingerprint.Clicked += (s, e) =>
            {
                XFingerprint.Current.CheckPermissions();
                if (XFingerprint.Current.IsAvailable())
                {
                    this.btFingerprint.Text = "Wait for fingerprint read";
                    this.btFingerprint.TextColor = Color.White;

                    XFingerprint.Current.RequestFingerprint(
                   () =>
                   {
                       this.btFingerprint.Text = "Passed";
                       this.btFingerprint.TextColor = Color.Green;
                   },
                   () =>
                   {
                       this.btFingerprint.Text = "Refused";
                       this.btFingerprint.TextColor = Color.Red;
                   },
                   10);
                }
                else
                {
                    this.btFingerprint.TextColor = Color.Red;
                    this.btFingerprint.Text = "No fingerprint available";
                }
            };

        }

        public void OnFingerprintAccepted()
        {

        }
    }

}
