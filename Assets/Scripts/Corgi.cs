using UnityEngine;

public class Corgi : MonoBehaviour
{
    private SpriteRenderer corgiSpriteRenderer;

    public void Awake()
    {
        corgiSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 direction)
    {
        corgiSpriteRenderer.transform.Translate(direction);
    }

}
