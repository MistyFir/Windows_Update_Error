# Windows_Update_Error Project Description

## I. Project Overview
This project presents a code example of malicious software, aiming to reveal the potential hazards and operation mechanisms of malicious programs. It must not be used for illegal or malicious purposes.

## II. Functional Features
1. **Camouflage and Startup Hijacking**
   - The program disguises itself as a Windows update process. Once run, it modifies the startup item of the resource manager to itself, preventing users from accessing the normal desktop and interfering with the normal startup process of the system.
2. **System Critical Function Destruction**
   - It can disable the Windows Recovery Environment, making it impossible for users to repair system problems using this environment.
   - It is capable of disabling the registry, restricting users from viewing and modifying critical system settings.
   - It turns off User Account Control (UAC), reducing the system's security protection and creating conditions for its own malicious operations.
   - It disables the Control Panel and Task Manager, limiting users' regular operations and process management of the system.
3. **Hard Disk Master Boot Record Tampering**
   - Under specific conditions, it attempts to modify the hard disk's Master Boot Record (MBR), resulting in the computer being unable to boot normally and potentially causing data loss and system paralysis.

## III. Code Structure
1. **`Update_Error.cs`**
   - Defines a series of methods related to low-level system operations.
   - For example, the `Nt_Error` method uses specific system calls to mark the program process as a critical process and then exits, causing a blue screen.
   - The `killmbr` and `killmbrA` methods maliciously modify the MBR in different ways (using.NET's own code and Windows API respectively).
   - There are also methods to control computer restart and disable Windows_RE.
2. **`Regedit_Set.cs`**
   - Mainly responsible for registry-related operations.
   - Including creating key values to disable the Control Panel, Task Manager, registry tools, etc., as well as methods to operate files to record startup times, turn off UAC, and set startup items.
3. **`Program.cs`**
   - The entry point of the program. In the `Main` method, a series of initialization operations are completed, relevant class objects are created, and the startup code is obtained.
   - According to the startup code, different malicious operation processes are executed, such as adding startup items, disabling system functions, and performing corresponding operations (updating startup codes, restarting, tampering with MBR, etc.) at different startup stages.
4. **`Form2.cs`**
   - Implements a simple form interface. Its button controls can trigger malicious operations such as modifying the MBR, causing a blue screen, or restarting, providing an additional trigger method for the malicious program.

## IV. Usage and Warning
1. This project is only for educational and research purposes. It helps security enthusiasts, researchers, etc. understand the working principles and potential hazards of malicious software, so as to better prevent such threats.
2. It is strictly prohibited to run or spread the code of this project on any unauthorized system. Otherwise, it will violate laws and regulations and may lead to serious legal consequences.

Please be sure to abide by legal and ethical standards and use the information provided by this project correctly. 
