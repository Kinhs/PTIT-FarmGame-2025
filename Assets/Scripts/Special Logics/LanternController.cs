using UnityEngine;

public class LanternController : MonoBehaviour
{
    public GameObject lightSource;

    private void Update()
    {
        if (lightSource.activeSelf == false && TimeController.instance.shouldEnableLight)
        {
            lightSource.SetActive(true);
        }
        else if (lightSource.activeSelf == true && !TimeController.instance.shouldEnableLight)
        {
            lightSource.SetActive(false);
        }
    }
}
