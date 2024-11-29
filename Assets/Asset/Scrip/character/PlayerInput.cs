using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    public bool attackInput;
    public bool jumpInput;    // Nhảy
    public bool crouchInput;  // Cúi
    public bool laughInput;   // Cười
    public bool sprintInput;  // Chạy nhanh

    private void Update()
    {
        // Lấy giá trị đầu vào di chuyển
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Kiểm tra phím tấn công (chuột trái)
        if (!attackInput && Time.timeScale != 0)
        {
            attackInput = Input.GetMouseButtonDown(0);
        }

        // Kiểm tra phím nhảy
        if (!jumpInput && Time.timeScale != 0)
        {
            jumpInput = Input.GetKeyDown(KeyCode.J); // Nhấn phím J
        }

        // Kiểm tra phím cúi
        crouchInput = Input.GetKey(KeyCode.C); // Giữ phím C để cúi

        // Kiểm tra phím cười
        if (!laughInput && Time.timeScale != 0)
        {
            laughInput = Input.GetKeyDown(KeyCode.X); // Nhấn phím X
        }

        // Kiểm tra phím chạy nhanh
        sprintInput = Input.GetKey(KeyCode.LeftShift); // Giữ phím Shift để chạy nhanh
    }

    private void OnDisable()
    {
        horizontalInput = 0;
        verticalInput = 0;
        attackInput = false;
        jumpInput = false;
        laughInput = false;
        crouchInput = false;
        sprintInput = false;
    }
}
