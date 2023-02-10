using UnityEngine.EventSystems;

namespace Card.CardZones
{
    public class ZoneBattleField : BaseDropZone
    {
        protected override void OnPointerUp(PointerEventData eventData)
        {
            CardHand?.PlaySelected();
        }
    }
}