using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Questitem : NetworkBehaviour
{
    public string QuestItemName; // Tên của nhiệm vụ
    public int QuestTargetAmount; // Số lượng cần tìm
    [Networked] public int CurrentAmount { get; set; } // Số lượng hiện tại được đồng bộ hóa
    public string TargetItemTag;
}