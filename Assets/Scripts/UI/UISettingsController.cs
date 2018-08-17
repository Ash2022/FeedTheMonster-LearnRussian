using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISettingsController : UIPauseController
{
	public Image ProfileIcon;

    public GameObject m_timer_check;
    public GameObject m_hint_check;


    void OnEnable()
	{
		transform.SetAsLastSibling ();

		if(ProfileIcon != null) {
			Sprite sp = UsersController.Instance.CurrentProfileSprite;
			if (sp != null) {
				ProfileIcon.sprite = sp;
			}
		}

		ChangeMusicButtonColor ();
		ChangeSoundButtonColor ();
        CheckTimerSettings();
        CheckHintSettings();

    }
    
    public void CheckTimerSettings()
    {
        bool timer_disabled = UserInfo.Instance.IsTimerDisabled();

        m_timer_check.SetActive(!timer_disabled);
        
    }

    public void CheckHintSettings()
    {
        bool hint_disabled = UserInfo.Instance.IsHintDisabled();

        m_hint_check.SetActive(!hint_disabled);
    }

    public void Button_Timer_Check()
    {
        if (m_timer_check.activeSelf)
        {
            m_timer_check.SetActive(false);
            UserInfo.Instance.SetTimerDisabled(true);
        }
        else
        {
            m_timer_check.SetActive(true);
            UserInfo.Instance.SetTimerDisabled(false);
        }

    }

    public void Button_Hint_Check()
    {
        if (m_hint_check.activeSelf)
        {
            m_hint_check.SetActive(false);
            UserInfo.Instance.SetHintDisabled(true);
        }
        else
        {
            m_hint_check.SetActive(true);
            UserInfo.Instance.SetHintDisabled(false);
        }
    }

}
