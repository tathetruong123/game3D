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
}
