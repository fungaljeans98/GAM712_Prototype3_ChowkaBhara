using UnityEngine;

public class ExposureFadeIn : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string nameOfAnimation;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void FadeInFunction() {
            Debug.Log("Fade In");
            animator.SetTrigger("FadeIn");
    }

    public void FadeOutFunction() {
            Debug.Log("Fade Out");
            animator.SetTrigger("FadeOut");
    }
}
