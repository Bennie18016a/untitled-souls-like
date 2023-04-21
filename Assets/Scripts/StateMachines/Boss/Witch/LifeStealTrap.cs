using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LifeStealTrap : MonoBehaviour
{
    public Slider spammingBarSlider;

    [SerializeField] private InputAction m_SpamAction;
    public bool m_IsTrapped = true;
    private float m_CurrentSpammingProgress = 0f;
    private float m_SpammingBarDecreaseRate = 0.2f;
    private float m_HealthDecreaseInterval = 2f;
    private float m_HealthDecreaseTimer = 0f;

    private void OnEnable()
    {
        m_CurrentSpammingProgress = 0;
    }

    private void Start()
    {
        m_SpamAction.performed += ctx => HandleSpamButton();
        spammingBarSlider.maxValue = 1f;
    }

    private void Update()
    {
        if (m_IsTrapped)
        {
            GameObject.Find("Player").GetComponent<PlayerStateMachine>().CanMove = false;
            m_SpamAction.Enable();
            spammingBarSlider.gameObject.SetActive(true);
            m_CurrentSpammingProgress -= m_SpammingBarDecreaseRate * Time.deltaTime;
            m_CurrentSpammingProgress = Mathf.Clamp01(m_CurrentSpammingProgress);
            spammingBarSlider.value = m_CurrentSpammingProgress;

            // Decrease player health every 2 seconds while trapped
            m_HealthDecreaseTimer += Time.deltaTime;
            if (m_HealthDecreaseTimer >= m_HealthDecreaseInterval)
            {
                // Resets the timer
                m_HealthDecreaseTimer = 0f;
                // Decrease player health by 25
                GameObject.Find("Player").GetComponent<Health>().DealDamage(25, true);
                // Add the witches health by 25
                GameObject.Find("Witch").GetComponent<Health>().AddHealth(25);
            }
        }
        else
        {
            spammingBarSlider.gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerStateMachine>().CanMove = true;
            m_SpamAction.Disable();
            this.enabled = false;
        }
    }



    private void HandleSpamButton()
    {
        m_CurrentSpammingProgress += 0.1f;

        if (m_CurrentSpammingProgress >= 1f)
        {
            m_IsTrapped = false;
            GameObject.Find("Player").GetComponent<PlayerStateMachine>().CanMove = true;
        }

        spammingBarSlider.value = m_CurrentSpammingProgress;
    }
}
