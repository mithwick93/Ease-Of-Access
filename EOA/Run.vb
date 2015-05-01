Public Class Run
    Dim app As String
    Dim ok As Boolean = False
    Private Sub cmdRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRun.Click
        If CBAdmin.Checked = True Then
            Call RunAdmin()
        ElseIf CBAdmin.Checked = False Then
            Call run()
        End If

    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        app = ""
        My.Settings.Rcx = CInt(Me.Location.X)
        My.Settings.Rcy = CInt(Me.Location.Y)
        My.Settings.Save()
        Timer1.Enabled = True

    End Sub
    Private Sub Run_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Rcx = CInt(Me.Location.X)
        My.Settings.Rcy = CInt(Me.Location.Y)
        My.Settings.Save()
        Timer1.Enabled = True
        e.Cancel = True
        Exit Sub
    End Sub
    Private Sub Run_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Hide()

            My.Settings.Reload()
            Me.Icon = My.Resources._0030
            If My.Forms.Aurora.AOTCB.Checked = True Then
                Me.TopMost = True
            Else
                Me.TopMost = False
            End If
            Me.Opacity = 0.9
            If Environment.Is64BitOperatingSystem = True Then
                Me.Text = "Run x64"
            Else
                Me.Text = "Run x86"
            End If

            Me.SetDesktopLocation(My.Forms.Aurora.Location.X, My.Forms.Aurora.Location.Y)

            Me.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click

        Try
            Dim id As String = Environment.SystemDirectory
            OFDRun.InitialDirectory = id.Substring(0, 3)
            OFDRun.ShowDialog()

            If Not OFDRun.FileName = "" Then
                CBRun.Text = OFDRun.FileName

            End If

        Catch ex As Exception

        End Try


    End Sub
    Private Sub run()

        Try
            Dim ch As Double = CBRun.Text.Length
            If ch > 0 Then
                Try
                    app = CBRun.Text.ToString
                    Process.Start(app)
                    ok = True
                Catch ex As Exception
                    ok = False
                End Try

            End If

            If Not CBRun.Items.Contains(app) Then
                If Not app = "" Then
                    If ok = True Then
                        CBRun.Items.Add(app)
                    End If

                End If

            End If

            CBRun.SelectedItem = Nothing
            app = ""
        Catch ex As Exception

            app = ""

        End Try
    End Sub

    Private Sub RunAdmin()

        Try

            Dim ch As Double = CBRun.Text.ToString.Length
            If ch > 0 Then

                app = CBRun.Text.ToString

                Dim process As System.Diagnostics.Process = Nothing
                Dim Papp As System.Diagnostics.ProcessStartInfo

                Papp = New System.Diagnostics.ProcessStartInfo()
                Papp.FileName = app

                If System.Environment.OSVersion.Version.Major >= 6 Then
                    Papp.Verb = "runas"
                Else

                End If

                With Papp
                    .Arguments = ""
                    .WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                    .UseShellExecute = True

                End With

                Try
                    process = System.Diagnostics.Process.Start(Papp)
                    ok = True
                Catch ex As Exception
                    ok = False
                End Try


            End If

            If Not CBRun.Items.Contains(app) Then
                If Not app = "" Then
                    If ok = True Then
                        CBRun.Items.Add(app)
                    End If

                End If

            End If

            CBRun.SelectedItem = Nothing
            app = ""

        Catch ex As Exception

            app = ""

        End Try


    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Try
            Me.Opacity = Me.Opacity - 0.1

            If Me.Opacity <= 0 Then
                Timer1.Enabled = False
                Me.Hide()
            End If
        Catch ex As Exception

        End Try


    End Sub
End Class