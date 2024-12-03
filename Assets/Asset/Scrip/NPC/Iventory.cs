using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Iventory : MonoBehaviour
{
    public int currentHPAmount = 0; // Số lượng HP trong rương
    public int currentMPAmount = 0; // Số lượng MP trong rương

    public TextMeshProUGUI hpText; // Tham chiếu đến TextMeshProUGUI để hiển thị số lượng HP
    public TextMeshProUGUI mpText; // Tham chiếu đến TextMeshProUGUI để hiển thị số lượng MP

    // Hàm này sẽ được gọi khi người chơi nhặt HP hoặc MP
    public void AddHP(int amount)
    {
        currentHPAmount += amount; // Cộng HP vào số lượng hiện tại
        UpdateUI(); // Cập nhật UI sau khi thay đổi HP
    }

    public void AddMP(int amount)
    {
        currentMPAmount += amount; // Cộng MP vào số lượng hiện tại
        UpdateUI(); // Cập nhật UI sau khi thay đổi MP
    }

    // Hàm cập nhật UI để hiển thị số lượng HP và MP
    private void UpdateUI()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHPAmount;
        }

        if (mpText != null)
        {
            mpText.text = "MP: " + currentMPAmount;
        }
    }
}
