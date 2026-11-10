using System;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI moneyText;

    private int currentMoney = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }
    
    public void DeductMoney(int amount)
    {
        currentMoney -= amount;
        if(currentMoney < 0) currentMoney = 0; 
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Uang: " + currentMoney.ToString();
        }
    }

    public int GetCurrentMoney()
    {
        return currentMoney;
    }
}
