using UnityEngine;

public class ItemAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator; // Animation Á¦¾î
    [SerializeField] PlayerController playerController;
    [SerializeField] private float score;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    private void Update()
    {
        score = playerController.score;
        if (score > 5000)
        {
            animator.speed = 0.2f;
        }
        else if (score > 4000)
        {
            animator.speed = 0.4f;
        }
        else if (score > 3000)
        {
            animator.speed = 0.6f;
        }
        else if (score > 2000)
        {
            animator.speed = 0.8f;
        }
    }
}
