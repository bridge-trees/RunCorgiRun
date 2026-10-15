using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corgi : MonoBehaviour
{
    public UI UI;
    private SpriteRenderer corgiSpriteRenderer;
    public Sprite DrunkSprite;
    public Sprite SoberSprite;

    private bool isDrunk = false;
    private bool isPlastered = false;

    private int randomMoveCounter = 120;
    private int lastRandomDirection = 0;
    
    private Coroutine soberUpCoroutine;

    public void Awake()
    {
        corgiSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (isPlastered)
        {
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        int direction = lastRandomDirection;
        
        if (randomMoveCounter == 0)
        {
            direction = Random.Range(0, 4);
            lastRandomDirection = direction;
            randomMoveCounter = Random.Range(60, 240);
        }
        
        switch (direction)
        {
            case 0:
                Move(new Vector2(1, 0));
                break;
            case 1:
                Move(new Vector2(-1, 0));
                break;
            case 2:
                Move(new Vector2(0, 1));
                break;
            case 3:
                Move(new Vector2(0, -1));
                break;
        }
        randomMoveCounter--;
    }

    public void MoveManually(Vector2 direction)
    {
        if (isPlastered == false)
        {
            Move(direction);
        }
    }

    public void Move(Vector2 direction)
    {
        direction = ApplyDrunkenness(direction);
        
        FaceCorrectDirection(direction);
        Vector2 movement = direction * GameParameters.CorgiMoveSpeed * Time.deltaTime;
        corgiSpriteRenderer.transform.Translate(movement);

        corgiSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(corgiSpriteRenderer);
    }

    private Vector2 ApplyDrunkenness(Vector2 direction)
    {
        if (isDrunk)
        {
            direction.x *= -1;
            direction.y *= -1;
        }
        return direction;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Moonshine"))
        {
            Destroy(other.gameObject);
            GetPlastered();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beer"))
        {
            Destroy(other.gameObject);
            GetDrunk();
        }

        if (other.gameObject.CompareTag("Pill"))
        {
            SoberUp();
        }
        
        if (other.gameObject.CompareTag("Bone"))
        {
            ScorePoint();
            Destroy(other.gameObject);
            print("score is: " + ScoreKeeper.GetScore());
        }
    }

    private void ScorePoint()
    {
        ScoreKeeper.AddPoint();
        UI.SetScoreText(ScoreKeeper.GetScore());
    }

    private void Inebriate()
    {
        ChangeToDrunkSprite();
        StartSoberingUp();
    }
    
    private void GetPlastered()
    {
        isPlastered = true;
        Inebriate();
    }

    private void GetDrunk()
    {
        isDrunk = true;
        Inebriate();
    }
    private void StartSoberingUp()
    {
        if (soberUpCoroutine != null)
        {
            StopCoroutine(soberUpCoroutine);
        }
        soberUpCoroutine = StartCoroutine(CountdownUntilSober());
    }

    IEnumerator CountdownUntilSober()
    {
        yield return new WaitForSeconds(GameParameters.CorgiDrunkSeconds);
        SoberUp();
    }

    private void SoberUp()
    {
        isDrunk = false;
        isPlastered = false;
        ChangeToSoberSprite();
    }

    private void ChangeToSoberSprite()
    {
        corgiSpriteRenderer.sprite = SoberSprite;
    }

    private void ChangeToDrunkSprite()
    {
        corgiSpriteRenderer.sprite = DrunkSprite;
    }

    public void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            corgiSpriteRenderer.flipX = false;
        } else if (direction.x < 0)
        {
            corgiSpriteRenderer.flipX = true;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
