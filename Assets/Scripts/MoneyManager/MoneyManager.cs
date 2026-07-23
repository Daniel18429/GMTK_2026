using UnityEngine;
using System;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public float Money;

    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }
    public static void AddMoney(float amount) => Instance.Money += amount;
    public static void SubtractMoney(float amount) => Instance.Money -= amount;
    public static bool HasMoney(float amount) => Instance.Money >= amount;

}