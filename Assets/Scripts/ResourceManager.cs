#define USE_ASSETBUNDLE
using UnityEngine;
using System.Collections;

public class ResourceManager:MonoBehaviour
{
	//不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
	public static readonly string PathURL =
		#if UNITY_ANDROID
		"jar:file://" + Application.dataPath + "!/assets/";
	#elif UNITY_IPHONE
	Application.dataPath + "/Raw/";
	#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
	"file://" + Application.dataPath + "/StreamingAssets/";
	#else
	string.Empty;
	#endif

	public static Object load (string path)
	{
		#if USE_ASSETBUNDLE
		WWW bundle = new WWW (PathURL + GetAssetBuddleName (path) + ".assetbundle");
		
		Object obj0 = bundle.assetBundle.Load (GetResourceName (path));
		
		return obj0;
		#else
		return Resources.Load (path);
#endif
	}

	public static Object load (string path, System.Type type)
	{
		#if USE_ASSETBUNDLE
		WWW bundle = new WWW (PathURL + GetAssetBuddleName (path) + ".assetbundle");

		Object obj0 = bundle.assetBundle.Load (GetResourceName (path), type);

		return obj0;
#else
		return Resources.Load(path, type);
#endif
	}

	public static string GetAssetBuddleName (string path)
	{
		return path.Substring (0, path.LastIndexOf ("/")).Replace ("/", "_");
	}

	public static string GetResourceName (string path)
	{
		return path.Substring (path.LastIndexOf ("/") + 1);
	}

}
