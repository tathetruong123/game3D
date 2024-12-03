using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Katana katana; // Gắn Katana từ Inspector
    public BoxCollider katanaBox;

    private void Awake()
    {
        katanaBox.enabled = false;
    }
    public void OnAttack()
    {
        katanaBox.enabled = true;
    }

    public void EndAttack()
    {
        katanaBox.enabled = false;
    }

}
