using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType { HP, MP } // Loại vật phẩm
    public ItemType itemType; // Loại vật phẩm (HP hoặc MP)
    public int amount = 1; // Số lượng HP hoặc MP

    private Iventory inventory;

    void Start()
    {
        // Lấy Inventory của người chơi (có thể thêm qua Inspector nếu cần)
        inventory = GameObject.FindObjectOfType<Iventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with item.");

            if (inventory != null)
            {
                if (itemType == ItemType.HP)
                {
                    Debug.Log("Adding HP to inventory");
                    inventory.AddHP(amount); // Cộng HP vào rương
                }
                else if (itemType == ItemType.MP)
                {
                    Debug.Log("Adding MP to inventory");
                    inventory.AddMP(amount); // Cộng MP vào rương
                }

                Destroy(gameObject); // Xóa vật phẩm sau khi nhặt
                Debug.Log("Item destroyed.");
            }
            else
            {
                Debug.LogError("Inventory is not assigned!");
            }
        }
    }

}
