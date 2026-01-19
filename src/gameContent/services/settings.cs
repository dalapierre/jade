class Settings {
    public static string[] SFXVolume = new string[]{"100%", "75%", "50%", "25%", "0%"};
    public static string[] BFXVolume = new string[]{"100%", "75%", "50%", "25%", "0%"};
    public static string[] Fullscreen = new string[] { "No", "Yes" };
    public static int currentSFXVolume { get; private set; }
    public static int currentBFXVolume { get; private set; }
    public static int currentFullscreen { get; private set; }

    static Settings() {
        currentSFXVolume = 0;
        currentBFXVolume = 0;
        currentFullscreen = 0;
    }

    public static void SetSFXVolume(int val) { currentSFXVolume = val; }
    public static void SetBFXVolume(int val) { currentBFXVolume = val; }
    public static void SetFullscreen(int isFS) { currentFullscreen = isFS; }
}