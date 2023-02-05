using UnityEngine;

public class GeneratorRing : MonoBehaviour
{
	public RotateObject rotateObject;
	public Renderer ringRender;
	public float acceleration = 5;
	public float initialSpeed = 5;
	private float _targetSpeed;

	private void Start()
	{
		SetSpeed(initialSpeed);
	}

	public void SetSpeed(float speed)
	{
		_targetSpeed = speed;
	}

	private void Update()
	{
		rotateObject.speed = Mathf.Lerp(rotateObject.speed, _targetSpeed, Time.deltaTime * acceleration);
	}
}

