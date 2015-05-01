Imports Microsoft.Win32

Public Class Aurora

#Region " Variables "

	Dim regKey_StartUP_Key As RegistryKey
	Dim regKey_ExplorerSettingsKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
	Dim regKeyValue_HiddenKey, regKeyValue_HiddenSystemKey, regKeyValue_FileExtentionKey As Int32
	Const regKey_Run_At_Startup_Path As String = "Software\Microsoft\Windows\CurrentVersion\Run"
	Dim appPath As String = Application.StartupPath & "\" & (My.Application.Info.ProductName & ".exe")
	Dim system_Directory_Path As String = Environment.SystemDirectory
	Dim load_At_Windows_Startup As Boolean
	Dim keepHiddenState As Boolean = False
	Private Declare Function SHChangeNotify Lib "Shell32.dll" (ByVal wEventID As Int32, ByVal uFlags As Int32, ByVal dwItem1 As Int32, ByVal dwItem2 As Int32) As Int32

#End Region

#Region " Functions "

	Private Sub Initiate()

		Try
			Me.Hide()

			If My.User.IsAuthenticated = False Then
				MsgBox("You don't have adminstrative privilages!!!" & vbCrLf & "Some features might not work Properly", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "Worning")
			End If

			Me.Icon = My.Resources.Easy_Access
			NI.Icon = My.Resources.Easy_Access
			NI.Visible = True

			If My.Settings.firstRun Then
				My.Settings.firstRun = False
				My.Settings.closeOnUserInteraction = True
				My.Settings.coupleSysAndHide = True
				My.Settings.Save()
				My.Settings.Reload()
			End If

			enablemsMenu("Access.Application", msAccess)
			enablemsMenu("Excel.Application", msExcel)
			enablemsMenu("PowerPoint.Application", msPwrPoint)
			enablemsMenu("Word.Application", msWord)

			regKey_StartUP_Key = Registry.CurrentUser.OpenSubKey(regKey_Run_At_Startup_Path, True)
			If regKey_StartUP_Key.GetValue("Aurora Easy Access") Is Nothing Then
				LASTSM.Checked = False
				load_At_Windows_Startup = False
			Else
				LASTSM.Checked = True
				load_At_Windows_Startup = True
			End If

			If My.Settings.coupleSysAndHide Then
				AutoUnhideWhenShowingSystemFilesTSM.Checked = True
				mainMenu.AutoClose = True
			Else
				AutoUnhideWhenShowingSystemFilesTSM.Checked = False
				mainMenu.AutoClose = False
			End If

			If My.Settings.closeOnUserInteraction Then
				closeOnInteract.Checked = True
			Else
				closeOnInteract.Checked = False
			End If


			If My.Settings.fileOperations Then
				FileOperations_Toggle.Checked = True

				showHideFiles.Visible = True
				showHideFiles.Enabled = True

				showHideSystem.Visible = True
				showHideSystem.Enabled = True

				showHideExtentions.Visible = True
				showHideExtentions.Enabled = True

				seperatorFileOperations.Visible = True
			Else
				FileOperations_Toggle.Checked = False

				showHideFiles.Visible = False
				showHideFiles.Enabled = False

				showHideSystem.Visible = False
				showHideSystem.Enabled = False

				showHideExtentions.Visible = False
				showHideExtentions.Enabled = False

				seperatorFileOperations.Visible = False
			End If

			If My.Settings.msOfficeInstalled Then
				If My.Settings.msOffice Then
					msOffice_Toggle.Checked = True

					msOfficeMenu.Visible = True
					msOfficeMenu.Enabled = True
					seperatorMsOffice.Visible = True
				Else
					msOffice_Toggle.Checked = False

					msOfficeMenu.Visible = False
					msOfficeMenu.Enabled = False

					seperatorMsOffice.Visible = False
				End If

			Else
				msOffice_Toggle.Visible = False
				msOffice_Toggle.Enabled = False
			End If
			

			If My.Settings.windowsShortcuts Then
				wndowsShortcuts_Toggle.Checked = True

				windowsShortcutsMenu.Visible = True
				windowsShortcutsMenu.Enabled = True

				seperatorWindowsShortcuts.Visible = True

			Else
				wndowsShortcuts_Toggle.Checked = False

				windowsShortcutsMenu.Visible = False
				windowsShortcutsMenu.Enabled = False

				seperatorWindowsShortcuts.Visible = False
			End If

		

			Me.ShowInTaskbar = False
			Me.WindowState = FormWindowState.Minimized
			Me.Hide()
			NI.Visible = True

		Catch
		End Try

	End Sub

	Private Sub enablemsMenu(ByVal programKeyToLook As String, ByVal menuitem As System.Windows.Forms.ToolStripItem)

		Dim programKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(programKeyToLook, False)
		If programKey Is Nothing Then
		Else
			If Not My.Settings.msOfficeInstalled Then My.Settings.msOfficeInstalled = True
			menuitem.Visible = True
			menuitem.Enabled = True
			If Not msOfficeMenu.Enabled Then msOfficeMenu.Enabled = True
		End If
		programKey.Close()
	End Sub

	Private Sub showMenu()
		Try
			NI.ContextMenuStrip = mainMenu
			NI.GetType.GetMethod("ShowContextMenu", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).Invoke(NI, Nothing)
			NI.ContextMenuStrip = Nothing
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

	Private Shared Sub RefreshExplorer()

		On Error Resume Next

		SHChangeNotify(&H8000000, &H0, 0, 0)

		Dim CLSID_ShellApplication As New Guid("13709620-C279-11CE-A49E-444553540000")
		Dim shellApplicationType As Type = Type.GetTypeFromCLSID(CLSID_ShellApplication, True)
		Dim shellApplication As Object = Activator.CreateInstance(shellApplicationType)
		Dim windows As Object = shellApplicationType.InvokeMember("Windows", System.Reflection.BindingFlags.InvokeMethod, Nothing, shellApplication, New Object() {})
		Dim windowsType As Type = windows.[GetType]()
		Dim count As Object = windowsType.InvokeMember("Count", System.Reflection.BindingFlags.GetProperty, Nothing, windows, Nothing)
		If CInt(count) = 0 Then Exit Sub
		For i As Integer = 0 To CInt(count) - 1
			Dim item As Object = windowsType.InvokeMember("Item", System.Reflection.BindingFlags.InvokeMethod, Nothing, windows, New Object() {i})
			Dim itemType As Type = item.[GetType]()

			Dim itemName As String = DirectCast(itemType.InvokeMember("Name", System.Reflection.BindingFlags.GetProperty, Nothing, item, Nothing), String)

			If itemName = "Windows Explorer" Then
				itemType.InvokeMember("Refresh", System.Reflection.BindingFlags.InvokeMethod, Nothing, item, Nothing)
			End If
		Next

	End Sub

	Private Sub openShortcuts(ByVal executableName As String)
		Try
			Process.Start(executableName)
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

	Private Sub openShortcuts(ByVal executableName As String, ByVal parameter As String, ByVal useParameter As Boolean)
		Try
			If (useParameter) Then Process.Start(executableName, parameter)
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

	Private Sub openShortcuts(ByVal executableName As String, ByVal pathIf_x64 As String)
		Try
			Select Case Environment.Is64BitOperatingSystem
				Case True
					Try
						Try
							Process.Start(system_Directory_Path.Substring(0, 10) & pathIf_x64)
						Catch ex As Exception
							Process.Start(system_Directory_Path & executableName)
						End Try
					Catch ex As Exception
						MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
					End Try
				Case False
					Try
						Try
							Process.Start(system_Directory_Path & executableName)
						Catch ex As Exception
							MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
						End Try
					Catch ex As Exception
						MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
					End Try

			End Select

		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

