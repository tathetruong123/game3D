using UnityEngine;
using Fusion;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public NetworkObject PlayerPrefab; // Đổi kiểu từ GameObject -> NetworkObject

    public void PlayerJoined(PlayerRef player)
    {
        // Kiểm tra xem Runner đã được khởi tạo chưa
        if (Runner == null) return;

        // Chỉ spawn nếu đây là Local Player
        if (player == Runner.LocalPlayer)
        {
            Vector3 spawnPosition = new Vector3(215, 100, 385);

            // Spawn nhân vật
            NetworkObject playerNetworkObj = Runner.Spawn(
                PlayerPrefab,
                spawnPosition,
                Quaternion.identity,
                player
            );

            if (playerNetworkObj == null)
            {
                Debug.LogError("[Fusion] Không thể spawn nhân vật! Kiểm tra Network Prefab List.");
                return;
            }

            GameObject playerObj = playerNetworkObj.gameObject; // Lấy GameObject từ NetworkObject

            Debug.Log($"[Fusion] Player {player.ToString()} đã spawn tại {spawnPosition}");

            // Nếu `PlayerSetup` có phương thức SetupCamera() thì gọi, nếu không thì bỏ dòng này
            var playerSetup = playerObj.GetComponent<PlayerSetup>();
            if (playerSetup != null)
            {
                // playerSetup.SetupCamera(); // Kiểm tra nếu có
            }
        }
    }
}
