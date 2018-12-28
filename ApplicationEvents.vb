Imports System.Management
Namespace My

    ' Los siguientes eventos est�n disponibles para MyApplication:
    ' 
    ' Inicio: se desencadena cuando se inicia la aplicaci�n, antes de que se cree el formulario de inicio.
    ' Apagado: generado despu�s de cerrar todos los formularios de la aplicaci�n. Este evento no se genera si la aplicaci�n termina de forma an�mala.
    ' UnhandledException: generado si la aplicaci�n detecta una excepci�n no controlada.
    ' StartupNextInstance: se desencadena cuando se inicia una aplicaci�n de instancia �nica y la aplicaci�n ya est� activa. 
    ' NetworkAvailabilityChanged: se desencadena cuando la conexi�n de red est� conectada o desconectada.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            SysInfo.GetSysInfo()
            If My.Computer.FileSystem.DirectoryExists(My.Settings.DefaultMacFolder) = False Then
                My.Settings.DefaultMacFolder = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Macs"
            End If
            If My.Settings.vMacROM = "" Then
                My.Settings.vMacROM = My.Application.Info.DirectoryPath & "\Emulators\vMac\MacII.ROM"
            End If
            If My.Settings.BasiliskROM = "" Then
                My.Settings.BasiliskROM = My.Application.Info.DirectoryPath & "\Emulators\BasiliskII\Mac_OS_ROM"
            End If
            If My.Settings.SheepShaverROM = "" Then
                My.Settings.SheepShaverROM = My.Application.Info.DirectoryPath & "\Emulators\SheepShaver\Mac_OS_ROM"
            End If

            If Not My.Application.CommandLineArgs.Count = 0 Then
                For x As Integer = 0 To My.Application.CommandLineArgs.Count - 1
                    Select Case My.Application.CommandLineArgs.Item(x)
                        Case "/debug"
                            frmMain.mnuFileDebug.Visible = True
                            frmMain.mnuHelpCrash.Visible = True
                        Case "/log"
                    End Select
                Next
            End If
        End Sub

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            If MsgBox("Virtual Mac is already running. Click Ok to show Virtual Mac Console. Click Cancel to close this dialog", MsgBoxStyle.OkCancel, "Virtual Mac is alerady running") = MsgBoxResult.Ok Then
                frmMain.Show()
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            For x As Integer = 0 To My.Application.CommandLineArgs.Count - 1
                Select Case My.Application.CommandLineArgs.Item(x)
                    Case "/debug"
                        MsgBox(e.Exception.Message)
                        Resume Next
                    Case "/log"
                        Logger.Log(e.Exception.Message)
                        Resume Next
                    Case ""
                        Resume Next
                End Select
            Next
        End Sub
    End Class
End Namespace