#End Region

#Region " Form "

	Private Sub Aurora_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Try
			Call Initiate()
			If Environment.Is64BitOperatingSystem = True Then
				NI.Text += " - x 64"
			Else
				NI.Text += " - x 86"
			End If
		Catch ex As Exception

		End Try
	End Sub

#End Region

#Region " GUI "

	Private Sub NI_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NI.MouseUp
		Try
			If e.Button = MouseButtons.Left Then
				showMenu()
			End If
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try

	End Sub

	Private Sub msAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msAccess.Click
		openShortcuts("msaccess")
	End Sub

	Private Sub msExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msExcel.Click
		openShortcuts("excel")
	End Sub

	Private Sub msPwrPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msPwrPoint.Click
		openShortcuts("powerpnt")
	End Sub

	Private Sub msWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msWord.Click
		openShortcuts("winword")
	End Sub

	Private Sub AdvancedUserControlsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedUserControls_MenuShortcut.Click
		openShortcuts("Netplwiz")
	End Sub

	Private Sub GroupPolicyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupPolicy_MenuShortcut.Click
		openShortcuts("gpedit.msc")
	End Sub

	Private Sub WindowsFirewallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WindowsFirewall_MenuShortcut.Click
		openShortcuts("wf.msc")
	End Sub

	Private Sub ComputerManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputerManagement_MenuShortcut.Click
		openShortcuts("CompMgmtLauncher")
	End Sub

	Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles backup_MenuShortcut.Click
		openShortcuts("sdclt")
	End Sub

	Private Sub MSConfigToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MSConfig_MenuShortcut.Click
		openShortcuts("\msconfig.exe", "\Sysnative\msconfig.exe")
	End Sub

	Private Sub AdvancedSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedSettings_MenuShortcut.Click
		openShortcuts("SystemPropertiesAdvanced")
	End Sub

	Private Sub DxDiagToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DxDiag_MenuShortcut.Click
		openShortcuts("dxdiag")
	End Sub

	Private Sub RegistryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Registry_MenuShortcut.Click
		openShortcuts("regedit")
	End Sub

	Private Sub ServicesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Services_MenuShortcut.Click
		openShortcuts("services.msc")
	End Sub

	Private Sub DeviceManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceManager_MenuShortcut.Click
		openShortcuts("\devmgmt.msc", "\SysWOW64\devmgmt.msc")
	End Sub

	Private Sub UninstallerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Uninstaller_MenuShortcut.Click
		openShortcuts("appwiz.cpl", " ,0", True)
	End Sub

