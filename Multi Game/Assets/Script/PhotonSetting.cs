using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonSetting : MonoBehaviour
{
    public InputField region;
    public InputField loginEmail;
    public InputField loginPassword;
    public InputField signupEmail;
    public InputField signupPassword;
    public InputField username;
    public Canvas login;
    public Canvas signup;

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.GameVersion = "1.0f";
        PhotonNetwork.NickName = "junyoung";
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;
        PhotonNetwork.LoadLevel("Photon Lobby");
    }
    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("로그인 실패");
    }
    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원 가입 성공");
        signup.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
        
    }
    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원 가입 실패");
    }

    public void Createaccount()
    {
        login.gameObject.SetActive(false);
        signup.gameObject.SetActive(true);
    }

    public void Letlogin()
    {
        signup.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
    }

    public  void SignUp()
    {
        var request = new RegisterPlayFabUserRequest { Email = signupEmail.text, Password = signupPassword.text, Username = username.text };

        PlayFabClientAPI.RegisterPlayFabUser(request, SignUpSuccess, SignUpFailure);
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = loginEmail.text, Password = loginPassword.text };

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailure);
    }
    
}
