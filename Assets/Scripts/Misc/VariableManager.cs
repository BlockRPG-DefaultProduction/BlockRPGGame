using TMPro;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public bool isActive = false; // Biến để kiểm tra xem biến có đang hoạt động hay không
    private TextMeshPro variableText;
    void Start()
    {
        variableText = GetComponentInChildren<TextMeshPro>();
        Variable = 0; // Khởi tạo biến với giá trị mặc định
        if (variableText != null)
        {
            variableText.gameObject.SetActive(false);
        }
    }
    private int variable;
    public int Variable
    {
        get { return variable; }
        set
        {
            variable = value;
            if (variableText != null)
            {
                variableText.text = variable.ToString();
            }
        }
    }

    public void Increment()
    {
        Variable++;
    }

    public void Decrement()
    {
        Variable--;
    }

    public void Reset()
    {
        Variable = 0;
    }

    public void Initialize()
    {
        isActive = true;
        variableText.gameObject.SetActive(true);
    }

    public void Clear()
    {
        Reset();
        variableText.gameObject.SetActive(false);
        isActive = false;
    }
}
