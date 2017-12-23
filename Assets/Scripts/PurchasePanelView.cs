using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PurchasePanelView : MonoBehaviour {

	[SerializeField]TMP_Text	m_feedback_test=null;
	[SerializeField]Button 		m_buy_button = null;

	public TMP_Text M_feedback_test {
		get {
			return m_feedback_test;
		}
		set {
			m_feedback_test = value;
		}
	}

	public Button M_buy_button {
		get {
			return m_buy_button;
		}
		set {
			m_buy_button = value;
		}
	}
}