#End Region

#Region " Timers "

	Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerReport.Tick

		Try

			regKeyValue_HiddenKey = CInt(regKey_ExplorerSettingsKey.GetValue("Hidden"))
			regKeyValue_HiddenSystemKey = CInt(regKey_ExplorerSettingsKey.GetValue("ShowSuperHidden"))
			regKeyValue_FileExtentionKey = CInt(regKey_ExplorerSettingsKey.GetValue("HideFileExt", True))

			Select Case regKeyValue_HiddenKey
				Case 2
					If Not showHideFiles.Text = "Show  Hidden Files" Then showHideFiles.Text = "Show  Hidden Files"
					If Not showHideFiles.BackColor = Color.FromName("White") Then showHideFiles.BackColor = Color.FromName("White")
				Case 1
					If Not showHideFiles.Text = "Hide  Hidden Files" Then showHideFiles.Text = "Hide  Hidden Files"
					If Not showHideFiles.BackColor = Color.FromName("DeepSkyBlue") Then showHideFiles.BackColor = Color.FromName("DeepSkyBlue")
				Case Else
			End Select

			Select Case regKeyValue_HiddenSystemKey
				Case 0
					If Not showHideSystem.Text = "Show System Files" Then showHideSystem.Text = "Show System Files"
					If Not showHideSystem.BackColor = Color.FromName("White") Then showHideSystem.BackColor = Color.FromName("White")
				Case 1
					If Not showHideSystem.Text = "Hide System Files" Then showHideSystem.Text = "Hide System Files"
					If Not showHideSystem.BackColor = Color.FromName("DeepSkyBlue") Then showHideSystem.BackColor = Color.FromName("DeepSkyBlue")
				Case Else

			End Select

			Select Case regKeyValue_FileExtentionKey
				Case 1
					If Not showHideExtentions.Text = "Show File Extentions" Then showHideExtentions.Text = "Show File Extentions"
					If Not showHideExtentions.BackColor = Color.FromName("White") Then showHideExtentions.BackColor = Color.FromName("White")
				Case 0
					If Not showHideExtentions.Text = "Hide File Extentions" Then showHideExtentions.Text = "Hide File Extentions"
					If Not showHideExtentions.BackColor = Color.FromName("DeepSkyBlue") Then showHideExtentions.BackColor = Color.FromName("DeepSkyBlue")
				Case Else

			End Select

		Catch
		End Try
	End Sub

