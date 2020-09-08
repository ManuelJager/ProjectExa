// GENERATED AUTOMATICALLY FROM 'Assets/Project/Source/Input/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Exa.Input
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Editor"",
            ""id"": ""ddda1cd8-bd9b-4895-b667-214d683e0e16"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""d4726475-8b79-45d1-9532-28ed98f0eec0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""6ce42356-795d-497f-a813-8f86d4d0bca7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""bb03ea49-7fd7-43e5-bf85-acd7f986412d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""74076920-1a4a-44c6-9eed-0a5e545732ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""0c25f458-dd75-4756-9108-297733f06b3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleMirror"",
                    ""type"": ""Button"",
                    ""id"": ""5cd15413-dd5d-4c6c-aed7-035969a257fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""491d8e4b-11b2-40f6-9188-67d4ab7e0bf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f000d51a-3bf6-494e-b022-1e27fdf2901a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a9978e7d-5891-4e26-9cea-3d38037b4720"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c6d95be6-8d84-46cd-9358-85985e7f8fd7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""193be172-a61e-489b-82ec-9428a2c1da95"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8aa6fcb9-dbde-42be-a650-cec1a9150dd7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""99cab014-b995-403d-85ad-3140c01037aa"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16aee5bf-7843-4f6b-ad08-eac99f6a0cb5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16af04c7-5a85-4ef6-8f5d-14083def649a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8e8c7ea-a53f-4182-b207-e1ba6b69c473"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9edb89d0-2990-477b-8851-8db2e565f78b"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""ToggleMirror"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d592b28c-df45-4c5d-a5bb-a3577412b6e0"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""f0082add-904f-492a-abda-60fefe8da8ff"",
            ""actions"": [
                {
                    ""name"": ""ToggleConsole"",
                    ""type"": ""Button"",
                    ""id"": ""7e486db0-7abd-4570-86e6-53b181833168"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""33ac8a8d-0546-4121-8858-7134b251890b"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""ToggleConsole"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ReturnNavigateable"",
            ""id"": ""c8b9e2d9-a691-4930-80bc-7f36809a9bb6"",
            ""actions"": [
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""9f2ee82f-6b2c-4de1-834d-8b988278babe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c5a4399-91ac-4590-8129-3ded1ee8fdc0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gameplay"",
            ""id"": ""61cd0ddd-a0ce-4af1-8555-837eaeb9cd6a"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c7195519-c99e-4f5a-af47-8d0d9164bc25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""50bb47ae-7f50-45b0-9e96-b252abb46662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""9ee6f204-606b-4201-b062-76a13c822018"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""b82d7752-30f4-4315-95fd-e85d984f38bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""124fa870-a9a7-4c9a-a2f6-5462f85096a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NumKeys"",
                    ""type"": ""Value"",
                    ""id"": ""f1457bf0-3320-4d7d-86b7-f7ddc25cd9e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2aac21f5-8e79-40b5-bedc-1f0ed276a573"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6a5c9fe1-92cc-4dfe-8bbf-8154a0976c52"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""958d0fe2-0af9-4fe2-8256-d75bff1922a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""80b671d7-af07-4bb8-92ec-646be3a33dbd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""65e4f846-71df-467c-87f8-fdb53031968f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0e38678e-a39c-47fc-9884-a7664d3f7b1b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""483a399c-6bc9-4b68-9014-8661d8c4175b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5df59e7-36ee-46a3-9c25-70f0d52a7153"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05f9f505-f901-4b0a-a703-43e6e97b6bcb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""575af4d0-e09b-4525-aee3-a94e51b2bbff"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""604737ac-8321-462d-b3aa-8e72f348fbdc"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c74fed5-23ba-4264-9f93-91e461a1ad8a"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58683b3d-93f4-4246-9eb2-e295fd21a6a3"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d4a7c05-d969-4aec-8a36-bffe7da6e4e7"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ea73dbb-4b0a-438b-9080-3d0fdaf70939"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2efd8d0-0116-4309-9404-dbf51f179155"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec1355f6-abd8-4305-8180-4cd1ff49fa90"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""885ff937-4a91-45cb-9f6d-c5651d283870"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8debbf68-4854-4872-9467-6bd60d8596f5"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""NumKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseKb"",
            ""bindingGroup"": ""MouseKb"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Editor
            m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
            m_Editor_Movement = m_Editor.FindAction("Movement", throwIfNotFound: true);
            m_Editor_LeftClick = m_Editor.FindAction("LeftClick", throwIfNotFound: true);
            m_Editor_RightClick = m_Editor.FindAction("RightClick", throwIfNotFound: true);
            m_Editor_RotateLeft = m_Editor.FindAction("RotateLeft", throwIfNotFound: true);
            m_Editor_RotateRight = m_Editor.FindAction("RotateRight", throwIfNotFound: true);
            m_Editor_ToggleMirror = m_Editor.FindAction("ToggleMirror", throwIfNotFound: true);
            m_Editor_Zoom = m_Editor.FindAction("Zoom", throwIfNotFound: true);
            // Debug
            m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
            m_Debug_ToggleConsole = m_Debug.FindAction("ToggleConsole", throwIfNotFound: true);
            // ReturnNavigateable
            m_ReturnNavigateable = asset.FindActionMap("ReturnNavigateable", throwIfNotFound: true);
            m_ReturnNavigateable_Return = m_ReturnNavigateable.FindAction("Return", throwIfNotFound: true);
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Zoom = m_Gameplay.FindAction("Zoom", throwIfNotFound: true);
            m_Gameplay_RightClick = m_Gameplay.FindAction("RightClick", throwIfNotFound: true);
            m_Gameplay_LeftClick = m_Gameplay.FindAction("LeftClick", throwIfNotFound: true);
            m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
            m_Gameplay_Escape = m_Gameplay.FindAction("Escape", throwIfNotFound: true);
            m_Gameplay_NumKeys = m_Gameplay.FindAction("NumKeys", throwIfNotFound: true);
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

        // Editor
        private readonly InputActionMap m_Editor;
        private IEditorActions m_EditorActionsCallbackInterface;
        private readonly InputAction m_Editor_Movement;
        private readonly InputAction m_Editor_LeftClick;
        private readonly InputAction m_Editor_RightClick;
        private readonly InputAction m_Editor_RotateLeft;
        private readonly InputAction m_Editor_RotateRight;
        private readonly InputAction m_Editor_ToggleMirror;
        private readonly InputAction m_Editor_Zoom;
        public struct EditorActions
        {
            private readonly @GameControls m_Wrapper;
            public EditorActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Editor_Movement;
            public InputAction @LeftClick => m_Wrapper.m_Editor_LeftClick;
            public InputAction @RightClick => m_Wrapper.m_Editor_RightClick;
            public InputAction @RotateLeft => m_Wrapper.m_Editor_RotateLeft;
            public InputAction @RotateRight => m_Wrapper.m_Editor_RotateRight;
            public InputAction @ToggleMirror => m_Wrapper.m_Editor_ToggleMirror;
            public InputAction @Zoom => m_Wrapper.m_Editor_Zoom;
            public InputActionMap Get() { return m_Wrapper.m_Editor; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
            public void SetCallbacks(IEditorActions instance)
            {
                if (m_Wrapper.m_EditorActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnMovement;
                    @LeftClick.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnLeftClick;
                    @RightClick.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRightClick;
                    @RotateLeft.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateLeft;
                    @RotateLeft.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateLeft;
                    @RotateLeft.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateLeft;
                    @RotateRight.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateRight;
                    @RotateRight.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateRight;
                    @RotateRight.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateRight;
                    @ToggleMirror.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnToggleMirror;
                    @ToggleMirror.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnToggleMirror;
                    @ToggleMirror.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnToggleMirror;
                    @Zoom.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnZoom;
                }
                m_Wrapper.m_EditorActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @RotateLeft.started += instance.OnRotateLeft;
                    @RotateLeft.performed += instance.OnRotateLeft;
                    @RotateLeft.canceled += instance.OnRotateLeft;
                    @RotateRight.started += instance.OnRotateRight;
                    @RotateRight.performed += instance.OnRotateRight;
                    @RotateRight.canceled += instance.OnRotateRight;
                    @ToggleMirror.started += instance.OnToggleMirror;
                    @ToggleMirror.performed += instance.OnToggleMirror;
                    @ToggleMirror.canceled += instance.OnToggleMirror;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                }
            }
        }
        public EditorActions @Editor => new EditorActions(this);

        // Debug
        private readonly InputActionMap m_Debug;
        private IDebugActions m_DebugActionsCallbackInterface;
        private readonly InputAction m_Debug_ToggleConsole;
        public struct DebugActions
        {
            private readonly @GameControls m_Wrapper;
            public DebugActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToggleConsole => m_Wrapper.m_Debug_ToggleConsole;
            public InputActionMap Get() { return m_Wrapper.m_Debug; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
            public void SetCallbacks(IDebugActions instance)
            {
                if (m_Wrapper.m_DebugActionsCallbackInterface != null)
                {
                    @ToggleConsole.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleConsole;
                    @ToggleConsole.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleConsole;
                    @ToggleConsole.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleConsole;
                }
                m_Wrapper.m_DebugActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToggleConsole.started += instance.OnToggleConsole;
                    @ToggleConsole.performed += instance.OnToggleConsole;
                    @ToggleConsole.canceled += instance.OnToggleConsole;
                }
            }
        }
        public DebugActions @Debug => new DebugActions(this);

        // ReturnNavigateable
        private readonly InputActionMap m_ReturnNavigateable;
        private IReturnNavigateableActions m_ReturnNavigateableActionsCallbackInterface;
        private readonly InputAction m_ReturnNavigateable_Return;
        public struct ReturnNavigateableActions
        {
            private readonly @GameControls m_Wrapper;
            public ReturnNavigateableActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Return => m_Wrapper.m_ReturnNavigateable_Return;
            public InputActionMap Get() { return m_Wrapper.m_ReturnNavigateable; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ReturnNavigateableActions set) { return set.Get(); }
            public void SetCallbacks(IReturnNavigateableActions instance)
            {
                if (m_Wrapper.m_ReturnNavigateableActionsCallbackInterface != null)
                {
                    @Return.started -= m_Wrapper.m_ReturnNavigateableActionsCallbackInterface.OnReturn;
                    @Return.performed -= m_Wrapper.m_ReturnNavigateableActionsCallbackInterface.OnReturn;
                    @Return.canceled -= m_Wrapper.m_ReturnNavigateableActionsCallbackInterface.OnReturn;
                }
                m_Wrapper.m_ReturnNavigateableActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Return.started += instance.OnReturn;
                    @Return.performed += instance.OnReturn;
                    @Return.canceled += instance.OnReturn;
                }
            }
        }
        public ReturnNavigateableActions @ReturnNavigateable => new ReturnNavigateableActions(this);

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Zoom;
        private readonly InputAction m_Gameplay_RightClick;
        private readonly InputAction m_Gameplay_LeftClick;
        private readonly InputAction m_Gameplay_Movement;
        private readonly InputAction m_Gameplay_Escape;
        private readonly InputAction m_Gameplay_NumKeys;
        public struct GameplayActions
        {
            private readonly @GameControls m_Wrapper;
            public GameplayActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Zoom => m_Wrapper.m_Gameplay_Zoom;
            public InputAction @RightClick => m_Wrapper.m_Gameplay_RightClick;
            public InputAction @LeftClick => m_Wrapper.m_Gameplay_LeftClick;
            public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
            public InputAction @Escape => m_Wrapper.m_Gameplay_Escape;
            public InputAction @NumKeys => m_Wrapper.m_Gameplay_NumKeys;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Zoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                    @RightClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightClick;
                    @LeftClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftClick;
                    @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Escape.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @NumKeys.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNumKeys;
                    @NumKeys.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNumKeys;
                    @NumKeys.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNumKeys;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                    @NumKeys.started += instance.OnNumKeys;
                    @NumKeys.performed += instance.OnNumKeys;
                    @NumKeys.canceled += instance.OnNumKeys;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        private int m_MouseKbSchemeIndex = -1;
        public InputControlScheme MouseKbScheme
        {
            get
            {
                if (m_MouseKbSchemeIndex == -1) m_MouseKbSchemeIndex = asset.FindControlSchemeIndex("MouseKb");
                return asset.controlSchemes[m_MouseKbSchemeIndex];
            }
        }
        public interface IEditorActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnLeftClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnRotateLeft(InputAction.CallbackContext context);
            void OnRotateRight(InputAction.CallbackContext context);
            void OnToggleMirror(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
        }
        public interface IDebugActions
        {
            void OnToggleConsole(InputAction.CallbackContext context);
        }
        public interface IReturnNavigateableActions
        {
            void OnReturn(InputAction.CallbackContext context);
        }
        public interface IGameplayActions
        {
            void OnZoom(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnLeftClick(InputAction.CallbackContext context);
            void OnMovement(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
            void OnNumKeys(InputAction.CallbackContext context);
        }
    }
}
