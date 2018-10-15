using UnityEngine;
using TMPro;

public class CreateRoom : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _roomName;
    private TextMeshProUGUI RoomName
   {
        get { return _roomName; }
    }


    public void OnClick_CreateRoom()
{

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default)) 
        {
            print("create sent");
        }
        else
        {
            print("failed to create");
        }
}

        private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("create room failed" + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {

        print("Room Create succesfully");
    }
  

}
