// GENERATED AUTOMATICALLY FROM 'Assets/Player/Scripts/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMain"",
            ""id"": ""7f92eb3f-e0a3-40ba-92c7-ba36e81fd80c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ccbc27d5-8b8c-4cb1-9d12-fff5050176ef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""d8fe9b9a-1273-47a6-9b79-b42f2ad2eeab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Energy Skill 1"",
                    ""type"": ""Button"",
                    ""id"": ""0af7098f-affe-4e18-b6d7-9efb94f8060f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Energy Skill 2"",
                    ""type"": ""Button"",
                    ""id"": ""20ccd738-0da1-45dd-8e31-480ea6e8eac6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stamina Skill"",
                    ""type"": ""Button"",
                    ""id"": ""e534ce3d-9846-4da7-acb4-b4da7ddb7717"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch Projectile (Debug)"",
                    ""type"": ""Button"",
                    ""id"": ""3db333b4-5c7a-45e3-b51b-227929e61f10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""451cdf40-6c08-47fc-b64f-d948507ef9bf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""2947c8ac-267e-498f-86a0-e8aa92d2b1a0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0c923f16-d3b2-408c-9f72-6d6d0412ad91"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3e432024-3ebb-4f55-ae7a-8d6db8b6f283"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9186d657-931f-443a-83d7-9dba85037896"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c4cf8d8d-5e0b-452e-8994-ca8046ed5be4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f4135ac3-5fca-4a0b-a9a7-23f5a050f371"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d2c6a37-0c84-44a5-adf4-d5978c6f91b9"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Energy Skill 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ea55010-8781-4933-a10b-ad0739713f70"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Energy Skill 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32fd27dc-5c60-4124-8a57-b8dc4c322cfb"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stamina Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b18a15e3-3aeb-472d-afc3-577b7f98b33d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch Projectile (Debug)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMain
        m_PlayerMain = asset.FindActionMap("PlayerMain", throwIfNotFound: true);
        m_PlayerMain_Move = m_PlayerMain.FindAction("Move", throwIfNotFound: true);
        m_PlayerMain_Attack = m_PlayerMain.FindAction("Attack", throwIfNotFound: true);
        m_PlayerMain_EnergySkill1 = m_PlayerMain.FindAction("Energy Skill 1", throwIfNotFound: true);
        m_PlayerMain_EnergySkill2 = m_PlayerMain.FindAction("Energy Skill 2", throwIfNotFound: true);
        m_PlayerMain_StaminaSkill = m_PlayerMain.FindAction("Stamina Skill", throwIfNotFound: true);
        m_PlayerMain_SwitchProjectileDebug = m_PlayerMain.FindAction("Switch Projectile (Debug)", throwIfNotFound: true);
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

    // PlayerMain
    private readonly InputActionMap m_PlayerMain;
    private IPlayerMainActions m_PlayerMainActionsCallbackInterface;
    private readonly InputAction m_PlayerMain_Move;
    private readonly InputAction m_PlayerMain_Attack;
    private readonly InputAction m_PlayerMain_EnergySkill1;
    private readonly InputAction m_PlayerMain_EnergySkill2;
    private readonly InputAction m_PlayerMain_StaminaSkill;
    private readonly InputAction m_PlayerMain_SwitchProjectileDebug;
    public struct PlayerMainActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMainActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMain_Move;
        public InputAction @Attack => m_Wrapper.m_PlayerMain_Attack;
        public InputAction @EnergySkill1 => m_Wrapper.m_PlayerMain_EnergySkill1;
        public InputAction @EnergySkill2 => m_Wrapper.m_PlayerMain_EnergySkill2;
        public InputAction @StaminaSkill => m_Wrapper.m_PlayerMain_StaminaSkill;
        public InputAction @SwitchProjectileDebug => m_Wrapper.m_PlayerMain_SwitchProjectileDebug;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMain; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMainActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMainActions instance)
        {
            if (m_Wrapper.m_PlayerMainActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Attack.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnAttack;
                @EnergySkill1.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnEnergySkill1;
                @EnergySkill1.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnEnergySkill1;
                @EnergySkill1.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnEnergySkill1;
                @EnergySkill2.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnEnergySkill2;
                @EnergySkill2.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnEnergySkill2;
                @EnergySkill2.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnEnergySkill2;
                @StaminaSkill.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnStaminaSkill;
                @StaminaSkill.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnStaminaSkill;
                @StaminaSkill.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnStaminaSkill;
                @SwitchProjectileDebug.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnSwitchProjectileDebug;
                @SwitchProjectileDebug.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnSwitchProjectileDebug;
                @SwitchProjectileDebug.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnSwitchProjectileDebug;
            }
            m_Wrapper.m_PlayerMainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @EnergySkill1.started += instance.OnEnergySkill1;
                @EnergySkill1.performed += instance.OnEnergySkill1;
                @EnergySkill1.canceled += instance.OnEnergySkill1;
                @EnergySkill2.started += instance.OnEnergySkill2;
                @EnergySkill2.performed += instance.OnEnergySkill2;
                @EnergySkill2.canceled += instance.OnEnergySkill2;
                @StaminaSkill.started += instance.OnStaminaSkill;
                @StaminaSkill.performed += instance.OnStaminaSkill;
                @StaminaSkill.canceled += instance.OnStaminaSkill;
                @SwitchProjectileDebug.started += instance.OnSwitchProjectileDebug;
                @SwitchProjectileDebug.performed += instance.OnSwitchProjectileDebug;
                @SwitchProjectileDebug.canceled += instance.OnSwitchProjectileDebug;
            }
        }
    }
    public PlayerMainActions @PlayerMain => new PlayerMainActions(this);
    public interface IPlayerMainActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnEnergySkill1(InputAction.CallbackContext context);
        void OnEnergySkill2(InputAction.CallbackContext context);
        void OnStaminaSkill(InputAction.CallbackContext context);
        void OnSwitchProjectileDebug(InputAction.CallbackContext context);
    }
}
