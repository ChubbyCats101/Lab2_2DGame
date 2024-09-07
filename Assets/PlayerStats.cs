using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private Animator animator;
    private bool canTakeDamage = true;
    public Text gameOverText;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        health = maxHealth;

        gameOverText.gameObject.SetActive(false);
    }
    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) { return; }

        health -= damage;
        animator.SetBool("Damage", true);
        Debug.Log("Player health " + health);
        if (health <= 0)
        { 
            GetComponentInParent<GatherInput>().DisableControls();
            animator.SetBool("Dead", true);// เปลี่ยนเป็น Dead แทน Death
            animator.SetBool("Damage", false);
            Debug.Log("Player is dead");

            ShowGameOver();
        }
        StartCoroutine(DamagePrevention());
    }
    private IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);  // เพิ่มระยะเวลาเพื่อให้ Damage เล่นครบ

        if (health > 0)
        {
            canTakeDamage = true;
            animator.SetBool("Damage", false);  // กลับไป Idle หลังจาก hurt เล่นจบ
        }
        
    }
    private void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);  // แสดงข้อความ Game Over
    }
}