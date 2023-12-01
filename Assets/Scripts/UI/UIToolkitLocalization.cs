using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
[RequireComponent(typeof(UIDocument))]
public class UIToolkitLocalization : MonoBehaviour
{
    [SerializeField] LocalizedStringTable table;
    List<LocalizeObj> elementList = new();

    private void OnEnable()
    {
        var document = gameObject.GetComponentInParent<UIDocument>(includeInactive: true);
        RegisterElements(document.rootVisualElement); //Register all text elements
        table.TableChanged += OnTableChanged;
    } 

    private void OnDisable() => table.TableChanged -= OnTableChanged;

    private void OnTableChanged(StringTable stringTable) //if you change the language, the values are updated
    {
        var tb = table.GetTableAsync();
        tb.Completed -= OnTableLoaded;
        tb.Completed += OnTableLoaded;
    }

    private void OnTableLoaded(AsyncOperationHandle<StringTable> table) //Updates values if language changes
    {
        Localize(table.Result);
    }

    private void RegisterElements(VisualElement element) //Register all elements ( recursive )
    {
        //if have text
        if (typeof(TextElement).IsInstanceOfType(element))
        {
            var textElement = (TextElement)element;
            var key = textElement.text;
            if (!string.IsNullOrEmpty(key) && key[0] == '#')
            {
                key = key.TrimStart('#');
                elementList.Add(new LocalizeObj
                {
                    Element = textElement,
                    Key = key
                });
            }
        }

        //if have child
        var hierarchy = element.hierarchy;
        var childs = hierarchy.childCount;
        for (int i = 0; i < childs; i++)
        {
            RegisterElements(hierarchy.ElementAt(i));
        }
    }

    private void Localize(StringTable stringTable) //Change text values
    {
        foreach (var item in elementList)
        {
            var entry = stringTable[item.Key];
            if (entry != null)
                item.Element.text = entry.LocalizedValue;
            else
                Debug.LogWarning($"No {stringTable.LocaleIdentifier.Code} translation for key: '{item.Key}'");
        }
    }

    private class LocalizeObj //Used for translation
    {
        public string Key;
        public TextElement Element;
    }
}