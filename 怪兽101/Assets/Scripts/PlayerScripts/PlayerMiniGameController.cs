using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMiniGameController : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    public MiniGameLandmark landmark;
    private float currentCooldown = 0.0f;
    public bool canAttack = true;

    public TextMeshProUGUI text;

    Animator anim;

    private void Start()
    {
        landmark = GameObject.FindGameObjectWithTag("Landmark").GetComponent<MiniGameLandmark>();
        anim = GetComponent<Animator>();
        anim.SetBool("idle", true);
    }

    public void TriggerAttack()
    {
        anim.SetBool("idle", false);
        anim.SetBool("attack", true);
    }

    public void Attack()
    {
        if(landmark != null)
        {
            landmark.TakeDamage(playerData.attackDamage);
        }
        else { return; }
    }

    public void StopAttackAnimation()
    {
        anim.SetBool("attack", false);
        anim.SetBool("idle", true);
    }

    private void Update()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TriggerAttack();
                currentCooldown = playerData.attackSpeed;
                canAttack = false;
            }
        }

        else
        {
            text.faceColor = new Color32(255, 128, 0, 120);
            currentCooldown -= Time.deltaTime;

            if(currentCooldown <= 0.0f)
            {
                text.faceColor = new Color32(255, 128, 0, 255);
                canAttack = true;
            }
        }
    }
}
