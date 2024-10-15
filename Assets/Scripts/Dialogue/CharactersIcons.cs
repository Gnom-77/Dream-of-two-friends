using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharactersIconsScriptableObject", order = 1)]
public class CharactersIcons : ScriptableObject
{
    [SerializeField] private List<CharacterIcon> _icons;

    public Sprite GetCharacterIcon(string characterName)
    {
        CharacterIcon characterIcon;

        characterIcon = _icons.Where(item => item.name == characterName).FirstOrDefault();

        return characterIcon.iconImage;
    }
}
