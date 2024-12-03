using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Iventory : MonoBehaviour
{
    public int currentHP = 0;  // HP hiện tại trong rương
    public int currentMP = 0;  // MP hiện tại trong rương

    public TextMeshProUGUI hpText; // Hiển thị HP trong UI
    public TextMeshProUGUI mpText; // Hiển thị MP trong UI

    // Hàm cộng HP vào rương
    public void AddHP(int amount)
    {
        currentHP += amount;
        UpdateUI();
    }

    // Hàm cộng MP vào rương
    public void AddMP(int amount)
    {
        currentMP += amount;
        UpdateUI();
    }

    // Hàm cập nhật UI
    void UpdateUI()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
        if (mpText != null)
        {
            mpText.text = "MP: " + currentMP;
        }
    }
}
