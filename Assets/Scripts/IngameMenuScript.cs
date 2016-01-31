using UnityEngine;
using UnityEngine.UI;
using System;

public class IngameMenuScript : MonoBehaviour {
    public delegate void ToDirtyAction();
    public event ToDirtyAction OnToDirty;

    public delegate void NoTimeAction();
    public event NoTimeAction OnNoTime;

    private Weapon[] weapons;

    public Canvas timeCanvas;
    public Text timeText;

    public Canvas dirtCanvas;
    public Text dirtText;
    public Slider dirtSlider;

    public Canvas weaponCanvas;
    public Text weaponText;

    public uint seconds=300;
    private uint timeLeft=1;
    private DateTime startTime;

    public uint dirty = 0;

    public Text chosenWeaponText;

    public string weapon1Text;
    public Image weapon1Image;
    public Sprite weapon1Sprite;
    public Sprite weapon1ActiveSprite;
    public string weapon2Text;
    public Image weapon2Image;
    public Sprite weapon2Sprite;
    public Sprite weapon2ActiveSprite;
    public string weapon3Text;
    public Image weapon3Image;
    public Sprite weapon3Sprite;
    public Sprite weapon3ActiveSprite;
    public string weapon4Text;
    public Image weapon4Image;
    public Sprite weapon4Sprite;
    public Sprite weapon4ActiveSprite;
    public string weapon5Text;
    public Image weapon5Image;
    public Sprite weapon5Sprite;
    public Sprite weapon5ActiveSprite;

    // Use this for initialization
    void Start () {
        timeCanvas = timeCanvas.GetComponent<Canvas>();
        timeText = timeText.GetComponent<Text>();

        dirtCanvas = dirtCanvas.GetComponent<Canvas>();
        dirtText = dirtText.GetComponent<Text>();
        dirtSlider = dirtSlider.GetComponent<Slider>();

        weaponCanvas = weaponCanvas.GetComponent<Canvas>();
        weaponText = weaponText.GetComponent<Text>();

        startTime = System.DateTime.Now;

        UpdateDirtValue();
        InitWeapons();

        InitWeapons();
        InitWeaponsCanvas();
    }

