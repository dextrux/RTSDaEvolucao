using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureSelectUi : MonoBehaviour
{
    private Button _player3Active;
    private Button _player4Active;
    private Button _player5Active;
    [SerializeField] private List<Animator> _playerAnimatorList;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _player3Active = root.Q<Button>("player3btn");
        _player4Active = root.Q<Button>("player4btn");
        _player5Active = root.Q<Button>("player5btn");
        _playerAnimatorList[0].SetBool("Active", true);
        _playerAnimatorList[1].SetBool("Active", true);
        _playerAnimatorList[2].SetBool("Active", false);
        _playerAnimatorList[3].SetBool("Active", false);
        _playerAnimatorList[4].SetBool("Active", false);
    }
}
