using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MTA.XFingerprint
{
    public class XFingerprint
    {
        public static IXFingerprint Current => DependencyService.Get<IXFingerprint>();      
    }
}
