using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseController : MonoBehaviour {


	public static PurchaseController Instance{ get; set;}

	[SerializeField] TMP_Text	m_feedback_text;
	[SerializeField]GameObject	m_purchase_view=null;
	int							m_bought_in_pp=0;
	bool						m_has_reciept=false;

	bool						m_already_bought=false;

	void Awake()
	{
		Instance = this;
	}

	public void InitStates()
	{
		int saved_in_pp = PlayerPrefs.GetInt ("AllLevelUnlocked");
		bool has_reciept = IAPManager.Instance.Already_purchased;

		if (saved_in_pp == 1 || has_reciept)
			m_already_bought = true;
	}

	void Start()
	{
		InitStates ();
	}

	public void BuyClicked()
	{
		IAPManager.Instance.BuyUnlockAllLevels ((bool succ)=>
			{

				m_feedback_text.text = succ.ToString();
				if(succ)
				{
					PlayerPrefs.SetInt("AllLevelUnlocked",1);
				}
				else
				{

				}
			});
	}

	public void Init()
	{
		m_feedback_text.text = "Clear";
	}

	public bool Already_bought {
		get {
			return m_already_bought;
		}
	}
}
