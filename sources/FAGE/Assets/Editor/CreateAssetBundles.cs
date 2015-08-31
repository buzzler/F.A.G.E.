using UnityEditor;
using UnityEngine;
using System;

public class CreateAssetBundles
{
	[MenuItem ("Assets/Build AssetBundles iOS")]
	static void BuildAllAssetBundlesIOS () {
		BuildPipeline.BuildAssetBundles ("AssetBundles", BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.iOS);
	}

	[MenuItem ("Assets/Build AssetBundles Android")]
	static void BuildAllAssetBundlesAndroid () {
		BuildPipeline.BuildAssetBundles ("AssetBundles", BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
	}
}