﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Parameters
{
    private static string[] scenes = new string[] { "Level 1", "Level 2", "Level 3", "Level 4" };
    private static int currentLevel = 0;
    private static float SFXVolume = 1;
    private static float MusicVolume = 1;

    public static float GetSfxVolume()
    {
        return SFXVolume;
    }

    public static void SetSfxVolume( float SFXVolume_ )
    {
        SFXVolume = SFXVolume_;
    }

    public static float GetMusicVolume()
    {
        return MusicVolume;
    }

    public static void SetMusicVolume( float MusicVolume_ )
    {
        MusicVolume = MusicVolume_;
    }

    public static void SetScene( string name )
    {
        currentLevel = Array.IndexOf( scenes, name );
    }

    public static string GetCurrentLevel()
    {
        return scenes[ currentLevel ];
    }

    public static string GetNextLevel()
    {
        if( currentLevel == scenes.Length - 1 ){
            currentLevel = 0;
        } else {
            currentLevel++;
        }

        return scenes[ currentLevel ];
    }
}
