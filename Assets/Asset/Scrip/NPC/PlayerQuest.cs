using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public List<Questitem> questItems = new List<Questitem>();

    public PlayerQuestPanel playerQuestRanel;

    // Tham chiếu đến UI Panel Victory
    public GameObject victoryPanel;

    public void TakeQuest(Questitem questItem)
    {
        var check = questItems.FirstOrDefault(x => x.QuestItemName == questItem.QuestItemName);
        if (check == null)
            questItems.Add(questItem);
        Debug.Log("Nhận nhiệm vụ: " + questItem.QuestItemName);
        playerQuestRanel.ShowAllQuestItems(questItems);
    }

    public void UpdateQuestProgress(string questName, int amount)
    {
        // Tìm nhiệm vụ trong danh sách
        var quest = questItems.FirstOrDefault(x => x.QuestItemName == questName);
        if (quest != null)
        {
            quest.CurrentAmount += amount; // Cộng thêm số lượng

            // Giới hạn CurrentAmount không vượt quá QuestTargetAmount
            if (quest.CurrentAmount > quest.QuestTargetAmount)
                quest.CurrentAmount = quest.QuestTargetAmount;

            // Hiển thị danh sách nhiệm vụ
            playerQuestRanel.ShowAllQuestItems(questItems);

            // Kiểm tra nếu hoàn thành nhiệm vụ
            if (quest.CurrentAmount >= quest.QuestTargetAmount)
            {
                Debug.Log($"Nhiệm vụ '{quest.QuestItemName}' đã hoàn thành!");
                CompleteQuest(quest);
            }
        }
        else
        {
            Debug.LogError($"Nhiệm vụ với tên '{questName}' không tồn tại trong danh sách!");
        }
    }

    private void CompleteQuest(Questitem quest)
    {
        Debug.Log($"Victory: {quest.QuestItemName} đã hoàn thành!");
        questItems.Remove(quest);

        playerQuestRanel.ShowAllQuestItems(questItems);

        
    }


    void Start()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false); // Ẩn bảng Victory khi bắt đầu
        }
    }
    void Update()
    {
        // Kiểm tra khi nhấn phím N
        if (Input.GetKeyDown(KeyCode.N))
        {
            ShowVictoryPanel();
        }
    }
    // Hàm hiện bảng Victory
    public void ShowVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true); // Hiển thị bảng Victory
            Debug.Log("Hiển thị bảng Victory!");
            Time.timeScale = 0f; // Tạm dừng trò chơi
        }
    }

    // Hàm ẩn bảng Victory (nếu cần)
    public void HideVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false); // Ẩn bảng Victory
        }
    }
}
