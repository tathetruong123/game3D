using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false; // Biến trạng thái tạm dừng
    public GameObject PauseMenuCanva; // Giao diện menu tạm dừng

    // Khởi tạo
    void Start()
    {
        Time.timeScale = 1f; // Đảm bảo game bắt đầu ở trạng thái bình thường
    }

    // Gọi mỗi khung hình
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Kiểm tra phím "Escape"
        {
            if (Paused)
            {
                Play(); // Tiếp tục trò chơi
            }
            else
            {
                Stop(); // Tạm dừng trò chơi
            }
        }
    }

    // Phương thức để tạm dừng trò chơi
    void Stop()
    {
        PauseMenuCanva.SetActive(true); // Hiển thị giao diện menu
        Time.timeScale = 0f; // Dừng thời gian trong trò chơi
        Paused = true; // Cập nhật trạng thái
    }

    // Phương thức để tiếp tục trò chơi
    void Play()
    {
        PauseMenuCanva.SetActive(false); // Ẩn giao diện menu
        Time.timeScale = 1f; // Tiếp tục thời gian
        Paused = false; // Cập nhật trạng thái
    }

    // Chuyển về menu chính
    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Chuyển về màn trước đó
    }
}
