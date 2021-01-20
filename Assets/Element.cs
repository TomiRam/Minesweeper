using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    // Is this a mine?
    public bool mine;
    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly decide if it's a mine or not
        mine = Random.value < 0.15;

    }
    // Load another texture
    public void loadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }
    // Is it still covered?
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }
    void OnMouseUpAsButton()
    {
        // It's a mine
        if (mine)
        {
            // uncover all mines
            Playfield.uncoverMines();

            // game over
            print("you lose");
        }
        // It's not a mine
        else
        {
            // show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Playfield.adjacentMines(x, y));

            // uncover area without mines
            Playfield.FFuncover(x, y, new bool[Playfield.w, Playfield.h]);

            // find out if the game was won now
            if (Playfield.isFinished()) { print("you win"); }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
