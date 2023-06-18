using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;

    private Vector3 InitPosition { get; set; }
    private Rigidbody2D Rigidbody { get; set; }

    private void Awake()
    {
        InitPosition = transform.position;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Rigidbody.AddForce(Vector2.up * 2.0f, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(Rigidbody.velocity.sqrMagnitude > 0)
            {
                return;
            }

            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var direction = endPosition - startPosition;
            Rigidbody.AddForce(direction.normalized * 12, ForceMode2D.Impulse);

            Invoke(nameof(ResetMe), 1.2f);
        }
    }

    private void ResetMe()
    {
        Rigidbody.velocity = Vector2.zero;
        Rigidbody.angularVelocity = 0;
        transform.position = InitPosition;

        Bucket.UpdatePositiion();
    }
}
