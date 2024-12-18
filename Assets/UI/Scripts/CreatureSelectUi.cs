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

    public List<Button> activePlayers;
    [SerializeField] private SelectionUIManager selectionManager;
    [SerializeField] private Material[] _playersMaterial;
    private Material _selectedMaterial;
    [SerializeField] private AudioClip _buttonConfirmation;
    [SerializeField] private AudioClip _buttonDenial;
    [SerializeField] private AudioClip _bgmClip;
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
        _skin1Btn = root.Q<Button>("skin1-btn");
        _skin2Btn = root.Q<Button>("skin2-btn");
        _skin3Btn = root.Q<Button>("skin3-btn");
        _skin4Btn = root.Q<Button>("skin4-btn");
        _title = root.Q<Label>("customization-title");

        _playBtn.RegisterCallback<ClickEvent>(OnClickPlay);
        _backBtn.RegisterCallback<ClickEvent>(OnClickBack);
        _player3ActiveBtn.RegisterCallback<ClickEvent>(OnClickActivep3);
        _player4ActiveBtn.RegisterCallback<ClickEvent>(OnClickActivep4);
        _player5ActiveBtn.RegisterCallback<ClickEvent>(OnClickActivep5);
        _player1EditBtn.RegisterCallback<ClickEvent>(OnClickp1Edit);
        _player2EditBtn.RegisterCallback<ClickEvent> (OnClickp2Edit);
        _player3EditBtn.RegisterCallback<ClickEvent>(OnClickp3Edit);
        _player4EditBtn.RegisterCallback<ClickEvent>(OnClickp4Edit);
        _player5EditBtn.RegisterCallback<ClickEvent>(OnClickp5Edit);
        _skin1Btn.RegisterCallback<ClickEvent>(OnClickSkin1);
        _skin2Btn.RegisterCallback<ClickEvent>(OnClickSkin2);
        _skin3Btn.RegisterCallback<ClickEvent>(OnClickSkin3);
        _skin4Btn.RegisterCallback<ClickEvent>(OnClickSkin4);
        SoundManagerSO.PlayBGMusicClip(_bgmClip, transform.position, 1);
    }
    private void OnClickPlay(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.ReadyButtonRoutine();
        SceneManager.LoadScene("Game");
    }
    private void OnClickBack(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonDenial, transform.position, 1);
        SceneManager.LoadScene("MainMenu");
    }
    private void OnClickp1Edit(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.ChangeSelection(0);
        _title.text = "Customizando Jogador 1";
    }
    private void OnClickp2Edit(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.ChangeSelection(1);
        _title.text = "Customizando Jogador 2";
    }
    private void OnClickp3Edit(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.ChangeSelection(2);
        _title.text = "Customizando Jogador 3";
    }
    private void OnClickp4Edit(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.ChangeSelection(3);
        _title.text = "Customizando Jogador 4";
    }
    private void OnClickp5Edit(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.ChangeSelection(4);
        _title.text = "Customizando Jogador 5";
    }
    private void OnClickSkin1(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.UpdateTexture(0);
    }
    private void OnClickSkin2(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.UpdateTexture(1);
    }
    private void OnClickSkin3(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.UpdateTexture(2);
    }
    private void OnClickSkin4(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        selectionManager.UpdateTexture(3);
    }
    private void OnClickActivep3(ClickEvent evt)
    {
        if (selectionManager.players.Contains(Owner.P3))
        {
            SoundManagerSO.PlaySoundFXClip(_buttonDenial, transform.position, 1);
            _player3ActiveBtn.RemoveFromClassList("internal-player-active-button");
            _player3ActiveBtn.AddToClassList("internal-player-inactive-button");
            _player3ActiveBtn.text = "Inativo";
            selectionManager.RemoveOwnerOnList(Owner.P3);
        }
        else
        {
            SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
            _player3ActiveBtn.AddToClassList("internal-player-active-button");
            _player3ActiveBtn.RemoveFromClassList("internal-player-inactive-button");
            _player3ActiveBtn.text = "Jogador\n3";
            selectionManager.AddOwnerOnList(Owner.P3);
        }
    }
    private void OnClickActivep4(ClickEvent evt)
    {
        if (selectionManager.players.Contains(Owner.P4))
        {
            SoundManagerSO.PlaySoundFXClip(_buttonDenial, transform.position, 1);
            _player4ActiveBtn.RemoveFromClassList("internal-player-active-button");
            _player4ActiveBtn.AddToClassList("internal-player-inactive-button");
            _player4ActiveBtn.text = "Inativo";
            selectionManager.RemoveOwnerOnList(Owner.P4);
        }
        else
        {
            SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
            _player4ActiveBtn.AddToClassList("internal-player-active-button");
            _player4ActiveBtn.RemoveFromClassList("internal-player-inactive-button");
            _player4ActiveBtn.text = "Jogador\n4";
            selectionManager.AddOwnerOnList(Owner.P4);
        }
    }
    private void OnClickActivep5(ClickEvent evt)
    {
        if (selectionManager.players.Contains(Owner.P5))
        {
            SoundManagerSO.PlaySoundFXClip(_buttonDenial, transform.position, 1);
            _player5ActiveBtn.RemoveFromClassList("internal-player-active-button");
            _player5ActiveBtn.AddToClassList("internal-player-inactive-button");
            _player5ActiveBtn.text = "Inativo";
            selectionManager.RemoveOwnerOnList(Owner.P5);
        }
        else
        {
            SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
            _player5ActiveBtn.AddToClassList("internal-player-active-button");
            _player5ActiveBtn.RemoveFromClassList("internal-player-inactive-button");
            _player5ActiveBtn.text = "Jogador\n5";
            selectionManager.AddOwnerOnList(Owner.P5);
        }
    }


}
