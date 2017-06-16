using UnityEngine;
using System.Collections;

namespace Devdog.SciFiDesign.UI
{
    public class TransformRotationFromMousePosition : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotation;

	    [SerializeField] private bool isNeedAdjust;
        private float _prevX;
        private float _prevY;
		void Start()
		{
			Debug.LogError(Screen.width);
			Debug.LogError(Screen.height);

		}
		protected void Update()
        {
            if (Mathf.Approximately(Input.mousePosition.x, _prevX) == false || Mathf.Approximately(Input.mousePosition.y, _prevY) == false)
            {
                // Cursor moved

                var normalized = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);//归一化?
				if(isNeedAdjust)
				{
					normalized.x -= 0.5f;
					normalized.x *= 2f;

					normalized.y -= 0.5f;
					normalized.y *= 2f;
				}
				//Debug.LogError("归一化x"+normalized.x);
				//Debug.LogError("归一化y" + normalized.y);

				var rot = _rotation;
                rot.x *= normalized.y;
                rot.y *= normalized.x;

				transform.rotation = Quaternion.Euler(rot);
			}

			_prevX = Input.mousePosition.x;
            _prevY = Input.mousePosition.y;
        }
    }
}