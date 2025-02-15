﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    // configuration
    [SerializeField] public AudioClip destroyedBlockSound;
    [SerializeField] public float soundVolume = 0.05f;
    [SerializeField] public GameObject destroyedBlockParticlesVFX;
    [SerializeField] public int maxHits;
    [SerializeField] public Sprite[] damageSprites;

    // references to other objects
    private LevelController _levelController;
    private Vector3 _soundPosition;

    // state
    private int _currentHits = 0;

    [SerializeField] private float itemDropRate = 10f;
    [SerializeField] private List<GameObject> dropItems; 
    
    void Start()
    {
        // selects other game object without SCENE binding: programatically via API
        _levelController = FindObjectOfType<LevelController>();
        _soundPosition = FindObjectOfType<Camera>().transform.position;
        
    }
    
    /**
     * Destroys the block upon a collision. 
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!CompareTag("Breakable")) return;
        
        // increases number of hits and destroy it, if necessary
        _currentHits++;
            
        if (_currentHits < maxHits)
        {
            // Updates sprite image if block has taken too much damage
            UpdateSpriteIfTooDamaged();
        }
        else
        {
            DestroyItself();    
        }
    }
    
    /**
     * Updates the block damage sprite when necessary based on the amount of taken hits.
     */
    private void UpdateSpriteIfTooDamaged()
    {
        var ix = GetDamageSpriteIndex(_currentHits, maxHits, damageSprites.Length);

        gameObject.GetComponent<SpriteRenderer>().sprite = damageSprites[ix];
    }
    
    /**
     * Calculates the number of required hits to change to the next damage sprite and based on that,
     * returns the sprite damage index of the sprites array for appropriate rendering.
     */
    private int GetDamageSpriteIndex(int currentHits, int totalHits, int numberOfDamageSprites)
    {
        var numberOfRequiredHitsToChangeSprite = totalHits/numberOfDamageSprites;
        var damageSpriteIndex = currentHits / numberOfRequiredHitsToChangeSprite;

        // returns the right dmg sprite or the last one
        if (damageSpriteIndex < numberOfDamageSprites)
        {
            return damageSpriteIndex;
        }
        return numberOfDamageSprites - 1;
    }
    

    /**
     * Upon a collision, the block must be destroyed. Once a block is destroyed, the blocks counter
     * of the level controller must be decremented, the score must be updated and effects played.
     *
     * The score each blocks gives is:
     *
     *  score = baseBlockValue * maxHits
     *
     * Hence, a block that takes 3 hits gives 3x more points than one that takes one hit.
     */    
    private void DestroyItself()
    {
        // adds player points
        var gameState = FindObjectOfType<GameSession>();  // singleton
        gameState.AddToPlayerScore(maxHits);

        // plays VFX and SFX for the destruction
        PlayDestructionEffects();

        // increments destroyed blocks of the level
        _levelController.DecrementBlocksCounter();
    }

    /**
     * Plays VFX and SFX when a block is destroyed.
     */
    private void PlayDestructionEffects()
    {
        // displays the block destruction particles VFX
        ShowDestroyedBlockParticles();

        // plays destroyed block sound SFX
        AudioSource.PlayClipAtPoint(destroyedBlockSound, _soundPosition, soundVolume);

        DropItem();

        gameObject.GetComponent<SpriteRenderer>().sprite = damageSprites[0];
        _currentHits = 0;
        BlockLoader.Instance.Return(gameObject);   
        
        // Destroy(gameObject);
    }

    /**
     * Method to render destroyed blocks particles system VFX.
     */
    private void ShowDestroyedBlockParticles()
    {
        // using Unity's API to instantiate a new GameObject -- the particles VFX
        Vector3 blockPosition = transform.position;
        Quaternion blockRotation = transform.rotation;
        
        GameObject destroyedBlockParticles = Instantiate(destroyedBlockParticlesVFX, blockPosition, blockRotation);
    }

    private bool SimpleRandom(float rate, float maxRate)
    {
        return Random.Range(0, maxRate) <= rate;
    }
    private void DropItem()
    {
        if (SimpleRandom(itemDropRate, 100))
        {
            int index = Random.Range(0, dropItems.Count);
            var item = Instantiate(dropItems[index], GameObject.FindWithTag("DropItemZone").transform, true);
            item.transform.position = transform.position;
        }
    }
}
