using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DevTools
{
	private static bool showLogs = true;
	private static bool showErrors = true;

	public static void SetDebugOutputs(bool showLogs, bool showErrors)
	{
		DevTools.showLogs = showLogs;
		DevTools.showErrors = showErrors;
	}

	public static void Log(string msg)
	{
		if (showLogs) Debug.Log(msg);
	}

	public static void Error(string err)
	{
		if (showErrors) Debug.LogError(err);
	}
}
