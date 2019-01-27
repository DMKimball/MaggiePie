using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private string m_sMainMenuLevel;
    [SerializeField] private string[] m_sTargetLevels;
    [SerializeField] private bool m_bStartHidden;

    private bool m_DisplayUI = false;

    private Image m_Panel;
    private Image[] m_UIImages;
    private Text[] m_UIText;
    private Button[] m_UIButtons;

    // Start is called before the first frame update
    void Start()
    {
        m_DisplayUI = !m_bStartHidden;

        m_Panel = GetComponent<Image>();
        m_UIImages = GetComponentsInChildren<Image>();
        m_UIText = GetComponentsInChildren<Text>();
        m_UIButtons = GetComponentsInChildren<Button>();

        UpdateUIHidden();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            m_DisplayUI = !m_DisplayUI;
            UpdateUIHidden();
        }
    }

    public void UpdateUIHidden()
    {
        if (m_Panel)
        {
            m_Panel.enabled = m_DisplayUI;
        }
        foreach (Image image in m_UIImages)
        {
            image.enabled = m_DisplayUI;
        }
        foreach (Text text in m_UIText)
        {
            text.enabled = m_DisplayUI;
        }
        foreach (Button button in m_UIButtons)
        {
            button.enabled = m_DisplayUI;
        }
    }

    public void SetFade( float alpha )
    {
        if (m_Panel)
        {
            Color c = m_Panel.color;
            c.a = alpha;
            m_Panel.color = c;
        }
        foreach (Image image in m_UIImages)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
        foreach (Text text in m_UIText)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }
    }

    public void LoadLevel( int index )
    {
        if (index < m_sTargetLevels.Length)
        {
            SceneManager.LoadScene(m_sTargetLevels[index]);
        }
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(m_sMainMenuLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
