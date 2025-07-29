using UnityEngine;

public class VariableManager : MonoBehaviour
{
    private int variable;
    public int Variable
    {
        get { return variable; }
        set { variable = value; }
    }

    public void Increment()
    {
        variable++;
    }

    public void Decrement()
    {
        variable--;
    }

    public void Reset()
    {
        variable = 0;
    }
}
