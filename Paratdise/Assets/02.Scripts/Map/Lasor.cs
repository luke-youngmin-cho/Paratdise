using UnityEngine;

public class Lasor : MonoBehaviour
{
    public float speed = 10f;

    private void OnEnable()
    {
        if (Player.instance != null)
        {
          
            Vector3 dir = Player.instance.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.localScale = new Vector3(1f, 0.05f, 1f);
        }
    }

    private void Update()
    {
        transform.localScale += Vector3.right * speed * Time.deltaTime;

        if (transform.localScale.x > MapCreater.mapHeight * 2)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.instance.Hurt(10f);
        }
    }

}