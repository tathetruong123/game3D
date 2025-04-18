using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ChatSystem : NetworkBehaviour
{
    public TextMeshProUGUI textMessage;
    public TMP_InputField inputFieldMessage;
    public Button buttonSend;

    public override void Spawned()
    {
        // Gọi Coroutine để đợi UI được khởi tạo đầy đủ
        StartCoroutine(InitializeChatUI());
    }

    private IEnumerator InitializeChatUI()
    {
        yield return new WaitForSeconds(0.2f); // Đợi 1 chút cho UI load xong

        // Tìm UI components trong scene
        var textObj = GameObject.Find("Text Massage");
        var inputObj = GameObject.Find("InputField Message");
        var buttonObj = GameObject.Find("Button Send");

        // Kiểm tra null và gán
        if (textObj != null && inputObj != null && buttonObj != null)
        {
            textMessage = textObj.GetComponent<TextMeshProUGUI>();
            inputFieldMessage = inputObj.GetComponent<TMP_InputField>();
            buttonSend = buttonObj.GetComponent<Button>();

            // Gán sự kiện nút bấm
            buttonSend.onClick.AddListener(SendMessageChat);
        }
        else
        {
            Debug.LogError("Không thể tìm thấy UI. Kiểm tra lại tên GameObject hoặc xem có bị tắt không!");
        }
    }

    public void SendMessageChat()
    {
        if (inputFieldMessage == null) return;

        var message = inputFieldMessage.text;
        if (string.IsNullOrWhiteSpace(message)) return;

        var id = Runner.LocalPlayer.PlayerId;
        var text = $"Player {id}: {message}";
        RpcNotifi(text);
        inputFieldMessage.text = "";
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcNotifi(string msg)
    {
        if (textMessage != null)
            textMessage.text += msg + "\n";
    }
}
