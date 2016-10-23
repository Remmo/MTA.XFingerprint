using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MTA.XFingerprint.Droid;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Support.V4.Content;
using Android;
using Java.Lang;
using Javax.Crypto;
using Android.Util;
using MTA.XFingerprint;

[assembly: Xamarin.Forms.Dependency(typeof(Fingerprint))]
namespace MTA.XFingerprint.Droid
{
    public class Fingerprint : IXFingerprint
    {
        public void CheckPermissions()
        {
            var context = Android.App.Application.Context;
            // The context is typically a reference to the current activity.
            Android.Content.PM.Permission permissionResult = ContextCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint);
            if (permissionResult == Android.Content.PM.Permission.Granted)
            {
                // Permission granted - go ahead and start the fingerprint scanner.
            }
            else
            {
                // No permission. Go and ask for permissions and don't start the scanner. See
                // http://developer.android.com/training/permissions/requesting.html
            }
        }

        public bool IsAvailable()
        {
            var context = Android.App.Application.Context;
            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);

            return fingerprintManager.IsHardwareDetected;
        }

        public void RequestFingerprint(Action onFingerprintAccepted, Action onFingerprintRefused, Int32 timeout)
        {
            var context = Android.App.Application.Context;

            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);

            if (!fingerprintManager.HasEnrolledFingerprints)
            {
                // Can't use fingerprint authentication - notify the user that they need to 
                // enroll at least one fingerprint with the device.
            }

            const int flags = 0; /* always zero (0) */

            // CryptoObjectHelper is described in the previous section.
            CryptoObjectHelper cryptoHelper = new CryptoObjectHelper();

            // cancellationSignal can be used to manually stop the fingerprint scanner. 
            var cancellationSignal = new Android.Support.V4.OS.CancellationSignal();
            FingerprintManagerCompat fingerPrintManager = FingerprintManagerCompat.From(context);

            // AuthenticationCallback is a base class that will be covered later on in this guide.
            FingerprintManagerCompat.AuthenticationCallback authenticationCallback = new XFAuthCallbackSample(onFingerprintAccepted, onFingerprintRefused);

            // Start the fingerprint scanner.
            //   fingerprintManager.Authenticate(cryptoHelper.BuildCryptoObject(), flags, cancellationSignal, authenticationCallback, null);
            fingerprintManager.Authenticate(null, flags, cancellationSignal, authenticationCallback, null);
        }
    }
    class XFAuthCallbackSample : FingerprintManagerCompat.AuthenticationCallback
    {
        // Can be any byte array, keep unique to application.
        static readonly byte[] SECRET_BYTES = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        // The TAG can be any string, this one is for demonstration.
        static readonly string TAG = "X:" + "adasdsa";// typeof(SimpleAuthCallbacks).Name;
        Action OnFingerprintAccepted; Action OnFingerprintRefused;
        public XFAuthCallbackSample(Action onFingerprintAccepted, Action onFingerprintRefused)
        {
            this.OnFingerprintAccepted = onFingerprintAccepted;
            this.OnFingerprintRefused = onFingerprintRefused;
        }

        public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
        {
            if (result.CryptoObject != null && result.CryptoObject.Cipher != null)
            {
                try
                {
                    // Calling DoFinal on the Cipher ensures that the encryption worked.
                    byte[] doFinalResult = result.CryptoObject.Cipher.DoFinal(SECRET_BYTES);
                    this.OnFingerprintAccepted.Invoke();
                    // No errors occurred, trust the results.              
                }
                catch (BadPaddingException bpe)
                {
                    // Can't really trust the results.
                    Log.Error(TAG, "Failed to encrypt the data with the generated key." + bpe);
                }
                catch (IllegalBlockSizeException ibse)
                {
                    // Can't really trust the results.
                    Log.Error(TAG, "Failed to encrypt the data with the generated key." + ibse);
                    this.OnFingerprintRefused.Invoke();

                }
            }
            else
            {
                this.OnFingerprintAccepted.Invoke();

                // No cipher used, assume that everything went well and trust the results.
            }
        }

        public override void OnAuthenticationError(int errMsgId, ICharSequence errString)
        {
            this.OnFingerprintRefused.Invoke();
            // Report the error to the user. Note that if the user canceled the scan,
            // this method will be called and the errMsgId will be FingerprintState.ErrorCanceled.
        }

        public override void OnAuthenticationFailed()
        {
            this.OnFingerprintRefused.Invoke();
            // Tell the user that the fingerprint was not recognized.
        }

        public override void OnAuthenticationHelp(int helpMsgId, ICharSequence helpString)
        {
            this.OnFingerprintRefused.Invoke();
            // Notify the user that the scan failed and display the provided hint.
        }
    }
   
}