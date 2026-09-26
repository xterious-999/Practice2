using UnityEngine;

public sealed class SoapRotator : MonoBehaviour
{
	private enum RotationAxis
	{
		X,
		Y,
		Z
	}

	[SerializeField] private RotationAxis axis = RotationAxis.Y;
	[SerializeField] private float speed = 90f;

	private void Update()
	{
		Vector3 rotationAxis = axis switch
		{
			RotationAxis.X => Vector3.right,
			RotationAxis.Y => Vector3.up,
			RotationAxis.Z => Vector3.forward,
			_ => Vector3.up
		};

		transform.Rotate(rotationAxis, speed * Time.deltaTime, Space.Self);
	}
}