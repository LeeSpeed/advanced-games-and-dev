using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuManager : MonoBehaviour {

    public GameObject createGame, joinGame, lobbyView2;

	public void OnCreate()
    {
        createGame.SetActive(true);
        lobbyView2.SetActive(false);
    }

    public void OnJoin()
    {
        joinGame.SetActive(true);
        lobbyView2.SetActive(false);
        joinGame.transform.SetAsLastSibling();
    }

}
