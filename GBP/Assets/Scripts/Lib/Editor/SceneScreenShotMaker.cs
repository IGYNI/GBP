using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class SceneScreenShotMaker : EditorWindow
{
	private const String ScreenShotFolder = "/ScreenShots/";
	
	[MenuItem("Window/Screenshot/1X")]
	public static void Make1X()
	{
		MakeScreen(1);
	}

	[MenuItem("Window/Screenshot/2X")]
	public static void Make2X()
	{
		MakeScreen(2);
	}

	[MenuItem("Window/Screenshot/3X")]
	public static void Make3X()
	{
		MakeScreen(3);
	}

	[MenuItem("Window/Screenshot/4X")]
	public static void Make4X()
	{
		MakeScreen(4);
	}

	[MenuItem("Window/Screenshot/5X")]
	public static void Make5X()
	{
		MakeScreen(5);
	}

	[MenuItem("Window/Screenshot/Open Folder")]
	public static void Test()
	{
		Open(Application.dataPath + ScreenShotFolder);
	}

	private static void MakeScreen(int factor)
	{
		if (!Directory.Exists(Application.dataPath + ScreenShotFolder))
		{
			Directory.CreateDirectory(Application.dataPath + ScreenShotFolder);
		}
		ScreenCapture.CaptureScreenshot("Assets"+ScreenShotFolder + "Screenshot_" + factor + "X_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".png", factor);
	}


	// public static bool IsInMacOS
	// {
	// 	get
	// 	{
	// 		return UnityEngine.SystemInfo.operatingSystem.IndexOf("Mac OS") != -1;
	// 	}
	// }

	// public static bool IsInWinOS
	// {
	// 	get
	// 	{
	// 		return UnityEngine.SystemInfo.operatingSystem.IndexOf("Windows") != -1;
	// 	}
	// }

	

	// public static void OpenInMac(string path)
	// {
	// 	bool openInsidesOfFolder = false;

	// 	// try mac
	// 	string macPath = path.Replace("\\", "/");

	// 	if (Directory.Exists(macPath))
	// 	{
	// 		openInsidesOfFolder = true;
	// 	}

	// 	if (!macPath.StartsWith("\""))
	// 	{
	// 		macPath = "\"" + macPath;
	// 	}

	// 	if (!macPath.EndsWith("\""))
	// 	{
	// 		macPath = macPath + "\"";
	// 	}

	// 	string arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;

	// 	try
	// 	{
	// 		System.Diagnostics.Process.Start("open", arguments);
	// 	}
	// 	catch (System.ComponentModel.Win32Exception e)
	// 	{			
	// 		e.HelpLink = "";
	// 	}
	// }

	// public static void OpenInWin(string path)
	// {
	// 	bool openInsidesOfFolder = false;
		
	// 	string winPath = path.Replace("/", "\\");

	// 	if (Directory.Exists(winPath))
	// 	{
	// 		openInsidesOfFolder = true;
	// 	}

	// 	try
	// 	{
	// 		System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
	// 	}
	// 	catch (System.ComponentModel.Win32Exception e)
	// 	{			
	// 		e.HelpLink = "";
	// 	}
	// }

	public static void Open(string path)
	{
		if (!Directory.Exists(Application.dataPath + ScreenShotFolder))
		{
			Directory.CreateDirectory(Application.dataPath + ScreenShotFolder);
		}
		Application.OpenURL("file:///"+Application.dataPath + ScreenShotFolder);
	}
}