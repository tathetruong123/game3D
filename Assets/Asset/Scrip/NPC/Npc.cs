using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class Npc : NetworkBehaviour
{
    public GameObject NPCPanel;
    public TextMeshProUGUI NPCTextContent;
    public string[] content;

    public Questitem questItem;
    public GameObject buttonTakeQuest;

    private PlayerQuest playerQuest;
    private Coroutine coroutine;

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
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }

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
            foreach (char c in line)
            {
                NPCTextContent.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        buttonTakeQuest.SetActive(true);
    }

    public void SkipContent()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    public void TakeQuest()
    {
        if (playerQuest != null)
        {
            RPC_TakeQuest(playerQuest.Object.InputAuthority);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_TakeQuest(PlayerRef playerRef)
    {
        if (playerQuest != null && playerQuest.Object.InputAuthority == playerRef)
        {
            playerQuest.TakeQuest(questItem);
            buttonTakeQuest.SetActive(false);
            NPCPanel.SetActive(false);
        }
    }
}
