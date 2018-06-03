// Version Incrementor Script for Unity by Francesco Forno (Fornetto Games)
// Inspired by http://forum.unity3d.com/threads/automatic-version-increment-script.144917/
/*
using UnityEditor;
using UnityEngine;
using System.Configuration;
using TMPro;

public class Version
{

	private static void IncrementVersion(int majorIncr, int minorIncr, int buildIncr)
	{
		string[] lines = PlayerSettings.bundleVersion.Split('.');

		int MajorVersion = int.Parse (lines [0]);
		int MinorVersion = int.Parse (lines [1]);
		int Build = int.Parse (lines [2]);
		if (majorIncr > 0) {
			MajorVersion += majorIncr;
			MinorVersion = 0;
			Build = 0;
		} else if (minorIncr > 0) {
			MinorVersion += minorIncr;
			Build = 0;
		} else if (buildIncr > 0) {
			Build += buildIncr;
		}

		string versionStr = MajorVersion.ToString("0") + "." +
			MinorVersion.ToString("0") + "." +
			Build.ToString("0");

		PlayerSettings.bundleVersion = versionStr;

		#if UNITY_ANDROID
		PlayerSettings.Android.bundleVersionCode = MajorVersion * 1000000 + MinorVersion * 1000 + Build;
		#endif

		EditorUtility.DisplayDialog ("Version","Version updated to: "+versionStr,"Ok");

	}

	[MenuItem("Version/Increase Build Number")]
	private static void IncreaseBuild()
	{
		IncrementVersion(0, 0, 1);
	}

	[MenuItem("Version/Increase Minor Version")]
	private static void IncreaseMinor()
	{
		IncrementVersion(0, 1, 0);
	}

	[MenuItem("Version/Increase Major Version")]
	private static void IncreaseMajor()
	{
		IncrementVersion(1, 0, 0);
	}

	[MenuItem("Version/Show Version")]
	private static void ShowVersion()
	{
		EditorUtility.DisplayDialog ("Version","Current version is: "+PlayerSettings.bundleVersion,"Ok");

		SetAndroidVersionCode ();
	}

	private static void SetAndroidVersionCode() {
		#if UNITY_ANDROID
		string[] lines = PlayerSettings.bundleVersion.Split('.');

		int MajorVersion = int.Parse (lines [0]);
		int MinorVersion = int.Parse (lines [1]);
		int Build = int.Parse (lines [2]);

		PlayerSettings.Android.bundleVersionCode = MajorVersion * 1000000 + MinorVersion * 1000 + Build;
		#endif
	}

	[MenuItem("Version/Dev Mode")]
	private static void UseDevMode()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.iOS, "DEV_MODE");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "DEV_MODE");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.WebGL, "DEV_MODE");


		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL 
			&& EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android
			&& EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
		{

			EditorUtility.DisplayDialog ("Cannot Change Mode!","Please change platform to WebGL, Android or iOS","Ok");
			return;
		}
			



        GameObject parent = GameObject.Find("LobbyCanvasLandscape").transform.Find("DevMode").gameObject;
        Transform fpsBg = parent.transform.Find("FPS BG");

		fpsBg.gameObject.SetActive (true);

		TMP_Text devModeTMP = fpsBg.Find ("DevMode_text").gameObject.GetComponent<TMP_Text> ();
		devModeTMP.SetText ("DEV MODE");

		EditorUtility.DisplayDialog ("Development Mode","All settings are now configured to DEV mode","Ok");
	}

	[MenuItem("Version/QA Mode")]
	private static void UseQAMode()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.iOS, "QA_MODE");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "QA_MODE");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.WebGL, "QA_MODE");


		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL 
			&& EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android
			&& EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
		{

			EditorUtility.DisplayDialog ("Cannot Change Mode!","Please change platform to WebGL, Android or iOS","Ok");
			return;
		}




        GameObject parent = GameObject.Find("LobbyCanvasLandscape").transform.Find("DevMode").gameObject;
        Transform fpsBg = parent.transform.Find("FPS BG");

        fpsBg.gameObject.SetActive (true);

        TMP_Text devModeTMP = fpsBg.Find ("DevMode_text").gameObject.GetComponent<TMP_Text> ();
		devModeTMP.SetText ("QA MODE");

		EditorUtility.DisplayDialog ("QA Mode","All settings are now configured to QA mode","Ok");
	}

	[MenuItem("Version/Prod Mode")]
	private static void UseProdMode()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.iOS, "");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "");
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.WebGL, "");



        GameObject parent = GameObject.Find("LobbyCanvasLandscape").transform.Find("DevMode").gameObject;
        Transform fpsBg = parent.transform.Find("FPS BG");

        fpsBg.gameObject.SetActive (false);

		EditorUtility.DisplayDialog ("Production Mode","All settings are now configured to PROD mode","Ok");
	}

}*/