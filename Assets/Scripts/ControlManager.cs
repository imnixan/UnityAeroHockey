using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    private Dictionary<int, PlayerControl> touchToStrikerMap = new Dictionary<int, PlayerControl>();

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                OnTouchStart(touch);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                OnTouchMove(touch);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                OnTouchEnd(touch);
            }
        }
    }

    private void OnTouchStart(Touch touch)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        // �������������� ������ ��� �����������, � ������ ������� ��������� ������� (��������, �������� �� ��������� �� ������ �������)

        PlayerControl striker = GetStrikerFromHitObject(touch); // ���������� ���� ����� � ������������ � ������ �������

        if (striker != null)
        {
            touchToStrikerMap[touch.fingerId] = striker;
        }
    }

    private void OnTouchMove(Touch touch)
    {
        if (touchToStrikerMap.ContainsKey(touch.fingerId))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchToStrikerMap[touch.fingerId].MoveTo(touchPosition);
        }
    }

    private void OnTouchEnd(Touch touch)
    {
        if (touchToStrikerMap.ContainsKey(touch.fingerId))
        {
            // ������, ����������� ��� ��������� �������

            // ������� ����� �������������� ������� � �������� Striker
            touchToStrikerMap.Remove(touch.fingerId);
        }
    }

    // ������ ������ ��� ����������� ����� �� �������, �� �������� ���� ����������� �������
    private PlayerControl GetStrikerFromHitObject(Touch touch)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null)
        {
            PlayerControl striker = hit.collider.GetComponent<PlayerControl>();

            if (striker != null)
            {
                return striker;
            }
        }

        return null;
    }
}
