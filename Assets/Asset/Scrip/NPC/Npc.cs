using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject NPCPanel;//tham chiếu đến panel
    public TextMeshProUGUI NPCTextContent;//tham chiếu đến text content
    public string[] content;// nội dung của NPC

    //nhiệm vụ của NPC
    public Questitem questItem;

    //player
    public PlayerQuest playerQuest;

    public GameObject buttonTakeQuest;

    Coroutine coroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerQuest = other.gameObject.GetComponent<PlayerQuest>();
            NPCPanel.SetActive(true);
            coroutine = StartCoroutine(ReadContent());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            NPCPanel.SetActive(false);
            StopCoroutine(coroutine);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        NPCPanel.SetActive(false);
        NPCTextContent.text = "";
        buttonTakeQuest.SetActive(false);
    }

    IEnumerator ReadContent()
    {

        foreach (var line in content)
        {
            NPCTextContent.text = "";
            for (int i = 0; i < line.Length; i++)
            {
                NPCTextContent.text += line[i];
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
        }
        buttonTakeQuest.SetActive(true);
    }
    public void SkipContent()
    {
        StopCoroutine(coroutine);// hiện nút nhận nhiệm vụ
    }

    public void TakeQuest()
    {
        if (playerQuest != null)
        {
            playerQuest.TakeQuest(questItem);
            buttonTakeQuest.SetActive(false);
            NPCPanel.SetActive(false);
        }
    }

}
