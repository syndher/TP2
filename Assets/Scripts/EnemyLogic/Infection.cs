using UnityEngine;
using UnityEngine.SceneManagement;

public class Infection : MonoBehaviour
{
    [SerializeField] private int HordeCount = 50;
    public void Counter()
    {
        HordeCount--;
        Debug.Log(HordeCount);
        if (HordeCount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            HordeCount = 50;
        }
    }

}
