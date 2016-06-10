using UnityEngine;


public class VariableId : MonoBehaviour
{
    public string id;
    public int idVar;
    public object value;

    void Update()
    {
        switch (idVar) {
            case 0:
                value = (float)InteractionGlass.quantity;
                break;
            default:
                break;
    }
    }

}
