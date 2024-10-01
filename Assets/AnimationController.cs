using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator; // Animation Á¦¾î
    [SerializeField] PlayerController playerController;
    [SerializeField] private float score;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        score = playerController.score;
        if (score > 35000)
        {
            animator.speed = 0.2f;
        }
        else if (score > 20000)
        {
            animator.speed = 0.4f;
        }
        else if (score > 10000)
        {
            animator.speed = 0.6f;
        }
        else if (score > 8000)
        {
            animator.speed = 0.8f;
        }
    }
}
