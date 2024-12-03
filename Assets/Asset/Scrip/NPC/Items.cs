using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType { HP, MP }
    public ItemType itemType; // Loại vật phẩm (HP hoặc MP)
    public int amount; // Số lượng HP/MP cần thêm vào rương
    public Iventory inventory; // Rương (đối tượng chứa HP và MP)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Kiểm tra va chạm với đối tượng có tag "Player"
        {
            Debug.Log("Player collided with item.");

            // Kiểm tra xem nhân vật có Inventory hay không
            if (inventory != null)
            {
                // Nếu là vật phẩm HP, cộng vào lượng HP
                if (itemType == ItemType.HP)
                {
                    inventory.AddHP(amount);
                    Debug.Log("Added HP: " + amount);
                }
                // Nếu là vật phẩm MP, cộng vào lượng MP
                else if (itemType == ItemType.MP)
                {
                    inventory.AddMP(amount);
                    Debug.Log("Added MP: " + amount);
                }

                // Sau khi nhặt, vật phẩm sẽ bị xóa
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Inventory is not assigned!");
            }
        }
    }

}
