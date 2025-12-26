using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.Rendering.ReloadAttribute;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public Rigidbody2D theRB;
    public float moveSpeed;

    public InputActionReference moveInput, actioninput;

    public Animator anim;

    public enum ToolType
    {
        plough,
        wateringCan,
        seeds,
        basket,
        fishingRod,
        axe,
        pickaxe,
        count
    }

    public ToolType currentTool;

    public float toolWaitTime = 0.5f;
    private float toolWaitCounter;

    public Transform toolIndicator;
    public float toolRange = 3f;

    public CropController.CropType seedCropType;

    public GameObject axe;
    public AxeController axeController;

    public GameObject pickaxe;
    public PickaxeController pickaxeController;

    public GameObject fishingRod;
    public FishingRodController fishingRodController;
    public LayerMask fishingLayer;
    public LayerMask fishingBlockerLayer;
    public LayerMask fishingBonusLayer;

    public float tiredEmoteShowTime = 1.0f;
    public GameObject tiredCallout;
    private float tiredEmoteShowTimer;

    public float getHitEmoteShowTime = 0.5f;
    public GameObject getHitCallout;
    private float getHitEmoteShowTimer;

    private bool isWalkingSFXPlayed;

    private bool isExhausted => TimeController.instance.isDayEnded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        isWalkingSFXPlayed = false;

        UIController.instance.SwitchTool((int)currentTool);

        UIController.instance.SwitchSeed(seedCropType);
    }

    // Update is called once per frame
    void Update()
    {
        if (tiredEmoteShowTimer > 0)
        {
            tiredEmoteShowTimer -= Time.deltaTime;
            if (tiredEmoteShowTimer <= 0)
            {
                tiredCallout.SetActive(false);
            }
        }

        if (getHitEmoteShowTimer > 0)
        {
            getHitEmoteShowTimer -= Time.deltaTime;
            if (getHitEmoteShowTimer <= 0)
            {
                getHitCallout.SetActive(false);
            }
        }

        if (UIController.instance != null)
        {
            if(UIController.instance.theIC != null)
            {
                if(UIController.instance.theIC.gameObject.activeSelf == true)
                {
                    theRB.linearVelocity = Vector2.zero;
                    return;
                }    
            }

            if (UIController.instance.theShop != null)
            {
                if (UIController.instance.theShop.gameObject.activeSelf == true)
                {
                    theRB.linearVelocity = Vector2.zero;
                    return;
                }
            }

            if (UIController.instance.pauseScreen != null)
            {
                if (UIController.instance.pauseScreen.gameObject.activeSelf == true)
                {
                    theRB.linearVelocity = Vector2.zero;
                    return;
                }
            }
        }

        if (toolWaitCounter > 0)
        {
            toolWaitCounter -= Time.deltaTime;
            theRB.linearVelocity = Vector2.zero;
        }
        else
        {
            //theRB.linearVelocity = new Vector2(moveSpeed, 0f);
            theRB.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

            if (!fishingRodController.isCast)
            {
                if (theRB.linearVelocity.x < 0f)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (theRB.linearVelocity.x > 0f)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }

        if (!fishingRodController.isCast)
        {
            bool hasSwitchedTool = false;

            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                currentTool++;

                if ((int)currentTool >= (int)ToolType.count)
                {
                    currentTool = ToolType.plough;
                }
                hasSwitchedTool = true;
            }

            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                currentTool = ToolType.plough;
                hasSwitchedTool = true;
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                currentTool = ToolType.wateringCan;
                hasSwitchedTool = true;
            }
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                currentTool = ToolType.seeds;
                hasSwitchedTool = true;
            }
            if (Keyboard.current.digit4Key.wasPressedThisFrame)
            {
                currentTool = ToolType.basket;
                hasSwitchedTool = true;
            }
            if (Keyboard.current.digit5Key.wasPressedThisFrame)
            {
                currentTool = ToolType.fishingRod;
                hasSwitchedTool = true;
            }
            if (Keyboard.current.digit6Key.wasPressedThisFrame)
            {
                currentTool = ToolType.axe;
                hasSwitchedTool = true;
            }
            if (Keyboard.current.digit7Key.wasPressedThisFrame)
            {
                currentTool = ToolType.pickaxe;
                hasSwitchedTool = true;
            }
            if (hasSwitchedTool == true)
            {
                UIController.instance.SwitchTool((int) currentTool);
            }
        }

        if (currentTool != ToolType.fishingRod && fishingRod.activeSelf == true) fishingRod.SetActive(false);
        if (currentTool == ToolType.fishingRod && fishingRod.activeSelf == false) fishingRod.SetActive(true);

        if (currentTool != ToolType.axe && axe.activeSelf == true) axe.SetActive(false);
        if (currentTool == ToolType.axe && axe.activeSelf == false) axe.SetActive(true);

        if (currentTool != ToolType.pickaxe && pickaxe.activeSelf == true) pickaxe.SetActive(false);
        if (currentTool == ToolType.pickaxe && pickaxe.activeSelf == false) pickaxe.SetActive(true);


        // PREVENTING MOVING WHILE FISHING
        if (fishingRodController.isCast)
        {
            theRB.linearVelocity = Vector2.zero;
        }

        float speed = theRB.linearVelocity.magnitude;
        anim.SetFloat("speed", speed);
        if (speed > 0)
        {
            if (isWalkingSFXPlayed == false)
            {
                AudioManager.instance.PlaySFX(1);
                isWalkingSFXPlayed = true;
            }
        }
        else
        {
            if (isWalkingSFXPlayed == true)
            {
                AudioManager.instance.StopSFX(1);
                isWalkingSFXPlayed = false;
            }
        }

        if (GridController.instance != null)
        {
            if (actioninput.action.WasPressedThisFrame())
            {
                UseTool();
            }


            toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

            if (Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
            {
                Vector2 direction = toolIndicator.position - transform.position;
                direction = direction.normalized * toolRange;
                toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
            }

            toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + .5f, Mathf.FloorToInt(toolIndicator.position.y) + .5f, 0f);
        }
        else
        {
            toolIndicator.position = new Vector3(0, 0, -20f);
        }

        // USING FISHING ROD
        if (currentTool == ToolType.fishingRod && Mouse.current.leftButton.wasPressedThisFrame)
        {
            UseFishingRod();
        }

        // USING AXE
        if (currentTool == ToolType.axe && Mouse.current.leftButton.wasPressedThisFrame)
        {
            UseAxe();
        }

        // USING PICKAXE
        if (currentTool == ToolType.pickaxe && Mouse.current.leftButton.wasPressedThisFrame)
        {
            UsePickaxe();
        }

    }

    void UsePickaxe()
    {
        if (isExhausted)
        {
            ShowTiredEmote();
            return;
        }
        pickaxeController.Use();
    }

    void UseAxe()
    {
        if (isExhausted)
        {
            ShowTiredEmote();
            return;
        }
        axeController.Use();
    }

    void UseFishingRod()
    {
        if (!fishingRodController.isCast)
        {
            if (isExhausted)
            {
                ShowTiredEmote();
                return;
            }

            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mousePos.z = 0f;

            bool hitFishing = Physics2D.OverlapPoint(mousePos, fishingLayer);
            bool hitBlocker = Physics2D.OverlapPoint(mousePos, fishingBlockerLayer);
            bool hitBonus = Physics2D.OverlapPoint(mousePos, fishingBonusLayer);

            if (hitBonus)
            {
                fishingRodController.isInBonusZone = true;
            }
            else
            {
                fishingRodController.isInBonusZone = false;
            }

            if (hitFishing && !hitBlocker)
            {
                fishingRodController.Cast(mousePos);
            }
        }
        else
        {
            fishingRodController.Retract();
        }
    }

    void UseTool()
    {
        if (isExhausted)
        {
            ShowTiredEmote();
            return;
        }

        GrowBlock block = null;

        //block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        block = GridController.instance.GetBlock(toolIndicator.position.x - .5f, toolIndicator.position.y - .5f);

        toolWaitCounter = toolWaitTime;

        if (block != null)
        {
            switch(currentTool)
            {
                case ToolType.plough:
                    block.PloughSoil();
                    anim.SetTrigger("usePlough");
                    break;

                case ToolType.wateringCan:
                    block.WaterSoil();
                    anim.SetTrigger("useWateringCan");
                    break;

                case ToolType.seeds:

                    if (CropController.instance.GetCropInfo(seedCropType).seedAmount > 0)
                    {
                        block.PlantCrop(seedCropType);

                        //CropController.instance.UseSeed(seedCropType);
                    }
                    break;

                case ToolType.basket:
                    block.HarvestCrop();
                    break;
            }
        }
    }

    void ShowTiredEmote()
    {
        tiredEmoteShowTimer = tiredEmoteShowTime;
        tiredCallout.SetActive(true);
    }
    void ShowGetHitEmote()
    {
        getHitEmoteShowTimer = getHitEmoteShowTime;
        getHitCallout.SetActive(true);
    }


    public void SwitchSeed(CropController.CropType newSeed)
    {
        seedCropType = newSeed;
    }

    public void TakeHit()
    {
        ShowGetHitEmote();

        // - stamina
    }
}
