using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    #endregion
    #region Private Fields

    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;

    // The currently running coroutine.
    private Coroutine flashRoutine;

    public int flickerAmount;

    #endregion

    #endregion

    #region Methods

    #region Unity Callbacks

    void Start()
    {
        // Get the SpriteRenderer to be used,
        // alternatively you could set it from the inspector.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the material that the SpriteRenderer uses, 
        // so we can switch back to it after the flash ended.
       
    }

    #endregion

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for(int i = 0; i < flickerAmount; i++)
        {

            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(duration);

            spriteRenderer.color = Color.white;

            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
            yield return new WaitForSeconds(duration);
        }
    }

    #endregion
}
