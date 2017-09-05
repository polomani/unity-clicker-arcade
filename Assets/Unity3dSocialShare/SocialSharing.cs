using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace IndigoBunting.SocialSharing
{
    public class SocialSharing : MonoBehaviour
    {
        #pragma warning disable 0414

        private string tmpCurrentShareText;

        void OnEnable()
        {
            #if UNITY_ANDROID
            this.OnAndroidScreenshotSaved += SocialSharing_OnAndroidScreenshotSaved;
            #endif
            #if UNITY_IOS
            IOSPhotoGalleryHandler.OnImageSaved += IOSPhotoGalleryHandler_OnImageFinishedSaving;
            #endif
        }

        void OnDisable()
        {
            #if UNITY_ANDROID
            this.OnAndroidScreenshotSaved -= SocialSharing_OnAndroidScreenshotSaved;
            #endif
            #if UNITY_IOS
            IOSPhotoGalleryHandler.OnImageSaved -= IOSPhotoGalleryHandler_OnImageFinishedSaving;
            #endif
        }

        public void ShareScreenshot(string shareText)
        {
            tmpCurrentShareText = shareText;

            StartCoroutine(CreateScreenshot());
        }

        public void ShareTexture2D(string shareText, Texture2D texture2D)
        {
            tmpCurrentShareText = shareText;

            string filename = System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            string pathToImage = Path.Combine(Application.persistentDataPath, filename + ".png");
            byte[] dataToSave = texture2D.EncodeToPNG();
            File.WriteAllBytes(pathToImage, dataToSave);

            #if UNITY_ANDROID
            if (OnAndroidScreenshotSaved != null)
            {
            OnAndroidScreenshotSaved(pathToImage);
            }
            #endif

            #if UNITY_IOS
            StartCoroutine(IOSPhotoGalleryHandler.SaveExisting(pathToImage));
            #endif
        }

        private IEnumerator CreateScreenshot()
        {
            yield return new WaitForEndOfFrame();
            Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
            screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
            screenTexture.Apply();

            string filename = System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            string pathToImage = Path.Combine(Application.persistentDataPath, filename + ".png");
            byte[] dataToSave = screenTexture.EncodeToPNG();
            File.WriteAllBytes(pathToImage, dataToSave);

            #if UNITY_ANDROID
            if (OnAndroidScreenshotSaved != null)
            {
                OnAndroidScreenshotSaved(pathToImage);
            }
            #endif
                
            #if UNITY_IOS
            StartCoroutine(IOSPhotoGalleryHandler.SaveExisting(pathToImage));
            #endif
        }

        private void NativeShare(string text, string pathToImage)
        {
            #if UNITY_ANDROID
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + pathToImage);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), text);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
            #endif

            #if UNITY_IOS
            GeneralSharingiOSBridge.ShareTextWithImage(pathToImage, text);
            #endif
        }

        private void IOSPhotoGalleryHandler_OnImageFinishedSaving(string pathToImage)
        {
            NativeShare(tmpCurrentShareText, pathToImage);
        }

        private void SocialSharing_OnAndroidScreenshotSaved (string pathToImage)
        {
            NativeShare(tmpCurrentShareText, pathToImage);
        }

        #if UNITY_ANDROID    
        public event Action<string> OnAndroidScreenshotSaved;
        #endif
    }
}
