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
        var quest = questItems.FirstOrDefault(x => x.QuestItemName == questName);
        if (quest != null)
        {
            quest.CurrentAmount += amount;
            if (quest.CurrentAmount > quest.QuestTargetAmount)
                quest.CurrentAmount = quest.QuestTargetAmount;

            playerQuestRanel.ShowAllQuestItems(questItems);

            if (quest.CurrentAmount >= quest.QuestTargetAmount)
            {
                Debug.Log($"Nhiệm vụ '{quest.QuestItemName}' hoàn thành!");
                CompleteQuest(quest);
            }
        }
    }

    private void CompleteQuest(Questitem quest)
    {
        Debug.Log($"Victory: {quest.QuestItemName} đã hoàn thành!");
        questItems.Remove(quest);

        playerQuestRanel.ShowAllQuestItems(questItems);

        // Hiển thị bảng Victory
        ShowVictoryPanel();
    }

    private void ShowVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Debug.Log("Bảng Victory đã được hiển thị!");
        }
        else
        {
            Debug.LogWarning("Victory Panel chưa được gán trong Inspector!");
        }
    }
}
