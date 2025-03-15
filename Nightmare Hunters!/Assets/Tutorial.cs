using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TMP_Text instructionText;  // Referens till textkomponenten
    private int currentStep = 0;  // H虱ler reda p・vilket steg vi 舐 p・
    private float stepDuration = 4f;  // Antal sekunder varje steg visas
    private float timer;  // Timer f att byta steg
    private string[] instructions = {
        "Steg 1: Ha bra belyst rum, tracka sedan ett plan genom att rikta enheten mot golvet.",
        "Steg 2: Rikta kameran mot en bild på ett jordklot för att få ditt vapen",
        "Steg 3: Sikta och skjut på monstrerna.",
        "Lycka till!!"
    };

    void Start()
    {
        ShowInstruction(currentStep);  // Visa fsta instruktionen
        timer = stepDuration;  // Starta timer f fsta steget
    }

    void Update()
    {
        // Uppdatera timer varje frame
        timer -= Time.deltaTime;

        // N舐 timern n蚌 0, g・vidare till n舖ta steg
        if (timer <= 0f)
        {
            currentStep++;
            if (currentStep < instructions.Length)
            {
                ShowInstruction(currentStep);
                timer = stepDuration;  // Nollst舁l timer f n舖ta steg
            }
            else
            {
                // Om alla steg 舐 slutfda, ta bort texten
                HideInstruction();
            }
        }
    }

    void ShowInstruction(int step)
    {
        // Visa instruktion baserat p・det aktuella steget
        instructionText.text = instructions[step];
    }

    void HideInstruction()
    {
        // Ta bort texten n舐 alla instruktioner 舐 klara
        instructionText.text = "";
    }
}
