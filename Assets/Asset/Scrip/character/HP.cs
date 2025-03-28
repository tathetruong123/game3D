using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class HP : NetworkBehaviour
{
    [Networked] public float currentHP { get; set; } // Đồng bộ máu
    [Networked] public float currentMP { get; set; } // Đồng bộ mana

    public float maxHP = 100f;
    public float maxMP = 50f;
    public float sprintMPConsumptionRate = 10f;
    public float mpRegenRate = 2f;

    public Slider healthBar;
    public Slider mpBar;
    public TMP_Text healingPotionText;
    public TMP_Text manaPotionText;

    public AudioClip healSound;
    public AudioClip manaSound;
    private AudioSource audioSource;

    public int healingPotionCount = 20;
    public int manaPotionCount = 20;
    private bool isSprinting;

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            currentHP = maxHP;
            currentMP = maxMP;
        }

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

        audioSource = GetComponent<AudioSource>();
        UpdatePotionText();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentMP > 0;

        if (isSprinting)
        {
            RPC_ConsumeMP(Time.deltaTime * sprintMPConsumptionRate);
        }
        else
        {
            RPC_RegenerateMP(Time.deltaTime * mpRegenRate);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && healingPotionCount > 0)
        {
            RPC_Heal(20);
            healingPotionCount--;
            UpdatePotionText();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && manaPotionCount > 0)
        {
            RPC_RestoreMana(15);
            manaPotionCount--;
            UpdatePotionText();
        }

        if (currentHP <= 0)
        {
            Debug.Log("Nhân vật đã chết!");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        UpdateHealthBar();
        PlaySound(healSound);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_RestoreMana(float amount)
    {
        currentMP = Mathf.Min(currentMP + amount, maxMP);
        UpdateMPBar();
        PlaySound(manaSound);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_ConsumeMP(float amount)
    {
        currentMP = Mathf.Max(0, currentMP - amount);
        UpdateMPBar();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_RegenerateMP(float amount)
    {
        currentMP = Mathf.Min(maxMP, currentMP + amount);
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

    private void UpdatePotionText()
    {
        if (healingPotionText != null)
        {
            healingPotionText.text = healingPotionCount.ToString();
        }
        if (manaPotionText != null)
        {
            manaPotionText.text = manaPotionCount.ToString();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            RPC_TakeDamage(2);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(float damage)
    {
        currentHP = Mathf.Max(0, currentHP - damage);
        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Debug.Log("Nhân vật đã chết do bị tấn công!");
        }
    }
}
