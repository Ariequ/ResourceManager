using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class Build : Editor
{
	private static string RESOURCES_PATH = Application.dataPath + "/Resources";

	[MenuItem("Custom Editor/Create AssetBunldes Main")]
	static void CreateAssetBunldesMain ()
	{
		DirectoryInfo raw = new DirectoryInfo (RESOURCES_PATH);		

		ExportAssetBunddle (raw);

		AssetDatabase.Refresh ();		
	}

	private static void ExportAssetBunddle (DirectoryInfo directory)
	{
		FileInfo[] fileInfo = directory.GetFiles ();
		int filesCount = fileInfo.Length;
		if (filesCount > 0) {
			List<Object> objectList = new List<Object>();
			for (int i = 0; i < filesCount; i++) {

				if (!fileInfo [i].Name.Contains (".meta")) {
					string fullPath = fileInfo [i].FullName;
					string strTempPath = fullPath.Replace (@"\", "/");
					strTempPath = strTempPath.Substring (strTempPath.IndexOf ("Assets"));
					Object obj = AssetDatabase.LoadAssetAtPath (strTempPath, typeof(Object));

					objectList.Add(obj);
				}
			}

			if (objectList.Count > 0) {
				string fullName = directory.FullName;
				string assetbunddleName = fullName.Substring(fullName.IndexOf("Resources/") + "Resources/".Length).Replace("/", "_");
				string targetPath = Application.dataPath + "/StreamingAssets/" + assetbunddleName + ".assetbundle";
				Debug.Log(objectList[0].name);
				if (BuildPipeline.BuildAssetBundle (objectList [0], objectList.ToArray(), targetPath, BuildAssetBundleOptions.CollectDependencies)) {
					Debug.Log (assetbunddleName + "资源打包成功");
				} else {
					Debug.Log (assetbunddleName + "资源打包失败");
				}
			}
		}

		foreach (DirectoryInfo childDirectory in directory.GetDirectories()) {
			ExportAssetBunddle (childDirectory);
		}
	}
}
