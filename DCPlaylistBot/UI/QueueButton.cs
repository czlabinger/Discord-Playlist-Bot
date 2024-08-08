using UnityEngine;

internal class QueueButton : MonoBehaviour {

    private GameObject Instance;

    void awake() {
        Instance = new GameObject("QueueButton");
        Instance.transform.position = new Vector3(0f, 0f, 0f);

        Instance.SetActive(true);
        Instance.AddComponent<TextButton>();
        Instance.GetComponent<TextButton>().enabled = true;
    }
}
