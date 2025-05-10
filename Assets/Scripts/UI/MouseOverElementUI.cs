using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverElementUI : MonoBehaviour
{
    public static MouseOverElementUI instance;
    [SerializeField] Transform panel;
    [SerializeField] TMPro.TextMeshProUGUI title, description;
    [SerializeField] float positionY = 5f;
    private void Start()
    {
        instance = this;
        Hide();
    }


    public void SetTitle( string text)
    {
        title.text = text;
    }

    public void SetDescription( string text )
    {
        description.text = text;
    }

    public void Show( string title, string description, Vector3 position )
    {
        SetTitle( title );
        SetDescription( description );
        panel.gameObject.transform.position = new Vector3 (position.x, position.y + positionY, position.z);
        panel.gameObject.SetActive( true );
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }
}
