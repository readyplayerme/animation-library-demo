using System.Collections;
using ReadyPlayerMe.Core;
using UnityEngine;

public class DanceController : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController  animatorControllerF;
    [SerializeField] private RuntimeAnimatorController  animatorControllerM;
    private Animator animator;
    private float currentClipLength;
    private int currentDanceIndex;

    private void Start()
    {
        AvatarData avatarData = GetComponent<AvatarData>();
        animator = GetComponent<Animator>();
        if (avatarData.AvatarMetadata.OutfitGender == OutfitGender.Masculine)
        {
            animator.runtimeAnimatorController  = animatorControllerM;
        }
        else 
        {
            animator.runtimeAnimatorController  = animatorControllerF;
        }
        StartCoroutine(StartDancing());
    }

    private IEnumerator StartDancing()
    {
        currentDanceIndex = Random.Range(0, 10);
        animator.SetInteger("DanceIndex", currentDanceIndex);
        animator.SetTrigger("Dance");
        //We wait 1 second just to make sure the Animator finished transitioning from one state to another
        yield return new WaitForSeconds(1);
        StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        currentClipLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(currentClipLength);
        //Once the current dance animation finishes, we play another dance again
        StartCoroutine(StartDancing());
    }
}