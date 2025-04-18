using UnityEngine;

public class VFXspam : MonoBehaviour
{
    public GameObject targetObject; // Gán GameObject muốn bật/tắt

    public float toggleInterval = 30f; // thời gian mỗi lần bật/tắt

    void Start()
    {
        StartCoroutine(ToggleRoutine());
    }

    System.Collections.IEnumerator ToggleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);

            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf); // đảo trạng thái
            }
        }
    }
}
