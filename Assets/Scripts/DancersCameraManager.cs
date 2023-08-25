using System.Collections;
using Cinemachine;
using UnityEngine;

public class DancersCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] vcams;
    private int currentVcamIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchCamera());
    }

    private IEnumerator SwitchCamera()
    {
        while (true)
        {
            vcams[currentVcamIndex].MoveToTopOfPrioritySubqueue();
            //We wait 5 seconds before switching to another camera
            yield return new WaitForSeconds(5);
            currentVcamIndex++;
            if (currentVcamIndex > vcams.Length-1) currentVcamIndex = 0;
        }
    }
}