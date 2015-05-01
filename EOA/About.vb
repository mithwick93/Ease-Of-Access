Public Class About

    
    Private Sub Aboutme_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Text = "About Aurora"

            Dim version As String = My.Application.Info.Version.ToString
            Dim VMain As String = version.Substring(0, 3)
            Me.LabelVersion.Text = String.Format("Version {0}", VMain)
            Me.LabelCopyright.Text = My.Application.Info.Copyright
            Me.LabelCompanyName.Text = My.Application.Info.CompanyName

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.Close()
    End Sub

End Class