#End Region

#Region "File System Commands "

	Private Sub showHideFiles_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showHideFiles.Click
		Try
			Select Case regKeyValue_HiddenKey
				Case 2
					regKey_ExplorerSettingsKey.SetValue("Hidden", 1) 'show hidden
					keepHiddenState = True
				Case 1
					regKey_ExplorerSettingsKey.SetValue("Hidden", 2) 'Hidden hidden
					If My.Settings.coupleSysAndHide Then
						regKey_ExplorerSettingsKey.SetValue("ShowSuperHidden", 0)
					End If
					keepHiddenState = False
			End Select
			Call RefreshExplorer()
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

	Private Sub showHideSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showHideSystem.Click
		Try
			Select Case regKeyValue_HiddenSystemKey
				Case 0
					regKey_ExplorerSettingsKey.SetValue("ShowSuperHidden", 1) ' show System
					If (My.Settings.coupleSysAndHide = True And keepHiddenState = False) Then
						regKey_ExplorerSettingsKey.SetValue("Hidden", 1)
					End If
				Case 1
					regKey_ExplorerSettingsKey.SetValue("ShowSuperHidden", 0) 'hide System
					If (My.Settings.coupleSysAndHide = True And keepHiddenState = False) Then
						regKey_ExplorerSettingsKey.SetValue("Hidden", 2)
					End If
			End Select
			Call RefreshExplorer()

		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

	Private Sub showHideExtentions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showHideExtentions.Click
		Try
			Select Case regKeyValue_FileExtentionKey
				Case 1 : regKey_ExplorerSettingsKey.SetValue("HideFileExt", 0) ' show Extensions
				Case 0 : regKey_ExplorerSettingsKey.SetValue("HideFileExt", 1) ' Hide Extensions
			End Select
			Call RefreshExplorer()
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
	End Sub

#End Region

