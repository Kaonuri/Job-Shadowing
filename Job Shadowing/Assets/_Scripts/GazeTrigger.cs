using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class GazeTrigger : MonoBehaviour
{
    [SerializeField] private float gazeDuration;
    [SerializeField] private Color gizmoColor = Color.green;

    private float gazeTimer = 0f;

    public GameObject sphere;

    public UnityEvent OnGazeStart;
    public UnityEvent OnGazeCancel;
    public UnityEvent OnGazeComplete;

    public bool Gazing { set; get; }
    public bool Interacted  { set; get;}
    public float SphereRadius { set; get; }

    private void Awake()
    {        
        Gazing = false;
        Interacted = false;
        SphereRadius = sphere.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2.0f * sphere.transform.localScale.x;
    }

    private void Update()
    {
        if (Gazing)
        {
            if (gazeTimer == 0f)
            {
                VRManager.Instance.SelectionRadial.Show();

                if (OnGazeStart != null)
                    OnGazeStart.Invoke();
            }

            gazeTimer += Time.deltaTime;
            VRManager.Instance.SelectionRadial.selectionImage.fillAmount = gazeTimer / gazeDuration;

            if (gazeTimer >= gazeDuration)
            {                
                gazeTimer = 0;
                Interacted = true;
                Gazing = false;

                VRManager.Instance.SelectionRadial.Hide();

                print("Gaze Complete");

                if (OnGazeComplete != null)
                    OnGazeComplete.Invoke();
            }                        
        }
        else
        {
            if (gazeTimer > 0f)
            {
                print("gaze Cancel");
                if (OnGazeCancel != null)
                    OnGazeCancel.Invoke();
                gazeTimer = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, GetComponent<SphereCollider>().radius);
    }

    public bool IsOnSphere()
    {
        float distance = Vector3.Distance(transform.position, sphere.transform.position);
        if (distance == SphereRadius)
        {
            return true;
        }
        return false;
    }

    public void SetOnSphere()
    {
        transform.position = (transform.position - sphere.transform.position).normalized * SphereRadius;
    }
}
