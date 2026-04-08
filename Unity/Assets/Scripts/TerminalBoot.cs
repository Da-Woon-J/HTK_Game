using System.Collections;
using TMPro;
using UnityEngine;

public class TerminalBoot : MonoBehaviour
{
    public TextMeshProUGUI terminalText;

    void Start()
    {
        StartCoroutine(BootSequence());
    }

    IEnumerator BootSequence()
    {
        terminalText.text = "";

        yield return new WaitForSeconds(0.5f);
        yield return TypeLine("COMPANY SYSTEM v0.9.17\n", 0.02f);

        yield return new WaitForSeconds(0.2f);
        yield return TypeLine("> LOGIN SUCCESSFUL\n\n", 0.02f);

        yield return new WaitForSeconds(0.2f);
        yield return TypeLine("> SELECT TASK:\n\n", 0.02f);

        yield return new WaitForSeconds(0.2f);
        yield return TypeLine("[1] NEW GAME\n\n", 0.02f);
        yield return TypeLine("[2] CONTINUE\n\n", 0.02f);
        yield return TypeLine("[3] OPTIONS\n\n", 0.02f);
        yield return TypeLine("[4] EXIT", 0.02f);
    }

    IEnumerator TypeLine(string line, float delay)
    {
        foreach (char c in line)
        {
            terminalText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}