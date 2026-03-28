;defining variables
#define Repository     "..\."
#define MyAppName      "OpenProject Revit AddIn"
#define MyAppVersion   "2.3.4"
#define MyAppPublisher "OpenProject"
#define MyAppURL       "http://www.openproject.org/"
#define MyAppExeName   "OpenProject.Revit.exe"

#define RevitAppName  "OpenProject.Revit"
#define RevitAddinFolder "{sd}\ProgramData\Autodesk\Revit\Addins"
#define RevitFolder26 RevitAddinFolder+"\2026\"+RevitAppName
#define RevitAddin26  RevitAddinFolder+"\2026\"

#define WinAppName    "OpenProject.Browser"

[Setup]
AppId={{5f96a79f-0e28-4d02-be10-251c8032a270}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf64}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
DisableWelcomePage=no
OutputDir={#Repository}\output
OutputBaseFilename=OpenProject.Revit
SetupIconFile={#Repository}\Assets\openproject.ico
Compression=lzma
SolidCompression=yes
WizardImageFile={#Repository}\Assets\openproject-bim-banner.bmp
ChangesAssociations=yes
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Components]
Name: revit26; Description: Addin for Autodesk Revit 2026;  Types: full

[Files]
;REVIT 2026                                                                                                                                    
Source: "{#Repository}\output\{#RevitAppName}\Release-2026\*"; DestDir: "{#RevitFolder26}"; Flags: ignoreversion recursesubdirs; Components: revit26 
Source: "{#Repository}\output\{#RevitAppName}\Release-2026\*.addin"; DestDir: "{#RevitAddin26}"; Flags: ignoreversion; Components: revit26
Source: "{#Repository}\output\OpenProject.Browser\*"; DestDir: "{#RevitFolder26}\OpenProject.Browser\"; Flags: ignoreversion recursesubdirs; Components: revit26 

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[Code]
function InitializeSetup(): Boolean;
begin
    result := true;
end;
