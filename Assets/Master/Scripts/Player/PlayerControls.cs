//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Master/Player/PlayerControls.inputactions
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

namespace Master.Scripts.Player
{
    public partial class @PlayerControls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
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
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""47ac5c30-cc03-4f74-83fe-e54d44fd5204"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
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
                    ""id"": ""d298a3e7-1572-4de4-a8a0-a545c515efd1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""6ba511eb-5277-43cd-9424-ad0daa5b8559"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TestMap"",
            ""id"": ""31e7379e-9ea0-47ef-89fa-bfecac056165"",
            ""actions"": [
                {
                    ""name"": ""NextScene"",
                    ""type"": ""Button"",
                    ""id"": ""2c322f10-d58b-4fb0-a647-1952d9653d00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrevScene"",
                    ""type"": ""Button"",
                    ""id"": ""a2885bfe-8146-4fad-be50-3d4fc21b79a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8717b6b0-94ad-4f66-8167-dc6894fb580a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""NextScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d07c52e1-481c-46b2-b102-59da44914a17"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""PrevScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIMenu"",
            ""id"": ""e3e37d02-9e81-448c-9da7-b41e6d50a815"",
            ""actions"": [
                {
                    ""name"": ""Point"",
                    ""type"": ""Value"",
                    ""id"": ""119931b0-2186-4a40-b901-232e772efdfe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""b660e3e4-fc2a-4cd2-a0cb-baa726201ed1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""909a7901-32b5-4898-a3ca-6c1f637c5569"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75cdd527-05d1-4d1f-ade2-05005f247422"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3212dfcf-1f3a-44a9-be3b-71c1592158c9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
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
            m_GamePlay_Pause = m_GamePlay.FindAction("Pause", throwIfNotFound: true);
            // TestMap
            m_TestMap = asset.FindActionMap("TestMap", throwIfNotFound: true);
            m_TestMap_NextScene = m_TestMap.FindAction("NextScene", throwIfNotFound: true);
            m_TestMap_PrevScene = m_TestMap.FindAction("PrevScene", throwIfNotFound: true);
            // UIMenu
            m_UIMenu = asset.FindActionMap("UIMenu", throwIfNotFound: true);
            m_UIMenu_Point = m_UIMenu.FindAction("Point", throwIfNotFound: true);
            m_UIMenu_Click = m_UIMenu.FindAction("Click", throwIfNotFound: true);
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
        private readonly InputAction m_GamePlay_Pause;
        public struct GamePlayActions
        {
            private @PlayerControls m_Wrapper;
            public GamePlayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @AimDash => m_Wrapper.m_GamePlay_AimDash;
            public InputAction @Dash => m_Wrapper.m_GamePlay_Dash;
            public InputAction @AimShoot => m_Wrapper.m_GamePlay_AimShoot;
            public InputAction @Shoot => m_Wrapper.m_GamePlay_Shoot;
            public InputAction @Pause => m_Wrapper.m_GamePlay_Pause;
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
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
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
                @Pause.started -= instance.OnPause;
                @Pause.performed -= instance.OnPause;
                @Pause.canceled -= instance.OnPause;
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

        // TestMap
        private readonly InputActionMap m_TestMap;
        private List<ITestMapActions> m_TestMapActionsCallbackInterfaces = new List<ITestMapActions>();
        private readonly InputAction m_TestMap_NextScene;
        private readonly InputAction m_TestMap_PrevScene;
        public struct TestMapActions
        {
            private @PlayerControls m_Wrapper;
            public TestMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @NextScene => m_Wrapper.m_TestMap_NextScene;
            public InputAction @PrevScene => m_Wrapper.m_TestMap_PrevScene;
            public InputActionMap Get() { return m_Wrapper.m_TestMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TestMapActions set) { return set.Get(); }
            public void AddCallbacks(ITestMapActions instance)
            {
                if (instance == null || m_Wrapper.m_TestMapActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_TestMapActionsCallbackInterfaces.Add(instance);
                @NextScene.started += instance.OnNextScene;
                @NextScene.performed += instance.OnNextScene;
                @NextScene.canceled += instance.OnNextScene;
                @PrevScene.started += instance.OnPrevScene;
                @PrevScene.performed += instance.OnPrevScene;
                @PrevScene.canceled += instance.OnPrevScene;
            }

            private void UnregisterCallbacks(ITestMapActions instance)
            {
                @NextScene.started -= instance.OnNextScene;
                @NextScene.performed -= instance.OnNextScene;
                @NextScene.canceled -= instance.OnNextScene;
                @PrevScene.started -= instance.OnPrevScene;
                @PrevScene.performed -= instance.OnPrevScene;
                @PrevScene.canceled -= instance.OnPrevScene;
            }

            public void RemoveCallbacks(ITestMapActions instance)
            {
                if (m_Wrapper.m_TestMapActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ITestMapActions instance)
            {
                foreach (var item in m_Wrapper.m_TestMapActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_TestMapActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public TestMapActions @TestMap => new TestMapActions(this);

        // UIMenu
        private readonly InputActionMap m_UIMenu;
        private List<IUIMenuActions> m_UIMenuActionsCallbackInterfaces = new List<IUIMenuActions>();
        private readonly InputAction m_UIMenu_Point;
        private readonly InputAction m_UIMenu_Click;
        public struct UIMenuActions
        {
            private @PlayerControls m_Wrapper;
            public UIMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Point => m_Wrapper.m_UIMenu_Point;
            public InputAction @Click => m_Wrapper.m_UIMenu_Click;
            public InputActionMap Get() { return m_Wrapper.m_UIMenu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIMenuActions set) { return set.Get(); }
            public void AddCallbacks(IUIMenuActions instance)
            {
                if (instance == null || m_Wrapper.m_UIMenuActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_UIMenuActionsCallbackInterfaces.Add(instance);
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }

            private void UnregisterCallbacks(IUIMenuActions instance)
            {
                @Point.started -= instance.OnPoint;
                @Point.performed -= instance.OnPoint;
                @Point.canceled -= instance.OnPoint;
                @Click.started -= instance.OnClick;
                @Click.performed -= instance.OnClick;
                @Click.canceled -= instance.OnClick;
            }

            public void RemoveCallbacks(IUIMenuActions instance)
            {
                if (m_Wrapper.m_UIMenuActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IUIMenuActions instance)
            {
                foreach (var item in m_Wrapper.m_UIMenuActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_UIMenuActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public UIMenuActions @UIMenu => new UIMenuActions(this);
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
            void OnPause(InputAction.CallbackContext context);
        }
        public interface ITestMapActions
        {
            void OnNextScene(InputAction.CallbackContext context);
            void OnPrevScene(InputAction.CallbackContext context);
        }
        public interface IUIMenuActions
        {
            void OnPoint(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
        }
    }
}
