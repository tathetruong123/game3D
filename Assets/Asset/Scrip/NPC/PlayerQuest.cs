using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public List<Questitem> questItems = new List<Questitem>();

    public PlayerQuestPanel playerQuestRanel;

    // Nhận nhiệm vụ
    public void TakeQuest(Questitem questItem)
    {
        // Kiểm tra xem nhiệm vụ đã tồn tại trong danh sách hay chưa
        var check = questItems.FirstOrDefault(x => x.QuestItemName == questItem.QuestItemName);
        if (check == null) // Nếu chưa có thì thêm vào danh sách
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

            // Kiểm tra nếu hoàn thành nhiệm vụ
            if (quest.CurrentAmount >= quest.QuestTargetAmount)
            {
                Debug.Log($"Nhiệm vụ '{quest.QuestItemName}' hoàn thành!");
                CompleteQuest(quest);
            }
        }
    }

    // Xử lý khi hoàn thành nhiệm vụ
    private void CompleteQuest(Questitem quest)
    {
        // Xử lý logic hoàn thành (ví dụ: trao thưởng, xóa nhiệm vụ...)
        Debug.Log($"Victory: {quest.QuestItemName} đã hoàn thành!");
        // Xóa nhiệm vụ khỏi danh sách nếu cần
        questItems.Remove(quest);

        // Cập nhật lại danh sách nhiệm vụ
        playerQuestRanel.ShowAllQuestItems(questItems);
    }
}
