using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TabPanel : NetworkBehaviour
{
    public GameObject playerPanel; // Panel sẽ hiển thị khi nhấn Tab
    [Networked] private bool isPanelVisible { get; set; } // Trạng thái hiển thị panel, được đồng bộ hóa

    void Update()
    {
        if (!HasInputAuthority) return;

        // Kiểm tra nếu người chơi nhấn phím Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            RPC_TogglePanel();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_TogglePanel()
    {
        isPanelVisible = !isPanelVisible; // Đảo ngược trạng thái hiển thị
        playerPanel.SetActive(isPanelVisible); // Bật hoặc tắt Player Panel
    }
}