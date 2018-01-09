using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class PurchaseController : MonoBehaviour {


	public static PurchaseController Instance{ get; set;}

	[SerializeField] TMP_Text	m_feedback_text;
	[SerializeField] TMP_Text	m_price_text;
	[SerializeField] TMP_Text	m_desc_text;
	[SerializeField]GameObject	m_purchase_view=null;
	int							m_bought_in_pp=0;
	bool						m_has_reciept=false;
	bool						m_price=false;

	bool						m_already_bought=false;

	Action<bool>					m_bought_callback;

	void Awake()
	{
		Instance = this;
	}

	public bool InitStates()
	{
		m_feedback_text.text = "Clear";
		int saved_in_pp = PlayerPrefs.GetInt ("AllLevelUnlocked");
		bool has_reciept = IAPManager.Instance.Already_purchased;
		m_price_text.text = IAPManager.Instance.String_price;
		m_desc_text.text = IAPManager.Instance.String_item_name;


		if (m_desc_text.text == null) //this means you are offline and also you dont have the key set in PP
		{
			m_desc_text.text = "Нет сети - попробуйте подключится заново";
		}

		Debug.Log("SAVED IN PP: " + saved_in_pp);
		Debug.Log("SAVED IN GOOGLE: " + has_reciept);

		if (saved_in_pp == 1 || has_reciept)
			m_already_bought = true;

		if(has_reciept)//this means he probably canceled the purchase - so still has local key - but not purchase key - next time will not work
			PlayerPrefs.SetInt("AllLevelUnlocked",1);
		else
			PlayerPrefs.SetInt("AllLevelUnlocked",0);

		return m_already_bought;
	}

	public void ShowBuyScreen(bool show, Action<bool> buy_callback=null)
	{
		if (buy_callback != null)
			m_bought_callback = buy_callback;

		m_purchase_view.SetActive (show);
	}

	public void BuyClicked()
	{
		IAPManager.Instance.BuyUnlockAllLevels ((bool succ)=>
			{

				m_feedback_text.text = succ.ToString();

				if(succ)
				{
					PlayerPrefs.SetInt("AllLevelUnlocked",1);
					m_already_bought = true;

					if (m_bought_callback!=null)
						m_bought_callback(true);
				}

			});
	}

	public void CloseClicked()
	{
		ShowBuyScreen (false);

		if (m_bought_callback!=null)
			m_bought_callback(false);

	}

	public bool Already_bought {
		get {
			return m_already_bought;
		}
	}

	public void ClearPP()
	{
		PlayerPrefs.DeleteKey ("AllLevelUnlocked");
		Debug.Log ("Deleted PP key");
	}


	public void CreateNames()
	{
		string path = "Assets/Resources/test.txt";

		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter(path, true);



		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Sounds/Voice/Words");
		FileInfo[] info = dir.GetFiles("*.mp3");
		foreach (FileInfo f in info) 
		{ 
			string temp = f.Name;
			int len = temp.Length;
			string word = temp.Substring (0, len - 4);

			writer.WriteLine(word);

		}

		writer.Close();

	}
}
