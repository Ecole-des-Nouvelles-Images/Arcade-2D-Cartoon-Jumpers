//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Charlie/PlayerControlsTest.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Charlie.Scripts.Player
{
    public partial class @PlayerControlsTest: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControlsTest()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlsTest"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""f41543c1-ffd8-44e0-816d-2fa079b9c8bf"",
            ""actions"": [
                {
                    ""name"": ""AimDash"",
                    ""type"": ""Value"",
                    ""id"": ""2d76b82d-d0c4-44ed-b356-ff6080eb5151"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e10f3140-384d-420c-a6c1-d904e9cb32f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AimShoot"",
                    ""type"": ""Value"",
                    ""id"": ""34e40ff0-a0d6-4125-b212-6faa38a01661"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""dfae5736-838e-4dd2-9584-82c789771c16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8e0e13c5-970f-4c35-8981-fdf6900194a2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AimDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b5a6bc8-28a3-44a9-8d1d-07d62c9b9097"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ca61eb8-0df7-46f1-8437-bb72bf446e44"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AimShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94040659-3c07-4e13-853e-25402b1138be"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TestInputs"",
            ""id"": ""784df5c0-64ff-410a-ae8b-5d0517bf562b"",
            ""actions"": [
                {
                    ""name"": ""NextScene"",
                    ""type"": ""Button"",
                    ""id"": ""7c8ace37-081c-4c40-9927-dcde18c3de68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrevScene"",
                    ""type"": ""Button"",
                    ""id"": ""155de992-98eb-4fc3-a872-cab880bb8c7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""75bf8fce-9588-4b3e-9d53-9c685e701bc6"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""NextScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99f76e48-b11c-4b11-8567-c8df886f4266"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""PrevScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // GamePlay
            m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
            m_GamePlay_AimDash = m_GamePlay.FindAction("AimDash", throwIfNotFound: true);
            m_GamePlay_Dash = m_GamePlay.FindAction("Dash", throwIfNotFound: true);
            m_GamePlay_AimShoot = m_GamePlay.FindAction("AimShoot", throwIfNotFound: true);
            m_GamePlay_Shoot = m_GamePlay.FindAction("Shoot", throwIfNotFound: true);
            // TestInputs
            m_TestInputs = asset.FindActionMap("TestInputs", throwIfNotFound: true);
            m_TestInputs_NextScene = m_TestInputs.FindAction("NextScene", throwIfNotFound: true);
            m_TestInputs_PrevScene = m_TestInputs.FindAction("PrevScene", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // GamePlay
        private readonly InputActionMap m_GamePlay;
        private List<IGamePlayActions> m_GamePlayActionsCallbackInterfaces = new List<IGamePlayActions>();
        private readonly InputAction m_GamePlay_AimDash;
        private readonly InputAction m_GamePlay_Dash;
        private readonly InputAction m_GamePlay_AimShoot;
        private readonly InputAction m_GamePlay_Shoot;
        public struct GamePlayActions
        {
            private @PlayerControlsTest m_Wrapper;
            public GamePlayActions(@PlayerControlsTest wrapper) { m_Wrapper = wrapper; }
            public InputAction @AimDash => m_Wrapper.m_GamePlay_AimDash;
            public InputAction @Dash => m_Wrapper.m_GamePlay_Dash;
            public InputAction @AimShoot => m_Wrapper.m_GamePlay_AimShoot;
            public InputAction @Shoot => m_Wrapper.m_GamePlay_Shoot;
            public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
            public void AddCallbacks(IGamePlayActions instance)
            {
                if (instance == null || m_Wrapper.m_GamePlayActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_GamePlayActionsCallbackInterfaces.Add(instance);
                @AimDash.started += instance.OnAimDash;
                @AimDash.performed += instance.OnAimDash;
                @AimDash.canceled += instance.OnAimDash;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @AimShoot.started += instance.OnAimShoot;
                @AimShoot.performed += instance.OnAimShoot;
                @AimShoot.canceled += instance.OnAimShoot;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }

            private void UnregisterCallbacks(IGamePlayActions instance)
            {
                @AimDash.started -= instance.OnAimDash;
                @AimDash.performed -= instance.OnAimDash;
                @AimDash.canceled -= instance.OnAimDash;
                @Dash.started -= instance.OnDash;
                @Dash.performed -= instance.OnDash;
                @Dash.canceled -= instance.OnDash;
                @AimShoot.started -= instance.OnAimShoot;
                @AimShoot.performed -= instance.OnAimShoot;
                @AimShoot.canceled -= instance.OnAimShoot;
                @Shoot.started -= instance.OnShoot;
                @Shoot.performed -= instance.OnShoot;
                @Shoot.canceled -= instance.OnShoot;
            }

            public void RemoveCallbacks(IGamePlayActions instance)
            {
                if (m_Wrapper.m_GamePlayActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IGamePlayActions instance)
            {
                foreach (var item in m_Wrapper.m_GamePlayActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_GamePlayActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public GamePlayActions @GamePlay => new GamePlayActions(this);

        // TestInputs
        private readonly InputActionMap m_TestInputs;
        private List<ITestInputsActions> m_TestInputsActionsCallbackInterfaces = new List<ITestInputsActions>();
        private readonly InputAction m_TestInputs_NextScene;
        private readonly InputAction m_TestInputs_PrevScene;
        public struct TestInputsActions
        {
            private @PlayerControlsTest m_Wrapper;
            public TestInputsActions(@PlayerControlsTest wrapper) { m_Wrapper = wrapper; }
            public InputAction @NextScene => m_Wrapper.m_TestInputs_NextScene;
            public InputAction @PrevScene => m_Wrapper.m_TestInputs_PrevScene;
            public InputActionMap Get() { return m_Wrapper.m_TestInputs; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TestInputsActions set) { return set.Get(); }
            public void AddCallbacks(ITestInputsActions instance)
            {
                if (instance == null || m_Wrapper.m_TestInputsActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_TestInputsActionsCallbackInterfaces.Add(instance);
                @NextScene.started += instance.OnNextScene;
                @NextScene.performed += instance.OnNextScene;
                @NextScene.canceled += instance.OnNextScene;
                @PrevScene.started += instance.OnPrevScene;
                @PrevScene.performed += instance.OnPrevScene;
                @PrevScene.canceled += instance.OnPrevScene;
            }

            private void UnregisterCallbacks(ITestInputsActions instance)
            {
                @NextScene.started -= instance.OnNextScene;
                @NextScene.performed -= instance.OnNextScene;
                @NextScene.canceled -= instance.OnNextScene;
                @PrevScene.started -= instance.OnPrevScene;
                @PrevScene.performed -= instance.OnPrevScene;
                @PrevScene.canceled -= instance.OnPrevScene;
            }

            public void RemoveCallbacks(ITestInputsActions instance)
            {
                if (m_Wrapper.m_TestInputsActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ITestInputsActions instance)
            {
                foreach (var item in m_Wrapper.m_TestInputsActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_TestInputsActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public TestInputsActions @TestInputs => new TestInputsActions(this);
        private int m_GamePadSchemeIndex = -1;
        public InputControlScheme GamePadScheme
        {
            get
            {
                if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
                return asset.controlSchemes[m_GamePadSchemeIndex];
            }
        }
        public interface IGamePlayActions
        {
            void OnAimDash(InputAction.CallbackContext context);
            void OnDash(InputAction.CallbackContext context);
            void OnAimShoot(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
        }
        public interface ITestInputsActions
        {
            void OnNextScene(InputAction.CallbackContext context);
            void OnPrevScene(InputAction.CallbackContext context);
        }
    }
}
