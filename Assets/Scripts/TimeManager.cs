using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [SerializeField] private float realSecondsPerHour = 20f;

    [SerializeField] private int startHour = 8;
    [SerializeField] private int closeHour = 16;

    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private GameObject dayEndPanel;
    [SerializeField] private TextMeshProUGUI statsText;

    private float timer = 0f;
    private int currentHour;
    private bool isDayEnded = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHour = startHour;
        dayEndPanel.SetActive(false);
        UpdateClockUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDayEnded) return; 
        timer += Time.deltaTime;

        if (timer >= realSecondsPerHour)
        {
            timer = 0f;
            currentHour++;
            UpdateClockUI();
            if (currentHour >= closeHour)
            {
                EndDay();
            }
        }
    }

    private void UpdateClockUI()
    {
        if (clockText != null)
        {
            clockText.text = currentHour.ToString("00")+":00";
        }
    }

    private void EndDay()
    {
        isDayEnded = true;
        OrderManager.Instance.isStoreOpen = false;
        int totalBenar = OrderManager.Instance.correctOrdersCount;
        int totalUang = MoneyManager.Instance.GetCurrentMoney();
        statsText.text = "Good job!\n\n" + "Total Correct Orders: " + totalBenar + "\n" 
            + "Total Money Earned: " + totalUang;
        dayEndPanel.SetActive(true);
    }
}
