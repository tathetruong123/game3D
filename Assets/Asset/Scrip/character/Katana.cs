using UnityEngine;

public class Katana : MonoBehaviour
{

    public float damage = 20f; // Lượng sát thương gây ra
    public bool isAttacking = false; // Trạng thái tấn công


    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking) // Kiểm tra nếu đang trong trạng thái tấn công
        {
            Health zombie = other.GetComponent<Health>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage); // Gây sát thương cho quái
            }
        }
    }


}
