using Scripts.Arkanoid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//This gives powerups to the bricks and manage them
public class BricksManager : MonoBehaviour
{
    [SerializeField] PowerUpController[] powerUpPrefabs;
    public List<GameObject> brickGOs = new List<GameObject>();
    Dictionary<PowerUpType, PowerUpController> typeToPowerUp;
    WorldManagerForBricks worldManager;

    int destroyedBricks = 0;
    int powerUpIndex;
    int numberofPowerUpsToAdd = 3;


    private void Awake()
    {
        worldManager = GetComponent<WorldManagerForBricks>();
        BricksSetUp();
    }

    void BricksSetUp()
    {
        //find all bricks GOs 
        GameObject[] bricksGOsAux = GameObject.FindGameObjectsWithTag("Brick");
        //converts the array to a list using Linq library
        brickGOs = bricksGOsAux.ToList();
        int bricksWithoutPowerUp = 0;
        foreach (GameObject brickGO in brickGOs)
        {
            BrickController brickGOController = brickGO.GetComponent<BrickController>();
            brickGOController.OnBrickDestroyedEvent += OnBrickDestroyedCallBack;
            if (!brickGOController.HasPowerUp())
            {
                bricksWithoutPowerUp++;
            }
        }
        numberofPowerUpsToAdd = Mathf.Min(bricksWithoutPowerUp, numberofPowerUpsToAdd);
        CreatePowerUps();

    }
    void CreatePowerUps()
    {
        //implementing a dictionary
        typeToPowerUp = new Dictionary<PowerUpType, PowerUpController>();
        foreach (PowerUpController powerUp in powerUpPrefabs)
        {
            typeToPowerUp.Add(powerUp.GetPowerUpType(), powerUp);
        }

        while (numberofPowerUpsToAdd > 0)
        {
            int brickIndex = Random.Range(0, brickGOs.Count);
            BrickController randomBrickController = brickGOs[brickIndex].GetComponent<BrickController>();
            if (!randomBrickController.HasPowerUp())
            {
                int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
                PowerUpType newPowerUp = powerUpPrefabs[powerUpIndex].GetPowerUpType();
                randomBrickController.SetPowerUp(newPowerUp);
                numberofPowerUpsToAdd--;
            }
        }


    }
    void OnBrickDestroyedCallBack(BrickController brickController)
    {
        worldManager.AddScore(brickController.GetScore());
        if (brickController.HasPowerUp())
        {
            switch (brickController.GetPowerupType())
            {
                case PowerUpType.Enlarged:
                    break;
                case PowerUpType.Catch:
                    break;
                case PowerUpType.Laser:
                    break;
                case PowerUpType.Disruption:
                    break;
                case PowerUpType.Player:
                    break;
                case PowerUpType.Break:
                    break;
                case PowerUpType.Slow:
                    break;
                case PowerUpType.Enemy:
                    break;
            }

            PowerUpController powerUpController = Instantiate(typeToPowerUp[brickController.GetPowerupType()], brickController.transform.position, brickController.transform.rotation);
            powerUpController.OnPowerUpActivatedEvent += worldManager.OnPowerUpActivatedCallBack;
            if (powerUpController.GetPowerUpType() == PowerUpType.Enemy)
            {
                powerUpController.GetComponent<PowerUpEnemy>().OnVausGethitEvent += worldManager.OnVausGetHitCallback;
                StartCoroutine(ActivateBrickEnemy(powerUpController));
            }
        }

        destroyedBricks++;
        if (destroyedBricks == brickGOs.Count)
        {
            Debug.Log("win");
            worldManager.NextLevel();
        }
        brickController.gameObject.SetActive(false);


    }
    
    IEnumerator ActivateBrickEnemy(PowerUpController enemy)
    {
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<CapsuleCollider2D>().enabled = true;


    }
}
