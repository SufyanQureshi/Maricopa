﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Michsky.DreamOS
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Button))]
    public class SwitchManager : MonoBehaviour
    {
        // Events
        public UnityEvent OnEvents;
        public UnityEvent OffEvents;

        // Saving
        public bool saveValue = true;
        public string switchTag = "Switch";

        // Settings
        public bool isOn = true;
        public bool invokeAtStart = true;

        // Resources
        public Animator switchAnimator;
        public Button switchButton;

        void Start()
        {
            if (switchAnimator == null)
                switchAnimator = gameObject.GetComponent<Animator>();

            if (switchButton == null)
                switchButton = gameObject.GetComponent<Button>();

            switchButton.onClick.AddListener(AnimateSwitch);
            CheckForData();

            if (invokeAtStart == true && isOn == true)
                OnEvents.Invoke();
            else if (invokeAtStart == true && isOn == false)
                OffEvents.Invoke();
        }

        void OnEnable()
        {
            if (switchAnimator == null)
                return;

            CheckForData();
        }

        public void CheckForData()
        {
            if (saveValue == true)
            {
                if (PlayerPrefs.GetString(switchTag + "Switch") == "")
                {
                    if (isOn == true)
                    {
                        switchAnimator.Play("Switch On");
                        isOn = true;
                        PlayerPrefs.SetString(switchTag + "Switch", "true");
                    }

                    else
                    {
                        switchAnimator.Play("Switch Off");
                        isOn = false;
                        PlayerPrefs.SetString(switchTag + "Switch", "false");
                    }
                }

                else if (PlayerPrefs.GetString(switchTag + "Switch") == "true")
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else if (PlayerPrefs.GetString(switchTag + "Switch") == "false")
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            else
                UpdateUI();
        }

        public void UpdateUI()
        {
            if (isOn == true && switchAnimator != null && switchAnimator.gameObject.activeInHierarchy == true)
            {
                isOn = true;
                switchAnimator.Play("Switch On");
            }

            else if (isOn == false && switchAnimator != null && switchAnimator.gameObject.activeInHierarchy == true)
            {
                isOn = false;
                switchAnimator.Play("Switch Off");
            }
        }

        public void AnimateSwitch()
        {
            if (isOn == true)
            {
                switchAnimator.Play("Switch Off");
                isOn = false;
                OffEvents.Invoke();

                if (saveValue == true)
                    PlayerPrefs.SetString(switchTag + "Switch", "false");
            }

            else
            {
                switchAnimator.Play("Switch On");
                isOn = true;
                OnEvents.Invoke();

                if (saveValue == true)
                    PlayerPrefs.SetString(switchTag + "Switch", "true");
            }
        }
    }
}