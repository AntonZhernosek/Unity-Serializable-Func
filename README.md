### Serializable Func
A UnityEvent for function calls with a return value. 
Allows you to assign Func\<T\> via the Inspector.
Looks and acts like a UnityEvent.
Supports both GUI and UI Toolkit. Tested with Unity 2020.3, 2021.3, 2022.2, 2023.1.
Tested in standalone Windows and Android builds, both with Mono and IL2CPP.

GUI Representation
![unity_inspector](https://i.imgur.com/pR4uo7H.png)

UI Toolkit Representation
![unity_inspector](https://i.imgur.com/tGmKW1m.png)

### Example Usage
```csharp
public class ExampleClass : MonoBehaviour 
{
    [Header("My Bool Func")]
    [SerializeField] private SerializableFunc<bool> boolFunc;

    private void Start()
    {
        bool result = boolFunc.Invoke();
        Debug.Log(result);
    }
}
```


