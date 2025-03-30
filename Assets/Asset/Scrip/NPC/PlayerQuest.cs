using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Fusion;
using System.Globalization;

public class PlayerQuest : NetworkBehaviour
{
    public List<Questitem> questItems = new List<Questitem>();
    public PlayerQuestPanel playerQuestPanel;
    public GameObject victoryPanel;

    public void TakeQuest(Questitem questItem)
    {
        if (!Object.HasInputAuthority) return;
        RPC_TakeQuest(questItem);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_TakeQuest(Questitem questItem)
    {
        var check = questItems.FirstOrDefault(x => x.QuestItemName == questItem.QuestItemName);
        if (check == null)
            questItems.Add(questItem);
        Debug.Log("Nhận nhiệm vụ: " + questItem.QuestItemName);
        playerQuestPanel.ShowAllQuestItems(questItems);
    }

    public void UpdateQuestProgress(string questName, int amount)
    {
        if (!Object.HasInputAuthority) return;
        RPC_UpdateQuestProgress(questName, amount);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_UpdateQuestProgress(string questName, int amount)
    {
        var quest = questItems.FirstOrDefault(x => x.QuestItemName == questName);
        if (quest != null)
        {
            quest.CurrentAmount += amount;
            if (quest.CurrentAmount > quest.QuestTargetAmount)
                quest.CurrentAmount = quest.QuestTargetAmount;
            playerQuestPanel.ShowAllQuestItems(questItems);
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
        playerQuestPanel.ShowAllQuestItems(questItems);
    }

    void Start()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ShowVictoryPanel();
        }
    }

    public void ShowVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Debug.Log("Hiển thị bảng Victory!");
            Time.timeScale = 0f;
        }
    }

    public void HideVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }
}