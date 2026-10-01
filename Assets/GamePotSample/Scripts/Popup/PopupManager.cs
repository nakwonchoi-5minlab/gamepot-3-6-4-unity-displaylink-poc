using UnityEngine;

namespace GamePotDemo
{
    public class PopupManager : MonoBehaviour
    {
        public static GameObject ShowPopup(GameObject parent, string popupName)
        {
            GameObject popupGameObject = Resources.Load<GameObject>(popupName);

            if (popupGameObject == null)
            {
                Debug.LogError("GamePot Sample - Could not found " + popupName + " in Resources folder");
            }

            popupGameObject = Instantiate(popupGameObject) as GameObject;
            popupGameObject.transform.parent = parent.transform;
            popupGameObject.transform.localScale = new Vector3(1, 1, 1);
            popupGameObject.transform.localPosition = Vector3.zero;
            popupGameObject.transform.SetAsLastSibling();

            return popupGameObject;
        }


        public static void ShowCustomPopup(GameObject parent, string prefab_name, string title, string message, CustomizedPopup.PopupButtonInfo[] btn_info = null)
        {
            CustomizedPopup customPopup = ShowPopup(parent, prefab_name).GetComponent<CustomizedPopup>();

            if (customPopup != null)
            {
                customPopup.SetPopup(title, message, btn_info);
            }
        }


    }
}