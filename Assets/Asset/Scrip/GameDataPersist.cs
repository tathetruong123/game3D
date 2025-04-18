using System;
using UnityEngine;


    public class GameDataPersist
    {
        public static void Save1(PlayerData playerData)
        {
            var json = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString("PlayerData", json);
        }

        public static PlayerData Load1()
        {
            var json = PlayerPrefs.GetString("PlayerData");
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }




// lớp chứa thông tin của người chơi
[Serializable]
public class PlayerData
{
    public string PlayerName; // tên người chơi
    public string PlayerClass; // lớp nhân vật
    public int Health; // máu của người chơi
    public float Speed; // tốc độ di chuyển của nhân vật
}
