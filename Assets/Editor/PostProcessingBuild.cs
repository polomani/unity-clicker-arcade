#if UNITY_IOS
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using System.IO;

public class PostProcessingBuild
{
    [PostProcessBuild]
    public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            //Plist changes
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
           
            // Get root
            PlistElementDict rootDict = plist.root;
           
            //new in iOS 10
            rootDict.SetString("NSCameraUsageDescription", "Used to share with your friends");
            rootDict.SetString("NSPhotoLibraryUsageDescription", "Used to share with your friends");
            
            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}
#endif
