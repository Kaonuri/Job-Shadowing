using UnityEngine;

public class VRManager : MonoBehaviour
{
    public VREyeRaycaster EyeRaycaster { private set; get; }
    public VRInputHandler InputHandler { private set; get; }
    public VRCameraFade CameraFade { private set; get; }
    public Reticle Reticle { private set; get; }
    public SelectionRadial SelectionRadial { private set; get; }

    public static VRManager Instance { private set; get; }

    private VRManager()
    {        
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        EyeRaycaster = GetComponent<VREyeRaycaster>();
        InputHandler = GetComponent<VRInputHandler>();
        CameraFade = GetComponent<VRCameraFade>();
        Reticle = GetComponent<Reticle>();
        SelectionRadial = GetComponent<SelectionRadial>();
    }
}
