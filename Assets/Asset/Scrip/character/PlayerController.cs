using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Katana katana; // Gắn Katana từ Inspector

    void Update()
    {
        // Kiểm tra khi người chơi bấm nút tấn công
        if (Input.GetKeyDown(KeyCode.K)) // Click chuột trái
        {
            Attack();
        }
    }

    void Attack()
    {
        // Đặt trạng thái tấn công trong thời gian ngắn
        katana.isAttacking = true;
        Invoke(nameof(ResetAttack), 0.5f); // Reset sau 0.5 giây
    }

    void ResetAttack()
    {
        katana.isAttacking = false;
    }
}
