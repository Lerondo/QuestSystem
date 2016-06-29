using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    
    private float drawStrenght;
    private int damage;
	[SerializeField] private GameObject prefab;
    public float setDrawStrenght
    {
        set { drawStrenght = value; }
    }
    public int setDamage
    {
        set { damage = value; }
    }
    void Start()
    {
        Fly();
    }
	void Update()
	{
		GameObject.Instantiate(prefab, this.transform.position, Quaternion.FromToRotation(transform.parent.position, prefab.transform.position));
	}
    void Fly()
    {
        //Ray ray;
        //RaycastHit hit;
		GetComponent<Rigidbody>().AddForce(Vector3.forward * drawStrenght, ForceMode.Impulse);
		//GetComponent<Rigidbody>().force
    }
    void OnColliderEnter(Collider other)
    {
		HitTarget(other);
    }
	private void HitTarget(Collider other)
	{
		transform.SetParent(other.gameObject.transform);

		Destroy(GetComponent<Rigidbody>());
		Destroy(GetComponent<SphereCollider>());
		if (other.gameObject.tag == Tags.Enemy)
		{
			other.GetComponent<Health>().TakeDamage(damage);
		}
	}
}