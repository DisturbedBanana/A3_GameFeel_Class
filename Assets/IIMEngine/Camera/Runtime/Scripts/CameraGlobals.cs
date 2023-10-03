
namespace IIMEngine.Camera
{
    public static class CameraGlobals
    {
        public static CameraManager Manager { get; set; } = null;
        public static CameraProfilesManager Profiles { get; set; } = null;
        public static CameraBoundsManager Bounds { get; set; } = null;
        public static CameraEffectsManager Effects { get; set; } = null;
    }
}