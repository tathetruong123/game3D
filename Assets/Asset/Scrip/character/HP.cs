using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public Slider healthBar; // Thanh máu

    public float maxMP;
    public float currentMP;
    public Slider mpBar; // Thanh MP

    public float sprintMPConsumptionRate = 10f; // Tốc độ tiêu hao MP khi chạy nhanh
    public float mpRegenRate = 20f; // Tốc độ hồi MP mỗi giây
    private bool isSprinting;

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        // Cập nhật thanh máu
        UpdateHealthBar();
    }

    private void Start()
    {
        // Khởi tạo HP và MP
        currentHP = maxHP;
        currentMP = maxMP;

        // Cập nhật thanh máu và thanh MP
        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = currentHP;
        }

        if (mpBar != null)
        {
            mpBar.maxValue = maxMP;
            mpBar.value = currentMP;
        }
    }

    private void Update()
    {
        // Kiểm tra trạng thái chạy nhanh
        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentMP > 0;

        if (isSprinting)
        {
            // Tiêu hao MP khi chạy nhanh
            ConsumeMP(Time.deltaTime * sprintMPConsumptionRate);
        }
        else
        {
            // Hồi MP khi không chạy nhanh
            RegenerateMP(Time.deltaTime * mpRegenRate);
        }
    }

    private void ConsumeMP(float amount)
    {
        currentMP -= amount;
        currentMP = Mathf.Max(0, currentMP); // Giữ giá trị không âm

        // Cập nhật thanh MP
        UpdateMPBar();
    }

    private void RegenerateMP(float amount)
    {
        currentMP += amount;
        currentMP = Mathf.Min(maxMP, currentMP); // Giữ giá trị không vượt quá maxMP

        // Cập nhật thanh MP
        UpdateMPBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHP;
        }
    }

    private void UpdateMPBar()
    {
        if (mpBar != null)
        {
            mpBar.value = currentMP;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            currentHP -= 1;
            healthBar.value = currentHP;
        }
    }
}
