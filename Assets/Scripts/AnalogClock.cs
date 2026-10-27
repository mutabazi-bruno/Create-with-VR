using System;
using UnityEngine;

// Turns the hands of an analog clock to show the current local time (DateTime.Now).
public class AnalogClock : MonoBehaviour
{
    // Leave these empty to find the hands automatically by name (..._Hour, ..._Minute, ..._Second)
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    private Quaternion hourStart;
    private Quaternion minuteStart;
    private Quaternion secondStart;

    void Awake()
    {
        if (hourHand == null) hourHand = FindHand("Hour");
        if (minuteHand == null) minuteHand = FindHand("Minute");
        if (secondHand == null) secondHand = FindHand("Second");

        // Remember the starting rotation of each hand (all pointing at 12)
        if (hourHand != null) hourStart = hourHand.localRotation;
        if (minuteHand != null) minuteStart = minuteHand.localRotation;
        if (secondHand != null) secondStart = secondHand.localRotation;
    }

    void Update()
    {
        DateTime now = DateTime.Now;

        float minutes = now.Minute + now.Second / 60f;
        float hours = now.Hour % 12 + minutes / 60f;

        // 360 degrees / 12 hours = 30 degrees per hour, 360 / 60 = 6 degrees per minute or second
        SetHand(hourHand, hourStart, hours * 30f);
        SetHand(minuteHand, minuteStart, minutes * 6f);
        SetHand(secondHand, secondStart, now.Second * 6f);
    }

    void SetHand(Transform hand, Quaternion start, float angle)
    {
        if (hand == null) return;

        // The hands spin around their own Y axis, which points out of the clock face
        hand.localRotation = start * Quaternion.Euler(0f, angle, 0f);
    }

    Transform FindHand(string suffix)
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name.EndsWith(suffix)) return child;
        }
        return null;
    }
}
