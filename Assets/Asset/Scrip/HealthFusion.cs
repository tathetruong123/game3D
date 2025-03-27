using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class HealthFusion : NetworkBehaviour
{
    [Header("Cấu hình HP")]
    public float maxHP;

    // Biến CurrentHP được đồng bộ hoá qua mạng; khi thay đổi sẽ gọi hàm OnHealthChanged
    [Networked, OnChangedRender(nameof(UpdateHealthUI))]
    public float CurrentHP { get; set; }

    [Header("UI")]
    public Slider sliderHP;

    // Hàm được gọi ngay sau khi object được spawn từ Fusion
    public override void Spawned()
    {
        // Chỉ state authority mới khởi tạo lại CurrentHP
        if (Object.HasStateAuthority)
        {
            CurrentHP = maxHP;
        }
        if (sliderHP != null)
        {
            sliderHP.maxValue = maxHP;
            sliderHP.value = CurrentHP;
        }
    }

    /// <summary>
    /// Hàm nhận damage, chỉ được thực hiện bởi state authority.
    /// </summary>
    /// <param name="damage">Lượng damage nhận vào</param>
    public void TakeDamage(float damage)
    {
        // Chỉ state authority mới cập nhật giá trị sức khỏe
        if (!Object.HasStateAuthority)
            return;

        CurrentHP = Mathf.Max(0, CurrentHP - damage);

        // Khi sức khỏe bằng 0, gọi hàm Die
        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Callback được gọi khi giá trị CurrentHP thay đổi.
    /// Nó đảm bảo rằng UI được cập nhật trên tất cả các client.
    /// </summary>
    //private static void OnHealthChanged()
    //{
    //    changed.Behaviour.UpdateHealthUI();
    //}

    /// <summary>
    /// Cập nhật UI slider dựa trên giá trị CurrentHP đồng bộ.
    /// </summary>
    private void UpdateHealthUI()
    {
        if (sliderHP != null)
        {
            sliderHP.value = CurrentHP;
        }
    }

    /// <summary>
    /// Xử lý khi đối tượng mất hết HP.
    /// State authority sẽ thực hiện xoá (despawn) đối tượng khỏi network.
    /// </summary>
    private void Die()
    {
        Debug.Log("Zombie đã chết!");
        if (Object.HasStateAuthority)
        {
            // Có thể thêm delay bằng cách sử dụng coroutine hoặc Invoke nếu cần
            Runner.Despawn(Object);
        }
    }
}