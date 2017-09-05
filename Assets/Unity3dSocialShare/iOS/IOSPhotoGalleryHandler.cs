using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace IndigoBunting.SocialSharing
{
    public class IOSPhotoGalleryHandler : MonoBehaviour
    {
#if UNITY_IOS
        public static event Action<string> OnImageSaved;

        [DllImport("__Internal")]
        private static extern bool saveToGallery (string path);

        public static IEnumerator SaveExisting(string filePath)
        {
            yield return 0;
            bool photoSaved = false;
        
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                while (!photoSaved) 
                {
                    photoSaved = saveToGallery(filePath);
                    yield return new WaitForSeconds (.5f);
                }
                
                UnityEngine.iOS.Device.SetNoBackupFlag(filePath);
            }

            if (OnImageSaved != null) 
            { 
                OnImageSaved(filePath);
            }
        }
#endif
    }
}

