using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CreatureSelectUi : MonoBehaviour
{
    private Button _playBtn;
    private Button _backBtn;
    private Button _player3ActiveBtn;
    private Button _player4ActiveBtn;
    private Button _player5ActiveBtn;
    private Button _player1EditBtn;
    private Button _player2EditBtn;
    private Button _player3EditBtn;
    private Button _player4EditBtn;
    private Button _player5EditBtn;
    private Button _skin1Btn;
    private Button _skin2Btn;
    private Button _skin3Btn;
    private Button _skin4Btn;
    private Label _title;
    [SerializeField] private List<Animator> _playerAnimatorList;
    [SerializeField] private SelectionUIManager selectionManager;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _playBtn = root.Q<Button>("playBtn");
        _backBtn = root.Q<Button>("menuBtn");
        _player3ActiveBtn = root.Q<Button>("player3btn");
        _player4ActiveBtn = root.Q<Button>("player4btn");
        _player5ActiveBtn = root.Q<Button>("player5btn");
        _player1EditBtn = root.Q<Button>("p1SelectBtn");
        _player2EditBtn = root.Q<Button>("p2SelectBtn");
        _player3EditBtn = root.Q<Button>("p3SelectBtn");
        _player4EditBtn = root.Q<Button>("p4SelectBtn");
        _player5EditBtn = root.Q<Button>("p5SelectBtn");
        _playerAnimatorList[0].SetBool("Active", true);
        _playerAnimatorList[1].SetBool("Active", true);
        _playerAnimatorList[2].SetBool("Active", false);
        _playerAnimatorList[3].SetBool("Active", false);
        _playerAnimatorList[4].SetBool("Active", false);

        _playBtn.RegisterCallback<ClickEvent>(OnClickPlay);
        _backBtn.RegisterCallback<ClickEvent>(OnClickBack);
    }
    private void OnClickPlay(ClickEvent evt)
    {
        selectionManager.ReadyButtonRoutine();
        SceneManager.LoadScene("Game");
    }
    private void OnClickBack(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnClickp1Edit(ClickEvent evt)
    {

    }
    private void OnClickp2Edit(ClickEvent evt)
    {

    }
}
