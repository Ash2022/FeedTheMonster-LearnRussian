using UnityEngine;
using System.Collections;

public class Analitics : MonoBehaviour
{
	public static Analitics Instance;

	public GoogleAnalyticsV4 googleAnalytics;


	void Awake()
	{
		Instance = this;
	}


	// Use this for initialization
	void Start ()
	{
		if (googleAnalytics != null) {
			googleAnalytics.StartSession ();
		}
	}

	void OnDisable() {
		if (googleAnalytics != null) {
			googleAnalytics.StopSession ();
		}
	}


	public void treckScreen (string screenName)
	{
		if (googleAnalytics != null) {
			googleAnalytics.LogScreen (screenName);
		}
	}


	public void treckEvent (AnaliticsCategory category, AnaliticsAction action, string label, long value = 0)
	{
		treckEvent (category, action.ToString (), label, value);
	}

	public void treckEvent (AnaliticsCategory category, string action, string label, long value = 0)
	{
		if (googleAnalytics != null) {
			googleAnalytics.LogEvent (category.ToString(), action, label, value);
		}
	}

    public void LogTransaction(string transID,double revenue, string currency)
    {
        if (googleAnalytics != null)
        {
            googleAnalytics.LogTransaction(transID, "InApp", revenue, 0, 0, currency);
        }
    }

}
