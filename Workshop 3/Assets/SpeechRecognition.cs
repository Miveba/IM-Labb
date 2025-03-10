#if UNITY_WSA && !UNITY_EDITOR
using UnityEngine;
using Windows.Media.SpeechRecognition;
using System.Threading.Tasks;

public class VoiceControl : MonoBehaviour
{
    private SpeechRecognizer recognizer;

    void Start()
    {
        InitializeVoiceRecognition();
    }

    async void InitializeVoiceRecognition()
    {
        recognizer = new SpeechRecognizer();
        await recognizer.CompileConstraintsAsync();
        recognizer.ContinuousRecognitionSession.ResultGenerated += RecognitionResult;
        recognizer.ContinuousRecognitionSession.StartAsync();
    }

    private void RecognitionResult(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
    {
        string command = args.Result.Text.ToLower();
        Debug.Log("Recognized command: " + command);

        switch (command)
        {
            case "start":
                // Här kan du ladda en scen eller starta spelet.
                Debug.Log("Game started");
                break;
            case "pause":
                // Pausar spelet
                Time.timeScale = 0;
                break;
            case "resume":
                // Återupptar spelet
                Time.timeScale = 1;
                break;
            case "exit":
                // Stänger spelet
                Application.Quit();
                break;
        }
    }

    void OnApplicationQuit()
    {
        if (recognizer != null)
        {
            recognizer.Dispose();
        }
    }
}
#endif
