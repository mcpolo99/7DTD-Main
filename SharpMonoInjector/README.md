# SharpMonoInjector wh0am1 Mod (Fixed and Updated)

#### Updated SharpMonoInjector to fix the process detection bug, x86/x64 detection bug fixed, and a couple fixes to make it more efficient. No modifications to his injection engine other than some added error checking. Built off Net 4.0 for those that are still on Win7 and can't run NetStandard 2.0. Since he didn't build-in any privilege checking I added some checks and the GUI version will automatically restart as Admin. The console version you'll get a warning and instructions on to 'fix' the game.

#### This was a great Unity injector until that nasty process detection and x86/64 bug, now it's actually usable again. I still prefer my injector though, it uses syscalls he uses CreateRemoteThread that is noisy to Anti-Cheat products. I might be partial, but either way Warbler the original creator of SharpMonoInjector deserves credit for his work.

#### SharpMonoInjector is a tool for injecting assemblies into Mono embedded applications, commonly Unity Engine based games. The target process *usually* does not have to be restarted in order to inject an updated version of the assembly. Your unload method must destroy all of its resources (such as game objects).

#### SharpMonoInjector works by dynamically generating machine code, writing it to the target process and executing it using CreateRemoteThread. The code calls functions in the mono embedded API. The return value is obtained with ReadProcessMemory.

#### Both x86 and x64 processes are supported.

#### In order for the injector to work, the load/unload (Unload isn't required, optional) methods need to match the following method signature:

####    static void Method()

# You can find the latest binary releases here: (https://www.unknowncheats.me/forum/unity/408878-sharpmonoinjector-fixed-updated.html), there is a console application and a GUI application available.

![Screen](https://github.com/wh0am15533/SharpMonoInjector/blob/master/Images/ScreenFull.png)


### Credit's to Original Project and Author: (https://github.com/warbler/SharpMonoInjector)
