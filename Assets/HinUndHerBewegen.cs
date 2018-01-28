using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class HinUndHerBewegen : MonoBehaviour
{
    [SerializeField]
    private float Zeitraum;
    [SerializeField]
    private float Wartezeit;
    //[SerializeField]
    private float Threshold = 0.05f;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private bool useAlternativePoints = false;
    [SerializeField]
    private Vector3 alternativerAnfang;
    [SerializeField]
    private Vector3 alternativesEnde;

    private Vector3 beginning, end;
    private bool toBeginning = true;
    private Transform _transform;
    private Rigidbody2D _rb;

    private float currentLerptime = 0f;
    private float delayTimer = 0f;
    private bool delaying = false;
    private SpriteRenderer sprite;

    void Awake()
    {
        if (!useAlternativePoints)
        {
            Vector3 beginningT = transform.Find("Anfang").position;
            beginning = new Vector3(beginningT.x, beginningT.y, beginningT.z);

            Vector3 endT = transform.Find("Ende").position;
            end = new Vector3(endT.x, endT.y, endT.z);
        }
        else
        {
            beginning = transform.position + alternativerAnfang;
            end = transform.position + alternativesEnde;
        }

        _transform = GetComponent<Transform>();
        Assert.IsNotNull<Transform>(_transform);
        _rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull<Rigidbody2D>(_rb);

        _rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        currentLerptime += Time.deltaTime;
        if (currentLerptime > Zeitraum)
            currentLerptime = Zeitraum;

        float t = currentLerptime / Zeitraum;
        t = curve.Evaluate(t);

        if (toBeginning)
        {
            _rb.MovePosition(Vector3.Lerp(end, beginning, t));

        }
        else
        {
            _rb.MovePosition(Vector3.Lerp(beginning, end, t));
        }

        if ((1f - t) < Threshold)
        {
            delaying = true;
        }

        if (delaying)
        {
            delayTimer += Time.deltaTime;

            if (toBeginning)
            {
                if (delayTimer > Wartezeit)
                {
                    ResetTimer();
                }
            }
            else
            {
                if (delayTimer > Wartezeit)
                {
                    ResetTimer();
                }
            }
        }
    }

    void ResetTimer()
    {
        toBeginning = !toBeginning;
        currentLerptime = 0f;
        delaying = false;
        delayTimer = 0f;
        Flip();
    }

    void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
}