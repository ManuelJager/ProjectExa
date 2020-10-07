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
        public InputActionAsset Asset { get; }
        public @GameControls()
        {
            Asset = InputActionAsset.FromJson(@"{
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
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""0b87a355-27b6-40d2-842e-51238e7d1ac3"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""02fde2ea-86ba-48b5-9360-8600e5ef0a0e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Drag"",
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
            _mEditor = Asset.FindActionMap("Editor", throwIfNotFound: true);
            _mEditorMovement = _mEditor.FindAction("Movement", throwIfNotFound: true);
            _mEditorLeftClick = _mEditor.FindAction("LeftClick", throwIfNotFound: true);
            _mEditorRightClick = _mEditor.FindAction("RightClick", throwIfNotFound: true);
            _mEditorRotateLeft = _mEditor.FindAction("RotateLeft", throwIfNotFound: true);
            _mEditorRotateRight = _mEditor.FindAction("RotateRight", throwIfNotFound: true);
            _mEditorToggleMirror = _mEditor.FindAction("ToggleMirror", throwIfNotFound: true);
            _mEditorZoom = _mEditor.FindAction("Zoom", throwIfNotFound: true);
            // Debug
            _mDebug = Asset.FindActionMap("Debug", throwIfNotFound: true);
            _mDebugToggleConsole = _mDebug.FindAction("ToggleConsole", throwIfNotFound: true);
            _mDebugDrag = _mDebug.FindAction("Drag", throwIfNotFound: true);
            // ReturnNavigateable
            _mReturnNavigateable = Asset.FindActionMap("ReturnNavigateable", throwIfNotFound: true);
            _mReturnNavigateableReturn = _mReturnNavigateable.FindAction("Return", throwIfNotFound: true);
            // Gameplay
            _mGameplay = Asset.FindActionMap("Gameplay", throwIfNotFound: true);
            _mGameplayZoom = _mGameplay.FindAction("Zoom", throwIfNotFound: true);
            _mGameplayRightClick = _mGameplay.FindAction("RightClick", throwIfNotFound: true);
            _mGameplayLeftClick = _mGameplay.FindAction("LeftClick", throwIfNotFound: true);
            _mGameplayMovement = _mGameplay.FindAction("Movement", throwIfNotFound: true);
            _mGameplayEscape = _mGameplay.FindAction("Escape", throwIfNotFound: true);
            _mGameplayNumKeys = _mGameplay.FindAction("NumKeys", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Asset);
        }

        public InputBinding? bindingMask
        {
            get => Asset.bindingMask;
            set => Asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => Asset.devices;
            set => Asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => Asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return Asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return Asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            Asset.Enable();
        }

        public void Disable()
        {
            Asset.Disable();
        }

        // Editor
        private readonly InputActionMap _mEditor;
        private IEditorActions _mEditorActionsCallbackInterface;
        private readonly InputAction _mEditorMovement;
        private readonly InputAction _mEditorLeftClick;
        private readonly InputAction _mEditorRightClick;
        private readonly InputAction _mEditorRotateLeft;
        private readonly InputAction _mEditorRotateRight;
        private readonly InputAction _mEditorToggleMirror;
        private readonly InputAction _mEditorZoom;
        public struct EditorActions
        {
            private @GameControls _mWrapper;
            public EditorActions(@GameControls wrapper) { _mWrapper = wrapper; }
            public InputAction @Movement => _mWrapper._mEditorMovement;
            public InputAction @LeftClick => _mWrapper._mEditorLeftClick;
            public InputAction @RightClick => _mWrapper._mEditorRightClick;
            public InputAction @RotateLeft => _mWrapper._mEditorRotateLeft;
            public InputAction @RotateRight => _mWrapper._mEditorRotateRight;
            public InputAction @ToggleMirror => _mWrapper._mEditorToggleMirror;
            public InputAction @Zoom => _mWrapper._mEditorZoom;
            public InputActionMap Get() { return _mWrapper._mEditor; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool Enabled => Get().enabled;
            public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
            public void SetCallbacks(IEditorActions instance)
            {
                if (_mWrapper._mEditorActionsCallbackInterface != null)
                {
                    @Movement.started -= _mWrapper._mEditorActionsCallbackInterface.OnMovement;
                    @Movement.performed -= _mWrapper._mEditorActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnMovement;
                    @LeftClick.started -= _mWrapper._mEditorActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= _mWrapper._mEditorActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnLeftClick;
                    @RightClick.started -= _mWrapper._mEditorActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= _mWrapper._mEditorActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnRightClick;
                    @RotateLeft.started -= _mWrapper._mEditorActionsCallbackInterface.OnRotateLeft;
                    @RotateLeft.performed -= _mWrapper._mEditorActionsCallbackInterface.OnRotateLeft;
                    @RotateLeft.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnRotateLeft;
                    @RotateRight.started -= _mWrapper._mEditorActionsCallbackInterface.OnRotateRight;
                    @RotateRight.performed -= _mWrapper._mEditorActionsCallbackInterface.OnRotateRight;
                    @RotateRight.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnRotateRight;
                    @ToggleMirror.started -= _mWrapper._mEditorActionsCallbackInterface.OnToggleMirror;
                    @ToggleMirror.performed -= _mWrapper._mEditorActionsCallbackInterface.OnToggleMirror;
                    @ToggleMirror.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnToggleMirror;
                    @Zoom.started -= _mWrapper._mEditorActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= _mWrapper._mEditorActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= _mWrapper._mEditorActionsCallbackInterface.OnZoom;
                }
                _mWrapper._mEditorActionsCallbackInterface = instance;
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
        private readonly InputActionMap _mDebug;
        private IDebugActions _mDebugActionsCallbackInterface;
        private readonly InputAction _mDebugToggleConsole;
        private readonly InputAction _mDebugDrag;
        public struct DebugActions
        {
            private @GameControls _mWrapper;
            public DebugActions(@GameControls wrapper) { _mWrapper = wrapper; }
            public InputAction @ToggleConsole => _mWrapper._mDebugToggleConsole;
            public InputAction @Drag => _mWrapper._mDebugDrag;
            public InputActionMap Get() { return _mWrapper._mDebug; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool Enabled => Get().enabled;
            public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
            public void SetCallbacks(IDebugActions instance)
            {
                if (_mWrapper._mDebugActionsCallbackInterface != null)
                {
                    @ToggleConsole.started -= _mWrapper._mDebugActionsCallbackInterface.OnToggleConsole;
                    @ToggleConsole.performed -= _mWrapper._mDebugActionsCallbackInterface.OnToggleConsole;
                    @ToggleConsole.canceled -= _mWrapper._mDebugActionsCallbackInterface.OnToggleConsole;
                    @Drag.started -= _mWrapper._mDebugActionsCallbackInterface.OnDrag;
                    @Drag.performed -= _mWrapper._mDebugActionsCallbackInterface.OnDrag;
                    @Drag.canceled -= _mWrapper._mDebugActionsCallbackInterface.OnDrag;
                }
                _mWrapper._mDebugActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToggleConsole.started += instance.OnToggleConsole;
                    @ToggleConsole.performed += instance.OnToggleConsole;
                    @ToggleConsole.canceled += instance.OnToggleConsole;
                    @Drag.started += instance.OnDrag;
                    @Drag.performed += instance.OnDrag;
                    @Drag.canceled += instance.OnDrag;
                }
            }
        }
        public DebugActions @Debug => new DebugActions(this);

        // ReturnNavigateable
        private readonly InputActionMap _mReturnNavigateable;
        private IReturnNavigateableActions _mReturnNavigateableActionsCallbackInterface;
        private readonly InputAction _mReturnNavigateableReturn;
        public struct ReturnNavigateableActions
        {
            private @GameControls _mWrapper;
            public ReturnNavigateableActions(@GameControls wrapper) { _mWrapper = wrapper; }
            public InputAction @Return => _mWrapper._mReturnNavigateableReturn;
            public InputActionMap Get() { return _mWrapper._mReturnNavigateable; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool Enabled => Get().enabled;
            public static implicit operator InputActionMap(ReturnNavigateableActions set) { return set.Get(); }
            public void SetCallbacks(IReturnNavigateableActions instance)
            {
                if (_mWrapper._mReturnNavigateableActionsCallbackInterface != null)
                {
                    @Return.started -= _mWrapper._mReturnNavigateableActionsCallbackInterface.OnReturn;
                    @Return.performed -= _mWrapper._mReturnNavigateableActionsCallbackInterface.OnReturn;
                    @Return.canceled -= _mWrapper._mReturnNavigateableActionsCallbackInterface.OnReturn;
                }
                _mWrapper._mReturnNavigateableActionsCallbackInterface = instance;
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
        private readonly InputActionMap _mGameplay;
        private IGameplayActions _mGameplayActionsCallbackInterface;
        private readonly InputAction _mGameplayZoom;
        private readonly InputAction _mGameplayRightClick;
        private readonly InputAction _mGameplayLeftClick;
        private readonly InputAction _mGameplayMovement;
        private readonly InputAction _mGameplayEscape;
        private readonly InputAction _mGameplayNumKeys;
        public struct GameplayActions
        {
            private @GameControls _mWrapper;
            public GameplayActions(@GameControls wrapper) { _mWrapper = wrapper; }
            public InputAction @Zoom => _mWrapper._mGameplayZoom;
            public InputAction @RightClick => _mWrapper._mGameplayRightClick;
            public InputAction @LeftClick => _mWrapper._mGameplayLeftClick;
            public InputAction @Movement => _mWrapper._mGameplayMovement;
            public InputAction @Escape => _mWrapper._mGameplayEscape;
            public InputAction @NumKeys => _mWrapper._mGameplayNumKeys;
            public InputActionMap Get() { return _mWrapper._mGameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool Enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (_mWrapper._mGameplayActionsCallbackInterface != null)
                {
                    @Zoom.started -= _mWrapper._mGameplayActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnZoom;
                    @RightClick.started -= _mWrapper._mGameplayActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnRightClick;
                    @LeftClick.started -= _mWrapper._mGameplayActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnLeftClick;
                    @Movement.started -= _mWrapper._mGameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnMovement;
                    @Escape.started -= _mWrapper._mGameplayActionsCallbackInterface.OnEscape;
                    @Escape.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnEscape;
                    @NumKeys.started -= _mWrapper._mGameplayActionsCallbackInterface.OnNumKeys;
                    @NumKeys.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnNumKeys;
                    @NumKeys.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnNumKeys;
                }
                _mWrapper._mGameplayActionsCallbackInterface = instance;
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
        private int _mMouseKbSchemeIndex = -1;
        public InputControlScheme MouseKbScheme
        {
            get
            {
                if (_mMouseKbSchemeIndex == -1) _mMouseKbSchemeIndex = Asset.FindControlSchemeIndex("MouseKb");
                return Asset.controlSchemes[_mMouseKbSchemeIndex];
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
            void OnDrag(InputAction.CallbackContext context);
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
