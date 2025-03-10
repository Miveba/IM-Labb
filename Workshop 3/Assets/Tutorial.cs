using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TMP_Text instructionText;  // Referens till textkomponenten
    private int currentStep = 0;  // Håller reda på vilket steg vi är på
    private float stepDuration = 10f;  // Antal sekunder varje steg visas
    private float timer;  // Timer för att byta steg
    private string[] instructions = {
        "Steg 1: Ha bra belyst rum, tracka sedan ett plan genom att rikta enheten mot golvet.",
        "Steg 2: Rikta kameran mot en bild på ett jordklot för att få ditt vapen",
        "Steg 3: Sikta och skjut på monstrerna.",
        "Lycka till!!"
    };

    void Start()
    {
        ShowInstruction(currentStep);  // Visa första instruktionen
        timer = stepDuration;  // Starta timer för första steget
    }

    void Update()
    {
        // Uppdatera timer varje frame
        timer -= Time.deltaTime;

        // När timern når 0, gå vidare till nästa steg
        if (timer <= 0f)
        {
            currentStep++;
            if (currentStep < instructions.Length)
            {
                ShowInstruction(currentStep);
                timer = stepDuration;  // Nollställ timer för nästa steg
            }
            else
            {
                // Om alla steg är slutförda, ta bort texten
                HideInstruction();
            }
        }
    }

    void ShowInstruction(int step)
    {
        // Visa instruktion baserat på det aktuella steget
        instructionText.text = instructions[step];
    }

    void HideInstruction()
    {
        // Ta bort texten när alla instruktioner är klara
        instructionText.text = "";
    }
}
