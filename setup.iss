#define Name "TelMe"
#define Version "1.0.0"
#define Publisher "winsxtnhdw"
#define URL "https://github.com/winstxnhdw/TelMe"

[Setup]
Uninstallable=yes
AppId={{79A7BABF-E66D-4F8C-8FAD-BB11214DCEA6}}
AppName={#Name}
AppVerName={#Name} {#Version}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}
DefaultDirName={commonpf}\{#Name}
OutputBaseFilename=Setup
OutputDir=dist
Compression=lzma
SolidCompression=yes

[Files]
Source: "dist\TelMe.exe"; DestDir: "{app}"; Flags: ignoreversion

[InstallDelete]
Type: filesandordirs; Name: "{app}"

[INI]
Filename: "{app}\settings.ini"; Section: "User"; Key: "EMAIL_TO"; String: ""
Filename: "{app}\settings.ini"; Section: "User"; Key: "EMAIL_FROM"; String: ""
Filename: "{app}\settings.ini"; Section: "User"; Key: "SERVER_ENDPOINT"; String: ""

[Run]
Filename: "{app}\TelMe.exe"; Parameters: "install"; Flags: runhidden postinstall runascurrentuser

[UninstallRun]
Filename: "{app}\TelMe.exe"; Parameters: "uninstall"; Flags: runhidden runascurrentuser; RunOnceId: "TelMeUninstall"

[Code]
var
  UserInputPage: TInputQueryWizardPage;

procedure InitializeWizard;
begin
  UserInputPage := CreateInputQueryPage(wpWelcome, 'User Settings', 'Please enter the following details:', '');
  UserInputPage.Add('Recipient Email:', False);
  UserInputPage.Add('Sender Email:', False);
  UserInputPage.Add('Server Endpoint:', False);
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  PathToSettings: string;

begin
  if CurStep = ssPostInstall then
  begin
    PathToSettings := ExpandConstant('{app}\settings.ini');
    SetIniString('User', 'EMAIL_TO', UserInputPage.Values[0], PathToSettings);
    SetIniString('User', 'EMAIL_FROM', UserInputPage.Values[1], PathToSettings);
    SetIniString('User', 'SERVER_ENDPOINT', UserInputPage.Values[2], PathToSettings);
  end;
end;
