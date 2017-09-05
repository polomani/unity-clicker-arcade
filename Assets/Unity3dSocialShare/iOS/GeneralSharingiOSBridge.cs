using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace IndigoBunting.SocialSharing
{
    public class GeneralSharingiOSBridge
    {
#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _IB_ShareTextWithImage (string iosPath, string message);
        
        [DllImport("__Internal")]
        private static extern void _IB_ShareSimpleText (string message);
                
        public static void ShareSimpleText (string message)
        {
            _IB_ShareSimpleText (message);
        }

        public static void ShareTextWithImage (string imagePath, string message)
        {
            _IB_ShareTextWithImage (imagePath, message);
        }
#endif
    }
}

