using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menu : MonoBehaviour
{
    public GameObject instr1,instr2,skip1,skip2,returnInstr;
    public GameObject instruction;
    private Animator pauseMenuAnim,blackAnim;
    public GameObject pauseMenu,black;
    void Start()
    {
        if(pauseMenu)
        {
            pauseMenuAnim=GameObject.Find("pauseMenu").GetComponent<Animator>();
        }
        if(black)
        {
            blackAnim=GameObject.Find("black").GetComponent<Animator>();
        }
    }
    public void StartGame()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(1);
    }
    public void Skip()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadNextLevel();
    }
    public void ChangeInstr()
    {
        instr1.SetActive(false);
        skip1.SetActive(false);
        instr2.SetActive(true);
        skip2.SetActive(true);
        returnInstr.SetActive(true);
    }
    public void ReturnInstr()
    {
        instr2.SetActive(false);
        skip2.SetActive(false);
        returnInstr.SetActive(false);
        instr1.SetActive(true);
        skip1.SetActive(true);
    }
    public void Back()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadPreviousLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pauseMenuAnim.Play("pause");
        blackAnim.Play("pause");
        Time.timeScale=0f;
    }

    public void Resume()
    {
        pauseMenuAnim.Play("resume");
        blackAnim.Play("idle");
        Time.timeScale=1f;
    }

    public void Restart()
    {
        Time.timeScale=1f;
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadThisLevel();
    }

    public void Knowledge()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(3);
    }

    public void Oral_cavity()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(3);
    }

    public void Stomach()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(4);
    }

    public void Small_intestine()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(5);
    }

    public void Next1()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(6);
    }

    public void Previous1()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(5);
    }

    public void Large_intestine()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(7);
    }

    public void Next2()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(8);
    }

    public void Previous2()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(7);
    }

    public void Home()
    {
        Time.timeScale=1f;
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(2);
    }
    public void Instruction()
    {
        instruction.SetActive(true);
    }
    public void Return()
    {
        instruction.SetActive(false);
    }
    public void StartPage()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(0);
    }
    public void Level1()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(9);
    }
    public void Level2()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(11);
    }
    public void Level3()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(13);
    }
    public void Level4()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(18);
    }
}