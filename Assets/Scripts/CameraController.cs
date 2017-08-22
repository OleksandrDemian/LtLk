using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField][Range(1, 20)]
    private int speed = 10;
    private Transform target;
    private Vector3 offset = Vector3.zero;

    public static CameraController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        target = Player.Instance.transform;
        offset = transform.position - target.position;
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
