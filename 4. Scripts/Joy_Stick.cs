using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joy_Stick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Transform stick;
    public Player_Movement player;

    private Vector3 stick_offset_position;
    private Vector3 stick_direction;
    private float joy_stick_radius;

    #region "Unity"

    private void Start()
    {
        Initialize_Radius();
    }

    #endregion

    #region "Initialize"

    private void Initialize_Radius()
    {
        joy_stick_radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;

        float canvas_radius = transform.parent.GetComponent<RectTransform>().localScale.x;
        joy_stick_radius *= canvas_radius;
    }

    #endregion

    #region "Eventsystem Interface"

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 touch_position = Camera.main.ScreenToWorldPoint(eventData.position);
        touch_position.z = 0;

        Set_Stick_Position(touch_position);
        Set_Stick_Direction(touch_position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        stick_offset_position = stick.transform.position;

        player.Start_Move();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.position = stick_offset_position;
        stick_direction = Vector3.zero;

        player.Stop_Move();
    }

    #endregion

    #region "Stick Methoed"

    private void Set_Stick_Direction(Vector3 touch_position)
    {
        stick_direction = (touch_position - stick_offset_position).normalized;

        player.Set_Move_Direction(stick_direction);
    }

    private void Set_Stick_Position(Vector3 touch_position)
    {
        float touch_distance = Vector3.Distance(touch_position, stick_offset_position);

        if (touch_distance < joy_stick_radius)
        {
            stick.position = stick_offset_position + stick_direction * touch_distance;
        }
        else
        {
            stick.position = stick_offset_position + stick_direction * joy_stick_radius;
        }
    }

    #endregion
}
