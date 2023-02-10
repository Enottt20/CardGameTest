using UnityEngine.EventSystems;

namespace Card.CardZones
{
    public class ZoneHand : BaseDropZone
    {
        protected override void OnPointerUp(PointerEventData eventData)
        {
            CardHand?.Unselect();
        }
    }
}