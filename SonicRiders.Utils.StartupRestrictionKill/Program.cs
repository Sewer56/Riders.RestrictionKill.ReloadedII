using System;
using Reloaded.Memory.Sources;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace SonicRiders.Utils.StartupRestrictionKill
{
    public class Program : IMod
    {
        private IModLoader _modLoader;

        private static byte[] _jmpSkipLauncher    = { 0xEB, 0x23 }; // jmp 0x25
        private static byte[] _jmpSkipOneInstance = { 0xEB, 0x28 }; // jmp 0x2A

        public void Start(IModLoaderV1 loader)
        {
            _modLoader = (IModLoader)loader;

            // Ignore launcher check result.
            Memory.CurrentProcess.SafeWriteRaw((IntPtr) 0x005118CF, _jmpSkipLauncher);

            // Ignore only one instance check result.
            Memory.CurrentProcess.SafeWriteRaw((IntPtr) 0x0051190F, _jmpSkipOneInstance);
        }

        /* Mod loader actions. */
        public void Suspend() { }
        public void Resume()  { }
        public void Unload()  { }

        public bool CanUnload()  => false;
        public bool CanSuspend() => false;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }
    }
}
