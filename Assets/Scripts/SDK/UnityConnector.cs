using System.Runtime.InteropServices;
using UnityEngine;

public class UnityConnector : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UnityPluginRequestJs();

    public void RequestJs() // �������� �� ������� unity
    {
        UnityPluginRequestJs();
    }
}
