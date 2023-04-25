using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform respawnPoint;

    private GameObject characterInstance;

    private void Start()
    {
        // Spawn the character at the respawn point
        characterInstance = Instantiate(characterPrefab, respawnPoint.position, Quaternion.identity);
    }

    public void RespawnCharacter()
    {
        // Move the character to the respawn point
        characterInstance.transform.position = respawnPoint.position;

           // Reset the character's health
        characterInstance.GetComponent<Stats>().Reset();

        // Activate the character game object
        characterInstance.SetActive(true);
    }
}