using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TooltipForTMPbyClick
{
    [RequireComponent(typeof(TMP_Text))]
    public class LinkHandlerForTMPText : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _tmpTextBox;
        private Canvas _canvasToCheck;
        [SerializeField] private Camera cameraToUse;

        public delegate void ClickOnLinkEvent(string keyword);
        public static event ClickOnLinkEvent OnClickedOnLinkEvent;

        private void Awake()
        {
            _tmpTextBox = GetComponent<TMP_Text>();
            _canvasToCheck = GetComponentInParent<Canvas>();

            if (_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
                cameraToUse = null;
            else
                cameraToUse = _canvasToCheck.worldCamera;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);
            var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(_tmpTextBox, mousePosition, cameraToUse);

            if (linkTaggedText == -1) return;

            TMP_LinkInfo linkInfo = _tmpTextBox.textInfo.linkInfo[linkTaggedText];

            string linkID = linkInfo.GetLinkID();
            if (linkID.Contains("http"))
            {
                Application.OpenURL(linkID);
                return;
            }

            OnClickedOnLinkEvent?.Invoke(linkInfo.GetLinkText());
        }
    }
}