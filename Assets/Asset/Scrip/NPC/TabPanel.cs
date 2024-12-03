using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPanel : MonoBehaviour
{
    public GameObject playerPanel; // Panel sẽ hiển thị khi nhấn Tab
    private bool isPanelVisible = false; // Trạng thái hiển thị panel

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPanelVisible = !isPanelVisible; // Đảo ngược trạng thái hiển thị
            playerPanel.SetActive(isPanelVisible); // Bật hoặc tắt Player Panel
        }
    }
}
