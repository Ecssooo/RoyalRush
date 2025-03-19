using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("GameObject Component")]
    [SerializeField] private RectTransform _transform;
    [SerializeField] private Image _image;
    
    [Header("Slot")]
    [SerializeField] private Transform _slotInShop;
    public Transform SlotInShop { get => _slotInShop; set => _slotInShop = value; }

    [Header("Prefab")]
    [SerializeField] private GameObject _objectPrefab;
    public GameObject ObjectPrefab { get => _objectPrefab; }

    [Header("Object Settings")]
    [SerializeField] private int _cost;
    public int Cost { get => _cost; }
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _slotInShop = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _transform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_slotInShop);
        _image.raycastTarget = true;
    }
}
