using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class Inventory : NetworkBehaviour
{
    [Networked] public int currentHP { get; set; }  // HP hiện tại trong rương, được đồng bộ hóa
    [Networked] public int currentMP { get; set; }  // MP hiện tại trong rương, được đồng bộ hóa

    public TextMeshProUGUI hpText; // Hiển thị HP trong UI
    public TextMeshProUGUI mpText; // Hiển thị MP trong UI

    // Hàm cộng HP vào rương
    public void AddHP(int amount)
    {
        if (!Object.HasInputAuthority) return;
        RPC_AddHP(amount);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_AddHP(int amount)
    {
        currentHP += amount;
        UpdateUI();
    }

    // Hàm cộng MP vào rương
    public void AddMP(int amount)
    {
        if (!Object.HasInputAuthority) return;
        RPC_AddMP(amount);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_AddMP(int amount)
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