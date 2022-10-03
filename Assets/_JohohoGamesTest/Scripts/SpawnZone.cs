using UnityEngine;

public class SpawnZone : MonoBehaviour
{
	[SerializeField] private float _radius;

	public Vector3 SpawnPoint
	{
		get
		{
			var point = RandomCircle(transform.position, _radius);
			return point;
		}
	}
	Vector3 RandomCircle(Vector3 center, float radius)
	{
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

	private void OnDrawGizmos()
    {
		Gizmos.DrawWireSphere(transform.position, _radius);
    }
}