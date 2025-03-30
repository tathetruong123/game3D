using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;

public class PlayerInfo : NetworkBehaviour
{
    public GameObject playerPanel; // Panel gốc
    public GameObject panelRightInfo; // Panel thông tin
    public GameObject panelRightInventory; // Panel thông tin inventory

    public string sceneToLoad = "Menu";

    // Hàm hiển thị thêm Panel Info
    public void ShowPanelInfo()
    {
        panelRightInfo.SetActive(true); // Hiển thị Panel Right Info
    }

    // Hàm hiển thị thêm Panel Inventory
    public void ShowPanelInventory()
    {
        panelRightInventory.SetActive(true); // Hiển thị Panel Right Info Inventory
    }

    // Hàm ẩn các panel con và quay lại Player Panel
    public void HideSubPanels()
    {
        panelRightInfo.SetActive(false); // Ẩn Panel Right Info
        panelRightInventory.SetActive(false); // Ẩn Panel Right Info Inventory
    }

    // Hàm thoát game (chỉ chủ phòng mới có quyền thực hiện)
    public void QuitGame()
    {
        if (Object.HasStateAuthority)
        {
            Debug.Log("Game Over!");
            RPC_QuitGame();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