#Region " Settings "

	Private Sub FileOperations_Toggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileOperations_Toggle.Click
		Try
			If My.Settings.fileOperations = False Then
				My.Settings.fileOperations = True
				FileOperations_Toggle.Checked = True

				showHideFiles.Visible = True
				showHideFiles.Enabled = True

				showHideSystem.Visible = True
				showHideSystem.Enabled = True

				showHideExtentions.Visible = True
				showHideExtentions.Enabled = True

				seperatorFileOperations.Visible = True
			Else
				My.Settings.fileOperations = False
				FileOperations_Toggle.Checked = False

				showHideFiles.Visible = False
				showHideFiles.Enabled = False

				showHideSystem.Visible = False
				showHideSystem.Enabled = False

				showHideExtentions.Visible = False
				showHideExtentions.Enabled = False

				seperatorFileOperations.Visible = False
			End If
			My.Settings.Save()
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
		
	End Sub

	Private Sub msOffice_Toggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msOffice_Toggle.Click
		Try
			If My.Settings.msOffice = False Then
				My.Settings.msOffice = True
				msOffice_Toggle.Checked = True

				msOfficeMenu.Visible = True
				msOfficeMenu.Enabled = True
				seperatorMsOffice.Visible = True
			Else
				My.Settings.msOffice = False
				msOffice_Toggle.Checked = False

				msOfficeMenu.Visible = False
				msOfficeMenu.Enabled = False

				seperatorMsOffice.Visible = False
			End If
			My.Settings.Save()
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
		
	End Sub

	Private Sub wndowsShortcuts_Toggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wndowsShortcuts_Toggle.Click
		Try
			If My.Settings.windowsShortcuts = False Then
				My.Settings.windowsShortcuts = True
				wndowsShortcuts_Toggle.Checked = True

				windowsShortcutsMenu.Visible = True
				windowsShortcutsMenu.Enabled = True

				seperatorWindowsShortcuts.Visible = True

			Else
				My.Settings.windowsShortcuts = False
				wndowsShortcuts_Toggle.Checked = False

				windowsShortcutsMenu.Visible = False
				windowsShortcutsMenu.Enabled = False

				seperatorWindowsShortcuts.Visible = False
			End If
			My.Settings.Save()
		Catch ex As Exception
			MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
		End Try
		
	End Sub

	Private Sub AutoUnhideWhenShowingSystemFilesTSM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoUnhideWhenShowingSystemFilesTSM.Click

		Try
			If My.Settings.coupleSysAndHide Then
				My.Settings.coupleSysAndHide = False
				AutoUnhideWhenShowingSystemFilesTSM.Checked = False
			Else
				My.Settings.coupleSysAndHide = True
				AutoUnhideWhenShowingSystemFilesTSM.Checked = True
			End If
			My.Settings.Save()

		Catch ex As Exception

		End Try
	End Sub

	Private Sub closeOnInteract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeOnInteract.Click
		Try
			If My.Settings.closeOnUserInteraction Then
				My.Settings.closeOnUserInteraction = False
				mainMenu.AutoClose = False
				closeOnInteract.Checked = False
			Else
				My.Settings.closeOnUserInteraction = True
				mainMenu.AutoClose = True
				closeOnInteract.Checked = True
			End If
			My.Settings.Save()

		Catch ex As Exception

		End Try
	End Sub

	Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles aboutMenu.Click

		Try
			My.Forms.About.ShowDialog()
		Catch ex As Exception
			MsgBox("Error" & vbCrLf & ex.Message, vbOKOnly, "Ops")
		End Try

	End Sub

	Private Sub ExitCMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitMenu.Click

		Try
			Me.Close()
			NI.Icon = Nothing
			Me.Dispose()
			NI.Dispose()
			End
		Catch
		End Try

	End Sub

	Private Sub LASTSM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LASTSM.Click

		Try

			regKey_StartUP_Key = Registry.CurrentUser.OpenSubKey(regKey_Run_At_Startup_Path, True)
			If regKey_StartUP_Key.GetValue("Aurora Easy Access") = Nothing Then
				If load_At_Windows_Startup = False Then
					Try
						regKey_StartUP_Key = Registry.CurrentUser.OpenSubKey(regKey_Run_At_Startup_Path, True)
						regKey_StartUP_Key.SetValue("Aurora Easy Access", appPath, Microsoft.Win32.RegistryValueKind.String)
						load_At_Windows_Startup = True
						LASTSM.Checked = True
					Catch ex As Exception
						MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
					End Try
				End If

			Else
				If load_At_Windows_Startup = True Then
					Try
						My.Computer.Registry.CurrentUser.OpenSubKey(regKey_Run_At_Startup_Path, True).DeleteValue("Aurora Easy Access")
						load_At_Windows_Startup = False
						LASTSM.Checked = False
					Catch ex As Exception
						MsgBox(vbCrLf & ex.Message, vbCritical, "An error occurred")
					End Try
				End If

			End If

		Catch
		End Try

	End Sub

#End Region


End Class