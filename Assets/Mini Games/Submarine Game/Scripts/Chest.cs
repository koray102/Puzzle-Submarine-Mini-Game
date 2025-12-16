using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour, IInteractable
{
    public Transform lidPivot;

    [Header("Lid Opennin Animation Settings")]
    public Vector3 openRotation = new Vector3(-70f, 0f, 0f);
    public float animationSpeed = 1.5f;

    private bool isOpen = false;

    public void OnInteract()
    {
        if (!isOpen)
        {
            OpenChest();
        }
    }


    void OpenChest()
    {
        isOpen = true;
        SubmarineManager.Instance.CheckIsGameDone();
        
        StartCoroutine(AnimateLid());
    }


    private IEnumerator AnimateLid()
    {
        Quaternion endRot = Quaternion.Euler(openRotation);

        // 0.1 esik degeri koydum cunku tam ulasamicak
        while (Quaternion.Angle(lidPivot.localRotation, endRot) > 0.1f)
        {
            lidPivot.localRotation = Quaternion.Lerp(lidPivot.localRotation, endRot, Time.deltaTime * animationSpeed);
            
            yield return null;
        }

        lidPivot.localRotation = endRot;
    }
}