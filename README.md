# README of Windows_Update_Error Project

## I. Project Overview

This project focuses on an in-depth analysis of the malicious program `Windows_Update_Error.exe`. It aims to uncover its potential hazards, operational mechanisms, and provide effective strategies for preventing such malicious programs. By closely examining the code, it helps developers, security researchers, and ordinary users gain a better understanding of the complexity of malicious software and enhance their security awareness.

## II. Project Structure

The project code is organized around the core functions of the malicious program and mainly consists of the following key parts:

**Core Code Directory**: Stores the main C# code of the malicious program. It is divided into multiple class files according to different functional modules. For example, `Regedit_Set.cs` is responsible for malicious operations related to the registry, `Test.cs` involves system-level attack means, `Key_Decrypt.cs` handles key decryption to support other malicious functions, `Program.cs` coordinates the overall process as the program entry point, and `Form2.cs` and `Form3.cs` are used for interacting with users and performing specific malicious actions.

**Resource File Directory**: Contains some configuration files, icon resources, etc. required for the malicious program to run. For instance, `Error_m1.txt` is used to store information related to program startup, assisting the program in achieving self-startup and status determination.

## III. Analysis of Core Code

**Regedit\_Set Class**: In `Regedit_Set.cs`, its methods maliciously tamper with the system registry.

`Disable_Settings`: Creates or modifies corresponding key-value pairs in the registry to disable the Control Panel and Task Manager, restricting users' access to critical system settings and process management. For example, by modifying specific registry entries, users are unable to open the Control Panel normally to adjust system settings, nor can they end the malicious program process through the Task Manager.

`Disable_Regedit`: Sets specific key-values to prevent users from accessing the Registry Editor, making it difficult for users to manually clear the traces left by the malicious program in the registry, further strengthening the existence of the malicious program.

`Start_Update`: Writes data to the file `Error_m1.txt` related to program startup, providing information for the subsequent logic of the program, such as recording the number of program runs, status identifiers, etc., enabling the program to execute corresponding malicious operations according to different situations.

`Start_Detection`: Reads data from the `Error_m1.txt` file. If the file does not exist, it creates the file and writes default values, providing a basis for the branch logic judgment of the program to decide whether to perform initialization settings for the first run or execute specific steps based on previous records.

`UAC_OFF`: Modifies the registry key-value to turn off User Account Control (UAC), reducing the system's security protection level, allowing the subsequent operations of the malicious program to proceed more smoothly and avoiding UAC pop-up prompts that might alert the user.

`PC_Start`: Sets the program path as a system startup item, enabling the program to start automatically with the system, ensuring the continuous operation of the malicious program, automatically activating each time the computer boots, and causing hidden and persistent harm to the system.

`Disable_Windows`: According to the passed-in parameters, by modifying the registry key-values, it enables or disables the operation of most software except this program.

**Test Class**: Defined in `Test.cs`, it launches a series of high-risk attacks by invoking system APIs.

`Nt_Error`: Calls the `NtSetInformationProcess` function to mark the current process as a critical process and then exits the application, triggering a system blue-screen crash, resulting in user data loss and the inability to use the system normally, causing great trouble to the user.

`killmbr` and `killmbrA`: Constructs malicious MBR data and writes it to the physical drive, destroying the Master Boot Record, causing the system to fail to start normally, paralyzing the computer. Unless the MBR is repaired, the operating system cannot be entered.

`Shutdown_PC`: Executes the shutdown command to restart the computer, which may be used to disrupt user operations. For example, when the user is doing important work or saving data, a forced restart can cause data damage; or under specific conditions, it can damage system data, such as restarting during a critical stage of system update, causing the update to fail and resulting in system failure.

`Disable_WinRE`: Executes commands to disable the Windows Recovery Environment, preventing users from using the recovery environment to repair the system, cutting off the user's self-rescue path, forcing users to seek professional help, and increasing the persistence of the malicious program's damage.

`GetMbr` and `WriteMbr`: Obtains and writes MBR data. On the one hand, it can be used to back up the original MBR to prevent other malicious programs from preemptively destroying it; on the other hand, it can also modify the MBR to control the computer's startup process and implant malicious boot code.

`Random_Key`: Generates random keys for encryption or other malicious operations, such as encrypting stolen user-sensitive data to prevent it from being easily accessed by others, increasing the concealment of data theft.

**Key\_Decrypt Class**: Located in `Key_Decrypt.cs`, according to the established encryption and decryption rules, the `GetDecryptKey` method decrypts the key. It restores the encrypted key based on the preset correspondence between encrypted and decrypted characters, ensuring the coherence of internal data interaction and the execution of key functions of the malicious program. For example, it provides decrypted key support for accessing restricted resources or triggering specific malicious logic, enabling the program to smoothly execute subsequent malicious steps.

**Program Class**: As the hub of program startup, the `Main` method in `Program.cs` initializes the application configuration and creates various instances. By reading the startup code, it performs different operations according to different values, including setting startup items, disabling system functions, shutting down the computer, obtaining MBR data, and launching corresponding forms, controlling the entire execution flow of the malicious program. For example, if the startup code is a specific value, it first performs registry tampering operations, then tries to obtain MBR data, and if it fails, restarts the computer; if the startup code meets another condition, it directly launches `Form2` that interacts with the user, inducing the user to input key information and further promoting the destruction process of the malicious program.

**Form2 Class and Form3 Class**: In `Form2.cs` and `Form3.cs`, `Form2` is responsible for generating a random key, obtaining a decryption key, and destroying the MBR when loaded, preventing the user from entering Safe Mode to delete the program. In terms of user interaction, it can verify whether the content entered by the user in the text box is equal to the decryption key. If they are equal, it executes a series of system restoration operations, including writing the correct MBR, lifting system setting restrictions, setting the system startup item to the normal `explorer.exe`, lifting registry editing restrictions, turning off related function restrictions, writing the startup code `114514`, pausing the thread, and then shutting down the computer; if they are not equal, it prompts an error. `Form3` mainly displays the random key generated by `Form2` and provides a function to copy it to the clipboard.