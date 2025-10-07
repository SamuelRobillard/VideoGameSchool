using UnityEngine;

public class IsAttacking : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] AudioSource audioSource;
    public float cooldown = 0.5f;
    private float lastAttackedAt = -9999f;

    private bool isAttacking = false;

    void Update()
    {
        HandleAttack();
    }

    public void HandleAttack()
    {
        // si la touhe est présser et que le temps d'attente est respcter
        // on déclanche l'animartion d'attque et le son
        if (Input.GetKeyDown(KeyCode.X) && Time.time > lastAttackedAt + cooldown)
        {
            lastAttackedAt = Time.time;
            isAttacking = true;

            playerMovement.animator.SetBool("Attack", true);
            audioSource.PlayOneShot(playerMovement.sfxAttack);
        }
        // apres le delit on remet la variable a false
        if (isAttacking && Time.time > lastAttackedAt + cooldown)
        {
            isAttacking = false;
            
        }
        
    }

    // 👉 fonction pour d’autres scripts
    public bool IsAttackingNow()
    {
        return isAttacking;
    }
}
