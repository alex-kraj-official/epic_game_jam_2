using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public float money;
    public float sheep;
    public float wood;
    public float people;
    public static float maxPeople;
    public float ore;
	public float wheat;


    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI sheepText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI peopleText;
    public TextMeshProUGUI maxPeopleText;
    public TextMeshProUGUI oreText;
    public TextMeshProUGUI wheatText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            getSheep(10f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            removeSheep(10f);
        }
    }
    public void updateText(TextMeshProUGUI text, float number)
    {
        text.SetText(number.ToString());
    }
    public void getSheep(float amount)
    {
        sheep = sheep + amount;
        updateText(sheepText,sheep);
    }
    public void removeSheep(float amount)
    {
        sheep = sheep - amount;
        updateText(sheepText, sheep);
    }
    public void getGold(float amount)
    {
        money = money + amount;
        updateText(moneyText,money);
    }
    public void removeGold(float amount)
    {
        money = money - amount;
        updateText(moneyText, money);
    }
    public void getWood(float amount)
    {
        wood = wood + amount;
        updateText(woodText, wood);
    }
    public void removeWood(float amount)
    {
        wood = wood - amount;
        updateText(woodText, wood);
    }
    public void getPeople(float amount)
    {
        people = people + amount;
        updateText(peopleText, people);
    }
    public void removePeople(float amount)
    {
        people = people - amount;
        updateText(peopleText, people);
    }
    public void getOre(float amount)
    {
        ore = ore + amount;
        updateText(oreText, ore);
    }
    public void removeOre(float amount)
    {
        ore = ore - amount;
        updateText(oreText, ore);
    }
    public void getWheat(float amount)
    {
        wheat = wheat + amount;
        updateText(wheatText, wheat);
    }
    public void removeWheat(float amount)
    {
        wheat = wheat - amount;
        updateText(wheatText, wheat);
    }

}
