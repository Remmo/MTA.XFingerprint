using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MTA.XFingerprint.Droid")]
namespace MTA.XFingerprint
{
    public interface IXFingerprint
    {
        void CheckPermissions();
        bool IsAvailable();
        void RequestFingerprint(Action onFingerprintAccepted, Action onFingerprintRefused, Int32 timeout);
    }

  
}
