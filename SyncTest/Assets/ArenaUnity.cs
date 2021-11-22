﻿using UnityEngine;

public static class ArenaUnity
{
    // Position Conversions:
    // all: z is inverted in a-frame
    public static dynamic ToArenaPosition(Vector3 position)
    {
        return new
        {
            x = position.x,
            y = position.y,
            z = -position.z
        };
    }
    public static Vector3 ToUnityPosition(dynamic position)
    {
        return new Vector3(
            (float)position.x,
            (float)position.y,
            -(float)position.z
        );
    }

    public static dynamic ToArenaRotationQuat(Quaternion rotationQuat)
    {
        return new
        {
            x = rotationQuat.x,
            y = rotationQuat.y,
            z = rotationQuat.z,
            w = rotationQuat.w
        };
    }
    public static Quaternion ToUnityRotationQuat(dynamic rotationQuat)
    {
        return new Quaternion(
            (float)rotationQuat.x,
            (float)rotationQuat.y,
            (float)rotationQuat.z,
            (float)rotationQuat.w
        );
    }
    public static dynamic ToArenaRotationEuler(Vector3 rotationEuler)
    {
        // TODO: quaternions are more direct, but a-frame doesn't like them somehow
        return new
        {
            x = rotationEuler.x,
            //x = -rotationEuler.x,
            y = rotationEuler.y,
            //y = -rotationEuler.y,
            z = rotationEuler.z
        };
    }
    public static Quaternion ToUnityRotationEuler(dynamic rotationEuler)
    {
        return Quaternion.Euler(
            (float)rotationEuler.x,
            (float)rotationEuler.y,
            (float)rotationEuler.z
        );
    }

    public static dynamic ToArenaScale(string object_type, Vector3 scale)
    {
        float[] f = GetScaleFactor(object_type);
        return new
        {
            x = scale.x * f[0],
            y = scale.y * f[1],
            z = scale.z * f[2]
        };
    }
    public static Vector3 ToUnityScale(string object_type, dynamic scale)
    {
        float[] f = GetScaleFactor(object_type);
        return new Vector3(
            (float)scale.x / f[0],
            (float)scale.y / f[1],
            (float)scale.z / f[2]
        );
    }

    private static float[] GetScaleFactor(string object_type)
    {
        // Scale Conversions
        // cube: unity (side) 1, a-frame (side)  1
        // sphere: unity (diameter) 1, a-frame (radius)  0.5
        // cylinder: unity (y height) 1, a-frame (y height) 2
        // cylinder: unity (x,z diameter) 1, a-frame (x,z radius) 0.5
        switch (object_type)
        {
            case "sphere":
                return new float[3] { 0.5f, 0.5f, 0.5f };
            case "cylinder":
                return new float[3] { 0.5f, 2f, 0.5f };
            default:
                return new float[3] { 1f, 1f, 1f };
        }
    }

    public static string ToArenaColor(Color color)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(color)}";
    }
    public static Color ToUnityColor(string color)
    {
        Color colorObj;
        ColorUtility.TryParseHtmlString(color, out colorObj);
        return colorObj;
    }

}