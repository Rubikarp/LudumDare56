using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class CheatSheet : MonoBehaviour
{
    #region Variables Essential
    // Primitive Types
    public int publicInt = 0;
    public bool publicBool = false;
    public float publicFloat = 0.0f;
    public string publicString = "Hello World!";

    // Unity Types
    public Vector3 publicVector3 = Vector3.zero;
    public Vector2 publicVector2 = Vector2.zero;

    public GameObject publicGameObject = null;
    public Transform publicTransform = null;
    // RectTransform is a subclass of Transform for UI elements
    public RectTransform publicRectTransform = null;
    #endregion

    #region Collections Essential
    //Basic Collections
    public int[] publicIntArray = new int[0];
    public List<int> publicIntList = new List<int>();
    public Dictionary<string, int> publicIntDictionary = new Dictionary<string, int>();

    //Other Collections
    public Queue<int> publicIntQueue = new Queue<int>();
    public Stack<int> publicIntStack = new Stack<int>();

    public HashSet<int> publicIntHashSet = new HashSet<int>();
    public LinkedList<int> publicIntLinkedList = new LinkedList<int>();
    #endregion

    #region Attribute Essential
    // SerializeField allows private variables to be shown in the Inspector
    [SerializeField] private int privateInt = 0;
    // HideInInspector hides a variable from the Inspector
    [HideInInspector] public int hiddenInt = 0;
    // Range limits the value of a variable in the Inspector
    [Range(0, 10)] public int rangedInt = 0;
    // Tooltip adds a tooltip to a variable in the Inspector
    [Tooltip("This is a tooltip")] public int tooltipInt = 0;
    // Multiline allows a string to be displayed over multiple lines in the Inspector
    [Multiline] public string multilineString = "Hello\nWorld!";

    // Header adds a header to a variable in the Inspector
    [Header("This is a header")] public int headerInt = 0;
    // Space adds space between variables in the Inspector
    [Space] public int spacedInt = 0;

    // AddComponentMenu adds a component to the GameObject menu
    [AddComponentMenu("KarpGame/FalconPunch")]
    // RequireComponent automatically adds required components to the GameObject
    [RequireComponent(typeof(Rigidbody))]
    public class FalconPunch : MonoBehaviour
    {
    }
    #endregion

    #region Enum Essential
    // Enum Declaration
    public enum MyEnum { First, Second, Third }

    // Enum Flags (allows multiple values to be selected)
    [Flags]
    public enum MyFlags
    {
        First = 1,
        Second = 2,
        Third = 4
    }
    // Enum Flags with bitshift
    [Flags]
    public enum MyFlagsBitshift
    {
        First = 1 << 0,
        Second = 1 << 1,
        Third = 1 << 2
    }

    // Enum Variables
    public MyEnum myEnum = MyEnum.First;
    public MyFlags myFlag = MyFlags.First;

    /// Explain bitwise operation
    /// ^ : XOR
    /// | : OR
    /// & : AND
    /// ~ : NOT

    // Enum Flags Methods
    public bool HasFlag(MyFlags flag)
    {
        return (myFlag & flag) == flag;
    }
    public void AddFlag(MyFlags flag)
    {
        myFlag |= flag;
    }
    public void RemoveFlag(MyFlags flag)
    {
        myFlag &= ~flag;
    }
    public void ToggleFlag(MyFlags flag)
    {
        myFlag ^= flag;
    }

    #endregion

    #region Property Essential
    // Property Declaration
    private int _propertyInt;
    public int PropertyIntGetterSetter
    {
        get { return _propertyInt; }
        set { _propertyInt = value; }
    }

    // Auto-Property (automatically generates a private variable)
    public int AutoPropertyInt { get; set; }

    // Useful for variables that should not be modified outside the class
    public int ReadOnlyPropertyInt { get; private set; }

    // Hidden Property (visible in the Inspector)
    [field: SerializeField]
    public int ReadOnlyPropertyIntVisibleInInspector { get; private set; }

    // Computed Property (uses a method to calculate the value)
    public int ComputedPropertyInt
    {
        get { return 0 + 1 + 2 + 3 + 4; }
    }
    // Can be written in a single line
    public int ComputedPropertyIntOneLine => 0 + 1 + 2 + 3 + 4;
    #endregion

    #region Methods Essential
    public void MethodOverload() { }
    // Method Overloading (methods with the same name but different parameters)
    public void MethodOverload(int value) { }
    // Optional Parameters (parameters with default values)
    public void MethodOverload(Vector2 value, string text = "defaultValue") { }

    // Static Method (can be called without an instance of the class)
    public static void StaticMethod() { }
    // Virtual Method (can be overridden in a subclass)
    public virtual void VirtualMethod() { }
    #endregion

    #region Unity Messages
    // Awake is called when the script instance is being loaded
    private void Awake() { }
    // This function is called when the MonoBehaviour will be destroyed
    private void OnDestroy() { }

    // Reset is called when the user hits the Reset button in the Inspector's
    private void Reset() { }
    // Start is called before the first frame update
    private void Start() { }

    // Update is called once per frame
    private void Update() { }
    // LateUpdate is called every frame, if the Behaviour is enabled
    private void LateUpdate() { }
    // FixedUpdate is called every fixed framerate frame
    private void FixedUpdate() { }

    // This function is called when the object becomes enabled and active
    private void OnEnable() { }
    // This function is called when the behaviour becomes disabled
    private void OnDisable() { }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnCollisionEnter(Collision collision) { }
    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnCollisionStay(Collision collision) { }
    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnCollisionExit(Collision collision) { }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter2D(Collider2D collision) { }
    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnTriggerStay2D(Collider2D collision) { }
    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit2D(Collider2D collision) { }

    // This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only)
    private void OnValidate() { }
    // OnDrawGizmos is called when the object is drawn in the scene
    private void OnDrawGizmos() { }
    // OnDrawGizmosSelected is called only if the object is selected
    private void OnDrawGizmosSelected() { }

    // Sent to all game objects when the player gets or loses focus
    private void OnApplicationFocus(bool focus) { }
    // Sent to all game objects before the application is quit
    private void OnApplicationQuit() { }
    #endregion

    #region Event Essential
    public UnityEvent onEvent = null;
    public UnityEvent<int> onEventInt = null;

    public UnityAction onAction = null;
    public UnityAction<int> onActionInt = null;
    #endregion

    #region Coroutine Essential
    // Coroutine Declaration
    private Coroutine _coroutine;
    // Start Coroutine
    private void StartCoroutineExample()
    {
        _coroutine = StartCoroutine(MyCoroutine());
    }
    // Stop Coroutine
    private void StopCoroutineExample()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
    // Coroutine Method
    private IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(1f);
    }
    #endregion

    #region Essential for Unity
    // Instantiate a GameObject
    public GameObject InstantiateGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }

    // Destroy a GameObject
    public void DestroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    // Find a GameObject by name
    // Note: Expensive operation, avoid using it
    public GameObject FindGameObjectByName(string name) 
    {
        return GameObject.Find(name);
    }

    // Exemple of FindObjectsOfType
    // Note: Expensive operation, avoid using it
    public void FindObjectsOfTypeExample()
    {
        // Find all objects of type Camera
        Camera[] cameras = FindObjectsOfType<Camera>();
    }

    // exemple of camera.main
    public void CameraMainExample()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;
    }

    //exemple of getComponent
    public void GetComponentExample()
    {
        // Get the Transform component
        Transform transform = GetComponent<Transform>();
    }
    #endregion

    #region Unity Pooling Exemple
    public void CreateObjectPool(GameObject prefab)
    {
        ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(
            () => Instantiate(prefab), 
            (obj) => OnGetPoolObject(obj), 
            (obj) => OnReleasePoolObject(obj), 
            (obj) => DestroyGameObject(obj)
            );
    }
    private void OnGetPoolObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void OnReleasePoolObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private ObjectPool<GameObject> _objectPool;
    public GameObject GetObjectFromPool()
    {
        return _objectPool.Get();
    }
    public void ReturnObjectToPool(GameObject gameObject)
    {
        _objectPool.Release(gameObject);
    }
    #endregion

    #region Linqs Exemple
    public void LinqsExample()
    {
        // Create a list of integers
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        // Select all numbers greater than 2
        IEnumerable<int> greaterThanTwo = numbers.Where(n => n > 2);

        // Select the first number greater than 2
        int firstGreaterThanTwo = numbers.First(n => n > 2);

        // Select the sum of all numbers
        int sum = numbers.Sum();

        // Select the average of all numbers
        float average = (float)numbers.Average();

        // Select the maximum number
        int max = numbers.Max();

        // Select the minimum number
        int min = numbers.Min();

        // Select the number of elements
        int count = numbers.Count();

        // Select the number of elements greater than 2
        int countGreaterThanTwo = numbers.Count(n => n > 2);

        // Select the sum of all numbers greater than 2
        int sumGreaterThanTwo = numbers.Where(n => n > 2).Sum();

        // Chain exemple
        int chainOf5 = numbers.Where(n => n > 2).OrderBy(n => n).Select(n => n * 2).Sum();
    }
    #endregion
}

#region Inheritance Essential
public class BaseClass
{
    // Virtual Method (can be overridden in a subclass)
    public virtual void Method()
    {
        Debug.Log("BaseClass Method");
    }
}
public class DerivedClass : BaseClass
{
    // Override Method (overrides the base class method)
    public override void Method()
    {
        Debug.Log("DerivedClass Method");
    }
}

public struct BaseStruct { }

public interface IMyInterface
{
    // Interface Method (must be implemented by a class)
    void InterfaceMethod();
}
public class InterfaceClass : IMyInterface
{
    // Implement Interface Method
    public void InterfaceMethod()
    {
        Debug.Log("InterfaceMethod");
    }
}

public abstract class AbstractClass
{
    // Abstract Method (must be implemented by a subclass)
    public abstract void AbstractMethod();
}
public class ConcreteClass : AbstractClass
{
    // Implement Abstract Method
    public override void AbstractMethod()
    {
        Debug.Log("AbstractMethod");
    }
}

[CreateAssetMenu(fileName = "MyScriptableObject", menuName = "MyScriptableObject")]
public class MyScriptableObject : ScriptableObject
{

}
#endregion