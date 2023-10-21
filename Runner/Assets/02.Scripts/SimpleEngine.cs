using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._02.Scripts
{
    internal class SimpleEngine
    {
        List<GameObject> gameObjects = new List<GameObject>();

        public void Register(GameObject go)
        {
            gameObjects.Add(go);
        }

        // Game Logic
        void Update()
        {

        }

        // Physics Logic]
        void FixedUpdate()
        {

        }
    }

    class GameObject
    {
        public List<MonoBehaviour> components = new List<MonoBehaviour>();
        public event Action OnUpdate;
        public event Action OnFixedUpdate;


        public void AddComponent<T>()
            where T : MonoBehaviour
        {
            T component = Activator.CreateInstance<T>();
            components.Add(component);
            component.gameObject = this;
        }
    }

    class MonoBehaviour
    {
        public GameObject gameObject
        {
            get => _gameObject;
            set
            {
                _gameObject = value;

                if (_hasAwaken == false)
                {
                    _hasAwaken = true;
                    Awake();
                    _gameObject.OnUpdate += () =>
                    {
                        if (_hasStarted == false)
                        {
                            _hasStarted = true;
                            Start();
                        }

                        Update();
                    };
                    _gameObject.OnFixedUpdate += () =>
                    {
                        FixedUpdate();
                    };
                }
            }
        }
        private GameObject _gameObject;
        private bool _hasAwaken;
        private bool _hasStarted;

        private void Awake() { }
        private void OnEnable() { }
        private void Start() { }
        private void Update() { }
        private void FixedUpdate() { }
    }
}
