using System.Collections;
using System.Collections.Generic;
using IIMEngine.Movements2D;
using IIMEngine.Music;
using IIMEngine.Save;
using IIMEngine.ScreenTransitions;
using LOK.Core.Room;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalDetectionTrigger : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField] [TagSelector] private string _tagToCheck = "";

    [Header("Transition")]
    [SerializeField] private string _enterTransitionID = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(_tagToCheck)) return;
        IMove2DLockedWriter move2DLockedWriter = other.GetComponentInParent<IMove2DLockedWriter>();
        if (move2DLockedWriter != null) {
            move2DLockedWriter.AreMovementsLocked = true;
        }

        StartCoroutine(_CoroutineResetSaveAndReloadScene());
    }

    private IEnumerator _CoroutineResetSaveAndReloadScene()
    {
        yield return ScreenTransitionsManager.Instance.PlayAndWaitTransition(_enterTransitionID);
        SaveSystem.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ;
    }
}