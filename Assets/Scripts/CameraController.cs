using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField][Range(1, 20)]
    private int speed = 10;
    private Transform target;
    private Vector3 offset = new Vector3(0, 3, -2.5f);

    public static CameraController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
	
	private void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
	}
}
