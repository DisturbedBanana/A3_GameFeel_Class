using IIMEngine.Camera;
using LOK.Core.UserProfiles;
using LOK.Core.Room;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOK.Core.Globals
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            CameraGlobals.Manager.Init();
            RoomsManager.Instance.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) 
                && !UIPopupPasswordValidator.Instance.IsOpened
                && !UIPopupPasswordEnterUserName.Instance.IsOpened) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.F2) 
                && !UIPopupPasswordValidator.Instance.IsOpened) {
                UIPopupPasswordValidator.Instance.Open("Movements_SM");
            }
        }
    }
}