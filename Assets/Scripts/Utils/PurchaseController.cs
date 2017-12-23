using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
		Debug.Log("SAVED IN PP: " + saved_in_pp);
		Debug.Log("SAVED IN GOOGLE: " + has_reciept);

		if (saved_in_pp == 1 || has_reciept)
			m_already_bought = true;

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
}