    // Update is called once per frame
    void Update () {
        CalculateTimeLeft();
        TriggerNoTimeLeftFunction();
        RedrawTimeText();
    }

    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ToggleWeapon();
        //}
    }

    ///////////////////////////////////////////////////////////////////////
    /// Time Section
    ///////////////////////////////////////////////////////////////////////

    private void CalculateTimeLeft()
    {
        if (timeLeft > 0)
        {
            TimeSpan ts = System.DateTime.Now - startTime;
            timeLeft = seconds - Convert.ToUInt32(ts.TotalSeconds);
        }
    }

    private void TriggerNoTimeLeftFunction()
    {
        if (timeLeft == 0)
        {
            //Description in https://unity3d.com/learn/tutorials/modules/intermediate/scripting/events
            if (OnNoTime != null) OnNoTime();
        }
    }

    private void RedrawTimeText()
    {
        uint minutes = timeLeft / 60;
        uint seconds = timeLeft % 60;

        timeText.text = minutes + ":" + ((seconds < 10) ? "0" : "") + seconds;
    }

    ///////////////////////////////////////////////////////////////////////
    /// Dirt Section
    ///////////////////////////////////////////////////////////////////////

    public void AddDirt(uint dirt)
    {
        dirty += dirt;

        if (dirty >= 100)
        {
            dirty = 100;
            TriggerToDirty();
        }

        UpdateDirtValue();
    }

    public void CleanDirt(uint dirt)
    {
        if (dirty - dirt < 0)
        {
            dirty = 0;
            UpdateDirtValue();
            return;
        }

        dirty -= dirt;
        UpdateDirtValue();
    }

    private void UpdateDirtValue()
    {
        Debug.Log(dirty);
        dirtText.text = (dirty < 10 ? "0":"") + dirty + "%";
        dirtSlider.normalizedValue = dirty / 100.0f;
    }

    private void TriggerToDirty()
    {
        //Description in https://unity3d.com/learn/tutorials/modules/intermediate/scripting/events
        if (OnToDirty != null) OnToDirty();
    }

    ///////////////////////////////////////////////////////////////////////
    /// Weapon Section
    ///////////////////////////////////////////////////////////////////////

    public Weapon ToggleWeapon()
    {
        Weapon currentlyActiveWeapon = null;
        bool foundActive = false;
        foreach (Weapon weapon in weapons)
        {
            if (weapon.active)
            {
                currentlyActiveWeapon = weapon;
                InactivateWeapon(weapon);
                foundActive = true;
                continue;
            }

            if (foundActive)
            {
                if (weapon.available)
                {
                    ActivateWeapon(weapon);
                    return weapon;
                }
            }
        }

        foreach (Weapon weapon in weapons)
        {
            if (weapon.available && ((weapon.ammo > 0) || weapon.ammo == -1))
            {
                ActivateWeapon(weapon);
                return weapon;
            }

            if (weapon == currentlyActiveWeapon)
            {
                ActivateWeapon(weapon);
                return weapon;
            }
        }

        chosenWeaponText.text = "";
        return null;
    }

    public void UnlockWeapon(string weaponName)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.text == weaponName)
            {
                weapon.available = true;
                WeaponAvailable(weapon);
                AddAmmo(weapon.text, 10);
            }
        }
    }

    public void AddAmmo(string weaponName, uint ammount)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weaponName == weapon.text && weapon.ammo != -1)
            {
                weapon.ammo += (int)ammount;
                WeaponAvailable(weapon);
            }
        }
    }

    public void FireWeapon()
    {
        Weapon weapon = getActiveWeapon();
        if (weapon != null && weapon.ammo != -1 && weapon.ammo > 0)
        {
            weapon.ammo--;

            if (weapon.ammo == 0)
            {
                WeaponUnavailable(weapon);
                ToggleWeapon();
            }
        }
    }

    public Weapon getActiveWeapon()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.active)
            {
                return weapon;
            }
        }

        return null;
    }

    private void WeaponUnavailable(Weapon weapon)
    {
        weapon.image.color = new Color(1f, 1f, 1f, 0.2f);
        weapon.available = false;
    }

    private void WeaponAvailable(Weapon weapon)
    {
        weapon.image.color = new Color(1f, 1f, 1f, 1f);
        weapon.available = true;
    }

    private void InitWeapons()
    {
        weapons = new Weapon[5];

        Weapon weapon1 = new Weapon();
        weapon1.text = weapon1Text;
        weapon1.image = weapon1Image;
        weapon1.inactiveSprite = weapon1Sprite;
        weapon1.activeSprite = weapon1ActiveSprite;
        weapon1.ammo = -1;
        weapon1.active = true;
        weapon1.available = true;
        weapons[0] = weapon1;

        chosenWeaponText.text = weapon1.text;

        Weapon weapon2 = new Weapon();
        weapon2.text = weapon2Text;
        weapon2.image = weapon2Image;
        weapon2.inactiveSprite = weapon2Sprite;
        weapon2.activeSprite = weapon2ActiveSprite;
        weapon2.ammo = -1;
        weapon2.active = false;
        weapon2.available = true;
        weapons[1] = weapon2;
        if (!weapon2.available) WeaponUnavailable(weapon2);

        Weapon weapon3 = new Weapon();
        weapon3.text = weapon3Text;
        weapon3.image = weapon3Image;
        weapon3.inactiveSprite = weapon3Sprite;
        weapon3.activeSprite = weapon3ActiveSprite;
        weapon3.ammo = -1;
        weapon3.active = false;
        weapon3.available = true;
        weapons[2] = weapon3;
        if (!weapon3.available) WeaponUnavailable(weapon3);

        Weapon weapon4 = new Weapon();
        weapon4.text = weapon4Text;
        weapon4.image = weapon4Image;
        weapon4.inactiveSprite = weapon4Sprite;
        weapon4.activeSprite = weapon4ActiveSprite;
        weapon4.ammo = -1;
        weapon4.active = false;
        weapon4.available = true;
        weapons[3] = weapon4;
        if (!weapon4.available) WeaponUnavailable(weapon4);

        Weapon weapon5 = new Weapon();
        weapon5.text = weapon5Text;
        weapon5.image = weapon5Image;
        weapon5.inactiveSprite = weapon5Sprite;
        weapon5.activeSprite = weapon5ActiveSprite;
        weapon5.ammo = -1;
        weapon5.active = false;
        weapon5.available = true;
        weapons[4] = weapon5;
        if (!weapon5.available) WeaponUnavailable(weapon5);
    }

    private void InitWeaponsCanvas()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.active)
            {
                weapon.image.sprite = weapon.activeSprite;
            } else
            {
                weapon.image.sprite = weapon.inactiveSprite;
            }
        }
    }

    private void ActivateWeapon(Weapon weapon)
    {
        weapon.active = true;
        chosenWeaponText.text = weapon.text;
        weapon.image.sprite = weapon.activeSprite;
    }

    private void InactivateWeapon(Weapon weapon)
    {
        weapon.active = false;
        weapon.image.sprite = weapon.inactiveSprite;
        chosenWeaponText.text = "";
    }
}

public class Weapon
{
    public string text { get; set; }
    public Image image { get; set; }
    public Sprite inactiveSprite { get; set; }
    public Sprite activeSprite { get; set; }
    public int ammo { get; set; }
    public bool active { get; set; }
    public bool available { get; set; }
}