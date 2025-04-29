using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // array com os 9 prefabs
    public Vector2 startPosition = new Vector2(-8f, 0f);
    public float spacing = 2f;

    void Start()
    {
        SpawnCharacters();
    }

    void SpawnCharacters()
    {
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            Vector2 position = startPosition + new Vector2(i * spacing, 0);
            Instantiate(characterPrefabs[i], position, Quaternion.identity);
        }
    }
}
